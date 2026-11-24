using System;
using System.Windows.Forms;

namespace Sistema_Toothly.Utilidades
{
    // Aplica los privilegios de la tabla RolPrivilegio (Roles y Privilegios) sobre los formularios.
    // Los IDs coinciden con dbo.Privilegio: 1 Insertar, 2 Consultar, 3 Modificar, 4 Eliminar, 5 Reportes.
    public static class Permisos
    {
        public const int Insertar = 1;
        public const int Consultar = 2;
        public const int Modificar = 3;
        public const int Eliminar = 4;
        public const int Reportes = 5;

        // Recorre los controles del formulario y oculta los botones cuyo privilegio no tiene el rol
        public static void Aplicar(Control raiz)
        {
            foreach (Control c in raiz.Controls)
            {
                if (c is Button b)
                {
                    int privilegio = PrivilegioDeBoton(b.Name);
                    if (privilegio > 0 && !Sesion.TienePrivilegio(privilegio))
                    {
                        b.Visible = false;
                    }
                }

                if (c.HasChildren) Aplicar(c);
            }
        }

        // Verificación dentro de la operación: ocultar un botón no basta para garantizar la seguridad
        public static bool Verificar(int privilegio)
        {
            if (Sesion.TienePrivilegio(privilegio)) return true;

            MessageBox.Show(
                "Su rol (" + Sesion.NombreRol + ") no tiene el privilegio \"" + Nombre(privilegio) + "\".\n" +
                "Solicite al Administrador que lo habilite en Seguridad > Roles y Privilegios.",
                "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        public static string Nombre(int privilegio)
        {
            switch (privilegio)
            {
                case Insertar: return "Insertar nuevos registros";
                case Consultar: return "Consultar / Filtrar";
                case Modificar: return "Modificar registros";
                case Eliminar: return "Eliminar / Dar de baja";
                case Reportes: return "Reportes";
                default: return "Desconocido";
            }
        }

        // Relaciona el nombre del botón con el privilegio que requiere (convención de nombres del proyecto)
        private static int PrivilegioDeBoton(string nombre)
        {
            if (string.IsNullOrEmpty(nombre)) return 0;

            if (nombre == "btnNuevo" || nombre == "btnGuardar" || nombre == "btnRegistrar" || nombre == "btnDespachar")
                return Insertar;

            if (nombre.StartsWith("btnModificar") || nombre == "btnReprogramar" || nombre == "btnRecibirOrden"
                || nombre == "btnCancelarCita" || nombre == "btnCancelarTratamiento")
                return Modificar;

            if (nombre.StartsWith("btnEliminar") || nombre == "btnDarDeBaja")
                return Eliminar;

            if (nombre == "btnImprimir")
                return Reportes;

            if (nombre == "btnBuscar" || nombre == "btnFiltrar" || nombre == "btnBuscarPaciente")
                return Consultar;

            return 0;
        }
    }
}
