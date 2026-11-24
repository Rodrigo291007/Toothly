using System;

namespace Sistema_Toothly.Clases
{
    public class Tratamiento
    {
        public int IdTratamiento { get; set; }
        public int IdPaciente { get; set; }
        public int IdOdontologo { get; set; }
        public string Descripcion { get; set; } = "";
        public DateTime FechaInicio { get; set; } = DateTime.Today;
        public DateTime FechaFin { get; set; } = DateTime.Today;
        public decimal Costo { get; set; }
        public string Estado { get; set; } = "Pendiente"; // Pendiente, En proceso, Finalizado, Anulado
    }
}
