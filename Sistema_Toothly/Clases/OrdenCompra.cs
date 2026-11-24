using System;

namespace Sistema_Toothly.Clases
{
    public class OrdenCompra
    {
        public int IdOrden { get; set; }
        public int IdProveedor { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Today;
        public decimal Total { get; set; }
        public string Estado { get; set; } = "Pendiente"; // Pendiente, Recibida, Cancelada
    }
}
