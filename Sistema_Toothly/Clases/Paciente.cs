using System;

namespace Sistema_Toothly.Clases
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public string Nombres { get; set; } = "";
        public string Apellidos { get; set; } = "";
        public string Dui { get; set; } = "";
        public DateTime FechaNacimiento { get; set; } = DateTime.Today;
        public string Sexo { get; set; } = "M"; // M o F
        public string Telefono { get; set; } = "";
        public string Correo { get; set; } = "";
        public string Direccion { get; set; } = "";
        public string Alergias { get; set; } = "";
        public string Antecedentes { get; set; } = "";
        public string Estado { get; set; } = "A"; // A = Activo, I = Inactivo
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
