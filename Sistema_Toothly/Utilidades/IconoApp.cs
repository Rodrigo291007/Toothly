using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Sistema_Toothly.Utilidades
{
    // Icono del sistema (diente de TOOTHLY) para las ventanas principales.
    // Se toma de Recursos\toothly.ico (copiado junto al programa); si no existe,
    // se usa el icono incrustado en el .exe mediante <ApplicationIcon> del .csproj.
    public static class IconoApp
    {
        private static Icon? icono;
        private static bool cargado;

        public static Icon? Obtener()
        {
            if (cargado) return icono;
            cargado = true;
            try
            {
                string ruta = Path.Combine(AppContext.BaseDirectory, "Recursos", "toothly.ico");
                if (File.Exists(ruta))
                {
                    icono = new Icon(ruta);
                }
                else if (!string.IsNullOrEmpty(Environment.ProcessPath))
                {
                    icono = Icon.ExtractAssociatedIcon(Environment.ProcessPath);
                }
            }
            catch
            {
                icono = null; // si falla, la ventana conserva el icono por defecto
            }
            return icono;
        }

        public static void Aplicar(Form formulario)
        {
            Icon? i = Obtener();
            if (i != null) formulario.Icon = i;
        }
    }
}
