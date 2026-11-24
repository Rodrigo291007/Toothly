using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmLogin : Form
    {
        private System.Windows.Forms.Timer tmrSplash;
        private double opacidad = 0;
        cls_encriptar objEncriptar = new cls_encriptar();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            // Icono de TOOTHLY en la barra de título y en la barra de tareas
            IconoApp.Aplicar(this);

            // Mostramos el logo azul con fondo transparente integrado perfectamente con el fondo blanco
            picLogo.Image = Properties.Resources.logo_azul;
            picLogo.Visible = true;
            lblLogoTexto.Visible = false;

            // Cambiamos el color al pasar el mouse por encima
            btnIngresar.MouseEnter += (s, ev) => { btnIngresar.BackColor = Color.FromArgb(61, 143, 203); };
            btnIngresar.MouseLeave += (s, ev) => { btnIngresar.BackColor = Color.FromArgb(108, 180, 234); };

            btnSalirApp.MouseEnter += (s, ev) => { btnSalirApp.BackColor = Color.FromArgb(185, 28, 28); };
            btnSalirApp.MouseLeave += (s, ev) => { btnSalirApp.BackColor = Color.FromArgb(220, 38, 38); };

            // Dibujamos un degradado azul en el panel izquierdo
            pnlIzquierdo.Paint += PnlIzquierdo_Paint;

            // Limpiamos las cajas de texto
            txtUsuario.Text = "";
            txtClave.Text = "";

            // Efecto suave de aparición de la ventana al abrir
            this.Opacity = 0;
            opacidad = 0;
            tmrSplash = new System.Windows.Forms.Timer();
            tmrSplash.Interval = 30;
            tmrSplash.Tick += TmrSplash_Tick;
            tmrSplash.Start();
        }

        private void PnlIzquierdo_Paint(object? sender, PaintEventArgs ev)
        {
            // Pintamos un fondo degradado con los tonos de azul del sistema
            using (LinearGradientBrush brush = new LinearGradientBrush(
                pnlIzquierdo.ClientRectangle,
                Color.FromArgb(108, 180, 234),
                Color.FromArgb(61, 143, 203),
                LinearGradientMode.Vertical))
            {
                ev.Graphics.FillRectangle(brush, pnlIzquierdo.ClientRectangle);
            }
        }

        private void TmrSplash_Tick(object? sender, EventArgs e)
        {
            // Va aumentando la opacidad poco a poco
            opacidad += 0.05;
            if (opacidad >= 1.0)
            {
                opacidad = 1.0;
                tmrSplash.Stop();
                tmrSplash.Dispose();
            }
            this.Opacity = opacidad;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtClave.Text.Trim();

            // Revisamos que no hayan dejado los campos en blanco
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(clave))
            {
                MessageBox.Show("Por favor ingrese su usuario y contraseña.", "Campos Requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EncriptarClavesPendientes();

            // Validamos si el usuario y la contraseña existen
            Usuario? u = UsuarioDatos.ValidarLogin(usuario, objEncriptar.encriptar(clave, cls_encriptar.Llave));
            if (u == null)
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Guardamos la información del usuario en la sesión para usarla en todo el sistema
            Sesion.IdUsuario = u.IdUsuario;
            Sesion.NombreUsuario = u.NombreUsuario;
            Sesion.NombreCompleto = u.NombreCompleto;
            Sesion.IdRol = u.IdRol;
            Sesion.NombreRol = UsuarioDatos.ObtenerNombreRol(u.IdRol);
            // Privilegios reales del rol (Seguridad > Roles y Privilegios)
            Sesion.CargarPrivilegios(UsuarioDatos.ObtenerPrivilegiosRol(u.IdRol));

            // Dejamos registro en la bitácora de que el usuario inició sesión
            BitacoraDatos.RegistrarAccion("Inicio de sesión", "El usuario " + u.NombreUsuario + " inició sesión exitosamente.");

            // Abrimos la pantalla principal y ocultamos el login
            FrmPrincipal frmPrincipal = new FrmPrincipal();
            this.Hide();
            frmPrincipal.Show();
        }

        private void EncriptarClavesPendientes()
        {
            foreach (Usuario usr in UsuarioDatos.Listar())
            {
                if (objEncriptar.desencriptar(usr.Clave, cls_encriptar.Llave) == cls_encriptar.ErrorDesencriptar)
                {
                    usr.Clave = objEncriptar.encriptar(usr.Clave, cls_encriptar.Llave);
                    UsuarioDatos.Modificar(usr);
                }
            }
        }

        private void btnSalirApp_Click(object sender, EventArgs e)
        {
            // Cerramos toda la aplicación
            Application.Exit();
        }
    }
}
