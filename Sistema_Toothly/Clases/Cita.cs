using System;

namespace Sistema_Toothly.Clases
{
    public class Cita
    {
        public int IdCita { get; set; }
        public int IdPaciente { get; set; }
        public int IdOdontologo { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Today;
        public string Hora { get; set; } = "08:00";
        public string Motivo { get; set; } = "";
        public string Estado { get; set; } = "Programada"; // Programada, Atendida, Cancelada, Reprogramada
    }
}
