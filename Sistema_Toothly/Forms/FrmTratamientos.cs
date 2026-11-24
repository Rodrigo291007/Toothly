using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmTratamientos : Form, IRecargable
    {
        private int idTratamientoSeleccionado = 0;

        public FrmTratamientos()
        {
            InitializeComponent();
        }

        private void FrmTratamientos_Load(object sender, EventArgs e)
        {
            // Botón de cerrar en el encabezado
            AgregarBotonCerrar();

            btnBuscar.Image = Properties.Resources.ico_buscar;

            btnNuevo.Image = Properties.Resources.ico_nuevo;

            btnGuardar.Image = Properties.Resources.ico_guardar;

            btnModificar.Image = Properties.Resources.ico_editar;

            btnCancelarTratamiento.Image = Properties.Resources.ico_cancelar;

            // Formato de la tabla
            ConfigurarEstiloTabla();

            ConfigurarColumnas();
            LlenarCombos();
            CargarTratamientos();
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

        // Colores y diseño de la tabla de tratamientos
        private void ConfigurarEstiloTabla()
        {
            dgvTratamientos.BackgroundColor = Color.White;
            dgvTratamientos.BorderStyle = BorderStyle.None;
            dgvTratamientos.EnableHeadersVisualStyles = false;
            dgvTratamientos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(108, 180, 234);
            dgvTratamientos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTratamientos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvTratamientos.ColumnHeadersHeight = 35;
            dgvTratamientos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(186, 230, 253);
            dgvTratamientos.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvTratamientos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 254);
            dgvTratamientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTratamientos.ReadOnly = true;
            dgvTratamientos.RowHeadersVisible = false;
            dgvTratamientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void PrepararNuevo()
        {
            btnNuevo_Click(this, EventArgs.Empty);
        }

        private void ConfigurarColumnas()
        {
            dgvTratamientos.Columns.Clear();
            dgvTratamientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTratamientos.MultiSelect = false;
            dgvTratamientos.ReadOnly = true;
            dgvTratamientos.Columns.Add("IdTratamiento", "ID");
            dgvTratamientos.Columns.Add("Paciente", "Paciente");
            dgvTratamientos.Columns.Add("Odontologo", "Odontólogo");
            dgvTratamientos.Columns.Add("Descripcion", "Descripción");
            dgvTratamientos.Columns.Add("FechaInicio", "Fecha Inicio");
            dgvTratamientos.Columns.Add("FechaFin", "Fecha Fin");
            dgvTratamientos.Columns.Add("Costo", "Costo ($)");
            dgvTratamientos.Columns.Add("Estado", "Estado");

            dgvTratamientos.Columns["IdTratamiento"].Width = 50;
            dgvTratamientos.Columns["Paciente"].Width = 160;
            dgvTratamientos.Columns["Odontologo"].Width = 160;
            dgvTratamientos.Columns["Descripcion"].Width = 220;
            dgvTratamientos.Columns["FechaInicio"].Width = 95;
            dgvTratamientos.Columns["FechaFin"].Width = 95;
            dgvTratamientos.Columns["Costo"].Width = 85;
            dgvTratamientos.Columns["Estado"].Width = 100;
        }

        private void LlenarCombos()
        {
            cmbPaciente.Items.Clear();
            foreach (var p in PacienteDatos.pacientes)
            {
                if (p.Estado == "A")
                {
                    cmbPaciente.Items.Add(p.IdPaciente + " - " + p.Nombres + " " + p.Apellidos);
                }
            }
            if (cmbPaciente.Items.Count > 0) cmbPaciente.SelectedIndex = 0;

            cmbOdontologo.Items.Clear();
            var odontologos = UsuarioDatos.ListarOdontologos();
            foreach (var od in odontologos)
            {
                cmbOdontologo.Items.Add(od.IdUsuario + " - " + od.NombreCompleto);
            }
            if (cmbOdontologo.Items.Count > 0) cmbOdontologo.SelectedIndex = 0;

            cmbEstado.SelectedIndex = 0;
        }

        private void CargarTratamientos()
        {
            dgvTratamientos.Rows.Clear();
            List<Tratamiento> lista = TratamientoDatos.Listar();
            foreach (var t in lista)
            {
                string paciente = PacienteDatos.ObtenerNombre(t.IdPaciente);
                string odontologo = "Dr. Asignado";
                foreach (var u in UsuarioDatos.usuarios)
                {
                    if (u.IdUsuario == t.IdOdontologo)
                    {
                        odontologo = u.NombreCompleto;
                        break;
                    }
                }
                dgvTratamientos.Rows.Add(t.IdTratamiento, paciente, odontologo, t.Descripcion, t.FechaInicio.ToString("dd/MM/yyyy"), t.FechaFin.ToString("dd/MM/yyyy"), "$" + t.Costo.ToString("F2"), t.Estado);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();
            dgvTratamientos.Rows.Clear();
            List<Tratamiento> lista = TratamientoDatos.Buscar(texto);
            foreach (var t in lista)
            {
                string paciente = PacienteDatos.ObtenerNombre(t.IdPaciente);
                string odontologo = "Dr. Asignado";
                foreach (var u in UsuarioDatos.usuarios)
                {
                    if (u.IdUsuario == t.IdOdontologo)
                    {
                        odontologo = u.NombreCompleto;
                        break;
                    }
                }
                dgvTratamientos.Rows.Add(t.IdTratamiento, paciente, odontologo, t.Descripcion, t.FechaInicio.ToString("dd/MM/yyyy"), t.FechaFin.ToString("dd/MM/yyyy"), "$" + t.Costo.ToString("F2"), t.Estado);
            }
        }

        public void Recargar()
        {
            Recarga.Combos(LlenarCombos, cmbPaciente, cmbOdontologo, cmbEstado);
            Recarga.Grid(dgvTratamientos, () =>
            {
                if (txtBuscar.Text.Trim() == "") CargarTratamientos();
                else btnBuscar_Click(this, EventArgs.Empty);
            });
        }

        private void dgvTratamientos_SelectionChanged(object sender, EventArgs e)
        {
            if (Recarga.EnCurso) return;
            if (dgvTratamientos.CurrentRow != null && dgvTratamientos.CurrentRow.Index >= 0)
            {
                var celdaId = dgvTratamientos.CurrentRow.Cells["IdTratamiento"].Value;
                if (celdaId != null)
                {
                    idTratamientoSeleccionado = Convert.ToInt32(celdaId);
                    foreach (var t in TratamientoDatos.tratamientos)
                    {
                        if (t.IdTratamiento == idTratamientoSeleccionado)
                        {
                            txtDescripcion.Text = t.Descripcion;
                            dtpFechaInicio.Value = t.FechaInicio;
                            dtpFechaFin.Value = t.FechaFin;
                            txtCosto.Text = t.Costo.ToString("0.00");
                            cmbEstado.SelectedItem = t.Estado;

                            for (int i = 0; i < cmbPaciente.Items.Count; i++)
                            {
                                if (cmbPaciente.Items[i].ToString().StartsWith(t.IdPaciente + " -"))
                                {
                                    cmbPaciente.SelectedIndex = i;
                                    break;
                                }
                            }
                            for (int i = 0; i < cmbOdontologo.Items.Count; i++)
                            {
                                if (cmbOdontologo.Items[i].ToString().StartsWith(t.IdOdontologo + " -"))
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
            idTratamientoSeleccionado = 0;
            txtDescripcion.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Evita duplicar un tratamiento (y su cargo) al pulsar Guardar con uno ya seleccionado
            if (idTratamientoSeleccionado != 0 && !Mensaje.Confirmar(
                    "Tiene seleccionado un tratamiento existente.\n¿Desea registrarlo como un tratamiento NUEVO (con un nuevo cargo)?\n\n" +
                    "Si solo quiere corregir el seleccionado, pulse \"No\" y use el botón Modificar.",
                    "Registrar nuevo tratamiento"))
                return;

            if (!ValidarCampos()) return;

            int idPaciente = Convert.ToInt32(cmbPaciente.SelectedItem.ToString().Split('-')[0].Trim());
            int idOdontologo = Convert.ToInt32(cmbOdontologo.SelectedItem.ToString().Split('-')[0].Trim());
            decimal costo = Convert.ToDecimal(txtCosto.Text.Trim());

            Tratamiento t = new Tratamiento();
            t.IdPaciente = idPaciente;
            t.IdOdontologo = idOdontologo;
            t.Descripcion = txtDescripcion.Text.Trim();
            t.FechaInicio = dtpFechaInicio.Value.Date;
            t.FechaFin = dtpFechaFin.Value.Date;
            t.Costo = costo;
            t.Estado = cmbEstado.SelectedItem?.ToString() ?? "Pendiente";

            TratamientoDatos.Agregar(t);

            // Generar cargo automático en estado de cuenta
            EstadoCuenta? ec = EstadoCuentaDatos.ObtenerPorPaciente(idPaciente);
            if (ec == null)
            {
                ec = EstadoCuentaDatos.CrearEstado(idPaciente);
            }
            MovimientoCuenta mc = new MovimientoCuenta();
            mc.IdEstado = ec.IdEstado;
            mc.Fecha = DateTime.Today;
            mc.Tipo = "Cargo";
            mc.Concepto = t.Descripcion;
            mc.Monto = costo;
            EstadoCuentaDatos.AgregarMovimiento(mc);

            BitacoraDatos.RegistrarAccion("Registro de tratamiento", "Se asignó tratamiento '" + t.Descripcion + "' a paciente ID " + idPaciente);

            MessageBox.Show("Tratamiento registrado exitosamente y cargo agregado al estado de cuenta.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarTratamientos();
            if (!Recarga.SeleccionarFila(dgvTratamientos, t.IdTratamiento)) LimpiarCampos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idTratamientoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un tratamiento de la tabla para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos()) return;

            int idPaciente = Convert.ToInt32(cmbPaciente.SelectedItem.ToString().Split('-')[0].Trim());
            int idOdontologo = Convert.ToInt32(cmbOdontologo.SelectedItem.ToString().Split('-')[0].Trim());
            decimal costo = Convert.ToDecimal(txtCosto.Text.Trim());

            foreach (var t in TratamientoDatos.tratamientos)
            {
                if (t.IdTratamiento == idTratamientoSeleccionado)
                {
                    string estadoAnterior = t.Estado;
                    int pacienteAnterior = t.IdPaciente;
                    decimal costoAnterior = t.Costo;

                    t.IdPaciente = idPaciente;
                    t.IdOdontologo = idOdontologo;
                    t.Descripcion = txtDescripcion.Text.Trim();
                    t.FechaInicio = dtpFechaInicio.Value.Date;
                    t.FechaFin = dtpFechaFin.Value.Date;
                    t.Costo = costo;
                    t.Estado = cmbEstado.SelectedItem?.ToString() ?? "Pendiente";
                    TratamientoDatos.Modificar(t);

                    decimal reembolso = 0;
                    if (estadoAnterior != "Anulado" && t.Estado == "Anulado")
                    {
                        reembolso = EstadoCuentaDatos.RevertirCargo(pacienteAnterior, "Anulación de tratamiento: " + t.Descripcion, costoAnterior);
                    }
                    else if (estadoAnterior == "Anulado" && t.Estado != "Anulado")
                    {
                        EstadoCuentaDatos.RegistrarCargo(t.IdPaciente, t.Descripcion, t.Costo);
                    }
                    else if (t.Estado != "Anulado")
                    {
                        // El cargo debe seguir al tratamiento si cambió el paciente o el costo
                        if (pacienteAnterior != t.IdPaciente)
                        {
                            reembolso = EstadoCuentaDatos.RevertirCargo(pacienteAnterior, "Tratamiento reasignado a otro paciente: " + t.Descripcion, costoAnterior);
                            EstadoCuentaDatos.RegistrarCargo(t.IdPaciente, t.Descripcion, t.Costo);
                        }
                        else if (t.Costo > costoAnterior)
                        {
                            EstadoCuentaDatos.RegistrarCargo(t.IdPaciente, "Ajuste de costo (aumento): " + t.Descripcion, t.Costo - costoAnterior);
                        }
                        else if (t.Costo < costoAnterior)
                        {
                            reembolso = EstadoCuentaDatos.RevertirCargo(t.IdPaciente, "Ajuste de costo (rebaja): " + t.Descripcion, costoAnterior - t.Costo);
                        }
                    }

                    if (reembolso > 0)
                    {
                        MessageBox.Show("El paciente ya había pagado este monto. Devuélvale $" + reembolso.ToString("F2") + " en caja (no se registra como saldo a favor).", "Reembolso al paciente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                }
            }

            BitacoraDatos.RegistrarAccion("Modificación de tratamiento", "Se actualizó el tratamiento ID " + idTratamientoSeleccionado);
            MessageBox.Show("Tratamiento actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            int id = idTratamientoSeleccionado;
            CargarTratamientos();
            if (!Recarga.SeleccionarFila(dgvTratamientos, id)) LimpiarCampos();
        }

        private void btnCancelarTratamiento_Click(object sender, EventArgs e)
        {
            if (idTratamientoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un tratamiento de la tabla para anular.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Tratamiento? tratamiento = null;
            foreach (var t in TratamientoDatos.tratamientos)
            {
                if (t.IdTratamiento == idTratamientoSeleccionado)
                {
                    tratamiento = t;
                    break;
                }
            }
            if (tratamiento == null) return;

            if (tratamiento.Estado == "Anulado")
            {
                MessageBox.Show("Este tratamiento ya está anulado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Mensaje.Confirmar("¿Está seguro de anular este tratamiento?\nSe revertirá su cargo de $" + tratamiento.Costo.ToString("F2") + " en el estado de cuenta del paciente.", "Confirmación"))
            {
                TratamientoDatos.DarDeBaja(idTratamientoSeleccionado);
                // Solo se abona lo que el paciente debe; si ya lo había pagado, se le reembolsa en caja
                decimal reembolso = EstadoCuentaDatos.RevertirCargo(tratamiento.IdPaciente, "Anulación de tratamiento: " + tratamiento.Descripcion, tratamiento.Costo);
                BitacoraDatos.RegistrarAccion("Anulación de tratamiento", "Se anuló el tratamiento ID " + idTratamientoSeleccionado + " y se revirtió su cargo de $" + tratamiento.Costo.ToString("F2"));
                string aviso = "Tratamiento anulado y cargo revertido en el estado de cuenta.";
                if (reembolso > 0) aviso += "\n\nEl paciente ya había pagado $" + reembolso.ToString("F2") + " de este tratamiento: devuélvale ese monto en caja.";
                MessageBox.Show(aviso, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                int id = idTratamientoSeleccionado;
                CargarTratamientos();
                if (!Recarga.SeleccionarFila(dgvTratamientos, id)) LimpiarCampos();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            dgvTratamientos.CurrentCell = null;
            dgvTratamientos.ClearSelection();
            idTratamientoSeleccionado = 0;
            txtDescripcion.Clear();
            txtCosto.Clear();
            dtpFechaInicio.Value = DateTime.Today;
            dtpFechaFin.Value = DateTime.Today.AddDays(7);
            if (cmbPaciente.Items.Count > 0) cmbPaciente.SelectedIndex = 0;
            if (cmbOdontologo.Items.Count > 0) cmbOdontologo.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
        }

        private bool ValidarCampos()
        {
            if (cmbPaciente.SelectedIndex < 0 || cmbOdontologo.SelectedIndex < 0)
            {
                MessageBox.Show("Debe seleccionar paciente y odontólogo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("La descripción del tratamiento es requerida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return false;
            }
            if (dtpFechaFin.Value.Date < dtpFechaInicio.Value.Date)
            {
                MessageBox.Show("La fecha de fin no puede ser menor a la fecha de inicio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            decimal costo;
            if (!decimal.TryParse(txtCosto.Text.Trim(), out costo) || costo <= 0)
            {
                MessageBox.Show("El costo debe ser un monto numérico mayor a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCosto.Focus();
                return false;
            }
            return true;
        }
    }
}

