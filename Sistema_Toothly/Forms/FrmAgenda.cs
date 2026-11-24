using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmAgenda : Form, IRecargable
    {
        public FrmAgenda()
        {
            InitializeComponent();
        }

        private void FrmAgenda_Load(object sender, EventArgs e)
        {
            // Botón de cerrar en el encabezado
            AgregarBotonCerrar();

            // Colores y presentación de los botones de acción (evitando superposición de texto e icono)
            btnHoy.Image = Properties.Resources.ico_cita;
            btnHoy.ImageAlign = ContentAlignment.MiddleLeft;
            btnHoy.TextAlign = ContentAlignment.MiddleCenter;
            btnHoy.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnHoy.Padding = new Padding(8, 0, 8, 0);

            btnActualizar.Image = Properties.Resources.ico_buscar;
            btnActualizar.ImageAlign = ContentAlignment.MiddleLeft;
            btnActualizar.TextAlign = ContentAlignment.MiddleCenter;
            btnActualizar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnActualizar.Padding = new Padding(8, 0, 8, 0);

            // Formato y colores de la tabla
            ConfigurarEstiloTabla();
            ConfigurarColumnas();
            ActualizarCalendario();
            CargarCitasDia(CalendarMes.SelectionStart);
        }

        public void Recargar()
        {
            ActualizarCalendario();
            CargarCitasDia(CalendarMes.SelectionStart);
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

        // Colores y presentación de la tabla de citas
        private void ConfigurarEstiloTabla()
        {
            dgvCitasDia.BackgroundColor = Color.White;
            dgvCitasDia.BorderStyle = BorderStyle.None;
            dgvCitasDia.EnableHeadersVisualStyles = false;
            dgvCitasDia.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(108, 180, 234);
            dgvCitasDia.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCitasDia.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvCitasDia.ColumnHeadersHeight = 35;
            dgvCitasDia.DefaultCellStyle.SelectionBackColor = Color.FromArgb(186, 230, 253);
            dgvCitasDia.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCitasDia.RowHeadersVisible = false;
            dgvCitasDia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ConfigurarColumnas()
        {
            dgvCitasDia.Columns.Clear();
            dgvCitasDia.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCitasDia.MultiSelect = false;
            dgvCitasDia.ReadOnly = true;
            dgvCitasDia.Columns.Add("Hora", "Hora");
            dgvCitasDia.Columns.Add("Paciente", "Paciente");
            dgvCitasDia.Columns.Add("Odontologo", "Odontólogo");
            dgvCitasDia.Columns.Add("Motivo", "Motivo");
            dgvCitasDia.Columns.Add("Estado", "Estado");

            dgvCitasDia.Columns["Hora"].Width = 70;
            dgvCitasDia.Columns["Paciente"].Width = 180;
            dgvCitasDia.Columns["Odontologo"].Width = 180;
            dgvCitasDia.Columns["Motivo"].Width = 220;
            dgvCitasDia.Columns["Estado"].Width = 110;
        }

        private void ActualizarCalendario()
        {
            var fechas = CitaDatos.Listar()
                .Select(c => c.Fecha.Date)
                .Distinct()
                .ToArray();
            CalendarMes.BoldedDates = fechas;
            CalendarMes.UpdateBoldedDates();
        }

        private void CargarCitasDia(DateTime fecha)
        {
            var ci = new System.Globalization.CultureInfo("es-ES");
            lblFechaSeleccionada.Text = "Citas del " + fecha.ToString("dddd, dd 'de' MMMM 'de' yyyy", ci);
            dgvCitasDia.Rows.Clear();

            List<Cita> citas = CitaDatos.Buscar(fecha, 0);
            List<Usuario> usuarios = UsuarioDatos.Listar(); // una sola consulta
            foreach (var c in citas)
            {
                string paciente = PacienteDatos.ObtenerNombre(c.IdPaciente);
                string odontologo = "Dr. Asignado";
                foreach (var u in usuarios)
                {
                    if (u.IdUsuario == c.IdOdontologo)
                    {
                        odontologo = u.NombreCompleto;
                        break;
                    }
                }

                int idx = dgvCitasDia.Rows.Add(c.Hora, paciente, odontologo, c.Motivo, c.Estado);

                // Pintamos la fila con un color suave según el estado de la cita
                if (c.Estado == "Cancelada")
                    dgvCitasDia.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(254, 226, 226);
                else if (c.Estado == "Atendida")
                    dgvCitasDia.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(220, 252, 231);
                else if (c.Estado == "Reprogramada")
                    dgvCitasDia.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(254, 243, 199);
            }

            int total = citas.Count;
            int programadas = citas.Count(c => c.Estado == "Programada");
            int completadas = citas.Count(c => c.Estado == "Atendida");
            int canceladas = citas.Count(c => c.Estado == "Cancelada");

            if (total == 0)
                lblResumenDia.Text = "No hay citas programadas para este día.";
            else
                lblResumenDia.Text = $"Total: {total} cita(s)   |   Programadas: {programadas}   |   Atendidas: {completadas}   |   Canceladas: {canceladas}";

            lblDetalle.Text = "";
        }

        private void CalendarMes_DateChanged(object sender, DateRangeEventArgs e)
        {
            CargarCitasDia(CalendarMes.SelectionStart);
        }

        private void btnHoy_Click(object sender, EventArgs e)
        {
            CalendarMes.SetDate(DateTime.Today);
            CargarCitasDia(DateTime.Today);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarCalendario();
            CargarCitasDia(CalendarMes.SelectionStart);
        }

        private void dgvCitasDia_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCitasDia.CurrentRow != null && dgvCitasDia.CurrentRow.Index >= 0)
            {
                var row = dgvCitasDia.CurrentRow;
                lblDetalle.Text = $"  ⏰ Hora: {row.Cells["Hora"].Value}   👤 Paciente: {row.Cells["Paciente"].Value}   🦷 Odontólogo: {row.Cells["Odontologo"].Value}   📋 Estado: {row.Cells["Estado"].Value}";
            }
        }
    }
}
