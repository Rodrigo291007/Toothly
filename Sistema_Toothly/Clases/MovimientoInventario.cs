using System;

namespace Sistema_Toothly.Clases
{
    public class MovimientoInventario
    {
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public string Tipo { get; set; } = "Entrada"; // Entrada, Salida, Ajuste
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Descripcion { get; set; } = "";
    }
}
