using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmFichaPaciente : Form, IRecargable
    {
        public FrmFichaPaciente()
        {
            InitializeComponent();
        }

        private void FrmFichaPaciente_Load(object sender, EventArgs e)
        {
            // Botón de cerrar en el encabezado
            AgregarBotonCerrar();

            // Configuramos las dos tablas de la ficha
            ConfigurarEstiloTabla(dgvCitasFicha);
            ConfigurarEstiloTabla(dgvTratamientosFicha);

            // Botón de cargar paciente
            btnCargar.Image = Properties.Resources.ico_buscar;

            ConfigurarColumnas();
            LlenarComboPacientes();
        }

        public void Recargar()
        {
            Recarga.Combos(LlenarComboPacientes, cmbPacientes);
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

        // Colores y diseño de las tablas
        private void ConfigurarEstiloTabla(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(108, 180, 234);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 35;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(186, 230, 253);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 254);
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ConfigurarColumnas()
        {
            // Citas
            dgvCitasFicha.Columns.Clear();
            dgvCitasFicha.Columns.Add("Fecha", "Fecha");
            dgvCitasFicha.Columns.Add("Hora", "Hora");
            dgvCitasFicha.Columns.Add("Odontologo", "Odontólogo");
            dgvCitasFicha.Columns.Add("Motivo", "Motivo");
            dgvCitasFicha.Columns.Add("Estado", "Estado");

            dgvCitasFicha.Columns["Fecha"].Width = 100;
            dgvCitasFicha.Columns["Hora"].Width = 80;
            dgvCitasFicha.Columns["Odontologo"].Width = 180;
            dgvCitasFicha.Columns["Motivo"].Width = 240;
            dgvCitasFicha.Columns["Estado"].Width = 110;

            // Tratamientos
            dgvTratamientosFicha.Columns.Clear();
            dgvTratamientosFicha.Columns.Add("Descripcion", "Descripción");
            dgvTratamientosFicha.Columns.Add("Odontologo", "Odontólogo");
            dgvTratamientosFicha.Columns.Add("Inicio", "Fecha Inicio");
            dgvTratamientosFicha.Columns.Add("Fin", "Fecha Fin");
            dgvTratamientosFicha.Columns.Add("Costo", "Costo ($)");
            dgvTratamientosFicha.Columns.Add("Estado", "Estado");

            dgvTratamientosFicha.Columns["Descripcion"].Width = 240;
            dgvTratamientosFicha.Columns["Odontologo"].Width = 180;
            dgvTratamientosFicha.Columns["Inicio"].Width = 100;
            dgvTratamientosFicha.Columns["Fin"].Width = 100;
            dgvTratamientosFicha.Columns["Costo"].Width = 90;
            dgvTratamientosFicha.Columns["Estado"].Width = 110;
        }

        private void LlenarComboPacientes()
        {
            cmbPacientes.Items.Clear();
            foreach (var p in PacienteDatos.pacientes)
            {
                cmbPacientes.Items.Add(p.IdPaciente + " - " + p.Nombres + " " + p.Apellidos + " (" + p.Dui + ")");
            }
            if (cmbPacientes.Items.Count > 0)
            {
                cmbPacientes.SelectedIndex = 0;
                CargarFicha();
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            CargarFicha();
        }

        private void cmbPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFicha();
        }

        private void CargarFicha()
        {
            if (cmbPacientes.SelectedIndex < 0) return;

            string item = cmbPacientes.SelectedItem.ToString();
            int idPaciente = Convert.ToInt32(item.Split('-')[0].Trim());

            Paciente? p = PacienteDatos.ObtenerPorId(idPaciente);
            if (p == null) return;

            // Mostrar datos generales
            lblNombre.Text = "Paciente: " + p.Nombres + " " + p.Apellidos;
            lblDui.Text = "DUI: " + p.Dui;
            lblTelefono.Text = "Teléfono: " + p.Telefono;
            lblCorreo.Text = "Correo: " + p.Correo;
            lblAlergias.Text = "Alergias: " + (string.IsNullOrEmpty(p.Alergias) ? "Ninguna reportada" : p.Alergias);

            // Obtener saldo
            EstadoCuenta? ec = EstadoCuentaDatos.ObtenerPorPaciente(idPaciente);
            decimal saldo = ec != null ? ec.SaldoActual : 0.00m;
            lblSaldo.Text = "Saldo Actual: " + EstadoCuentaDatos.FormatearSaldo(saldo);

            // Cargar citas
            dgvCitasFicha.Rows.Clear();
            List<Usuario> usuarios = UsuarioDatos.Listar(); // una sola consulta
            foreach (var c in CitaDatos.citas)
            {
                if (c.IdPaciente == idPaciente)
                {
                    string odontologo = "Dr. Asignado";
                    foreach (var u in usuarios)
                    {
                        if (u.IdUsuario == c.IdOdontologo)
                        {
                            odontologo = u.NombreCompleto;
                            break;
                        }
                    }
                    dgvCitasFicha.Rows.Add(c.Fecha.ToString("dd/MM/yyyy"), c.Hora, odontologo, c.Motivo, c.Estado);
                }
            }

            // Cargar tratamientos
            dgvTratamientosFicha.Rows.Clear();
            List<Tratamiento> tratamientos = TratamientoDatos.BuscarPorPaciente(idPaciente);
            foreach (var t in tratamientos)
            {
                string odontologo = "Dr. Asignado";
                foreach (var u in usuarios)
                {
                    if (u.IdUsuario == t.IdOdontologo)
                    {
                        odontologo = u.NombreCompleto;
                        break;
                    }
                }
                dgvTratamientosFicha.Rows.Add(t.Descripcion, odontologo, t.FechaInicio.ToString("dd/MM/yyyy"), t.FechaFin.ToString("dd/MM/yyyy"), "$" + t.Costo.ToString("F2"), t.Estado);
            }
        }
    }
}
