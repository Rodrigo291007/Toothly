using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Sistema_Toothly.Clases;

namespace Sistema_Toothly.Datos
{
    // Clase para administrar las citas medicas en SQL Server
    public static class CitaDatos
    {
        // Propiedad de compatibilidad que consulta directo a SQL
        public static List<Cita> citas => Listar();

        // Obtiene todas las citas ordenadas por fecha y hora
        public static List<Cita> Listar()
        {
            List<Cita> lista = new List<Cita>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_cita, id_paciente, id_odontologo, fecha, hora, motivo, estado 
                                 FROM dbo.Cita 
                                 ORDER BY fecha DESC, hora ASC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearCita(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Inserta una nueva cita en la base de datos
        public static void Agregar(Cita c)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"INSERT INTO dbo.Cita (id_paciente, id_odontologo, fecha, hora, motivo, estado)
                                 VALUES (@idPac, @idOdo, @fecha, @hora, @motivo, @estado);
                                 SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idPac", c.IdPaciente);
                    cmd.Parameters.AddWithValue("@idOdo", c.IdOdontologo);
                    cmd.Parameters.AddWithValue("@fecha", c.Fecha.Date);
                    cmd.Parameters.AddWithValue("@hora", c.Hora);
                    cmd.Parameters.AddWithValue("@motivo", (object?)c.Motivo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@estado", string.IsNullOrEmpty(c.Estado) ? "Programada" : c.Estado);

                    object idGen = cmd.ExecuteScalar();
                    if (idGen != null && idGen != DBNull.Value)
                    {
                        c.IdCita = Convert.ToInt32(idGen);
                    }
                }
            }
        }

        // Modifica una cita (usado tambien al reprogramar)
        public static void Modificar(Cita c)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"UPDATE dbo.Cita 
                                 SET id_paciente = @idPac, id_odontologo = @idOdo, fecha = @fecha, 
                                     hora = @hora, motivo = @motivo, estado = @estado 
                                 WHERE id_cita = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", c.IdCita);
                    cmd.Parameters.AddWithValue("@idPac", c.IdPaciente);
                    cmd.Parameters.AddWithValue("@idOdo", c.IdOdontologo);
                    cmd.Parameters.AddWithValue("@fecha", c.Fecha.Date);
                    cmd.Parameters.AddWithValue("@hora", c.Hora);
                    cmd.Parameters.AddWithValue("@motivo", (object?)c.Motivo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@estado", c.Estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Cancela una cita cambiando su estado a 'Cancelada'
        public static void DarDeBaja(int idCita)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "UPDATE dbo.Cita SET estado = 'Cancelada' WHERE id_cita = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", idCita);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Busca citas por fecha y por odontologo
        public static List<Cita> Buscar(DateTime? fecha, int idOdontologo)
        {
            List<Cita> lista = new List<Cita>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_cita, id_paciente, id_odontologo, fecha, hora, motivo, estado FROM dbo.Cita WHERE 1=1";
                if (fecha != null)
                {
                    query += " AND fecha = @fecha";
                }
                if (idOdontologo > 0)
                {
                    query += " AND id_odontologo = @idOdo";
                }
                query += " ORDER BY hora ASC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (fecha != null) cmd.Parameters.AddWithValue("@fecha", fecha.Value.Date);
                    if (idOdontologo > 0) cmd.Parameters.AddWithValue("@idOdo", idOdontologo);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearCita(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Verifica si un odontologo ya tiene otra cita en esa misma fecha y hora
        public static bool ExisteConflicto(int idOdontologo, DateTime fecha, string hora, int idCitaExcluir)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT COUNT(*) FROM dbo.Cita 
                                 WHERE id_odontologo = @idOdo 
                                   AND fecha = @fecha 
                                   AND hora = @hora 
                                   AND id_cita <> @idExcluir 
                                   AND estado <> 'Cancelada'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idOdo", idOdontologo);
                    cmd.Parameters.AddWithValue("@fecha", fecha.Date);
                    cmd.Parameters.AddWithValue("@hora", hora);
                    cmd.Parameters.AddWithValue("@idExcluir", idCitaExcluir);

                    int cant = Convert.ToInt32(cmd.ExecuteScalar());
                    return cant > 0;
                }
            }
        }

        // Metodo auxiliar para mapear una fila SQL a un objeto Cita
        private static Cita MapearCita(SqlDataReader dr)
        {
            return new Cita
            {
                IdCita = Convert.ToInt32(dr["id_cita"]),
                IdPaciente = Convert.ToInt32(dr["id_paciente"]),
                IdOdontologo = Convert.ToInt32(dr["id_odontologo"]),
                Fecha = Convert.ToDateTime(dr["fecha"]),
                Hora = dr["hora"].ToString()!,
                Motivo = dr["motivo"] != DBNull.Value ? dr["motivo"].ToString()! : "",
                Estado = dr["estado"].ToString()!
            };
        }
    }
}
