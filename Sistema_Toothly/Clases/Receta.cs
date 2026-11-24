using System;

namespace Sistema_Toothly.Clases
{
    public class Receta
    {
        public int IdReceta { get; set; }
        public int IdPaciente { get; set; }
        public int IdTratamiento { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Today;
        public string Indicaciones { get; set; } = "";
    }
}
