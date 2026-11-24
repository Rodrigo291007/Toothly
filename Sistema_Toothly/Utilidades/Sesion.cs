using System;
using System.Collections.Generic;
using Sistema_Toothly.Clases;

namespace Sistema_Toothly.Utilidades
{
    // Clase para guardar la sesión actual del usuario
    public static class Sesion
    {
        public static int IdUsuario { get; set; }
        public static string NombreUsuario { get; set; } = "";
        public static string NombreCompleto { get; set; } = "";
        public static int IdRol { get; set; }
        public static string NombreRol { get; set; } = "";

        // Privilegios del rol (tabla RolPrivilegio) cargados al iniciar sesión
        private static HashSet<int> privilegios = new HashSet<int>();

        // Carga los privilegios del rol actual desde la base de datos
        public static void CargarPrivilegios(IEnumerable<int> ids)
        {
            privilegios = new HashSet<int>(ids);
        }

        // El Administrador siempre conserva todos los privilegios para no quedar bloqueado del sistema
        public static bool TienePrivilegio(int idPrivilegio)
        {
            if (NombreRol == "Administrador") return true;
            return privilegios.Contains(idPrivilegio);
        }

        // Limpia la sesión al cerrar sesión
        public static void CerrarSesion()
        {
            IdUsuario = 0;
            NombreUsuario = "";
            NombreCompleto = "";
            IdRol = 0;
            NombreRol = "";
            privilegios.Clear();
        }

        // Verifica si el rol actual es el indicado
        public static bool EsRol(string rol)
        {
            return NombreRol == rol;
        }
    }
}
