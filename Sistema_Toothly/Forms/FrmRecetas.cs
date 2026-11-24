using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmRecetas : Form, IRecargable
    {
        private int idRecetaSeleccionada = 0;
        private List<RecetaDetalle> detallesTemporales = new List<RecetaDetalle>();

        // Botones de la lista de recetas (lado izquierdo)
        private Button btnModificarReceta = null!;
        private Button btnEliminarReceta = null!;

        public FrmRecetas()
        {
            InitializeComponent();
        }

        private void FrmRecetas_Load(object sender, EventArgs e)
        {
            // Botón de cerrar en el encabezado
            AgregarBotonCerrar();

            // Configuramos las dos tablas
            ConfigurarEstiloTabla(dgvRecetas);
            ConfigurarEstiloTabla(dgvMedicamentos);

            // Colores de los botones
            btnBuscar.Image = Properties.Resources.ico_buscar;

            btnNuevo.Image = Properties.Resources.ico_nuevo;

            btnGuardar.Image = Properties.Resources.ico_guardar;

            btnImprimir.Image = Properties.Resources.ico_imprimir;

            ConfigurarColumnas();
            LlenarComboPacientes();
            LlenarComboMedicamentos();
            CargarRecetas();

            // Botones Modificar / Eliminar debajo de la lista de recetas
            btnModificarReceta = BotonesAccion.CrearBoton("btnModificarReceta", "Modificar", Color.FromArgb(245, 124, 0), Properties.Resources.ico_editar, btnModificarReceta_Click);
            btnEliminarReceta = BotonesAccion.CrearBoton("btnEliminarReceta", "Eliminar", Color.FromArgb(220, 38, 38), Properties.Resources.ico_baja, btnEliminarReceta_Click);
            BotonesAccion.AgregarBarra(splPrincipal.Panel1, btnModificarReceta, btnEliminarReceta);
            ActualizarBotonesReceta();

            // Oculta los botones que el rol no tiene permitidos (Roles y Privilegios)
            Permisos.Aplicar(this);
        }

        private void LlenarComboMedicamentos()
        {
            cmbMedicamento.Items.Clear();
            cmbMedicamento.DisplayMember = "Nombre";
            foreach (var p in ProductoDatos.productos)
            {
                if (p.PrecioVenta > 0)
                {
                    cmbMedicamento.Items.Add(p);
                }
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

        public void PrepararNueva()
        {
            btnNuevo_Click(this, EventArgs.Empty);
        }

        private void ConfigurarColumnas()
        {
            // Recetas
            dgvRecetas.Columns.Clear();
            dgvRecetas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecetas.MultiSelect = false;
            dgvRecetas.ReadOnly = true;
            dgvRecetas.Columns.Add("IdReceta", "ID");
            dgvRecetas.Columns.Add("Paciente", "Paciente");
            dgvRecetas.Columns.Add("Fecha", "Fecha");
            dgvRecetas.Columns.Add("Indicaciones", "Indicaciones");

            dgvRecetas.Columns["IdReceta"].Width = 45;
            dgvRecetas.Columns["Paciente"].Width = 140;
            dgvRecetas.Columns["Fecha"].Width = 85;
            dgvRecetas.Columns["Indicaciones"].Width = 150;

            // Medicamentos
            dgvMedicamentos.Columns.Clear();
            dgvMedicamentos.Columns.Add("Medicamento", "Medicamento");
            dgvMedicamentos.Columns.Add("Cantidad", "Cant.");
            dgvMedicamentos.Columns.Add("Dosis", "Dosis");
            dgvMedicamentos.Columns.Add("Frecuencia", "Frecuencia");
            dgvMedicamentos.Columns.Add("Duracion", "Duración");

            dgvMedicamentos.Columns["Medicamento"].Width = 180;
            dgvMedicamentos.Columns["Cantidad"].Width = 60;
            dgvMedicamentos.Columns["Dosis"].Width = 100;
            dgvMedicamentos.Columns["Frecuencia"].Width = 110;
            dgvMedicamentos.Columns["Duracion"].Width = 110;
        }

        private void LlenarComboPacientes()
        {
            cmbPaciente.Items.Clear();
            foreach (var p in PacienteDatos.pacientes)
            {
                if (p.Estado == "A")
                {
                    cmbPaciente.Items.Add(p.IdPaciente + " - " + p.Nombres + " " + p.Apellidos);
                }
            }
            if (cmbPaciente.Items.Count > 0)
            {
                cmbPaciente.SelectedIndex = 0;
            }
        }

        private void cmbPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaciente.SelectedIndex < 0) return;

            int idPaciente = Convert.ToInt32(cmbPaciente.SelectedItem.ToString().Split('-')[0].Trim());

            // Llenar tratamientos de este paciente
            cmbTratamiento.Items.Clear();
            cmbTratamiento.Items.Add("0 - Consulta General (Sin tratamiento específico)");
            var tratamientos = TratamientoDatos.BuscarPorPaciente(idPaciente);
            foreach (var t in tratamientos)
            {
                cmbTratamiento.Items.Add(t.IdTratamiento + " - " + t.Descripcion + " (" + t.Estado + ")");
            }
            cmbTratamiento.SelectedIndex = 0;
        }

        private void CargarRecetas()
        {
            dgvRecetas.Rows.Clear();
            List<Receta> lista = RecetaDatos.Listar();
            foreach (var r in lista)
            {
                string paciente = PacienteDatos.ObtenerNombre(r.IdPaciente);
                dgvRecetas.Rows.Add(r.IdReceta, paciente, r.Fecha.ToString("dd/MM/yyyy"), r.Indicaciones);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();
            dgvRecetas.Rows.Clear();
            List<Receta> lista = RecetaDatos.Buscar(texto);
            foreach (var r in lista)
            {
                string paciente = PacienteDatos.ObtenerNombre(r.IdPaciente);
                dgvRecetas.Rows.Add(r.IdReceta, paciente, r.Fecha.ToString("dd/MM/yyyy"), r.Indicaciones);
            }
        }

        public void Recargar()
        {
            Recarga.Combos(LlenarComboPacientes, cmbPaciente, cmbTratamiento);
            string medicamento = cmbMedicamento.Text;
            LlenarComboMedicamentos();
            cmbMedicamento.Text = medicamento;
            Recarga.Grid(dgvRecetas, () =>
            {
                if (txtBuscar.Text.Trim() == "") CargarRecetas();
                else btnBuscar_Click(this, EventArgs.Empty);
            }, false);
        }

        private void dgvRecetas_SelectionChanged(object sender, EventArgs e)
        {
            if (Recarga.EnCurso) return;
            if (dgvRecetas.CurrentRow != null && dgvRecetas.CurrentRow.Index >= 0)
            {
                var celdaId = dgvRecetas.CurrentRow.Cells["IdReceta"].Value;
                if (celdaId != null)
                {
                    idRecetaSeleccionada = Convert.ToInt32(celdaId);
                    foreach (var r in RecetaDatos.recetas)
                    {
                        if (r.IdReceta == idRecetaSeleccionada)
                        {
                            dtpFecha.Value = r.Fecha;
                            txtIndicaciones.Text = r.Indicaciones;

                            // Seleccionar paciente
                            for (int i = 0; i < cmbPaciente.Items.Count; i++)
                            {
                                if (cmbPaciente.Items[i].ToString().StartsWith(r.IdPaciente + " -"))
                                {
                                    cmbPaciente.SelectedIndex = i;
                                    break;
                                }
                            }

                            // Seleccionar el tratamiento asociado (antes siempre quedaba "Consulta General")
                            if (r.IdTratamiento > 0)
                            {
                                for (int i = 0; i < cmbTratamiento.Items.Count; i++)
                                {
                                    if (cmbTratamiento.Items[i].ToString().StartsWith(r.IdTratamiento + " -"))
                                    {
                                        cmbTratamiento.SelectedIndex = i;
                                        break;
                                    }
                                }
                            }

                            // Cargar medicamentos guardados de la receta
                            detallesTemporales.Clear();
                            detallesTemporales.AddRange(RecetaDatos.ObtenerDetalles(idRecetaSeleccionada));
                            RefrescarGridMedicamentos();
                            ActualizarBotonesReceta();
                            break;
                        }
                    }
                }
            }
        }

        private void RefrescarGridMedicamentos()
        {
            dgvMedicamentos.Rows.Clear();
            foreach (var d in detallesTemporales)
            {
                dgvMedicamentos.Rows.Add(d.Medicamento, d.Cantidad > 0 ? d.Cantidad.ToString() : "-", d.Dosis, d.Frecuencia, d.Duracion);
            }
        }

        private void btnAgregarMed_Click(object sender, EventArgs e)
        {
            string med = cmbMedicamento.Text.Trim();
            string dosis = txtDosis.Text.Trim();
            string frec = txtFrecuencia.Text.Trim();
            string dur = txtDuracion.Text.Trim();

            if (string.IsNullOrWhiteSpace(med))
            {
                MessageBox.Show("Escriba o seleccione el medicamento.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMedicamento.Focus();
                return;
            }

            Producto? producto = null;
            foreach (var item in cmbMedicamento.Items)
            {
                if (item is Producto p && string.Equals(p.Nombre, med, StringComparison.OrdinalIgnoreCase))
                {
                    producto = p;
                    med = p.Nombre;
                    break;
                }
            }

            int cantidad = 0;
            if (!string.IsNullOrWhiteSpace(txtCantidadMed.Text) && (!int.TryParse(txtCantidadMed.Text.Trim(), out cantidad) || cantidad < 0))
            {
                MessageBox.Show("La cantidad debe ser un número entero de unidades.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCantidadMed.Focus();
                return;
            }

            if (producto != null && cantidad <= 0)
            {
                MessageBox.Show("Indique cuántas unidades de " + producto.Nombre + " se recetan.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCantidadMed.Focus();
                return;
            }

            if (producto != null && cantidad > producto.StockActual)
            {
                MessageBox.Show("En la clínica solo hay " + producto.StockActual + " unidad(es) de " + producto.Nombre + ".\nEl paciente podrá comprar el resto en una farmacia.", "Stock disponible", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            RecetaDetalle d = new RecetaDetalle();
            d.Medicamento = med;
            d.IdProducto = producto?.IdProducto;
            d.Cantidad = cantidad;
            d.Dosis = string.IsNullOrWhiteSpace(dosis) ? "1 tableta" : dosis;
            d.Frecuencia = string.IsNullOrWhiteSpace(frec) ? "Cada 8 horas" : frec;
            d.Duracion = string.IsNullOrWhiteSpace(dur) ? "5 días" : dur;

            detallesTemporales.Add(d);
            RefrescarGridMedicamentos();

            // Limpiar campos de medicamento
            cmbMedicamento.SelectedIndex = -1;
            cmbMedicamento.Text = "";
            txtCantidadMed.Clear();
            txtDosis.Clear();
            txtFrecuencia.Clear();
            txtDuracion.Clear();
            cmbMedicamento.Focus();
        }

        private void btnQuitarMed_Click(object sender, EventArgs e)
        {
            if (dgvMedicamentos.CurrentRow != null && dgvMedicamentos.CurrentRow.Index >= 0)
            {
                int index = dgvMedicamentos.CurrentRow.Index;
                if (index < detallesTemporales.Count)
                {
                    detallesTemporales.RemoveAt(index);
                    RefrescarGridMedicamentos();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un medicamento de la tabla para quitarlo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            dgvRecetas.CurrentCell = null;
            dgvRecetas.ClearSelection();
            idRecetaSeleccionada = 0;
            detallesTemporales.Clear();
            RefrescarGridMedicamentos();
            txtIndicaciones.Clear();
            dtpFecha.Value = DateTime.Today;
            ActualizarBotonesReceta();
            cmbMedicamento.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Evita duplicar la receta seleccionada al pulsar Guardar
            if (idRecetaSeleccionada != 0 && !Mensaje.Confirmar(
                    "Tiene seleccionada la receta #" + idRecetaSeleccionada + ".\n¿Desea guardar estos datos como una receta NUEVA?\n\n" +
                    "Para corregir la receta seleccionada, pulse \"No\" y use el botón Modificar.",
                    "Guardar como nueva receta"))
                return;

            Receta? r = ConstruirRecetaDesdeFormulario();
            if (r == null) return;

            try
            {
                RecetaDatos.AgregarConDetalles(r, detallesTemporales);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar la receta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BitacoraDatos.RegistrarAccion("Emisión de receta", "Se emitió receta ID " + r.IdReceta + " para paciente ID " + r.IdPaciente);
            MessageBox.Show("Receta guardada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CargarRecetas();
            if (!Recarga.SeleccionarFila(dgvRecetas, r.IdReceta)) btnNuevo_Click(sender, e);
        }

        // Valida el formulario y arma el objeto Receta (null si hay errores)
        private Receta? ConstruirRecetaDesdeFormulario()
        {
            if (cmbPaciente.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un paciente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            if (detallesTemporales.Count == 0)
            {
                MessageBox.Show("Agregue al menos un medicamento a la receta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            int idPaciente = Convert.ToInt32(cmbPaciente.SelectedItem.ToString().Split('-')[0].Trim());
            int idTratamiento = 0;
            if (cmbTratamiento.SelectedIndex > 0)
            {
                idTratamiento = Convert.ToInt32(cmbTratamiento.SelectedItem.ToString().Split('-')[0].Trim());
            }

            Receta r = new Receta();
            r.IdPaciente = idPaciente;
            r.IdTratamiento = idTratamiento;
            r.Fecha = dtpFecha.Value.Date;
            r.Indicaciones = txtIndicaciones.Text.Trim();
            return r;
        }

        // Solo el Administrador y el Odontólogo gestionan recetas; además se exige el privilegio del rol
        private bool PuedeGestionarReceta(int privilegio)
        {
            if (!Sesion.EsRol("Administrador") && !Sesion.EsRol("Odontólogo"))
            {
                MessageBox.Show("Solo el Administrador o el Odontólogo pueden modificar o eliminar recetas.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return Permisos.Verificar(privilegio);
        }

        // Habilita Modificar/Eliminar solo si hay una receta seleccionada sin medicamentos despachados
        private void ActualizarBotonesReceta()
        {
            if (btnModificarReceta == null || btnEliminarReceta == null) return;

            bool haySeleccion = idRecetaSeleccionada != 0;
            bool despachada = haySeleccion && RecetaDatos.TieneDespachos(idRecetaSeleccionada);
            btnModificarReceta.Enabled = haySeleccion && !despachada;
            btnEliminarReceta.Enabled = haySeleccion && !despachada;
        }

        private void btnModificarReceta_Click(object? sender, EventArgs e)
        {
            if (!PuedeGestionarReceta(Permisos.Modificar)) return;

            if (idRecetaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione en la lista la receta que desea modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (RecetaDatos.TieneDespachos(idRecetaSeleccionada))
            {
                MessageBox.Show("Esta receta ya tiene medicamentos despachados (se descontaron del inventario y se cobraron).\nNo se puede modificar; si necesita corregirla, emita una receta nueva.", "Receta despachada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Receta? r = ConstruirRecetaDesdeFormulario();
            if (r == null) return;
            r.IdReceta = idRecetaSeleccionada;

            if (!Mensaje.Confirmar("¿Desea guardar los cambios en la receta #" + r.IdReceta + "?\nSe reemplazarán sus medicamentos por los de la tabla.", "Modificar receta")) return;

            try
            {
                RecetaDatos.Modificar(r, detallesTemporales);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo modificar la receta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BitacoraDatos.RegistrarAccion("Modificación de receta", "Se modificó la receta ID " + r.IdReceta + " del paciente ID " + r.IdPaciente);
            MessageBox.Show("Receta actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CargarRecetas();
            if (!Recarga.SeleccionarFila(dgvRecetas, r.IdReceta)) btnNuevo_Click(this, EventArgs.Empty);
        }

        private void btnEliminarReceta_Click(object? sender, EventArgs e)
        {
            if (!PuedeGestionarReceta(Permisos.Eliminar)) return;

            if (idRecetaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione en la lista la receta que desea eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (RecetaDatos.TieneDespachos(idRecetaSeleccionada))
            {
                MessageBox.Show("Esta receta ya tiene medicamentos despachados y no se puede eliminar.", "Receta despachada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = idRecetaSeleccionada;
            if (!Mensaje.ConfirmarAdvertencia("¿Está seguro de eliminar por completo la receta #" + id + "?\nEsta acción no se puede deshacer.", "Eliminar receta")) return;

            try
            {
                RecetaDatos.Eliminar(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar la receta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BitacoraDatos.RegistrarAccion("Eliminación de receta", "Se eliminó la receta ID " + id);
            MessageBox.Show("Receta eliminada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CargarRecetas();
            btnNuevo_Click(this, EventArgs.Empty);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (idRecetaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una receta de la lista para imprimir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Receta? r = null;
            foreach (var rec in RecetaDatos.recetas)
            {
                if (rec.IdReceta == idRecetaSeleccionada)
                {
                    r = rec;
                    break;
                }
            }
            if (r == null) return;

            string nombrePaciente = PacienteDatos.ObtenerNombre(r.IdPaciente);
            var detalles = RecetaDatos.ObtenerDetalles(idRecetaSeleccionada);

            DataGridView dgvTemp = new DataGridView();
            dgvTemp.Columns.Add("Medicamento", "Medicamento");
            dgvTemp.Columns.Add("Cantidad", "Cantidad");
            dgvTemp.Columns.Add("Dosis", "Dosis");
            dgvTemp.Columns.Add("Frecuencia", "Frecuencia");
            dgvTemp.Columns.Add("Duracion", "Duración");

            foreach (var d in detalles)
            {
                dgvTemp.Rows.Add(d.Medicamento, d.Cantidad > 0 ? d.Cantidad + " und" : "-", d.Dosis, d.Frecuencia, d.Duracion);
            }

            GeneradorPDF.GenerarDesdeGrid($"Receta Médica #{r.IdReceta} - Paciente: {nombrePaciente}", dgvTemp);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            btnNuevo_Click(sender, e);
        }
    }
}

