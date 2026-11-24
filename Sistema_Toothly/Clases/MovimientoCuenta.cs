using System;

namespace Sistema_Toothly.Clases
{
    public class MovimientoCuenta
    {
        public int IdMovimiento { get; set; }
        public int IdEstado { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Tipo { get; set; } = "Cargo"; // Cargo o Abono
        public string Concepto { get; set; } = "";
        public decimal Monto { get; set; }
    }
}
