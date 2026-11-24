using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmPacientes : Form, IRecargable
    {
        private int idPacienteSeleccionado = 0;

        public FrmPacientes()
        {
            InitializeComponent();
        }

        private void FrmPacientes_Load(object sender, EventArgs e)
        {
            // Botón de cerrar en el encabezado
            AgregarBotonCerrar();

            btnBuscar.Image = Properties.Resources.ico_buscar;

            btnNuevo.Image = Properties.Resources.ico_nuevo;

            btnGuardar.Image = Properties.Resources.ico_guardar;

            btnModificar.Image = Properties.Resources.ico_editar;

            btnDarDeBaja.Image = Properties.Resources.ico_baja;

            btnCancelar.Image = Properties.Resources.ico_cancelar;

            // Formato de la tabla
            ConfigurarEstiloTabla();

            ConfigurarColumnas();

            cmbSexo.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
            cmbEstadoFiltro.SelectedIndex = 0;

            // Solo quien puede dar de baja puede cambiar el estado desde Modificar
            cmbEstado.Enabled = Sesion.TienePrivilegio(Permisos.Eliminar);
            dtpFechaNac.MaxDate = DateTime.Today;

            // Restricción por rol: Odontólogo y Recepcionista pueden registrar y modificar pacientes
            if (Sesion.NombreRol == "Asistente Odontológico")
            {
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                btnModificar.Enabled = false;
                btnDarDeBaja.Enabled = false;
                pnlCaptura.Enabled = false;
            }

            CargarPacientes();
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

        // Colores y diseño de la tabla de pacientes
        private void ConfigurarEstiloTabla()
        {
            dgvPacientes.BackgroundColor = Color.White;
            dgvPacientes.BorderStyle = BorderStyle.None;
            dgvPacientes.EnableHeadersVisualStyles = false;
            dgvPacientes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(108, 180, 234);
            dgvPacientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPacientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvPacientes.ColumnHeadersHeight = 35;
            dgvPacientes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(186, 230, 253);
            dgvPacientes.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvPacientes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 254);
            dgvPacientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPacientes.ReadOnly = true;
            dgvPacientes.RowHeadersVisible = false;
            dgvPacientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void PrepararNuevo()
        {
            btnNuevo_Click(this, EventArgs.Empty);
        }

        private void ConfigurarColumnas()
        {
            dgvPacientes.Columns.Clear();
            dgvPacientes.Columns.Add("IdPaciente", "ID");
            dgvPacientes.Columns.Add("Nombres", "Nombres");
            dgvPacientes.Columns.Add("Apellidos", "Apellidos");
            dgvPacientes.Columns.Add("Dui", "DUI");
            dgvPacientes.Columns.Add("Telefono", "Teléfono");
            dgvPacientes.Columns.Add("Correo", "Correo");
            dgvPacientes.Columns.Add("Estado", "Estado");

            dgvPacientes.Columns["IdPaciente"].Width = 50;
            dgvPacientes.Columns["Nombres"].Width = 160;
            dgvPacientes.Columns["Apellidos"].Width = 160;
            dgvPacientes.Columns["Dui"].Width = 110;
            dgvPacientes.Columns["Telefono"].Width = 100;
            dgvPacientes.Columns["Correo"].Width = 180;
            dgvPacientes.Columns["Estado"].Width = 90;
        }

        private void CargarPacientes()
        {
            dgvPacientes.Rows.Clear();
            List<Paciente> lista;

            string filtroEstado = cmbEstadoFiltro.SelectedItem?.ToString() ?? "Todos";
            if (filtroEstado == "Activos")
                lista = PacienteDatos.BuscarPorEstado("A");
            else if (filtroEstado == "Inactivos")
                lista = PacienteDatos.BuscarPorEstado("I");
            else
                lista = PacienteDatos.Listar();

            foreach (var p in lista)
            {
                string estadoTexto = (p.Estado == "A") ? "Activo" : "Inactivo";
                dgvPacientes.Rows.Add(p.IdPaciente, p.Nombres, p.Apellidos, p.Dui, p.Telefono, p.Correo, estadoTexto);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();
            dgvPacientes.Rows.Clear();
            List<Paciente> lista = PacienteDatos.Buscar(texto);

            foreach (var p in lista)
            {
                string estadoTexto = (p.Estado == "A") ? "Activo" : "Inactivo";
                dgvPacientes.Rows.Add(p.IdPaciente, p.Nombres, p.Apellidos, p.Dui, p.Telefono, p.Correo, estadoTexto);
            }
        }

        private void cmbEstadoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPacientes();
        }

        public void Recargar()
        {
            Recarga.Grid(dgvPacientes, () =>
            {
                if (txtBuscar.Text.Trim() == "") CargarPacientes();
                else btnBuscar_Click(this, EventArgs.Empty);
            });
        }

        private void dgvPacientes_SelectionChanged(object sender, EventArgs e)
        {
            if (Recarga.EnCurso) return;
            if (dgvPacientes.CurrentRow != null && dgvPacientes.CurrentRow.Index >= 0)
            {
                var celdaId = dgvPacientes.CurrentRow.Cells["IdPaciente"].Value;
                if (celdaId != null)
                {
                    idPacienteSeleccionado = Convert.ToInt32(celdaId);
                    Paciente? p = PacienteDatos.ObtenerPorId(idPacienteSeleccionado);
                    if (p != null)
                    {
                        txtNombres.Text = p.Nombres;
                        txtApellidos.Text = p.Apellidos;
                        txtDui.Text = p.Dui;
                        dtpFechaNac.Value = p.FechaNacimiento;
                        cmbSexo.SelectedItem = (p.Sexo == "M") ? "Masculino" : "Femenino";
                        txtTelefono.Text = p.Telefono;
                        txtCorreo.Text = p.Correo;
                        txtDireccion.Text = p.Direccion;
                        txtAlergias.Text = p.Alergias;
                        txtAntecedentes.Text = p.Antecedentes;
                        cmbEstado.SelectedItem = (p.Estado == "A") ? "Activo" : "Inactivo";
                    }
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            idPacienteSeleccionado = 0;
            txtNombres.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            string dui = txtDui.Text.Trim();
            if (DuiEnUso(dui, 0))
            {
                MessageBox.Show("Ya existe un paciente registrado con el número de DUI: " + dui, "Registro Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDui.Focus();
                return;
            }

            Paciente p = new Paciente();
            p.Nombres = txtNombres.Text.Trim();
            p.Apellidos = txtApellidos.Text.Trim();
            p.Dui = txtDui.Text.Trim();
            p.FechaNacimiento = dtpFechaNac.Value.Date;
            p.Sexo = (cmbSexo.SelectedItem?.ToString() == "Masculino") ? "M" : "F";
            p.Telefono = txtTelefono.Text.Trim();
            p.Correo = txtCorreo.Text.Trim();
            p.Direccion = txtDireccion.Text.Trim();
            p.Alergias = txtAlergias.Text.Trim();
            p.Antecedentes = txtAntecedentes.Text.Trim();
            p.Estado = (cmbEstado.SelectedItem?.ToString() == "Activo") ? "A" : "I";

            PacienteDatos.Agregar(p);

            // Crear automáticamente su estado de cuenta en 0
            EstadoCuentaDatos.CrearEstado(p.IdPaciente);

            BitacoraDatos.RegistrarAccion("Registro de paciente", "Se registró al paciente " + p.Nombres + " " + p.Apellidos);

            MessageBox.Show("Paciente registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarPacientes();
            if (!Recarga.SeleccionarFila(dgvPacientes, p.IdPaciente)) LimpiarCampos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idPacienteSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un paciente de la tabla para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos()) return;

            if (DuiEnUso(txtDui.Text.Trim(), idPacienteSeleccionado))
            {
                MessageBox.Show("El DUI " + txtDui.Text.Trim() + " ya pertenece a otro paciente.", "Registro Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDui.Focus();
                return;
            }

            Paciente? p = PacienteDatos.ObtenerPorId(idPacienteSeleccionado);
            if (p != null)
            {
                p.Nombres = txtNombres.Text.Trim();
                p.Apellidos = txtApellidos.Text.Trim();
                p.Dui = txtDui.Text.Trim();
                p.FechaNacimiento = dtpFechaNac.Value.Date;
                p.Sexo = (cmbSexo.SelectedItem?.ToString() == "Masculino") ? "M" : "F";
                p.Telefono = txtTelefono.Text.Trim();
                p.Correo = txtCorreo.Text.Trim();
                p.Direccion = txtDireccion.Text.Trim();
                p.Alergias = txtAlergias.Text.Trim();
                p.Antecedentes = txtAntecedentes.Text.Trim();
                p.Estado = (cmbEstado.SelectedItem?.ToString() == "Activo") ? "A" : "I";

                PacienteDatos.Modificar(p);
                BitacoraDatos.RegistrarAccion("Modificación de paciente", "Se actualizó al paciente " + p.Nombres + " " + p.Apellidos);

                MessageBox.Show("Paciente modificado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarPacientes();
                if (!Recarga.SeleccionarFila(dgvPacientes, p.IdPaciente)) LimpiarCampos();
            }
        }

        private void btnDarDeBaja_Click(object sender, EventArgs e)
        {
            if (idPacienteSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un paciente de la tabla para dar de baja.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Mensaje.Confirmar("¿Está seguro de dar de baja al paciente seleccionado?", "Confirmación"))
            {
                PacienteDatos.DarDeBaja(idPacienteSeleccionado);
                BitacoraDatos.RegistrarAccion("Baja de paciente", "Se dio de baja al paciente ID " + idPacienteSeleccionado);

                MessageBox.Show("Paciente dado de baja (estado Inactivo).", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                int id = idPacienteSeleccionado;
                CargarPacientes();
                if (!Recarga.SeleccionarFila(dgvPacientes, id)) LimpiarCampos();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            dgvPacientes.CurrentCell = null;
            dgvPacientes.ClearSelection();
            idPacienteSeleccionado = 0;
            txtNombres.Clear();
            txtApellidos.Clear();
            txtDui.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            txtAlergias.Clear();
            txtAntecedentes.Clear();
            dtpFechaNac.Value = DateTime.Today.AddYears(-20);
            cmbSexo.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombres.Text))
            {
                MessageBox.Show("Por favor ingrese los nombres del paciente.", "Campo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombres.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtApellidos.Text))
            {
                MessageBox.Show("Por favor ingrese los apellidos del paciente.", "Campo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellidos.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDui.Text))
            {
                MessageBox.Show("Por favor ingrese el número de DUI.", "Campo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDui.Focus();
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtDui.Text.Trim(), @"^\d{8}-\d$"))
            {
                MessageBox.Show("El DUI debe tener el formato 12345678-9.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDui.Focus();
                return false;
            }
            if (dtpFechaNac.Value.Date > DateTime.Today)
            {
                MessageBox.Show("La fecha de nacimiento no puede ser futura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFechaNac.Focus();
                return false;
            }
            if (txtCorreo.Text.Trim() != "" && !txtCorreo.Text.Contains("@"))
            {
                MessageBox.Show("El correo electrónico no es válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return false;
            }
            return true;
        }

        // Verifica si el DUI ya está registrado en otro paciente
        private static bool DuiEnUso(string dui, int idExcluir)
        {
            foreach (var p in PacienteDatos.pacientes)
            {
                if (p.IdPaciente != idExcluir && p.Dui.Equals(dui, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
