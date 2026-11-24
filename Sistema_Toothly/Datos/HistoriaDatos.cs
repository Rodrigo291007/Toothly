using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Sistema_Toothly.Clases;

namespace Sistema_Toothly.Datos
{
    // Clase para administrar las historias clinicas de los pacientes en SQL Server
    public static class HistoriaDatos
    {
        // Propiedad de compatibilidad que consulta directo a SQL
        public static List<HistoriaClinica> historias => Listar();

        // Obtiene todas las historias clinicas registradas
        public static List<HistoriaClinica> Listar()
        {
            List<HistoriaClinica> lista = new List<HistoriaClinica>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_historia, id_paciente, antecedentes, observaciones_generales, 
                                        observaciones_clinicas, fecha_apertura, motivo_consulta, diagnostico, id_odontologo 
                                 FROM dbo.HistoriaClinica 
                                 ORDER BY id_historia DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearHistoria(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Inserta una nueva historia clinica en la base de datos
        public static void Agregar(HistoriaClinica h)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"INSERT INTO dbo.HistoriaClinica 
                                 (id_paciente, antecedentes, observaciones_generales, observaciones_clinicas, 
                                  fecha_apertura, motivo_consulta, diagnostico, id_odontologo)
                                 VALUES 
                                 (@idPac, @ant, @obsGen, @obsClin, @fecha, @motivo, @diag, @idOdo);
                                 SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idPac", h.IdPaciente);
                    cmd.Parameters.AddWithValue("@ant", (object?)h.Antecedentes ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@obsGen", (object?)h.ObservacionesGenerales ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@obsClin", (object?)h.ObservacionesClinicas ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@fecha", h.FechaApertura);
                    cmd.Parameters.AddWithValue("@motivo", (object?)h.MotivoConsulta ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@diag", (object?)h.Diagnostico ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@idOdo", h.IdOdontologo <= 0 ? 1 : h.IdOdontologo);

                    object idGen = cmd.ExecuteScalar();
                    if (idGen != null && idGen != DBNull.Value)
                    {
                        h.IdHistoria = Convert.ToInt32(idGen);
                    }
                }
            }
        }

        // Actualiza los datos de una historia clinica existente
        public static void Modificar(HistoriaClinica h)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"UPDATE dbo.HistoriaClinica 
                                 SET id_paciente = @idPac, antecedentes = @ant, observaciones_generales = @obsGen, 
                                     observaciones_clinicas = @obsClin, motivo_consulta = @motivo, 
                                     diagnostico = @diag, id_odontologo = @idOdo 
                                 WHERE id_historia = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", h.IdHistoria);
                    cmd.Parameters.AddWithValue("@idPac", h.IdPaciente);
                    cmd.Parameters.AddWithValue("@ant", (object?)h.Antecedentes ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@obsGen", (object?)h.ObservacionesGenerales ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@obsClin", (object?)h.ObservacionesClinicas ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@motivo", (object?)h.MotivoConsulta ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@diag", (object?)h.Diagnostico ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@idOdo", h.IdOdontologo <= 0 ? 1 : h.IdOdontologo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Obtiene la historia clinica de un paciente especifico
        public static HistoriaClinica? BuscarPorPaciente(int idPaciente)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_historia, id_paciente, antecedentes, observaciones_generales, 
                                        observaciones_clinicas, fecha_apertura, motivo_consulta, diagnostico, id_odontologo 
                                 FROM dbo.HistoriaClinica 
                                 WHERE id_paciente = @idPac";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idPac", idPaciente);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return MapearHistoria(dr);
                        }
                    }
                }
            }
            return null;
        }

        // Busca historias clinicas por nombre de paciente o diagnostico
        public static List<HistoriaClinica> Buscar(string texto)
        {
            List<HistoriaClinica> lista = new List<HistoriaClinica>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT h.id_historia, h.id_paciente, h.antecedentes, h.observaciones_generales, 
                                        h.observaciones_clinicas, h.fecha_apertura, h.motivo_consulta, h.diagnostico, h.id_odontologo 
                                 FROM dbo.HistoriaClinica h
                                 INNER JOIN dbo.Paciente p ON h.id_paciente = p.id_paciente
                                 WHERE p.nombres LIKE @buscar OR p.apellidos LIKE @buscar OR h.diagnostico LIKE @buscar
                                 ORDER BY h.id_historia DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@buscar", "%" + texto + "%");
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearHistoria(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Metodo de ayuda para convertir fila SQL a objeto HistoriaClinica
        private static HistoriaClinica MapearHistoria(SqlDataReader dr)
        {
            return new HistoriaClinica
            {
                IdHistoria = Convert.ToInt32(dr["id_historia"]),
                IdPaciente = Convert.ToInt32(dr["id_paciente"]),
                Antecedentes = dr["antecedentes"] != DBNull.Value ? dr["antecedentes"].ToString()! : "",
                ObservacionesGenerales = dr["observaciones_generales"] != DBNull.Value ? dr["observaciones_generales"].ToString()! : "",
                ObservacionesClinicas = dr["observaciones_clinicas"] != DBNull.Value ? dr["observaciones_clinicas"].ToString()! : "",
                FechaApertura = Convert.ToDateTime(dr["fecha_apertura"]),
                MotivoConsulta = dr["motivo_consulta"] != DBNull.Value ? dr["motivo_consulta"].ToString()! : "",
                Diagnostico = dr["diagnostico"] != DBNull.Value ? dr["diagnostico"].ToString()! : "",
                IdOdontologo = Convert.ToInt32(dr["id_odontologo"])
            };
        }
    }
}
