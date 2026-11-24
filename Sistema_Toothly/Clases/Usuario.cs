using System;

namespace Sistema_Toothly.Clases
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = "";
        public string Clave { get; set; } = "";
        public string NombreCompleto { get; set; } = "";
        public string Correo { get; set; } = "";
        public int IdRol { get; set; }
        public string Estado { get; set; } = "A"; // A = Activo, I = Inactivo
    }
}
