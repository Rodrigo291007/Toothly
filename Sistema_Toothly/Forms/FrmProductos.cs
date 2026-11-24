using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmProductos : Form, IRecargable
    {
        private int idProductoSeleccionado = 0;

        public FrmProductos()
        {
            InitializeComponent();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            // Botón de cerrar en el encabezado
            AgregarBotonCerrar();

            // Colores de los botones
            btnBuscar.Image = Properties.Resources.ico_buscar;

            btnNuevo.Image = Properties.Resources.ico_nuevo;

            btnGuardar.Image = Properties.Resources.ico_guardar;

            btnModificar.Image = Properties.Resources.ico_editar;

            // Formato de la tabla
            ConfigurarEstiloTabla();

            ConfigurarColumnas();
            LlenarCategorias();
            CargarProductos();
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

        // Colores y diseño de la tabla de productos
        private void ConfigurarEstiloTabla()
        {
            dgvProductos.BackgroundColor = Color.White;
            dgvProductos.BorderStyle = BorderStyle.None;
            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(108, 180, 234);
            dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvProductos.ColumnHeadersHeight = 35;
            dgvProductos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(186, 230, 253);
            dgvProductos.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 254);
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ConfigurarColumnas()
        {
            dgvProductos.Columns.Clear();
            dgvProductos.Columns.Add("IdProducto", "ID");
            dgvProductos.Columns.Add("Codigo", "Código");
            dgvProductos.Columns.Add("Nombre", "Nombre");
            dgvProductos.Columns.Add("Categoria", "Categoría");
            dgvProductos.Columns.Add("StockActual", "Stock (und)");
            dgvProductos.Columns.Add("StockMinimo", "Stock Mín.");
            dgvProductos.Columns.Add("Precio", "Precio Caja ($)");
            dgvProductos.Columns.Add("UnidadesCaja", "Und/Caja");
            dgvProductos.Columns.Add("PrecioVenta", "Venta Und. ($)");
            dgvProductos.Columns.Add("Vencimiento", "Vencimiento");

            dgvProductos.Columns["IdProducto"].Width = 45;
            dgvProductos.Columns["Codigo"].Width = 90;
            dgvProductos.Columns["Nombre"].Width = 220;
            dgvProductos.Columns["Categoria"].Width = 130;
            dgvProductos.Columns["StockActual"].Width = 95;
            dgvProductos.Columns["StockMinimo"].Width = 95;
            dgvProductos.Columns["Precio"].Width = 110;
            dgvProductos.Columns["UnidadesCaja"].Width = 80;
            dgvProductos.Columns["PrecioVenta"].Width = 110;
            dgvProductos.Columns["Vencimiento"].Width = 100;
        }

        private void LlenarCategorias()
        {
            cmbCategoria.Items.Clear();
            foreach (var c in ProductoDatos.categorias)
            {
                cmbCategoria.Items.Add(c.IdCategoria + " - " + c.Nombre);
            }
            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
        }

        private void CargarProductos()
        {
            dgvProductos.Rows.Clear();
            List<Producto> lista = ProductoDatos.Listar();
            foreach (var p in lista)
            {
                AgregarFila(p);
            }
        }

        private void AgregarFila(Producto p)
        {
            string cat = ProductoDatos.ObtenerNombreCategoria(p.IdCategoria);
            dgvProductos.Rows.Add(p.IdProducto, p.Codigo, p.Nombre, cat, p.StockActual, p.StockMinimo, "$" + p.PrecioCompra.ToString("F2"), p.UnidadesPorCaja, "$" + p.PrecioVenta.ToString("F2"), p.FechaVencimiento.ToString("dd/MM/yyyy"));
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();
            dgvProductos.Rows.Clear();
            List<Producto> lista = ProductoDatos.Buscar(texto);
            foreach (var p in lista)
            {
                AgregarFila(p);
            }
        }

        public void Recargar()
        {
            Recarga.Combos(LlenarCategorias, cmbCategoria);
            Recarga.Grid(dgvProductos, () =>
            {
                if (txtBuscar.Text.Trim() == "") CargarProductos();
                else btnBuscar_Click(this, EventArgs.Empty);
            });
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (Recarga.EnCurso) return;
            if (dgvProductos.CurrentRow != null && dgvProductos.CurrentRow.Index >= 0)
            {
                var celdaId = dgvProductos.CurrentRow.Cells["IdProducto"].Value;
                if (celdaId != null)
                {
                    idProductoSeleccionado = Convert.ToInt32(celdaId);
                    Producto? p = ProductoDatos.ObtenerPorId(idProductoSeleccionado);
                    if (p != null)
                    {
                        txtCodigo.Text = p.Codigo;
                        txtNombre.Text = p.Nombre;
                        txtStockActual.Text = p.StockActual.ToString();
                        txtStockActual.ReadOnly = true;
                        txtStockMinimo.Text = p.StockMinimo.ToString();
                        txtPrecio.Text = p.PrecioCompra.ToString("0.00");
                        txtUnidadesCaja.Text = p.UnidadesPorCaja.ToString();
                        txtPrecioVenta.Text = p.PrecioVenta.ToString("0.00");
                        dtpVencimiento.Value = p.FechaVencimiento;

                        for (int i = 0; i < cmbCategoria.Items.Count; i++)
                        {
                            if (cmbCategoria.Items[i].ToString().StartsWith(p.IdCategoria + " -"))
                            {
                                cmbCategoria.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            idProductoSeleccionado = 0;
            txtCodigo.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Si hay un producto seleccionado, "Guardar" crearía una copia con el mismo código.
            // Se ofrece actualizar el producto seleccionado (lo que normalmente se quiere hacer).
            if (idProductoSeleccionado != 0)
            {
                if (Mensaje.Confirmar(
                        "Tiene seleccionado un producto existente.\n¿Desea guardar los cambios en ese producto (Modificar)?\n\n" +
                        "Para registrar un producto nuevo, pulse \"No\", luego \"Nuevo\" y escriba un código distinto.",
                        "Guardar cambios"))
                {
                    if (!Permisos.Verificar(Permisos.Modificar)) return;
                    btnModificar_Click(sender, e);
                }
                return;
            }

            if (!ValidarCampos()) return;

            if (ProductoDatos.ExisteCodigo(txtCodigo.Text.Trim()))
            {
                MessageBox.Show("Ya existe un producto con el código " + txtCodigo.Text.Trim() + ".\nUse un código diferente o seleccione ese producto y pulse Modificar.", "Código duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return;
            }

            int idCat = Convert.ToInt32(cmbCategoria.SelectedItem.ToString().Split('-')[0].Trim());

            Producto p = new Producto();
            p.Codigo = txtCodigo.Text.Trim();
            p.Nombre = txtNombre.Text.Trim();
            p.IdCategoria = idCat;
            p.StockActual = Convert.ToInt32(txtStockActual.Text.Trim());
            p.StockMinimo = Convert.ToInt32(txtStockMinimo.Text.Trim());
            p.PrecioCompra = Convert.ToDecimal(txtPrecio.Text.Trim());
            p.FechaVencimiento = dtpVencimiento.Value.Date;
            p.UnidadesPorCaja = Convert.ToInt32(txtUnidadesCaja.Text.Trim());
            p.PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text.Trim());

            try
            {
                ProductoDatos.Agregar(p);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Código duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            BitacoraDatos.RegistrarAccion("Registro de producto", "Se agregó producto: " + p.Nombre + " (" + p.Codigo + ")");

            MessageBox.Show("Producto guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarProductos();
            if (!Recarga.SeleccionarFila(dgvProductos, p.IdProducto)) LimpiarCampos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idProductoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un producto para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos()) return;

            if (ProductoDatos.ExisteCodigo(txtCodigo.Text.Trim(), idProductoSeleccionado))
            {
                MessageBox.Show("El código " + txtCodigo.Text.Trim() + " ya pertenece a otro producto.\nEscriba un código que no esté en uso.", "Código duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return;
            }

            int idCat = Convert.ToInt32(cmbCategoria.SelectedItem.ToString().Split('-')[0].Trim());
            Producto? p = ProductoDatos.ObtenerPorId(idProductoSeleccionado);
            if (p != null)
            {
                p.Codigo = txtCodigo.Text.Trim();
                p.Nombre = txtNombre.Text.Trim();
                p.IdCategoria = idCat;
                p.StockMinimo = Convert.ToInt32(txtStockMinimo.Text.Trim());
                p.PrecioCompra = Convert.ToDecimal(txtPrecio.Text.Trim());
                p.FechaVencimiento = dtpVencimiento.Value.Date;
                p.UnidadesPorCaja = Convert.ToInt32(txtUnidadesCaja.Text.Trim());
                p.PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text.Trim());

                try
                {
                    ProductoDatos.Modificar(p);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Código duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                BitacoraDatos.RegistrarAccion("Modificación de producto", "Se modificó producto ID " + idProductoSeleccionado);

                MessageBox.Show("Producto actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProductos();
                if (!Recarga.SeleccionarFila(dgvProductos, p.IdProducto)) LimpiarCampos();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            dgvProductos.CurrentCell = null;
            dgvProductos.ClearSelection();
            idProductoSeleccionado = 0;
            txtCodigo.Clear();
            txtNombre.Clear();
            txtStockActual.Clear();
            txtStockActual.ReadOnly = false;
            txtStockMinimo.Clear();
            txtPrecio.Clear();
            txtUnidadesCaja.Text = "1";
            txtPrecioVenta.Text = "0.00";
            dtpVencimiento.Value = DateTime.Today.AddMonths(12);
            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("El código es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }
            int actual, min;
            if (!int.TryParse(txtStockActual.Text.Trim(), out actual) || actual < 0)
            {
                MessageBox.Show("El stock actual debe ser un entero mayor o igual a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStockActual.Focus();
                return false;
            }
            if (!int.TryParse(txtStockMinimo.Text.Trim(), out min) || min < 0)
            {
                MessageBox.Show("El stock mínimo debe ser un entero mayor o igual a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStockMinimo.Focus();
                return false;
            }
            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text.Trim(), out precio) || precio <= 0)
            {
                MessageBox.Show("El precio de compra debe ser un monto mayor a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return false;
            }
            int unidades;
            if (!int.TryParse(txtUnidadesCaja.Text.Trim(), out unidades) || unidades <= 0)
            {
                MessageBox.Show("Las unidades por caja deben ser un entero mayor a 0 (use 1 si el producto no viene en caja).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnidadesCaja.Focus();
                return false;
            }
            decimal precioVenta;
            if (!decimal.TryParse(txtPrecioVenta.Text.Trim(), out precioVenta) || precioVenta < 0)
            {
                MessageBox.Show("El precio de venta por unidad debe ser un monto mayor o igual a 0 (0 si el producto no se vende).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioVenta.Focus();
                return false;
            }
            return true;
        }
    }
}
