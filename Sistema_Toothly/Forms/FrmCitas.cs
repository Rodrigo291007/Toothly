using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmCitas : Form, IRecargable
    {
        private int idCitaSeleccionada = 0;
        private DateTime? fechaFiltro = null;
        private int odontologoFiltro = 0;

        public FrmCitas()
        {
            InitializeComponent();
        }

        private void FrmCitas_Load(object sender, EventArgs e)
        {
            // Botón de cerrar en el encabezado
            AgregarBotonCerrar();

            btnFiltrar.Image = Properties.Resources.ico_buscar;

            btnNuevo.Image = Properties.Resources.ico_nuevo;

            btnGuardar.Image = Properties.Resources.ico_guardar;

            btnReprogramar.Image = Properties.Resources.ico_editar;

            btnCancelarCita.Image = Properties.Resources.ico_cancelar;

            // Formato visual de la tabla de citas
            ConfigurarEstiloTabla();

            ConfigurarColumnas();
            LlenarHoras();
            LlenarCombos();

            // Restricción por rol: Odontólogo y Recepcionista pueden gestionar citas
            if (Sesion.NombreRol == "Asistente Odontológico")
            {
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                btnReprogramar.Enabled = false;
                btnCancelarCita.Enabled = false;
                pnlCaptura.Enabled = false;
            }

            CargarCitas(null, 0);
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

        // Colores y formato de la tabla
        private void ConfigurarEstiloTabla()
        {
            dgvCitas.BackgroundColor = Color.White;
            dgvCitas.BorderStyle = BorderStyle.None;
            dgvCitas.EnableHeadersVisualStyles = false;
            dgvCitas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(108, 180, 234);
            dgvCitas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCitas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvCitas.ColumnHeadersHeight = 35;
            dgvCitas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(186, 230, 253);
            dgvCitas.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCitas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 254);
            dgvCitas.RowHeadersVisible = false;
            dgvCitas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void PrepararNueva()
        {
            btnNuevo_Click(this, EventArgs.Empty);
        }

        private void ConfigurarColumnas()
        {
            dgvCitas.Columns.Clear();
            dgvCitas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCitas.MultiSelect = false;
            dgvCitas.ReadOnly = true;
            dgvCitas.Columns.Add("IdCita", "ID");
            dgvCitas.Columns.Add("Fecha", "Fecha");
            dgvCitas.Columns.Add("Hora", "Hora");
            dgvCitas.Columns.Add("Paciente", "Paciente");
            dgvCitas.Columns.Add("Odontologo", "Odontólogo");
            dgvCitas.Columns.Add("Motivo", "Motivo");
            dgvCitas.Columns.Add("Estado", "Estado");

            dgvCitas.Columns["IdCita"].Width = 50;
            dgvCitas.Columns["Fecha"].Width = 95;
            dgvCitas.Columns["Hora"].Width = 75;
            dgvCitas.Columns["Paciente"].Width = 180;
            dgvCitas.Columns["Odontologo"].Width = 180;
            dgvCitas.Columns["Motivo"].Width = 240;
            dgvCitas.Columns["Estado"].Width = 110;
        }

        private void LlenarHoras()
        {
            cmbHora.Items.Clear();
            string[] horas = { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00" };
            foreach (var h in horas)
            {
                cmbHora.Items.Add(h);
            }
            cmbHora.SelectedIndex = 0;
        }

        private void LlenarCombos()
        {
            // Pacientes
            cmbPaciente.Items.Clear();
            foreach (var p in PacienteDatos.pacientes)
            {
                if (p.Estado == "A")
                {
                    cmbPaciente.Items.Add(p.IdPaciente + " - " + p.Nombres + " " + p.Apellidos);
                }
            }
            if (cmbPaciente.Items.Count > 0) cmbPaciente.SelectedIndex = 0;

            // Odontólogos
            cmbOdontologo.Items.Clear();
            cmbFiltroOdontologo.Items.Clear();
            cmbFiltroOdontologo.Items.Add("-- Todos --");

            var odontologos = UsuarioDatos.ListarOdontologos();
            foreach (var od in odontologos)
            {
                string desc = od.IdUsuario + " - " + od.NombreCompleto;
                cmbOdontologo.Items.Add(desc);
                cmbFiltroOdontologo.Items.Add(desc);
            }
            if (cmbOdontologo.Items.Count > 0) cmbOdontologo.SelectedIndex = 0;
            cmbFiltroOdontologo.SelectedIndex = 0;

            cmbEstado.SelectedIndex = 0;
        }

        private void CargarCitas(DateTime? fecha, int idOdontologo)
        {
            fechaFiltro = fecha;
            odontologoFiltro = idOdontologo;
            dgvCitas.Rows.Clear();
            List<Cita> lista;
            if (fecha == null && idOdontologo == 0)
                lista = CitaDatos.Listar();
            else
                lista = CitaDatos.Buscar(fecha, idOdontologo);

            foreach (var c in lista)
            {
                string paciente = PacienteDatos.ObtenerNombre(c.IdPaciente);
                string odontologo = "Dr. Asignado";
                foreach (var u in UsuarioDatos.usuarios)
                {
                    if (u.IdUsuario == c.IdOdontologo)
                    {
                        odontologo = u.NombreCompleto;
                        break;
                    }
                }
                dgvCitas.Rows.Add(c.IdCita, c.Fecha.ToString("dd/MM/yyyy"), c.Hora, paciente, odontologo, c.Motivo, c.Estado);
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            DateTime fecha = dtpFiltroFecha.Value.Date;
            int idOdontologo = 0;
            if (cmbFiltroOdontologo.SelectedIndex > 0)
            {
                string item = cmbFiltroOdontologo.SelectedItem.ToString();
                idOdontologo = Convert.ToInt32(item.Split('-')[0].Trim());
            }

            CargarCitas(fecha, idOdontologo);
        }

        private void btnVerTodas_Click(object sender, EventArgs e)
        {
            cmbFiltroOdontologo.SelectedIndex = 0;
            CargarCitas(null, 0);
        }

        public void Recargar()
        {
            Recarga.Combos(LlenarCombos, cmbPaciente, cmbOdontologo, cmbFiltroOdontologo, cmbEstado);
            Recarga.Grid(dgvCitas, () => CargarCitas(fechaFiltro, odontologoFiltro));
        }

        private void dgvCitas_SelectionChanged(object sender, EventArgs e)
        {
            if (Recarga.EnCurso) return;
            if (dgvCitas.CurrentRow != null && dgvCitas.CurrentRow.Index >= 0)
            {
                var celdaId = dgvCitas.CurrentRow.Cells["IdCita"].Value;
                if (celdaId != null)
                {
                    idCitaSeleccionada = Convert.ToInt32(celdaId);
                    foreach (var c in CitaDatos.citas)
                    {
                        if (c.IdCita == idCitaSeleccionada)
                        {
                            dtpFecha.Value = c.Fecha;
                            cmbHora.SelectedItem = c.Hora;
                            txtMotivo.Text = c.Motivo;
                            cmbEstado.SelectedItem = c.Estado;

                            // Seleccionar paciente
                            for (int i = 0; i < cmbPaciente.Items.Count; i++)
                            {
                                if (cmbPaciente.Items[i].ToString().StartsWith(c.IdPaciente + " -"))
                                {
                                    cmbPaciente.SelectedIndex = i;
                                    break;
                                }
                            }
                            // Seleccionar odontólogo
                            for (int i = 0; i < cmbOdontologo.Items.Count; i++)
                            {
                                if (cmbOdontologo.Items[i].ToString().StartsWith(c.IdOdontologo + " -"))
                                {
                                    cmbOdontologo.SelectedIndex = i;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            idCitaSeleccionada = 0;
            txtMotivo.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Evita crear un duplicado al pulsar Guardar con un registro ya seleccionado
            if (idCitaSeleccionada != 0 && !Mensaje.Confirmar(
                    "Tiene seleccionado un registro existente.\n¿Desea guardarlo como una NUEVA cita?\n\n" +
                    "Si solo quiere actualizar el registro seleccionado, pulse \"No\" y use el botón Reprogramar.",
                    "Guardar como nuevo"))
                return;
            if (cmbPaciente.SelectedIndex < 0 || cmbOdontologo.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un paciente y un odontólogo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                MessageBox.Show("El motivo de la cita es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMotivo.Focus();
                return;
            }

            int idPaciente = Convert.ToInt32(cmbPaciente.SelectedItem.ToString().Split('-')[0].Trim());
            int idOdontologo = Convert.ToInt32(cmbOdontologo.SelectedItem.ToString().Split('-')[0].Trim());
            DateTime fecha = dtpFecha.Value.Date;
            string hora = cmbHora.SelectedItem?.ToString() ?? "08:00";

            // No se agendan citas en fechas u horas que ya pasaron
            if (EsFechaPasada(fecha, hora))
            {
                MessageBox.Show("No se puede agendar una cita en una fecha u hora que ya pasó.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar no solapamiento
            if (CitaDatos.ExisteConflicto(idOdontologo, fecha, hora, 0))
            {
                MessageBox.Show("El odontólogo seleccionado ya tiene una cita agendada en esa misma fecha y hora. Seleccione otro horario.", "Conflicto de Horario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Cita c = new Cita();
            c.IdPaciente = idPaciente;
            c.IdOdontologo = idOdontologo;
            c.Fecha = fecha;
            c.Hora = hora;
            c.Motivo = txtMotivo.Text.Trim();
            c.Estado = "Programada"; // toda cita nueva inicia como Programada

            CitaDatos.Agregar(c);
            BitacoraDatos.RegistrarAccion("Agendar cita", "Se agendó cita para paciente ID " + idPaciente + " el " + fecha.ToString("dd/MM/yyyy") + " a las " + hora);

            MessageBox.Show("Cita agendada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarCitas(null, 0);
            if (!Recarga.SeleccionarFila(dgvCitas, c.IdCita)) LimpiarCampos();
        }

        private void btnReprogramar_Click(object sender, EventArgs e)
        {
            if (idCitaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una cita de la tabla para reprogramar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbOdontologo.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un odontólogo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Cita? cita = CitaDatos.citas.Find(x => x.IdCita == idCitaSeleccionada);
            if (cita == null) return;

            // Una cita cancelada o atendida ya no se puede reprogramar
            if (cita.Estado == "Cancelada" || cita.Estado == "Atendida")
            {
                MessageBox.Show("La cita está " + cita.Estado.ToLower() + " y ya no se puede modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idOdontologo = Convert.ToInt32(cmbOdontologo.SelectedItem.ToString().Split('-')[0].Trim());
            DateTime fecha = dtpFecha.Value.Date;
            string hora = cmbHora.SelectedItem?.ToString() ?? "08:00";
            bool cambioHorario = cita.Fecha.Date != fecha || cita.Hora != hora || cita.IdOdontologo != idOdontologo;

            if (cambioHorario)
            {
                if (EsFechaPasada(fecha, hora))
                {
                    MessageBox.Show("No se puede reprogramar a una fecha u hora que ya pasó.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar no solapamiento excluyendo la cita actual
                if (CitaDatos.ExisteConflicto(idOdontologo, fecha, hora, idCitaSeleccionada))
                {
                    MessageBox.Show("El odontólogo ya tiene otra cita en esa misma fecha y hora.", "Conflicto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            cita.IdOdontologo = idOdontologo;
            cita.Fecha = fecha;
            cita.Hora = hora;
            cita.Motivo = txtMotivo.Text.Trim();
            // Si cambió el horario queda Reprogramada; si no, se guarda el estado elegido (p. ej. Atendida)
            cita.Estado = cambioHorario ? "Reprogramada" : (cmbEstado.SelectedItem?.ToString() ?? cita.Estado);
            CitaDatos.Modificar(cita);

            BitacoraDatos.RegistrarAccion("Reprogramar cita", "Se reprogramó la cita ID " + idCitaSeleccionada + " para el " + fecha.ToString("dd/MM/yyyy") + " " + hora);
            MessageBox.Show("Cita reprogramada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            int id = idCitaSeleccionada;
            CargarCitas(null, 0);
            if (!Recarga.SeleccionarFila(dgvCitas, id)) LimpiarCampos();
        }

        private void btnCancelarCita_Click(object sender, EventArgs e)
        {
            if (idCitaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una cita de la tabla para cancelar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Cita? cita = CitaDatos.citas.Find(x => x.IdCita == idCitaSeleccionada);
            if (cita != null && (cita.Estado == "Cancelada" || cita.Estado == "Atendida"))
            {
                MessageBox.Show("La cita ya está " + cita.Estado.ToLower() + ".", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Mensaje.Confirmar("¿Está seguro de cancelar esta cita?", "Confirmación"))
            {
                CitaDatos.DarDeBaja(idCitaSeleccionada);
                BitacoraDatos.RegistrarAccion("Cancelar cita", "Se canceló la cita ID " + idCitaSeleccionada);

                MessageBox.Show("Cita cancelada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                int id = idCitaSeleccionada;
                CargarCitas(null, 0);
                if (!Recarga.SeleccionarFila(dgvCitas, id)) LimpiarCampos();
            }
        }

        // True si la fecha y hora indicadas ya pasaron
        private static bool EsFechaPasada(DateTime fecha, string hora)
        {
            if (fecha.Date < DateTime.Today) return true;
            if (fecha.Date == DateTime.Today && TimeSpan.TryParse(hora, out TimeSpan h))
                return fecha.Date + h < DateTime.Now;
            return false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            dgvCitas.CurrentCell = null;
            dgvCitas.ClearSelection();
            idCitaSeleccionada = 0;
            txtMotivo.Clear();
            dtpFecha.Value = DateTime.Today;
            if (cmbHora.Items.Count > 0) cmbHora.SelectedIndex = 0;
            if (cmbPaciente.Items.Count > 0) cmbPaciente.SelectedIndex = 0;
            if (cmbOdontologo.Items.Count > 0) cmbOdontologo.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
        }
    }
}

