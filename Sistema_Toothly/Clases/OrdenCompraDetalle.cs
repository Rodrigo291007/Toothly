using System;

namespace Sistema_Toothly.Clases
{
    public class OrdenCompraDetalle
    {
        public int IdDetalle { get; set; }
        public int IdOrden { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
