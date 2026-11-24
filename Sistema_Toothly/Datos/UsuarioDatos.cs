using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Sistema_Toothly.Clases;

namespace Sistema_Toothly.Datos
{
    // Clase para gestionar usuarios, roles y privilegios usando SQL Server
    public static class UsuarioDatos
    {
        // Propiedades que consultan directamente la base de datos para no romper ningun formulario
        public static List<Usuario> usuarios => Listar();
        public static List<Rol> roles => ListarRoles();
        public static List<Privilegio> privilegios => ListarPrivilegios();
        public static List<RolPrivilegio> rolPrivilegios
        {
            get => ListarRolPrivilegios();
            set => GuardarRolPrivilegios(value);
        }

        // Obtiene todos los usuarios desde la base de datos
        public static List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_usuario, nombre_usuario, clave, nombre_completo, correo, id_rol, estado FROM dbo.Usuario ORDER BY id_usuario";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["id_usuario"]),
                                NombreUsuario = dr["nombre_usuario"].ToString()!,
                                Clave = dr["clave"].ToString()!,
                                NombreCompleto = dr["nombre_completo"].ToString()!,
                                Correo = dr["correo"] != DBNull.Value ? dr["correo"].ToString()! : "",
                                IdRol = Convert.ToInt32(dr["id_rol"]),
                                Estado = dr["estado"].ToString()!
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // Inserta un nuevo usuario en la base de datos y obtiene su ID generado
        public static void Agregar(Usuario u)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"INSERT INTO dbo.Usuario (nombre_usuario, clave, nombre_completo, correo, id_rol, estado)
                                 VALUES (@usuario, @clave, @nombre, @correo, @rol, @estado);
                                 SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@usuario", u.NombreUsuario);
                    cmd.Parameters.AddWithValue("@clave", u.Clave);
                    cmd.Parameters.AddWithValue("@nombre", u.NombreCompleto);
                    cmd.Parameters.AddWithValue("@correo", (object?)u.Correo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@rol", u.IdRol);
                    cmd.Parameters.AddWithValue("@estado", string.IsNullOrEmpty(u.Estado) ? "A" : u.Estado);

                    object idGenerado = cmd.ExecuteScalar();
                    if (idGenerado != null && idGenerado != DBNull.Value)
                    {
                        u.IdUsuario = Convert.ToInt32(idGenerado);
                    }
                }
            }
        }

        // Actualiza los datos de un usuario existente
        public static void Modificar(Usuario u)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"UPDATE dbo.Usuario 
                                 SET nombre_usuario = @usuario, clave = @clave, nombre_completo = @nombre,
                                     correo = @correo, id_rol = @rol, estado = @estado
                                 WHERE id_usuario = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", u.IdUsuario);
                    cmd.Parameters.AddWithValue("@usuario", u.NombreUsuario);
                    cmd.Parameters.AddWithValue("@clave", u.Clave);
                    cmd.Parameters.AddWithValue("@nombre", u.NombreCompleto);
                    cmd.Parameters.AddWithValue("@correo", (object?)u.Correo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@rol", u.IdRol);
                    cmd.Parameters.AddWithValue("@estado", u.Estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Cambia el estado del usuario a 'I' (Inactivo)
        public static void DarDeBaja(int idUsuario)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "UPDATE dbo.Usuario SET estado = 'I' WHERE id_usuario = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Busca usuarios por nombre completo o por nombre de usuario
        public static List<Usuario> Buscar(string texto)
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_usuario, nombre_usuario, clave, nombre_completo, correo, id_rol, estado 
                                 FROM dbo.Usuario 
                                 WHERE nombre_completo LIKE @buscar OR nombre_usuario LIKE @buscar";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@buscar", "%" + texto + "%");
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["id_usuario"]),
                                NombreUsuario = dr["nombre_usuario"].ToString()!,
                                Clave = dr["clave"].ToString()!,
                                NombreCompleto = dr["nombre_completo"].ToString()!,
                                Correo = dr["correo"] != DBNull.Value ? dr["correo"].ToString()! : "",
                                IdRol = Convert.ToInt32(dr["id_rol"]),
                                Estado = dr["estado"].ToString()!
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // Valida las credenciales de inicio de sesion en SQL
        public static Usuario? ValidarLogin(string usuario, string clave)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_usuario, nombre_usuario, clave, nombre_completo, correo, id_rol, estado 
                                 FROM dbo.Usuario 
                                 WHERE nombre_usuario = @usuario AND clave = @clave AND estado = 'A'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["id_usuario"]),
                                NombreUsuario = dr["nombre_usuario"].ToString()!,
                                Clave = dr["clave"].ToString()!,
                                NombreCompleto = dr["nombre_completo"].ToString()!,
                                Correo = dr["correo"] != DBNull.Value ? dr["correo"].ToString()! : "",
                                IdRol = Convert.ToInt32(dr["id_rol"]),
                                Estado = dr["estado"].ToString()!
                            };
                        }
                    }
                }
            }
            return null;
        }

        // Obtiene el nombre del rol segun su ID
        public static string ObtenerNombreRol(int idRol)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT nombre_rol FROM dbo.Rol WHERE id_rol = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", idRol);
                    object res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value)
                    {
                        return res.ToString()!;
                    }
                }
            }
            return "";
        }

        // Obtiene solo los usuarios con rol de odontologo (id_rol = 3) y activos
        public static List<Usuario> ListarOdontologos()
        {
            List<Usuario> odontologos = new List<Usuario>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_usuario, nombre_usuario, clave, nombre_completo, correo, id_rol, estado 
                                 FROM dbo.Usuario 
                                 WHERE id_rol = 3 AND estado = 'A'
                                 ORDER BY nombre_completo";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            odontologos.Add(new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["id_usuario"]),
                                NombreUsuario = dr["nombre_usuario"].ToString()!,
                                Clave = dr["clave"].ToString()!,
                                NombreCompleto = dr["nombre_completo"].ToString()!,
                                Correo = dr["correo"] != DBNull.Value ? dr["correo"].ToString()! : "",
                                IdRol = Convert.ToInt32(dr["id_rol"]),
                                Estado = dr["estado"].ToString()!
                            });
                        }
                    }
                }
            }
            return odontologos;
        }

        // Verifica si un rol tiene un privilegio asignado
        public static bool TienePrivilegio(int idRol, int idPrivilegio)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT COUNT(*) FROM dbo.RolPrivilegio WHERE id_rol = @rol AND id_privilegio = @priv";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@rol", idRol);
                    cmd.Parameters.AddWithValue("@priv", idPrivilegio);
                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                    return cantidad > 0;
                }
            }
        }

        // Metodos auxiliares para roles y privilegios
        public static List<Rol> ListarRoles()
        {
            List<Rol> lista = new List<Rol>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_rol, nombre_rol FROM dbo.Rol ORDER BY id_rol";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Rol
                            {
                                IdRol = Convert.ToInt32(dr["id_rol"]),
                                NombreRol = dr["nombre_rol"].ToString()!
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public static List<Privilegio> ListarPrivilegios()
        {
            List<Privilegio> lista = new List<Privilegio>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_privilegio, nombre FROM dbo.Privilegio ORDER BY id_privilegio";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Privilegio
                            {
                                IdPrivilegio = Convert.ToInt32(dr["id_privilegio"]),
                                Nombre = dr["nombre"].ToString()!
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public static List<RolPrivilegio> ListarRolPrivilegios()
        {
            List<RolPrivilegio> lista = new List<RolPrivilegio>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_rol, id_privilegio FROM dbo.RolPrivilegio";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new RolPrivilegio
                            {
                                IdRol = Convert.ToInt32(dr["id_rol"]),
                                IdPrivilegio = Convert.ToInt32(dr["id_privilegio"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // Reemplaza la tabla RolPrivilegio dentro de una transacción:
        // si falla un INSERT no se pierden los privilegios que ya existían
        public static void GuardarRolPrivilegios(List<RolPrivilegio> nuevaLista)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            using (SqlTransaction tx = con.BeginTransaction())
            {
                try
                {
                    using (SqlCommand cmdBorrar = new SqlCommand("DELETE FROM dbo.RolPrivilegio", con, tx))
                    {
                        cmdBorrar.ExecuteNonQuery();
                    }

                    foreach (var rp in nuevaLista)
                    {
                        string query = "INSERT INTO dbo.RolPrivilegio (id_rol, id_privilegio) VALUES (@rol, @priv)";
                        using (SqlCommand cmdInsert = new SqlCommand(query, con, tx))
                        {
                            cmdInsert.Parameters.AddWithValue("@rol", rp.IdRol);
                            cmdInsert.Parameters.AddWithValue("@priv", rp.IdPrivilegio);
                            cmdInsert.ExecuteNonQuery();
                        }
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

        // Devuelve los IDs de privilegio asignados a un rol (se cargan en la sesión al iniciar)
        public static List<int> ObtenerPrivilegiosRol(int idRol)
        {
            List<int> lista = new List<int>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_privilegio FROM dbo.RolPrivilegio WHERE id_rol = @rol";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@rol", idRol);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(Convert.ToInt32(dr["id_privilegio"]));
                        }
                    }
                }
            }
            return lista;
        }
    }
}
