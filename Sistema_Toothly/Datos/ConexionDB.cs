using System;
using Microsoft.Data.SqlClient;

namespace Sistema_Toothly.Datos
{
    // Clase sencilla para manejar la conexion a SQL Server
    // Usamos el servidor localhost con autenticacion de Windows
    public static class ConexionDB
    {
        // Cadena de conexion a la base de datos ToothlyDB en localhost
        private static string cadenaConexion = "Server=localhost;Database=ToothlyDB;Integrated Security=True;TrustServerCertificate=True;";

        // Metodo que devuelve una conexion lista y abierta
        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            return conexion;
        }

        // Metodo opcional por si queremos probar si la base de datos responde
        public static bool ProbarConexion()
        {
            try
            {
                using (SqlConnection con = ObtenerConexion())
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
