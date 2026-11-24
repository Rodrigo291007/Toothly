using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Sistema_Toothly.Clases;

namespace Sistema_Toothly.Datos
{
    // Clase para guardar y consultar los registros de auditoria y bitacora en SQL Server
    public static class BitacoraDatos
    {
        // Propiedad de compatibilidad: consulta directamente la base de datos
        public static List<BitacoraEntry> registros => Listar();

        // Obtiene todos los registros de la bitacora ordenados desde el mas reciente
        public static List<BitacoraEntry> Listar()
        {
            List<BitacoraEntry> lista = new List<BitacoraEntry>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_bitacora, id_usuario, fecha, hora, accion, descripcion FROM dbo.Bitacora ORDER BY id_bitacora DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new BitacoraEntry
                            {
                                IdBitacora = Convert.ToInt32(dr["id_bitacora"]),
                                IdUsuario = Convert.ToInt32(dr["id_usuario"]),
                                Fecha = Convert.ToDateTime(dr["fecha"]),
                                Hora = dr["hora"].ToString()!,
                                Accion = dr["accion"].ToString()!,
                                Descripcion = dr["descripcion"] != DBNull.Value ? dr["descripcion"].ToString()! : ""
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // Inserta un nuevo registro en la tabla Bitacora
        public static void Agregar(BitacoraEntry b)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"INSERT INTO dbo.Bitacora (id_usuario, fecha, hora, accion, descripcion)
                                 VALUES (@usuario, @fecha, @hora, @accion, @descripcion);
                                 SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@usuario", b.IdUsuario <= 0 ? 1 : b.IdUsuario);
                    cmd.Parameters.AddWithValue("@fecha", b.Fecha.Date);
                    cmd.Parameters.AddWithValue("@hora", b.Hora);
                    cmd.Parameters.AddWithValue("@accion", b.Accion);
                    cmd.Parameters.AddWithValue("@descripcion", (object?)b.Descripcion ?? DBNull.Value);

                    object idGen = cmd.ExecuteScalar();
                    if (idGen != null && idGen != DBNull.Value)
                    {
                        b.IdBitacora = Convert.ToInt32(idGen);
                    }
                }
            }
        }

        // Busca registros de bitacora filtrados por usuario y rango de fechas
        public static List<BitacoraEntry> Buscar(int idUsuario, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            List<BitacoraEntry> lista = new List<BitacoraEntry>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_bitacora, id_usuario, fecha, hora, accion, descripcion FROM dbo.Bitacora WHERE 1=1";
                if (idUsuario > 0)
                {
                    query += " AND id_usuario = @usuario";
                }
                if (fechaDesde != null)
                {
                    query += " AND fecha >= @desde";
                }
                if (fechaHasta != null)
                {
                    query += " AND fecha <= @hasta";
                }
                query += " ORDER BY id_bitacora DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (idUsuario > 0) cmd.Parameters.AddWithValue("@usuario", idUsuario);
                    if (fechaDesde != null) cmd.Parameters.AddWithValue("@desde", fechaDesde.Value.Date);
                    if (fechaHasta != null) cmd.Parameters.AddWithValue("@hasta", fechaHasta.Value.Date);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new BitacoraEntry
                            {
                                IdBitacora = Convert.ToInt32(dr["id_bitacora"]),
                                IdUsuario = Convert.ToInt32(dr["id_usuario"]),
                                Fecha = Convert.ToDateTime(dr["fecha"]),
                                Hora = dr["hora"].ToString()!,
                                Accion = dr["accion"].ToString()!,
                                Descripcion = dr["descripcion"] != DBNull.Value ? dr["descripcion"].ToString()! : ""
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // Metodo rapido para registrar cualquier accion que realiza el usuario conectado
        public static void RegistrarAccion(string accion, string descripcion)
        {
            try
            {
                BitacoraEntry b = new BitacoraEntry();
                b.IdUsuario = Utilidades.Sesion.IdUsuario <= 0 ? 1 : Utilidades.Sesion.IdUsuario;
                b.Fecha = DateTime.Today;
                b.Hora = DateTime.Now.ToString("HH:mm:ss");
                b.Accion = accion;
                b.Descripcion = descripcion;
                Agregar(b);
            }
            catch
            {
            }
        }
    }
}
