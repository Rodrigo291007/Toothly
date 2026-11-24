using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Sistema_Toothly.Clases;

namespace Sistema_Toothly.Datos
{
    // Clase para administrar los proveedores en SQL Server
    public static class ProveedorDatos
    {
        // Propiedad de compatibilidad que consulta directo a SQL
        public static List<Proveedor> proveedores => Listar();

        // Obtiene todos los proveedores ordenados por nombre
        public static List<Proveedor> Listar()
        {
            List<Proveedor> lista = new List<Proveedor>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_proveedor, nombre, telefono, correo, direccion, estado FROM dbo.Proveedor ORDER BY nombre ASC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearProveedor(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Inserta un nuevo proveedor en la base de datos
        public static void Agregar(Proveedor p)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"INSERT INTO dbo.Proveedor (nombre, telefono, correo, direccion, estado)
                                 VALUES (@nombre, @telefono, @correo, @direccion, @estado);
                                 SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nombre", p.Nombre);
                    cmd.Parameters.AddWithValue("@telefono", (object?)p.Telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@correo", (object?)p.Correo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@direccion", (object?)p.Direccion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@estado", string.IsNullOrEmpty(p.Estado) ? "A" : p.Estado);

                    object idGen = cmd.ExecuteScalar();
                    if (idGen != null && idGen != DBNull.Value)
                    {
                        p.IdProveedor = Convert.ToInt32(idGen);
                    }
                }
            }
        }

        // Modifica un proveedor existente
        public static void Modificar(Proveedor p)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"UPDATE dbo.Proveedor 
                                 SET nombre = @nombre, telefono = @telefono, correo = @correo, 
                                     direccion = @direccion, estado = @estado 
                                 WHERE id_proveedor = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", p.IdProveedor);
                    cmd.Parameters.AddWithValue("@nombre", p.Nombre);
                    cmd.Parameters.AddWithValue("@telefono", (object?)p.Telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@correo", (object?)p.Correo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@direccion", (object?)p.Direccion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@estado", p.Estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Desactiva un proveedor cambiando su estado a 'I'
        public static void DarDeBaja(int idProveedor)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "UPDATE dbo.Proveedor SET estado = 'I' WHERE id_proveedor = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", idProveedor);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Busca proveedores por nombre
        public static List<Proveedor> Buscar(string texto)
        {
            List<Proveedor> lista = new List<Proveedor>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_proveedor, nombre, telefono, correo, direccion, estado FROM dbo.Proveedor WHERE nombre LIKE @buscar ORDER BY nombre ASC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@buscar", "%" + texto + "%");
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearProveedor(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Devuelve el nombre del proveedor por su ID
        public static string ObtenerNombre(int id)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT nombre FROM dbo.Proveedor WHERE id_proveedor = @id";
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

        // Metodo de ayuda para convertir fila SQL a objeto Proveedor
        private static Proveedor MapearProveedor(SqlDataReader dr)
        {
            return new Proveedor
            {
                IdProveedor = Convert.ToInt32(dr["id_proveedor"]),
                Nombre = dr["nombre"].ToString()!,
                Telefono = dr["telefono"] != DBNull.Value ? dr["telefono"].ToString()! : "",
                Correo = dr["correo"] != DBNull.Value ? dr["correo"].ToString()! : "",
                Direccion = dr["direccion"] != DBNull.Value ? dr["direccion"].ToString()! : "",
                Estado = dr["estado"].ToString()!
            };
        }
    }
}
