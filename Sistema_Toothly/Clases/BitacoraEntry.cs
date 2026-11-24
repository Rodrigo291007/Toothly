using System;

namespace Sistema_Toothly.Clases
{
    public class BitacoraEntry
    {
        public int IdBitacora { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Today;
        public string Hora { get; set; } = DateTime.Now.ToString("HH:mm:ss");
        public string Accion { get; set; } = "";
        public string Descripcion { get; set; } = "";
    }
}
