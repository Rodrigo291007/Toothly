using System;

namespace Sistema_Toothly.Clases
{
    public class RecetaDetalle
    {
        public int IdDetalle { get; set; }
        public int IdReceta { get; set; }
        public string Medicamento { get; set; } = "";
        public string Dosis { get; set; } = "";
        public string Frecuencia { get; set; } = "";
        public string Duracion { get; set; } = "";
        public int? IdProducto { get; set; }
        public int Cantidad { get; set; }
        public int CantidadDespachada { get; set; }
    }
}
