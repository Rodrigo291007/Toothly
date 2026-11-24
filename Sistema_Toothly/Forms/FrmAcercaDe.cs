using System;
using Sistema_Toothly.Utilidades;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema_Toothly.Forms
{
    public partial class FrmAcercaDe : Form
    {
        public FrmAcercaDe()
        {
            InitializeComponent();
            IconoApp.Aplicar(this);
        }

        private void FrmAcercaDe_Load(object sender, EventArgs e)
        {
            // Asignamos la imagen del logo con fondo transparente
            picLogo.Image = Properties.Resources.logo_azul;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            // Cerramos la ventana
            this.Close();
        }
    }
}
