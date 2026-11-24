using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmBitacora : Form, IRecargable
    {
        private DateTime? desdeFiltro = null;
        private DateTime? hastaFiltro = null;
        private int usuarioFiltro = 0;

        public FrmBitacora()
        {
            InitializeComponent();
        }

        private void FrmBitacora_Load(object sender, EventArgs e)
        {
            // Botón de cerrar en el encabezado
            AgregarBotonCerrar();

            // Colores de los botones
            btnFiltrar.Image = Properties.Resources.ico_buscar;

            // Formato de la tabla
            ConfigurarEstiloTabla();
            ConfigurarColumnas();
            LlenarComboUsuarios();

            dtpDesde.Value = DateTime.Today.AddDays(-7);
            dtpHasta.Value = DateTime.Today;

            CargarBitacora(null, null, 0);
        }

        // Agregamos un botón para cerrar la ventana en la esquina superior
        private void AgregarBotonCerrar()
        {
            Button btnCerrar = new Button();
            btnCerrar.Text = "✕ Cerrar";
            btnCerrar.Size = new Size(85, 30);
            btnCerrar.Location = new Point(pnlEncabezado.Width - 95, 10);
            btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrar.Click += (s, e) => this.Close();
            pnlEncabezado.Controls.Add(btnCerrar);
            btnCerrar.BackColor = Color.FromArgb(220, 38, 38);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Cursor = Cursors.Hand;
        }

        // Configuración visual de la tabla de bitácora
        private void ConfigurarEstiloTabla()
        {
            dgvBitacora.BackgroundColor = Color.White;
            dgvBitacora.BorderStyle = BorderStyle.None;
            dgvBitacora.EnableHeadersVisualStyles = false;
            dgvBitacora.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(108, 180, 234);
            dgvBitacora.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBitacora.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvBitacora.ColumnHeadersHeight = 35;
            dgvBitacora.DefaultCellStyle.SelectionBackColor = Color.FromArgb(186, 230, 253);
            dgvBitacora.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvBitacora.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 254);
            dgvBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBitacora.ReadOnly = true;
            dgvBitacora.RowHeadersVisible = false;
            dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ConfigurarColumnas()
        {
            dgvBitacora.Columns.Clear();
            dgvBitacora.Columns.Add("IdBitacora", "ID");
            dgvBitacora.Columns.Add("Usuario", "Usuario");
            dgvBitacora.Columns.Add("Fecha", "Fecha");
            dgvBitacora.Columns.Add("Hora", "Hora");
            dgvBitacora.Columns.Add("Accion", "Acción");
            dgvBitacora.Columns.Add("Descripcion", "Descripción");

            dgvBitacora.Columns["IdBitacora"].Width = 60;
            dgvBitacora.Columns["Usuario"].Width = 120;
            dgvBitacora.Columns["Fecha"].Width = 100;
            dgvBitacora.Columns["Hora"].Width = 90;
            dgvBitacora.Columns["Accion"].Width = 180;
            dgvBitacora.Columns["Descripcion"].Width = 350;
        }

        private void LlenarComboUsuarios()
        {
            cmbUsuario.Items.Clear();
            cmbUsuario.Items.Add("-- Todos los usuarios --");
            foreach (var u in UsuarioDatos.usuarios)
            {
                cmbUsuario.Items.Add(u.NombreUsuario);
            }
            cmbUsuario.SelectedIndex = 0;
        }

        public void Recargar()
        {
            Recarga.Combos(LlenarComboUsuarios, cmbUsuario);
            CargarBitacora(desdeFiltro, hastaFiltro, usuarioFiltro);
        }

        private void CargarBitacora(DateTime? desde, DateTime? hasta, int idUsuario)
        {
            desdeFiltro = desde;
            hastaFiltro = hasta;
            usuarioFiltro = idUsuario;
            dgvBitacora.Rows.Clear();
            List<BitacoraEntry> lista;

            if (desde == null && hasta == null && idUsuario == 0)
            {
                lista = BitacoraDatos.Listar();
            }
            else
            {
                lista = BitacoraDatos.Buscar(idUsuario, desde, hasta);
            }

            foreach (var b in lista)
            {
                string nombreUsuario = "Sistema";
                foreach (var u in UsuarioDatos.usuarios)
                {
                    if (u.IdUsuario == b.IdUsuario)
                    {
                        nombreUsuario = u.NombreUsuario;
                        break;
                    }
                }
                dgvBitacora.Rows.Add(b.IdBitacora, nombreUsuario, b.Fecha.ToString("dd/MM/yyyy"), b.Hora, b.Accion, b.Descripcion);
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            int idUsuario = 0;
            if (cmbUsuario.SelectedIndex > 0)
            {
                string nombre = cmbUsuario.SelectedItem.ToString();
                foreach (var u in UsuarioDatos.usuarios)
                {
                    if (u.NombreUsuario == nombre)
                    {
                        idUsuario = u.IdUsuario;
                        break;
                    }
                }
            }

            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date;

            if (desde > hasta)
            {
                MessageBox.Show("La fecha 'Desde' no puede ser mayor que la fecha 'Hasta'.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarBitacora(desde, hasta, idUsuario);
        }

        private void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            cmbUsuario.SelectedIndex = 0;
            dtpDesde.Value = DateTime.Today.AddDays(-7);
            dtpHasta.Value = DateTime.Today;
            CargarBitacora(null, null, 0);
        }
    }
}
