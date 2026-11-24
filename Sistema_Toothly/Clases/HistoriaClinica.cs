using System;

namespace Sistema_Toothly.Clases
{
    public class HistoriaClinica
    {
        public int IdHistoria { get; set; }
        public int IdPaciente { get; set; }
        public string Antecedentes { get; set; } = "";
        public string ObservacionesGenerales { get; set; } = "";
        public string ObservacionesClinicas { get; set; } = ""; // Observaciones por pieza dental
        public DateTime FechaApertura { get; set; } = DateTime.Now;
        public string MotivoConsulta { get; set; } = "";
        public string Diagnostico { get; set; } = "";
        public int IdOdontologo { get; set; }
    }
}
