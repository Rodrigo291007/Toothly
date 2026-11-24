using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Sistema_Toothly.Clases;

namespace Sistema_Toothly.Datos
{
    // Clase para administrar los pacientes en SQL Server
    public static class PacienteDatos
    {
        // Propiedad de compatibilidad que consulta directo a SQL
        public static List<Paciente> pacientes => Listar();

        // Obtiene todos los pacientes registrados
        public static List<Paciente> Listar()
        {
            List<Paciente> lista = new List<Paciente>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_paciente, nombres, apellidos, dui, fecha_nacimiento, sexo, 
                                        telefono, correo, direccion, alergias, antecedentes, estado, fecha_registro 
                                 FROM dbo.Paciente 
                                 ORDER BY id_paciente DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearPaciente(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Inserta un nuevo paciente en la base de datos
        public static void Agregar(Paciente p)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"INSERT INTO dbo.Paciente (nombres, apellidos, dui, fecha_nacimiento, sexo, 
                                                         telefono, correo, direccion, alergias, antecedentes, estado, fecha_registro)
                                 VALUES (@nombres, @apellidos, @dui, @fechaNac, @sexo, 
                                         @telefono, @correo, @direccion, @alergias, @antecedentes, @estado, @fechaReg);
                                 SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nombres", p.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", p.Apellidos);
                    cmd.Parameters.AddWithValue("@dui", (object?)p.Dui ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaNac", p.FechaNacimiento.Date);
                    cmd.Parameters.AddWithValue("@sexo", (object?)p.Sexo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@telefono", (object?)p.Telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@correo", (object?)p.Correo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@direccion", (object?)p.Direccion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@alergias", (object?)p.Alergias ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@antecedentes", (object?)p.Antecedentes ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@estado", string.IsNullOrEmpty(p.Estado) ? "A" : p.Estado);
                    cmd.Parameters.AddWithValue("@fechaReg", DateTime.Now);

                    object idGen = cmd.ExecuteScalar();
                    if (idGen != null && idGen != DBNull.Value)
                    {
                        p.IdPaciente = Convert.ToInt32(idGen);
                    }
                }

                // Creamos su estado de cuenta inicial con saldo en $0.00
                string queryCuenta = "IF NOT EXISTS (SELECT 1 FROM dbo.EstadoCuenta WHERE id_paciente = @idPac) " +
                                     "INSERT INTO dbo.EstadoCuenta (id_paciente, saldo_actual) VALUES (@idPac, 0.00)";
                using (SqlCommand cmdCuenta = new SqlCommand(queryCuenta, con))
                {
                    cmdCuenta.Parameters.AddWithValue("@idPac", p.IdPaciente);
                    cmdCuenta.ExecuteNonQuery();
                }
            }
        }

        // Actualiza la informacion de un paciente existente
        public static void Modificar(Paciente p)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"UPDATE dbo.Paciente 
                                 SET nombres = @nombres, apellidos = @apellidos, dui = @dui, 
                                     fecha_nacimiento = @fechaNac, sexo = @sexo, telefono = @telefono, 
                                     correo = @correo, direccion = @direccion, alergias = @alergias, 
                                     antecedentes = @antecedentes, estado = @estado
                                 WHERE id_paciente = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", p.IdPaciente);
                    cmd.Parameters.AddWithValue("@nombres", p.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", p.Apellidos);
                    cmd.Parameters.AddWithValue("@dui", (object?)p.Dui ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaNac", p.FechaNacimiento.Date);
                    cmd.Parameters.AddWithValue("@sexo", (object?)p.Sexo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@telefono", (object?)p.Telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@correo", (object?)p.Correo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@direccion", (object?)p.Direccion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@alergias", (object?)p.Alergias ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@antecedentes", (object?)p.Antecedentes ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@estado", p.Estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Cambia el estado del paciente a 'I' (Inactivo)
        public static void DarDeBaja(int idPaciente)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "UPDATE dbo.Paciente SET estado = 'I' WHERE id_paciente = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", idPaciente);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Busca pacientes por nombre, apellido o DUI
        public static List<Paciente> Buscar(string texto)
        {
            List<Paciente> lista = new List<Paciente>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_paciente, nombres, apellidos, dui, fecha_nacimiento, sexo, 
                                        telefono, correo, direccion, alergias, antecedentes, estado, fecha_registro 
                                 FROM dbo.Paciente 
                                 WHERE nombres LIKE @buscar OR apellidos LIKE @buscar OR dui LIKE @buscar
                                 ORDER BY id_paciente DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@buscar", "%" + texto + "%");
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearPaciente(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Filtra pacientes por estado ('A' o 'I')
        public static List<Paciente> BuscarPorEstado(string estado)
        {
            List<Paciente> lista = new List<Paciente>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_paciente, nombres, apellidos, dui, fecha_nacimiento, sexo, 
                                        telefono, correo, direccion, alergias, antecedentes, estado, fecha_registro 
                                 FROM dbo.Paciente 
                                 WHERE estado = @estado
                                 ORDER BY id_paciente DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@estado", estado);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearPaciente(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Obtiene un paciente especifico por su ID
        public static Paciente? ObtenerPorId(int id)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_paciente, nombres, apellidos, dui, fecha_nacimiento, sexo, 
                                        telefono, correo, direccion, alergias, antecedentes, estado, fecha_registro 
                                 FROM dbo.Paciente 
                                 WHERE id_paciente = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return MapearPaciente(dr);
                        }
                    }
                }
            }
            return null;
        }

        // Devuelve el nombre completo del paciente por su ID
        public static string ObtenerNombre(int id)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT RTRIM(nombres) + ' ' + RTRIM(apellidos) FROM dbo.Paciente WHERE id_paciente = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    object res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value)
                    {
                        return res.ToString()!;
                    }
                }
            }
            return "";
        }

        // Metodo de ayuda para convertir la fila de la base de datos en un objeto Paciente
        private static Paciente MapearPaciente(SqlDataReader dr)
        {
            return new Paciente
            {
                IdPaciente = Convert.ToInt32(dr["id_paciente"]),
                Nombres = dr["nombres"].ToString()!,
                Apellidos = dr["apellidos"].ToString()!,
                Dui = dr["dui"] != DBNull.Value ? dr["dui"].ToString()! : "",
                FechaNacimiento = dr["fecha_nacimiento"] != DBNull.Value ? Convert.ToDateTime(dr["fecha_nacimiento"]) : DateTime.Today,
                Sexo = dr["sexo"] != DBNull.Value ? dr["sexo"].ToString()! : "M",
                Telefono = dr["telefono"] != DBNull.Value ? dr["telefono"].ToString()! : "",
                Correo = dr["correo"] != DBNull.Value ? dr["correo"].ToString()! : "",
                Direccion = dr["direccion"] != DBNull.Value ? dr["direccion"].ToString()! : "",
                Alergias = dr["alergias"] != DBNull.Value ? dr["alergias"].ToString()! : "",
                Antecedentes = dr["antecedentes"] != DBNull.Value ? dr["antecedentes"].ToString()! : "",
                Estado = dr["estado"].ToString()!,
                FechaRegistro = Convert.ToDateTime(dr["fecha_registro"])
            };
        }
    }
}
