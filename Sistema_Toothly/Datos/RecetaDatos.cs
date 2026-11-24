using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Sistema_Toothly.Clases;

namespace Sistema_Toothly.Datos
{
    // Clase para administrar las recetas medicas y sus detalles en SQL Server
    public static class RecetaDatos
    {
        // Propiedad de compatibilidad que consulta directo a SQL
        public static List<Receta> recetas => Listar();
        public static List<RecetaDetalle> detalles => ListarTodosDetalles();

        // Obtiene todas las recetas ordenadas de mas reciente a mas antigua
        public static List<Receta> Listar()
        {
            List<Receta> lista = new List<Receta>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_receta, id_paciente, id_tratamiento, fecha, indicaciones FROM dbo.Receta ORDER BY id_receta DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearReceta(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Inserta una nueva receta y obtiene su ID generado
        public static void Agregar(Receta r)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"INSERT INTO dbo.Receta (id_paciente, id_tratamiento, fecha, indicaciones)
                                 VALUES (@idPac, @idTrat, @fecha, @ind);
                                 SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idPac", r.IdPaciente);
                    cmd.Parameters.AddWithValue("@idTrat", r.IdTratamiento > 0 ? (object)r.IdTratamiento : DBNull.Value);
                    cmd.Parameters.AddWithValue("@fecha", r.Fecha.Date);
                    cmd.Parameters.AddWithValue("@ind", (object?)r.Indicaciones ?? DBNull.Value);

                    object idGen = cmd.ExecuteScalar();
                    if (idGen != null && idGen != DBNull.Value)
                    {
                        r.IdReceta = Convert.ToInt32(idGen);
                    }
                }
            }
        }

        // Guarda la receta y sus medicamentos en una sola transacción (maestro-detalle)
        public static void AgregarConDetalles(Receta r, List<RecetaDetalle> detalles)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            using (SqlTransaction tx = con.BeginTransaction())
            {
                try
                {
                    string query = @"INSERT INTO dbo.Receta (id_paciente, id_tratamiento, fecha, indicaciones)
                                     VALUES (@idPac, @idTrat, @fecha, @ind);
                                     SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(query, con, tx))
                    {
                        AgregarParametrosReceta(cmd, r);
                        r.IdReceta = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    InsertarDetalles(con, tx, r.IdReceta, detalles);
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        // Modifica la receta y reemplaza sus medicamentos en una sola transacción.
        // No se permite si ya se despachó algún medicamento (ya se descontó inventario y se cobró).
        public static void Modificar(Receta r, List<RecetaDetalle> detalles)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            using (SqlTransaction tx = con.BeginTransaction())
            {
                try
                {
                    if (ContarDespachados(con, tx, r.IdReceta) > 0)
                        throw new InvalidOperationException("La receta #" + r.IdReceta + " ya tiene medicamentos despachados y no se puede modificar.");

                    string query = @"UPDATE dbo.Receta
                                     SET id_paciente = @idPac, id_tratamiento = @idTrat, fecha = @fecha, indicaciones = @ind
                                     WHERE id_receta = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con, tx))
                    {
                        AgregarParametrosReceta(cmd, r);
                        cmd.Parameters.AddWithValue("@id", r.IdReceta);
                        if (cmd.ExecuteNonQuery() == 0)
                            throw new InvalidOperationException("La receta #" + r.IdReceta + " ya no existe.");
                    }

                    using (SqlCommand cmdDel = new SqlCommand("DELETE FROM dbo.RecetaDetalle WHERE id_receta = @id", con, tx))
                    {
                        cmdDel.Parameters.AddWithValue("@id", r.IdReceta);
                        cmdDel.ExecuteNonQuery();
                    }

                    InsertarDetalles(con, tx, r.IdReceta, detalles);
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        // Elimina por completo una receta (encabezado y medicamentos) si no tiene despachos
        public static void Eliminar(int idReceta)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            using (SqlTransaction tx = con.BeginTransaction())
            {
                try
                {
                    if (ContarDespachados(con, tx, idReceta) > 0)
                        throw new InvalidOperationException("La receta #" + idReceta + " ya tiene medicamentos despachados y no se puede eliminar.");

                    using (SqlCommand cmdDet = new SqlCommand("DELETE FROM dbo.RecetaDetalle WHERE id_receta = @id", con, tx))
                    {
                        cmdDet.Parameters.AddWithValue("@id", idReceta);
                        cmdDet.ExecuteNonQuery();
                    }
                    using (SqlCommand cmdRec = new SqlCommand("DELETE FROM dbo.Receta WHERE id_receta = @id", con, tx))
                    {
                        cmdRec.Parameters.AddWithValue("@id", idReceta);
                        cmdRec.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        // Indica si algún medicamento de la receta ya fue despachado
        public static bool TieneDespachos(int idReceta)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                return ContarDespachados(con, null, idReceta) > 0;
            }
        }

        private static int ContarDespachados(SqlConnection con, SqlTransaction? tx, int idReceta)
        {
            string query = "SELECT COUNT(*) FROM dbo.RecetaDetalle WHERE id_receta = @id AND cantidad_despachada > 0";
            using (SqlCommand cmd = new SqlCommand(query, con, tx))
            {
                cmd.Parameters.AddWithValue("@id", idReceta);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private static void AgregarParametrosReceta(SqlCommand cmd, Receta r)
        {
            cmd.Parameters.AddWithValue("@idPac", r.IdPaciente);
            cmd.Parameters.AddWithValue("@idTrat", r.IdTratamiento > 0 ? (object)r.IdTratamiento : DBNull.Value);
            cmd.Parameters.AddWithValue("@fecha", r.Fecha.Date);
            cmd.Parameters.AddWithValue("@ind", (object?)r.Indicaciones ?? DBNull.Value);
        }

        private static void InsertarDetalles(SqlConnection con, SqlTransaction tx, int idReceta, List<RecetaDetalle> detalles)
        {
            string query = @"INSERT INTO dbo.RecetaDetalle (id_receta, medicamento, dosis, frecuencia, duracion, id_producto, cantidad, cantidad_despachada)
                             VALUES (@idRec, @med, @dosis, @frec, @dur, @idProd, @cant, 0);
                             SELECT SCOPE_IDENTITY();";
            foreach (var d in detalles)
            {
                d.IdReceta = idReceta;
                using (SqlCommand cmd = new SqlCommand(query, con, tx))
                {
                    cmd.Parameters.AddWithValue("@idRec", idReceta);
                    cmd.Parameters.AddWithValue("@med", d.Medicamento);
                    cmd.Parameters.AddWithValue("@dosis", (object?)d.Dosis ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@frec", (object?)d.Frecuencia ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@dur", (object?)d.Duracion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@idProd", d.IdProducto.HasValue ? (object)d.IdProducto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@cant", d.Cantidad);
                    d.IdDetalle = Convert.ToInt32(cmd.ExecuteScalar());
                    d.CantidadDespachada = 0;
                }
            }
        }

        // Inserta un medicamento al detalle de la receta
        public static void AgregarDetalle(RecetaDetalle d)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"INSERT INTO dbo.RecetaDetalle (id_receta, medicamento, dosis, frecuencia, duracion, id_producto, cantidad, cantidad_despachada)
                                 VALUES (@idRec, @med, @dosis, @frec, @dur, @idProd, @cant, 0);
                                 SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idRec", d.IdReceta);
                    cmd.Parameters.AddWithValue("@med", d.Medicamento);
                    cmd.Parameters.AddWithValue("@dosis", (object?)d.Dosis ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@frec", (object?)d.Frecuencia ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@dur", (object?)d.Duracion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@idProd", d.IdProducto.HasValue ? (object)d.IdProducto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@cant", d.Cantidad);

                    object idGen = cmd.ExecuteScalar();
                    if (idGen != null && idGen != DBNull.Value)
                    {
                        d.IdDetalle = Convert.ToInt32(idGen);
                    }
                }
            }
        }

        // Obtiene todos los medicamentos asignados a una receta
        public static List<RecetaDetalle> ObtenerDetalles(int idReceta)
        {
            List<RecetaDetalle> lista = new List<RecetaDetalle>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_detalle, id_receta, medicamento, dosis, frecuencia, duracion, id_producto, cantidad, cantidad_despachada
                                 FROM dbo.RecetaDetalle
                                 WHERE id_receta = @idRec";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idRec", idReceta);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearDetalle(dr));
                        }
                    }
                }
            }
            return lista;
        }

        public static List<RecetaDetalle> ListarPendientesDespacho(int idPaciente)
        {
            List<RecetaDetalle> lista = new List<RecetaDetalle>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT d.id_detalle, d.id_receta, d.medicamento, d.dosis, d.frecuencia, d.duracion, d.id_producto, d.cantidad, d.cantidad_despachada
                                 FROM dbo.RecetaDetalle d
                                 INNER JOIN dbo.Receta r ON d.id_receta = r.id_receta
                                 WHERE r.id_paciente = @idPac
                                   AND d.id_producto IS NOT NULL
                                   AND d.cantidad_despachada < d.cantidad
                                 ORDER BY r.id_receta DESC, d.id_detalle";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idPac", idPaciente);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearDetalle(dr));
                        }
                    }
                }
            }
            return lista;
        }

        public static void RegistrarDespacho(int idDetalle, int cantidad)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "UPDATE dbo.RecetaDetalle SET cantidad_despachada = cantidad_despachada + @cant WHERE id_detalle = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@cant", cantidad);
                    cmd.Parameters.AddWithValue("@id", idDetalle);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static RecetaDetalle MapearDetalle(SqlDataReader dr)
        {
            return new RecetaDetalle
            {
                IdDetalle = Convert.ToInt32(dr["id_detalle"]),
                IdReceta = Convert.ToInt32(dr["id_receta"]),
                Medicamento = dr["medicamento"].ToString()!,
                Dosis = dr["dosis"] != DBNull.Value ? dr["dosis"].ToString()! : "",
                Frecuencia = dr["frecuencia"] != DBNull.Value ? dr["frecuencia"].ToString()! : "",
                Duracion = dr["duracion"] != DBNull.Value ? dr["duracion"].ToString()! : "",
                IdProducto = dr["id_producto"] != DBNull.Value ? Convert.ToInt32(dr["id_producto"]) : (int?)null,
                Cantidad = Convert.ToInt32(dr["cantidad"]),
                CantidadDespachada = Convert.ToInt32(dr["cantidad_despachada"])
            };
        }

        // Elimina los detalles de una receta
        public static void EliminarDetalles(int idReceta)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "DELETE FROM dbo.RecetaDetalle WHERE id_receta = @idRec";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idRec", idReceta);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Busca recetas por nombre de paciente
        public static List<Receta> Buscar(string texto)
        {
            List<Receta> lista = new List<Receta>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT r.id_receta, r.id_paciente, r.id_tratamiento, r.fecha, r.indicaciones 
                                 FROM dbo.Receta r
                                 INNER JOIN dbo.Paciente p ON r.id_paciente = p.id_paciente
                                 WHERE p.nombres LIKE @buscar OR p.apellidos LIKE @buscar
                                 ORDER BY r.id_receta DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@buscar", "%" + texto + "%");
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearReceta(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Obtiene las recetas de un paciente en especifico
        public static List<Receta> BuscarPorPaciente(int idPaciente)
        {
            List<Receta> lista = new List<Receta>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_receta, id_paciente, id_tratamiento, fecha, indicaciones 
                                 FROM dbo.Receta 
                                 WHERE id_paciente = @idPac 
                                 ORDER BY id_receta DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idPac", idPaciente);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearReceta(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Metodo auxiliar para mapear fila a objeto Receta
        private static Receta MapearReceta(SqlDataReader dr)
        {
            return new Receta
            {
                IdReceta = Convert.ToInt32(dr["id_receta"]),
                IdPaciente = Convert.ToInt32(dr["id_paciente"]),
                IdTratamiento = dr["id_tratamiento"] != DBNull.Value ? Convert.ToInt32(dr["id_tratamiento"]) : 0,
                Fecha = Convert.ToDateTime(dr["fecha"]),
                Indicaciones = dr["indicaciones"] != DBNull.Value ? dr["indicaciones"].ToString()! : ""
            };
        }

        // Metodo auxiliar para traer todos los detalles si algun reporte los necesita
        private static List<RecetaDetalle> ListarTodosDetalles()
        {
            List<RecetaDetalle> lista = new List<RecetaDetalle>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_detalle, id_receta, medicamento, dosis, frecuencia, duracion, id_producto, cantidad, cantidad_despachada FROM dbo.RecetaDetalle";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearDetalle(dr));
                        }
                    }
                }
            }
            return lista;
        }
    }
}
