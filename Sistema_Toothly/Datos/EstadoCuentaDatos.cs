using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Sistema_Toothly.Clases;

namespace Sistema_Toothly.Datos
{
    // Clase para administrar los estados de cuenta y movimientos (cargos/abonos) en SQL Server
    public static class EstadoCuentaDatos
    {
        // Propiedades de compatibilidad que consultan directo a SQL
        public static List<EstadoCuenta> estados => Listar();
        public static List<MovimientoCuenta> movimientos => ListarTodosMovimientos();

        // Obtiene todos los estados de cuenta
        public static List<EstadoCuenta> Listar()
        {
            List<EstadoCuenta> lista = new List<EstadoCuenta>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_estado, id_paciente, saldo_actual FROM dbo.EstadoCuenta ORDER BY id_estado";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new EstadoCuenta
                            {
                                IdEstado = Convert.ToInt32(dr["id_estado"]),
                                IdPaciente = Convert.ToInt32(dr["id_paciente"]),
                                SaldoActual = Convert.ToDecimal(dr["saldo_actual"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // Obtiene el estado de cuenta de un paciente
        public static EstadoCuenta? ObtenerPorPaciente(int idPaciente)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_estado, id_paciente, saldo_actual FROM dbo.EstadoCuenta WHERE id_paciente = @idPac";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idPac", idPaciente);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new EstadoCuenta
                            {
                                IdEstado = Convert.ToInt32(dr["id_estado"]),
                                IdPaciente = Convert.ToInt32(dr["id_paciente"]),
                                SaldoActual = Convert.ToDecimal(dr["saldo_actual"])
                            };
                        }
                    }
                }
            }
            return null;
        }

        // Crea un nuevo estado de cuenta para un paciente si no lo tiene
        public static EstadoCuenta CrearEstado(int idPaciente)
        {
            // Primero verificamos si ya existe
            EstadoCuenta? existente = ObtenerPorPaciente(idPaciente);
            if (existente != null) return existente;

            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"INSERT INTO dbo.EstadoCuenta (id_paciente, saldo_actual)
                                 VALUES (@idPac, 0.00);
                                 SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idPac", idPaciente);
                    int idGen = Convert.ToInt32(cmd.ExecuteScalar());
                    return new EstadoCuenta
                    {
                        IdEstado = idGen,
                        IdPaciente = idPaciente,
                        SaldoActual = 0.00m
                    };
                }
            }
        }

        // Inserta un cargo o abono y recalcula el saldo dentro de una transacción.
        // Regla de negocio: un abono nunca puede ser mayor al saldo pendiente,
        // de modo que el saldo del paciente nunca queda por debajo de $0.00.
        public static void AgregarMovimiento(MovimientoCuenta m)
        {
            if (m.Monto <= 0)
                throw new InvalidOperationException("El monto del movimiento debe ser mayor a $0.00.");
            if (m.Tipo != "Cargo" && m.Tipo != "Abono")
                throw new InvalidOperationException("Tipo de movimiento no válido: " + m.Tipo);

            using (SqlConnection con = ConexionDB.ObtenerConexion())
            using (SqlTransaction tx = con.BeginTransaction())
            {
                try
                {
                    if (m.Tipo == "Abono")
                    {
                        // UPDLOCK evita que dos abonos simultáneos superen el saldo
                        decimal saldo = CalcularSaldo(con, tx, m.IdEstado, true);
                        if (m.Monto > saldo)
                        {
                            throw new InvalidOperationException(
                                "El abono ($" + m.Monto.ToString("F2") + ") no puede ser mayor al saldo pendiente ($" + Math.Max(saldo, 0).ToString("F2") + ").");
                        }
                    }

                    string query = @"INSERT INTO dbo.MovimientoCuenta (id_estado, fecha, tipo, concepto, monto)
                                     VALUES (@idEst, @fecha, @tipo, @concepto, @monto);
                                     SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(query, con, tx))
                    {
                        cmd.Parameters.AddWithValue("@idEst", m.IdEstado);
                        cmd.Parameters.AddWithValue("@fecha", m.Fecha);
                        cmd.Parameters.AddWithValue("@tipo", m.Tipo);
                        cmd.Parameters.AddWithValue("@concepto", m.Concepto);
                        cmd.Parameters.AddWithValue("@monto", m.Monto);

                        object idGen = cmd.ExecuteScalar();
                        if (idGen != null && idGen != DBNull.Value)
                        {
                            m.IdMovimiento = Convert.ToInt32(idGen);
                        }
                    }

                    ActualizarSaldo(con, tx, m.IdEstado);
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        // Recalcula el saldo sumando los cargos y restando los abonos
        public static void RecalcularSaldo(int idEstado)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                ActualizarSaldo(con, null, idEstado);
            }
        }

        // Saldo pendiente calculado desde los movimientos (fuente de verdad)
        public static decimal ObtenerSaldo(int idEstado)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                return CalcularSaldo(con, null, idEstado, false);
            }
        }

        // Registra un cargo al paciente (crea su estado de cuenta si no existe)
        public static void RegistrarCargo(int idPaciente, string concepto, decimal monto)
        {
            if (monto <= 0) return;
            EstadoCuenta ec = ObtenerPorPaciente(idPaciente) ?? CrearEstado(idPaciente);
            AgregarMovimiento(new MovimientoCuenta
            {
                IdEstado = ec.IdEstado,
                Fecha = DateTime.Now,
                Tipo = "Cargo",
                Concepto = concepto,
                Monto = monto
            });
        }

        // Revierte un cargo (anulación o rebaja de un tratamiento) sin dejar el saldo negativo.
        // Se abona como máximo lo que el paciente debe; el excedente ya lo había pagado,
        // por lo que se devuelve como "monto a reembolsar" para que la clínica entregue ese dinero.
        public static decimal RevertirCargo(int idPaciente, string concepto, decimal monto)
        {
            if (monto <= 0) return 0;
            EstadoCuenta ec = ObtenerPorPaciente(idPaciente) ?? CrearEstado(idPaciente);
            decimal saldo = Math.Max(ObtenerSaldo(ec.IdEstado), 0);
            decimal abonar = Math.Min(monto, saldo);

            if (abonar > 0)
            {
                AgregarMovimiento(new MovimientoCuenta
                {
                    IdEstado = ec.IdEstado,
                    Fecha = DateTime.Now,
                    Tipo = "Abono",
                    Concepto = concepto,
                    Monto = abonar
                });
            }
            return monto - abonar;
        }

        // Texto amigable del saldo: nunca muestra montos negativos como "$-10.00"
        public static string FormatearSaldo(decimal saldo)
        {
            if (saldo < 0) return "$0.00 (a favor: $" + (-saldo).ToString("F2") + ")";
            return "$" + saldo.ToString("F2");
        }

        private static decimal CalcularSaldo(SqlConnection con, SqlTransaction? tx, int idEstado, bool bloquear)
        {
            string query = @"SELECT 
                ISNULL(SUM(CASE WHEN tipo = 'Cargo' THEN monto ELSE 0 END), 0) -
                ISNULL(SUM(CASE WHEN tipo = 'Abono' THEN monto ELSE 0 END), 0) AS NuevoSaldo
                FROM dbo.MovimientoCuenta " + (bloquear ? "WITH (UPDLOCK, HOLDLOCK) " : "") + @"
                WHERE id_estado = @idEst";
            using (SqlCommand cmd = new SqlCommand(query, con, tx))
            {
                cmd.Parameters.AddWithValue("@idEst", idEstado);
                object res = cmd.ExecuteScalar();
                return (res != null && res != DBNull.Value) ? Convert.ToDecimal(res) : 0m;
            }
        }

        private static void ActualizarSaldo(SqlConnection con, SqlTransaction? tx, int idEstado)
        {
            decimal nuevoSaldo = CalcularSaldo(con, tx, idEstado, false);
            string queryUpd = "UPDATE dbo.EstadoCuenta SET saldo_actual = @saldo WHERE id_estado = @idEst";
            using (SqlCommand cmdUpd = new SqlCommand(queryUpd, con, tx))
            {
                cmdUpd.Parameters.AddWithValue("@saldo", nuevoSaldo);
                cmdUpd.Parameters.AddWithValue("@idEst", idEstado);
                cmdUpd.ExecuteNonQuery();
            }
        }

        // Obtiene todos los movimientos de un estado de cuenta
        public static List<MovimientoCuenta> ObtenerMovimientos(int idEstado)
        {
            List<MovimientoCuenta> lista = new List<MovimientoCuenta>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_movimiento, id_estado, fecha, tipo, concepto, monto 
                                 FROM dbo.MovimientoCuenta 
                                 WHERE id_estado = @idEst 
                                 ORDER BY fecha ASC, id_movimiento ASC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idEst", idEstado);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new MovimientoCuenta
                            {
                                IdMovimiento = Convert.ToInt32(dr["id_movimiento"]),
                                IdEstado = Convert.ToInt32(dr["id_estado"]),
                                Fecha = Convert.ToDateTime(dr["fecha"]),
                                Tipo = dr["tipo"].ToString()!,
                                Concepto = dr["concepto"].ToString()!,
                                Monto = Convert.ToDecimal(dr["monto"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // Ventas registradas por el módulo de Despacho (cargos con concepto "Venta: ...").
        // Devuelve cada cargo junto con el ID del paciente para el reporte de despachos.
        public static List<(MovimientoCuenta Movimiento, int IdPaciente)> ListarVentasDespacho()
        {
            var lista = new List<(MovimientoCuenta, int)>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT m.id_movimiento, m.id_estado, m.fecha, m.tipo, m.concepto, m.monto, e.id_paciente
                                 FROM dbo.MovimientoCuenta m
                                 INNER JOIN dbo.EstadoCuenta e ON m.id_estado = e.id_estado
                                 WHERE m.tipo = 'Cargo' AND m.concepto LIKE 'Venta: %'
                                 ORDER BY m.fecha DESC, m.id_movimiento DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MovimientoCuenta m = new MovimientoCuenta
                            {
                                IdMovimiento = Convert.ToInt32(dr["id_movimiento"]),
                                IdEstado = Convert.ToInt32(dr["id_estado"]),
                                Fecha = Convert.ToDateTime(dr["fecha"]),
                                Tipo = dr["tipo"].ToString()!,
                                Concepto = dr["concepto"].ToString()!,
                                Monto = Convert.ToDecimal(dr["monto"])
                            };
                            lista.Add((m, Convert.ToInt32(dr["id_paciente"])));
                        }
                    }
                }
            }
            return lista;
        }

        // Metodo auxiliar para traer todos los movimientos si algun reporte los requiere
        private static List<MovimientoCuenta> ListarTodosMovimientos()
        {
            List<MovimientoCuenta> lista = new List<MovimientoCuenta>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_movimiento, id_estado, fecha, tipo, concepto, monto FROM dbo.MovimientoCuenta ORDER BY id_movimiento";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new MovimientoCuenta
                            {
                                IdMovimiento = Convert.ToInt32(dr["id_movimiento"]),
                                IdEstado = Convert.ToInt32(dr["id_estado"]),
                                Fecha = Convert.ToDateTime(dr["fecha"]),
                                Tipo = dr["tipo"].ToString()!,
                                Concepto = dr["concepto"].ToString()!,
                                Monto = Convert.ToDecimal(dr["monto"])
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
