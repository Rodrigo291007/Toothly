using System;
using System.Drawing;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmCambiarClave : Form
    {
        cls_encriptar objEncriptar = new cls_encriptar();

        public FrmCambiarClave()
        {
            InitializeComponent();
            IconoApp.Aplicar(this);
        }

        private void FrmCambiarClave_Load(object sender, EventArgs e)
        {
            btnGuardar.Image = Properties.Resources.ico_guardar;

            btnCancelar.Image = Properties.Resources.ico_cancelar;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string claveActual = txtClaveActual.Text.Trim();
            string claveNueva = txtClaveNueva.Text.Trim();
            string confirmar = txtConfirmar.Text.Trim();

            // Verificamos que no haya dejado cajas vacías
            if (string.IsNullOrEmpty(claveActual) || string.IsNullOrEmpty(claveNueva) || string.IsNullOrEmpty(confirmar))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscamos al usuario que tiene la sesión iniciada
            Usuario? u = null;
            foreach (var user in UsuarioDatos.usuarios)
            {
                if (user.IdUsuario == Sesion.IdUsuario)
                {
                    u = user;
                    break;
                }
            }

            // Comprobamos si la contraseña anterior coincide
            if (u == null || u.Clave != objEncriptar.encriptar(claveActual, cls_encriptar.Llave))
            {
                MessageBox.Show("La contraseña actual no es correcta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verificamos que haya escrito bien la nueva clave en ambas cajas
            if (claveNueva != confirmar)
            {
                MessageBox.Show("La nueva contraseña y su confirmación no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (claveNueva == claveActual)
            {
                MessageBox.Show("La nueva contraseña debe ser diferente a la actual.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cambiamos la clave en la base de datos y dejamos registro
            u.Clave = objEncriptar.encriptar(claveNueva, cls_encriptar.Llave);
            UsuarioDatos.Modificar(u);
            BitacoraDatos.RegistrarAccion("Cambio de contraseña", "El usuario " + u.NombreUsuario + " actualizó su contraseña.");

            MessageBox.Show("Contraseña actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Cerramos la ventana sin guardar cambios
            this.Close();
        }
    }
}
