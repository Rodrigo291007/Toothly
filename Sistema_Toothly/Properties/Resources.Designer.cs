using System.Drawing;
using System.Reflection;

namespace Sistema_Toothly.Properties
{
    // Imágenes del sistema (logos e íconos) incrustadas en el ejecutable.
    // Se leen como recursos incrustados para no depender de archivos .resx.
    public static class Resources
    {
        private static Bitmap Cargar(string archivo)
        {
            using var flujo = Assembly.GetExecutingAssembly().GetManifestResourceStream("Toothly." + archivo)!;
            using var temporal = new Bitmap(flujo);
            return new Bitmap(temporal);
        }

        public static Bitmap logo => Cargar("logo.png");
        public static Bitmap logo_blanco => Cargar("logo_blanco.png");
        public static Bitmap logo_azul => Cargar("logo_azul.png");
        public static Bitmap ico_alerta => Cargar("ico_alerta.png");
        public static Bitmap ico_ayuda => Cargar("ico_ayuda.png");
        public static Bitmap ico_baja => Cargar("ico_baja.png");
        public static Bitmap ico_bitacora => Cargar("ico_bitacora.png");
        public static Bitmap ico_buscar => Cargar("ico_buscar.png");
        public static Bitmap ico_cancelar => Cargar("ico_cancelar.png");
        public static Bitmap ico_candado => Cargar("ico_candado.png");
        public static Bitmap ico_cita => Cargar("ico_cita.png");
        public static Bitmap ico_cuenta => Cargar("ico_cuenta.png");
        public static Bitmap ico_editar => Cargar("ico_editar.png");
        public static Bitmap ico_grafico => Cargar("ico_grafico.png");
        public static Bitmap ico_guardar => Cargar("ico_guardar.png");
        public static Bitmap ico_historia => Cargar("ico_historia.png");
        public static Bitmap ico_imprimir => Cargar("ico_imprimir.png");
        public static Bitmap ico_inventario => Cargar("ico_inventario.png");
        public static Bitmap ico_nuevo => Cargar("ico_nuevo.png");
        public static Bitmap ico_orden => Cargar("ico_orden.png");
        public static Bitmap ico_paciente => Cargar("ico_paciente.png");
        public static Bitmap ico_proveedor => Cargar("ico_proveedor.png");
        public static Bitmap ico_receta => Cargar("ico_receta.png");
        public static Bitmap ico_reporte => Cargar("ico_reporte.png");
        public static Bitmap ico_rol => Cargar("ico_rol.png");
        public static Bitmap ico_salir => Cargar("ico_salir.png");
        public static Bitmap ico_tratamiento => Cargar("ico_tratamiento.png");
        public static Bitmap ico_usuario => Cargar("ico_usuario.png");
    }
}
