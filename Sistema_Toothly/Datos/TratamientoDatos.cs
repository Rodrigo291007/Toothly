using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Sistema_Toothly.Clases;

namespace Sistema_Toothly.Datos
{
    // Clase para administrar los tratamientos dentales en SQL Server
    public static class TratamientoDatos
    {
        // Propiedad de compatibilidad que consulta directo a SQL
        public static List<Tratamiento> tratamientos => Listar();

        // Obtiene todos los tratamientos ordenados por ID descendente
        public static List<Tratamiento> Listar()
        {
            List<Tratamiento> lista = new List<Tratamiento>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_tratamiento, id_paciente, id_odontologo, descripcion, 
                                        fecha_inicio, fecha_fin, costo, estado 
                                 FROM dbo.Tratamiento 
                                 ORDER BY id_tratamiento DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearTratamiento(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Inserta un nuevo tratamiento en la base de datos
        public static void Agregar(Tratamiento t)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"INSERT INTO dbo.Tratamiento 
                                 (id_paciente, id_odontologo, descripcion, fecha_inicio, fecha_fin, costo, estado)
                                 VALUES 
                                 (@idPac, @idOdo, @desc, @fInicio, @fFin, @costo, @estado);
                                 SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idPac", t.IdPaciente);
                    cmd.Parameters.AddWithValue("@idOdo", t.IdOdontologo <= 0 ? 1 : t.IdOdontologo);
                    cmd.Parameters.AddWithValue("@desc", t.Descripcion);
                    cmd.Parameters.AddWithValue("@fInicio", t.FechaInicio.Date);
                    cmd.Parameters.AddWithValue("@fFin", t.FechaFin.Date);
                    cmd.Parameters.AddWithValue("@costo", t.Costo);
                    cmd.Parameters.AddWithValue("@estado", string.IsNullOrEmpty(t.Estado) ? "Pendiente" : t.Estado);

                    object idGen = cmd.ExecuteScalar();
                    if (idGen != null && idGen != DBNull.Value)
                    {
                        t.IdTratamiento = Convert.ToInt32(idGen);
                    }
                }
            }
        }

        // Modifica un tratamiento existente
        public static void Modificar(Tratamiento t)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"UPDATE dbo.Tratamiento 
                                 SET id_paciente = @idPac, id_odontologo = @idOdo, descripcion = @desc, 
                                     fecha_inicio = @fInicio, fecha_fin = @fFin, costo = @costo, estado = @estado 
                                 WHERE id_tratamiento = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", t.IdTratamiento);
                    cmd.Parameters.AddWithValue("@idPac", t.IdPaciente);
                    cmd.Parameters.AddWithValue("@idOdo", t.IdOdontologo <= 0 ? 1 : t.IdOdontologo);
                    cmd.Parameters.AddWithValue("@desc", t.Descripcion);
                    cmd.Parameters.AddWithValue("@fInicio", t.FechaInicio.Date);
                    cmd.Parameters.AddWithValue("@fFin", t.FechaFin.Date);
                    cmd.Parameters.AddWithValue("@costo", t.Costo);
                    cmd.Parameters.AddWithValue("@estado", t.Estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Anula un tratamiento cambiando su estado a 'Anulado'
        public static void DarDeBaja(int idTratamiento)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "UPDATE dbo.Tratamiento SET estado = 'Anulado' WHERE id_tratamiento = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", idTratamiento);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Busca tratamientos por nombre de paciente o descripcion
        public static List<Tratamiento> Buscar(string texto)
        {
            List<Tratamiento> lista = new List<Tratamiento>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT t.id_tratamiento, t.id_paciente, t.id_odontologo, t.descripcion, 
                                        t.fecha_inicio, t.fecha_fin, t.costo, t.estado 
                                 FROM dbo.Tratamiento t
                                 INNER JOIN dbo.Paciente p ON t.id_paciente = p.id_paciente
                                 WHERE p.nombres LIKE @buscar OR p.apellidos LIKE @buscar OR t.descripcion LIKE @buscar
                                 ORDER BY t.id_tratamiento DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@buscar", "%" + texto + "%");
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearTratamiento(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Busca todos los tratamientos de un paciente en especifico
        public static List<Tratamiento> BuscarPorPaciente(int idPaciente)
        {
            List<Tratamiento> lista = new List<Tratamiento>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_tratamiento, id_paciente, id_odontologo, descripcion, 
                                        fecha_inicio, fecha_fin, costo, estado 
                                 FROM dbo.Tratamiento 
                                 WHERE id_paciente = @idPac 
                                 ORDER BY id_tratamiento DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idPac", idPaciente);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearTratamiento(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Metodo de ayuda para convertir fila SQL a objeto Tratamiento
        private static Tratamiento MapearTratamiento(SqlDataReader dr)
        {
            return new Tratamiento
            {
                IdTratamiento = Convert.ToInt32(dr["id_tratamiento"]),
                IdPaciente = Convert.ToInt32(dr["id_paciente"]),
                IdOdontologo = Convert.ToInt32(dr["id_odontologo"]),
                Descripcion = dr["descripcion"].ToString()!,
                FechaInicio = Convert.ToDateTime(dr["fecha_inicio"]),
                FechaFin = dr["fecha_fin"] != DBNull.Value ? Convert.ToDateTime(dr["fecha_fin"]) : DateTime.Today,
                Costo = Convert.ToDecimal(dr["costo"]),
                Estado = dr["estado"].ToString()!
            };
        }
    }
}
