using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmHistoriaClinica : Form, IRecargable
    {
        private int idPacienteActual = 0;
        private int idHistoriaActual = 0;

        public FrmHistoriaClinica()
        {
            InitializeComponent();
        }

        private void FrmHistoriaClinica_Load(object sender, EventArgs e)
        {
            // Botón de cerrar en el encabezado
            AgregarBotonCerrar();

            btnBuscarPaciente.Image = Properties.Resources.ico_buscar;

            btnNuevo.Image = Properties.Resources.ico_nuevo;

            btnGuardar.Image = Properties.Resources.ico_guardar;

            btnModificar.Image = Properties.Resources.ico_editar;

            btnImprimir.Image = Properties.Resources.ico_imprimir;

            LlenarComboOdontologos();

            // REGLA CLAVE ANEXO A: El Asistente puede editar todo menos el diagnóstico
            if (Sesion.NombreRol == "Asistente Odontológico")
            {
                txtDiagnostico.ReadOnly = true;
                txtDiagnostico.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            }

            // Cargar por defecto el primer paciente con historia clínica si existe
            if (PacienteDatos.pacientes.Count > 0)
            {
                CargarDatosPaciente(PacienteDatos.pacientes[0].IdPaciente);
            }
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

        public void Recargar()
        {
            Recarga.Combos(LlenarComboOdontologos, cmbOdontologo);
        }

        private void LlenarComboOdontologos()
        {
            cmbOdontologo.Items.Clear();
            var odontologos = UsuarioDatos.ListarOdontologos();
            foreach (var od in odontologos)
            {
                cmbOdontologo.Items.Add(od.IdUsuario + " - " + od.NombreCompleto);
            }
            if (cmbOdontologo.Items.Count > 0)
                cmbOdontologo.SelectedIndex = 0;
        }

        private void btnBuscarPaciente_Click(object sender, EventArgs e)
        {
            string texto = txtBuscarPaciente.Text.Trim();
            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show("Ingrese un nombre o DUI para buscar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<Paciente> encontrados = PacienteDatos.Buscar(texto);
            if (encontrados.Count == 0)
            {
                MessageBox.Show("No se encontró ningún paciente con ese criterio.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Seleccionar el primer paciente encontrado
            if (encontrados.Count > 1)
                MessageBox.Show("Se encontraron " + encontrados.Count + " pacientes. Se cargó el primero: " +
                    encontrados[0].Nombres + " " + encontrados[0].Apellidos + ".\nSi no es el correcto, busque por DUI.",
                    "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarDatosPaciente(encontrados[0].IdPaciente);
        }

        private void CargarDatosPaciente(int idPaciente)
        {
            Paciente? p = PacienteDatos.ObtenerPorId(idPaciente);
            if (p == null) return;

            idPacienteActual = p.IdPaciente;
            lblPacienteActual.Text = "Paciente: " + p.Nombres + " " + p.Apellidos + " (DUI: " + p.Dui + ")";

            // Buscar si ya tiene historia clínica
            HistoriaClinica? h = HistoriaDatos.BuscarPorPaciente(idPaciente);
            if (h != null)
            {
                idHistoriaActual = h.IdHistoria;
                dtpFechaApertura.Value = h.FechaApertura;
                txtMotivo.Text = h.MotivoConsulta;
                txtAntecedentes.Text = h.Antecedentes;
                txtObservacionesGenerales.Text = h.ObservacionesGenerales;
                txtDiagnostico.Text = h.Diagnostico;
                txtObservacionesClinicas.Text = h.ObservacionesClinicas;

                // Seleccionar odontólogo
                for (int i = 0; i < cmbOdontologo.Items.Count; i++)
                {
                    if (cmbOdontologo.Items[i].ToString().StartsWith(h.IdOdontologo + " -"))
                    {
                        cmbOdontologo.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                // No tiene historia aún, precargar antecedentes del paciente
                idHistoriaActual = 0;
                dtpFechaApertura.Value = DateTime.Today;
                txtMotivo.Clear();
                txtAntecedentes.Text = p.Antecedentes + (string.IsNullOrEmpty(p.Alergias) ? "" : " | Alergias: " + p.Alergias);
                txtObservacionesGenerales.Clear();
                txtDiagnostico.Clear();
                txtObservacionesClinicas.Text = "Pieza 11: \r\nPieza 21: \r\nPieza 31: \r\nPieza 41: ";
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            idHistoriaActual = 0;
            txtMotivo.Clear();
            txtAntecedentes.Clear();
            txtObservacionesGenerales.Clear();
            txtDiagnostico.Clear();
            txtObservacionesClinicas.Clear();
            dtpFechaApertura.Value = DateTime.Today;
            txtMotivo.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (idPacienteActual == 0)
            {
                MessageBox.Show("Primero debe seleccionar un paciente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbOdontologo.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione el odontólogo tratante.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                MessageBox.Show("El motivo de consulta es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMotivo.Focus();
                return;
            }

            int idOdontologo = Convert.ToInt32(cmbOdontologo.SelectedItem.ToString().Split('-')[0].Trim());

            // Cada paciente tiene una sola historia clínica: si ya existe, se actualiza
            if (idHistoriaActual == 0)
            {
                HistoriaClinica? existente = HistoriaDatos.BuscarPorPaciente(idPacienteActual);
                if (existente != null)
                {
                    if (!Mensaje.Confirmar("Este paciente ya tiene una historia clínica.\n¿Desea actualizarla con los datos actuales?", "Historia existente"))
                        return;
                    idHistoriaActual = existente.IdHistoria;
                }
            }

            HistoriaClinica h;
            if (idHistoriaActual == 0)
            {
                h = new HistoriaClinica();
                h.IdPaciente = idPacienteActual;
                h.FechaApertura = dtpFechaApertura.Value;
                h.IdOdontologo = idOdontologo;
                h.MotivoConsulta = txtMotivo.Text.Trim();
                h.Antecedentes = txtAntecedentes.Text.Trim();
                h.ObservacionesGenerales = txtObservacionesGenerales.Text.Trim();
                h.Diagnostico = txtDiagnostico.Text.Trim();
                h.ObservacionesClinicas = txtObservacionesClinicas.Text.Trim();

                HistoriaDatos.Agregar(h);
                idHistoriaActual = h.IdHistoria;
                BitacoraDatos.RegistrarAccion("Apertura de historia", "Se creó historia clínica para paciente ID " + idPacienteActual);
                MessageBox.Show("Historia clínica guardada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                btnModificar_Click(sender, e);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idHistoriaActual == 0)
            {
                MessageBox.Show("Guarde primero la historia antes de modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbOdontologo.SelectedIndex < 0 || string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                MessageBox.Show("Seleccione el odontólogo y escriba el motivo de consulta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idOdontologo = Convert.ToInt32(cmbOdontologo.SelectedItem.ToString().Split('-')[0].Trim());

            foreach (var h in HistoriaDatos.historias)
            {
                if (h.IdHistoria == idHistoriaActual)
                {
                    h.IdOdontologo = idOdontologo;
                    h.MotivoConsulta = txtMotivo.Text.Trim();
                    h.Antecedentes = txtAntecedentes.Text.Trim();
                    h.ObservacionesGenerales = txtObservacionesGenerales.Text.Trim();
                    // Si el rol es Asistente, NO modifica el diagnóstico
                    if (Sesion.NombreRol != "Asistente Odontológico")
                    {
                        h.Diagnostico = txtDiagnostico.Text.Trim();
                    }
                    h.ObservacionesClinicas = txtObservacionesClinicas.Text.Trim();
                    HistoriaDatos.Modificar(h);
                    break;
                }
            }

            BitacoraDatos.RegistrarAccion("Actualización de historia", "Se actualizó historia clínica ID " + idHistoriaActual);
            MessageBox.Show("Historia clínica actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtMotivo.Clear();
            txtAntecedentes.Clear();
            txtObservacionesGenerales.Clear();
            txtDiagnostico.Clear();
            txtObservacionesClinicas.Clear();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (idPacienteActual == 0)
            {
                MessageBox.Show("Seleccione un paciente para imprimir su historia clínica.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Paciente? p = PacienteDatos.ObtenerPorId(idPacienteActual);
            if (p == null) return;

            // Construir tabla temporal para el reporte PDF
            DataGridView dgvTemp = new DataGridView();
            dgvTemp.Columns.Add("Campo", "Campo");
            dgvTemp.Columns.Add("Detalle", "Detalle Clínico");

            dgvTemp.Rows.Add("Paciente", p.Nombres + " " + p.Apellidos);
            dgvTemp.Rows.Add("DUI", p.Dui);
            dgvTemp.Rows.Add("Fecha Nacimiento", p.FechaNacimiento.ToString("dd/MM/yyyy"));
            dgvTemp.Rows.Add("Teléfono", p.Telefono);
            dgvTemp.Rows.Add("Odontólogo Asignado", cmbOdontologo.SelectedItem?.ToString() ?? "--");
            dgvTemp.Rows.Add("Fecha Apertura", dtpFechaApertura.Value.ToString("dd/MM/yyyy"));
            dgvTemp.Rows.Add("Motivo Consulta", txtMotivo.Text.Trim());
            dgvTemp.Rows.Add("Antecedentes", txtAntecedentes.Text.Trim());
            dgvTemp.Rows.Add("Diagnóstico", txtDiagnostico.Text.Trim());
            dgvTemp.Rows.Add("Observaciones Generales", txtObservacionesGenerales.Text.Trim());
            dgvTemp.Rows.Add("Observaciones Clínicas", txtObservacionesClinicas.Text.Trim());

            GeneradorPDF.GenerarDesdeGrid("Historia Clínica - " + p.Nombres + " " + p.Apellidos, dgvTemp);
        }
    }
}
