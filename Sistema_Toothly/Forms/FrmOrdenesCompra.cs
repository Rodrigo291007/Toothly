using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmOrdenesCompra : Form, IRecargable
    {
        private int idOrdenSeleccionada = 0;
        private List<OrdenCompraDetalle> detallesTemporales = new List<OrdenCompraDetalle>();
        private string estadoOrdenSeleccionada = "";

        // Botones de la lista de órdenes (lado izquierdo)
        private Button btnModificarOrden = null!;
        private Button btnEliminarOrden = null!;

        public FrmOrdenesCompra()
        {
            InitializeComponent();
        }

        private void FrmOrdenesCompra_Load(object sender, EventArgs e)
        {
            // Botón de cerrar en el encabezado
            AgregarBotonCerrar();

            // Configuramos las dos tablas
            ConfigurarEstiloTabla(dgvOrdenes);
            ConfigurarEstiloTabla(dgvDetalle);

            btnBuscar.Image = Properties.Resources.ico_buscar;

            btnNuevo.Image = Properties.Resources.ico_nuevo;

            btnGuardar.Image = Properties.Resources.ico_guardar;

            btnRecibirOrden.Image = Properties.Resources.ico_orden;

            ConfigurarColumnas();
            LlenarCombos();
            CargarOrdenes();

            // Botones Modificar / Eliminar debajo de la lista de órdenes
            btnModificarOrden = BotonesAccion.CrearBoton("btnModificarOrden", "Modificar", Color.FromArgb(245, 124, 0), Properties.Resources.ico_editar, btnModificarOrden_Click);
            btnEliminarOrden = BotonesAccion.CrearBoton("btnEliminarOrden", "Eliminar", Color.FromArgb(220, 38, 38), Properties.Resources.ico_baja, btnEliminarOrden_Click);
            BotonesAccion.AgregarBarra(splOrdenes.Panel1, btnModificarOrden, btnEliminarOrden);
            ActualizarBotonesOrden();

            // Oculta los botones que el rol no tiene permitidos (Roles y Privilegios)
            Permisos.Aplicar(this);
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
            // Órdenes
            dgvOrdenes.Columns.Clear();
            dgvOrdenes.Columns.Add("IdOrden", "ID");
            dgvOrdenes.Columns.Add("Proveedor", "Proveedor");
            dgvOrdenes.Columns.Add("Fecha", "Fecha");
            dgvOrdenes.Columns.Add("Total", "Total ($)");
            dgvOrdenes.Columns.Add("Estado", "Estado");

            dgvOrdenes.Columns["IdOrden"].Width = 45;
            dgvOrdenes.Columns["Proveedor"].Width = 140;
            dgvOrdenes.Columns["Fecha"].Width = 85;
            dgvOrdenes.Columns["Total"].Width = 80;
            dgvOrdenes.Columns["Estado"].Width = 90;

            // Detalle
            dgvDetalle.Columns.Clear();
            dgvDetalle.Columns.Add("Producto", "Producto");
            dgvDetalle.Columns.Add("Cantidad", "Cajas");
            dgvDetalle.Columns.Add("Precio", "Precio Caja");
            dgvDetalle.Columns.Add("Subtotal", "Subtotal ($)");

            dgvDetalle.Columns["Producto"].Width = 200;
            dgvDetalle.Columns["Cantidad"].Width = 80;
            dgvDetalle.Columns["Precio"].Width = 100;
            dgvDetalle.Columns["Subtotal"].Width = 110;
        }

        private void LlenarCombos()
        {
            // Proveedores
            cmbProveedor.Items.Clear();
            foreach (var p in ProveedorDatos.proveedores)
            {
                if (p.Estado == "A")
                {
                    cmbProveedor.Items.Add(p.IdProveedor + " - " + p.Nombre);
                }
            }
            if (cmbProveedor.Items.Count > 0) cmbProveedor.SelectedIndex = 0;

            // Productos
            cmbProducto.Items.Clear();
            foreach (var prod in ProductoDatos.productos)
            {
                cmbProducto.Items.Add(prod.IdProducto + " - " + prod.Nombre);
            }
            if (cmbProducto.Items.Count > 0)
            {
                cmbProducto.SelectedIndex = 0;
                CargarPrecioProducto();
            }
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPrecioProducto();
        }

        private void CargarPrecioProducto()
        {
            if (Recarga.EnCurso) return;
            if (cmbProducto.SelectedIndex < 0) return;
            int id = Convert.ToInt32(cmbProducto.SelectedItem.ToString().Split('-')[0].Trim());
            Producto? prod = ProductoDatos.ObtenerPorId(id);
            if (prod != null)
            {
                txtPrecioUnitario.Text = prod.PrecioCompra.ToString("0.00");
                txtCantidad.Text = "1";
            }
        }

        private void CargarOrdenes()
        {
            dgvOrdenes.Rows.Clear();
            List<OrdenCompra> lista = OrdenCompraDatos.Listar();
            foreach (var o in lista)
            {
                string prov = ProveedorDatos.ObtenerNombre(o.IdProveedor);
                dgvOrdenes.Rows.Add(o.IdOrden, prov, o.Fecha.ToString("dd/MM/yyyy"), "$" + o.Total.ToString("F2"), o.Estado);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();
            dgvOrdenes.Rows.Clear();
            List<OrdenCompra> lista = OrdenCompraDatos.Buscar(texto);
            foreach (var o in lista)
            {
                string prov = ProveedorDatos.ObtenerNombre(o.IdProveedor);
                dgvOrdenes.Rows.Add(o.IdOrden, prov, o.Fecha.ToString("dd/MM/yyyy"), "$" + o.Total.ToString("F2"), o.Estado);
            }
        }

        public void Recargar()
        {
            Recarga.Combos(LlenarCombos, cmbProveedor, cmbProducto);
            Recarga.Grid(dgvOrdenes, () =>
            {
                if (txtBuscar.Text.Trim() == "") CargarOrdenes();
                else btnBuscar_Click(this, EventArgs.Empty);
            }, false);
        }

        private void dgvOrdenes_SelectionChanged(object sender, EventArgs e)
        {
            if (Recarga.EnCurso) return;
            if (dgvOrdenes.CurrentRow != null && dgvOrdenes.CurrentRow.Index >= 0)
            {
                var celdaId = dgvOrdenes.CurrentRow.Cells["IdOrden"].Value;
                if (celdaId != null)
                {
                    idOrdenSeleccionada = Convert.ToInt32(celdaId);
                    foreach (var o in OrdenCompraDatos.ordenes)
                    {
                        if (o.IdOrden == idOrdenSeleccionada)
                        {
                            dtpFecha.Value = o.Fecha;
                            lblTotal.Text = "Total: $" + o.Total.ToString("F2");

                            for (int i = 0; i < cmbProveedor.Items.Count; i++)
                            {
                                if (cmbProveedor.Items[i].ToString().StartsWith(o.IdProveedor + " -"))
                                {
                                    cmbProveedor.SelectedIndex = i;
                                    break;
                                }
                            }

                            // Habilitar o deshabilitar botones según estado (solo las Pendientes se reciben, modifican o eliminan)
                            estadoOrdenSeleccionada = o.Estado;
                            btnRecibirOrden.Enabled = (o.Estado == "Pendiente");
                            ActualizarBotonesOrden();

                            // Cargar detalles
                            detallesTemporales.Clear();
                            var detalles = OrdenCompraDatos.ObtenerDetalles(idOrdenSeleccionada);
                            foreach (var d in detalles)
                            {
                                detallesTemporales.Add(new OrdenCompraDetalle
                                {
                                    IdDetalle = d.IdDetalle,
                                    IdOrden = d.IdOrden,
                                    IdProducto = d.IdProducto,
                                    Cantidad = d.Cantidad,
                                    PrecioUnitario = d.PrecioUnitario,
                                    Subtotal = d.Subtotal
                                });
                            }
                            RefrescarGridDetalle();
                            break;
                        }
                    }
                }
            }
        }

        private void RefrescarGridDetalle()
        {
            dgvDetalle.Rows.Clear();
            decimal granTotal = 0;
            foreach (var d in detallesTemporales)
            {
                string prodNombre = "Producto Desconocido";
                Producto? p = ProductoDatos.ObtenerPorId(d.IdProducto);
                if (p != null) prodNombre = p.Nombre + " (" + p.Codigo + ")";

                dgvDetalle.Rows.Add(prodNombre, d.Cantidad, "$" + d.PrecioUnitario.ToString("F2"), "$" + d.Subtotal.ToString("F2"));
                granTotal += d.Subtotal;
            }
            lblTotal.Text = "Total: $" + granTotal.ToString("F2");
        }

        private void btnAgregarProd_Click(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedIndex < 0) return;

            int cantidad;
            if (!int.TryParse(txtCantidad.Text.Trim(), out cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida mayor a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCantidad.Focus();
                return;
            }

            decimal precio;
            if (!decimal.TryParse(txtPrecioUnitario.Text.Trim(), out precio) || precio <= 0)
            {
                MessageBox.Show("Ingrese un precio unitario mayor a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioUnitario.Focus();
                return;
            }

            int idProd = Convert.ToInt32(cmbProducto.SelectedItem.ToString().Split('-')[0].Trim());

            OrdenCompraDetalle det = new OrdenCompraDetalle();
            det.IdProducto = idProd;
            det.Cantidad = cantidad;
            det.PrecioUnitario = precio;
            det.Subtotal = cantidad * precio;

            detallesTemporales.Add(det);
            RefrescarGridDetalle();

            txtCantidad.Text = "1";
        }

        private void btnQuitarProd_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.CurrentRow != null && dgvDetalle.CurrentRow.Index >= 0)
            {
                int index = dgvDetalle.CurrentRow.Index;
                if (index < detallesTemporales.Count)
                {
                    detallesTemporales.RemoveAt(index);
                    RefrescarGridDetalle();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto del detalle para quitarlo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            dgvOrdenes.CurrentCell = null;
            dgvOrdenes.ClearSelection();
            idOrdenSeleccionada = 0;
            detallesTemporales.Clear();
            RefrescarGridDetalle();
            dtpFecha.Value = DateTime.Today;
            btnRecibirOrden.Enabled = false;
            estadoOrdenSeleccionada = "";
            ActualizarBotonesOrden();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Evita duplicar la orden seleccionada al pulsar Guardar
            if (idOrdenSeleccionada != 0 && !Mensaje.Confirmar(
                    "Tiene seleccionada la orden #" + idOrdenSeleccionada + ".\n¿Desea guardar estos datos como una orden NUEVA?\n\n" +
                    "Para corregir la orden seleccionada (si está Pendiente), pulse \"No\" y use el botón Modificar.",
                    "Guardar como nueva orden"))
                return;

            OrdenCompra? o = ConstruirOrdenDesdeFormulario();
            if (o == null) return;
            o.Estado = "Pendiente";

            try
            {
                OrdenCompraDatos.AgregarConDetalles(o, detallesTemporales);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar la orden: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BitacoraDatos.RegistrarAccion("Registro de orden de compra", "Se creó la orden de compra ID " + o.IdOrden + " con total de $" + o.Total.ToString("F2"));
            MessageBox.Show("Orden de compra guardada con éxito (Estado: Pendiente).", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CargarOrdenes();
            if (!Recarga.SeleccionarFila(dgvOrdenes, o.IdOrden)) btnNuevo_Click(sender, e);
        }

        // Valida el formulario y arma la orden (null si hay errores)
        private OrdenCompra? ConstruirOrdenDesdeFormulario()
        {
            if (cmbProveedor.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            if (detallesTemporales.Count == 0)
            {
                MessageBox.Show("Agregue al menos un producto a la orden de compra.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            int idProv = Convert.ToInt32(cmbProveedor.SelectedItem.ToString().Split('-')[0].Trim());

            decimal total = 0;
            foreach (var d in detallesTemporales)
            {
                total += d.Subtotal;
            }

            OrdenCompra o = new OrdenCompra();
            o.IdProveedor = idProv;
            o.Fecha = dtpFecha.Value.Date;
            o.Total = total;
            return o;
        }

        // Solo el Administrador y el Asistente Odontológico gestionan órdenes; además se exige el privilegio
        private bool PuedeGestionarOrden(int privilegio)
        {
            if (!Sesion.EsRol("Administrador") && !Sesion.EsRol("Asistente Odontológico"))
            {
                MessageBox.Show("Solo el Administrador o el Asistente Odontológico pueden modificar o eliminar órdenes de compra.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return Permisos.Verificar(privilegio);
        }

        private void ActualizarBotonesOrden()
        {
            if (btnModificarOrden == null || btnEliminarOrden == null) return;
            bool pendiente = idOrdenSeleccionada != 0 && estadoOrdenSeleccionada == "Pendiente";
            btnModificarOrden.Enabled = pendiente;
            btnEliminarOrden.Enabled = pendiente;
        }

        private void btnModificarOrden_Click(object? sender, EventArgs e)
        {
            if (!PuedeGestionarOrden(Permisos.Modificar)) return;

            if (idOrdenSeleccionada == 0)
            {
                MessageBox.Show("Seleccione en la lista la orden que desea modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (estadoOrdenSeleccionada != "Pendiente")
            {
                MessageBox.Show("Solo se pueden modificar órdenes Pendientes. Esta orden está " + estadoOrdenSeleccionada + ".", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OrdenCompra? o = ConstruirOrdenDesdeFormulario();
            if (o == null) return;
            o.IdOrden = idOrdenSeleccionada;
            o.Estado = "Pendiente";

            if (!Mensaje.Confirmar("¿Desea guardar los cambios en la orden #" + o.IdOrden + "?\nNuevo total: $" + o.Total.ToString("F2"), "Modificar orden")) return;

            try
            {
                OrdenCompraDatos.ModificarConDetalles(o, detallesTemporales);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo modificar la orden: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BitacoraDatos.RegistrarAccion("Modificación de orden de compra", "Se modificó la orden ID " + o.IdOrden + " (total $" + o.Total.ToString("F2") + ")");
            MessageBox.Show("Orden de compra actualizada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CargarOrdenes();
            if (!Recarga.SeleccionarFila(dgvOrdenes, o.IdOrden)) btnNuevo_Click(this, EventArgs.Empty);
        }

        private void btnEliminarOrden_Click(object? sender, EventArgs e)
        {
            if (!PuedeGestionarOrden(Permisos.Eliminar)) return;

            if (idOrdenSeleccionada == 0)
            {
                MessageBox.Show("Seleccione en la lista la orden que desea eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (estadoOrdenSeleccionada != "Pendiente")
            {
                MessageBox.Show("Solo se pueden eliminar órdenes Pendientes. Una orden Recibida ya sumó sus productos al inventario.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = idOrdenSeleccionada;
            if (!Mensaje.ConfirmarAdvertencia("¿Está seguro de eliminar la orden de compra #" + id + "?\nEsta acción no se puede deshacer.", "Eliminar orden")) return;

            try
            {
                OrdenCompraDatos.Eliminar(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar la orden: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BitacoraDatos.RegistrarAccion("Eliminación de orden de compra", "Se eliminó la orden ID " + id);
            MessageBox.Show("Orden de compra eliminada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CargarOrdenes();
            btnNuevo_Click(this, EventArgs.Empty);
        }

        private void btnRecibirOrden_Click(object sender, EventArgs e)
        {
            if (idOrdenSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una orden pendiente de la tabla para recibirla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Mensaje.Confirmar(
                "¿Desea marcar esta orden como RECIBIDA?\nEsto sumará automáticamente las cantidades al stock actual de cada producto.",
                "Confirmación de Recepción"))
            {
                // REGLA CLAVE: Recibir orden suma stock
                OrdenCompraDatos.RecibirOrden(idOrdenSeleccionada);
                BitacoraDatos.RegistrarAccion("Recepción de orden", "Se recibió la orden ID " + idOrdenSeleccionada + " y se actualizó el stock de productos.");

                MessageBox.Show("Orden marcada como Recibida y existencias actualizadas en el inventario.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                int id = idOrdenSeleccionada;
                CargarOrdenes();
                if (!Recarga.SeleccionarFila(dgvOrdenes, id)) btnNuevo_Click(sender, e);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            btnNuevo_Click(sender, e);
        }
    }
}
