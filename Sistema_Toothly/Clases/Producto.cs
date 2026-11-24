using System;

namespace Sistema_Toothly.Clases
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public int IdCategoria { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public decimal PrecioCompra { get; set; }
        public DateTime FechaVencimiento { get; set; } = DateTime.Today.AddYears(1);
        public int UnidadesPorCaja { get; set; } = 1;
        public decimal PrecioVenta { get; set; }
    }
}
