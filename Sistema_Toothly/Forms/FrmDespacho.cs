using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmDespacho : Form, IRecargable
    {
        public FrmDespacho()
        {
            InitializeComponent();
        }

        private void FrmDespacho_Load(object sender, EventArgs e)
        {
            AgregarBotonCerrar();
            btnDespachar.Image = Properties.Resources.ico_guardar;
            ConfigurarEstiloTabla();
            ConfigurarColumnas();
            LlenarComboPacientes();
        }

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

        private void ConfigurarEstiloTabla()
        {
            dgvPendientes.BackgroundColor = Color.White;
            dgvPendientes.BorderStyle = BorderStyle.None;
            dgvPendientes.EnableHeadersVisualStyles = false;
            dgvPendientes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(108, 180, 234);
            dgvPendientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPendientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvPendientes.ColumnHeadersHeight = 35;
            dgvPendientes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(186, 230, 253);
            dgvPendientes.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvPendientes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 254);
            dgvPendientes.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvPendientes.RowHeadersVisible = false;
            dgvPendientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ConfigurarColumnas()
        {
            dgvPendientes.Columns.Clear();
            dgvPendientes.Columns.Add("IdDetalle", "ID");
            dgvPendientes.Columns.Add("Receta", "Receta");
            dgvPendientes.Columns.Add("Producto", "Producto");
            dgvPendientes.Columns.Add("Recetado", "Recetado");
            dgvPendientes.Columns.Add("Despachado", "Ya llevó");
            dgvPendientes.Columns.Add("Pendiente", "Pendiente");
            dgvPendientes.Columns.Add("Stock", "Stock");
            dgvPendientes.Columns.Add("Precio", "Precio Und.");
            dgvPendientes.Columns.Add("ADespachar", "A despachar");
            dgvPendientes.Columns.Add("Subtotal", "Subtotal");

            dgvPendientes.Columns["IdDetalle"].Visible = false;
            dgvPendientes.Columns["Receta"].Width = 70;
            dgvPendientes.Columns["Producto"].Width = 260;
            dgvPendientes.Columns["Recetado"].Width = 80;
            dgvPendientes.Columns["Despachado"].Width = 80;
            dgvPendientes.Columns["Pendiente"].Width = 80;
            dgvPendientes.Columns["Stock"].Width = 70;
            dgvPendientes.Columns["Precio"].Width = 90;
            dgvPendientes.Columns["ADespachar"].Width = 100;
            dgvPendientes.Columns["Subtotal"].Width = 100;

            foreach (DataGridViewColumn col in dgvPendientes.Columns)
            {
                col.ReadOnly = col.Name != "ADespachar";
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgvPendientes.Columns["ADespachar"].DefaultCellStyle.BackColor = Color.FromArgb(255, 251, 230);
            dgvPendientes.Columns["ADespachar"].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
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
            if (cmbPaciente.Items.Count > 0) cmbPaciente.SelectedIndex = 0;
        }

        public void Recargar()
        {
            Recarga.Combos(LlenarComboPacientes, cmbPaciente);
            CargarPendientes();
        }

        private void cmbPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPendientes();
        }

        private int ObtenerIdPaciente()
        {
            if (cmbPaciente.SelectedIndex < 0) return 0;
            return Convert.ToInt32(cmbPaciente.SelectedItem.ToString().Split('-')[0].Trim());
        }

        private void CargarPendientes()
        {
            dgvPendientes.Rows.Clear();
            int idPaciente = ObtenerIdPaciente();
            if (idPaciente == 0)
            {
                ActualizarTotal();
                return;
            }

            foreach (var d in RecetaDatos.ListarPendientesDespacho(idPaciente))
            {
                Producto? prod = ProductoDatos.ObtenerPorId(d.IdProducto!.Value);
                if (prod == null) continue;

                int pendiente = d.Cantidad - d.CantidadDespachada;
                int sugerido = Math.Min(pendiente, prod.StockActual);
                dgvPendientes.Rows.Add(d.IdDetalle, "#" + d.IdReceta, prod.Nombre + " (" + prod.Codigo + ")", d.Cantidad, d.CantidadDespachada, pendiente, prod.StockActual, prod.PrecioVenta, sugerido, sugerido * prod.PrecioVenta);
            }

            foreach (DataGridViewRow fila in dgvPendientes.Rows)
            {
                fila.Cells["Precio"].Style.Format = "$0.00";
                fila.Cells["Subtotal"].Style.Format = "$0.00";
                if (Convert.ToInt32(fila.Cells["Stock"].Value) < Convert.ToInt32(fila.Cells["Pendiente"].Value))
                {
                    fila.Cells["Stock"].Style.ForeColor = Color.FromArgb(198, 40, 40);
                }
            }

            ActualizarTotal();
        }

        private void dgvPendientes_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvPendientes.Columns[e.ColumnIndex].Name != "ADespachar") return;

            DataGridViewRow fila = dgvPendientes.Rows[e.RowIndex];
            int maximo = Math.Min(Convert.ToInt32(fila.Cells["Pendiente"].Value), Convert.ToInt32(fila.Cells["Stock"].Value));
            int cantidad;
            if (!int.TryParse(Convert.ToString(e.FormattedValue), out cantidad) || cantidad < 0 || cantidad > maximo)
            {
                MessageBox.Show("Escriba un número de unidades entre 0 y " + maximo + ".", "Cantidad no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void dgvPendientes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow fila = dgvPendientes.Rows[e.RowIndex];
            int cantidad = Convert.ToInt32(fila.Cells["ADespachar"].Value);
            decimal precio = Convert.ToDecimal(fila.Cells["Precio"].Value);
            fila.Cells["ADespachar"].Value = cantidad;
            fila.Cells["Subtotal"].Value = cantidad * precio;
            ActualizarTotal();
        }

        private decimal ActualizarTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow fila in dgvPendientes.Rows)
            {
                total += Convert.ToDecimal(fila.Cells["Subtotal"].Value);
            }
            lblTotal.Text = "Total: $" + total.ToString("F2");
            btnDespachar.Enabled = dgvPendientes.Rows.Count > 0;
            return total;
        }

        private void btnDespachar_Click(object sender, EventArgs e)
        {
            dgvPendientes.EndEdit();
            int idPaciente = ObtenerIdPaciente();
            if (idPaciente == 0) return;

            decimal total = ActualizarTotal();
            int lineas = 0;
            foreach (DataGridViewRow fila in dgvPendientes.Rows)
            {
                if (Convert.ToInt32(fila.Cells["ADespachar"].Value) > 0) lineas++;
            }

            if (lineas == 0)
            {
                MessageBox.Show("No hay unidades para despachar. Si el paciente no compra nada en la clínica, solo entréguele la receta impresa.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string mensaje = "Se despacharán " + lineas + " producto(s) por un total de $" + total.ToString("F2") + ".\nSe descontará del inventario y se cargará al estado de cuenta del paciente.";
            if (chkPagaAhora.Checked) mensaje += "\nTambién se registrará el pago (abono).";
            if (!Mensaje.Confirmar(mensaje + "\n\n¿Continuar?", "Confirmar despacho")) return;

            EstadoCuenta? ec = EstadoCuentaDatos.ObtenerPorPaciente(idPaciente);
            if (ec == null)
            {
                ec = EstadoCuentaDatos.CrearEstado(idPaciente);
            }

            decimal totalCobrado = 0;
            List<string> sinStock = new List<string>();
            foreach (DataGridViewRow fila in dgvPendientes.Rows)
            {
                int cantidad = Convert.ToInt32(fila.Cells["ADespachar"].Value);
                if (cantidad <= 0) continue;

                int idDetalle = Convert.ToInt32(fila.Cells["IdDetalle"].Value);
                string producto = Convert.ToString(fila.Cells["Producto"].Value) ?? "";
                string receta = Convert.ToString(fila.Cells["Receta"].Value) ?? "";
                decimal precio = Convert.ToDecimal(fila.Cells["Precio"].Value);

                RecetaDetalle? detalle = null;
                foreach (var d in RecetaDatos.ListarPendientesDespacho(idPaciente))
                {
                    if (d.IdDetalle == idDetalle)
                    {
                        detalle = d;
                        break;
                    }
                }
                if (detalle == null) continue;

                MovimientoInventario m = new MovimientoInventario();
                m.IdProducto = detalle.IdProducto!.Value;
                m.Tipo = "Salida";
                m.Cantidad = cantidad;
                m.Fecha = DateTime.Now;
                m.Descripcion = "Venta por receta " + receta + " - " + PacienteDatos.ObtenerNombre(idPaciente);

                if (!ProductoDatos.AgregarMovimiento(m))
                {
                    sinStock.Add(producto);
                    continue;
                }

                RecetaDatos.RegistrarDespacho(idDetalle, cantidad);

                MovimientoCuenta cargo = new MovimientoCuenta();
                cargo.IdEstado = ec.IdEstado;
                cargo.Fecha = DateTime.Now;
                cargo.Tipo = "Cargo";
                cargo.Concepto = "Venta: " + producto + " x " + cantidad + " und (Receta " + receta + ")";
                cargo.Monto = cantidad * precio;
                if (cargo.Monto > 0)
                {
                    EstadoCuentaDatos.AgregarMovimiento(cargo);
                    totalCobrado += cargo.Monto;
                }
            }

            // El pago no puede superar el saldo pendiente (si el paciente tenía saldo a favor, se descuenta)
            decimal montoPago = Math.Min(totalCobrado, Math.Max(EstadoCuentaDatos.ObtenerSaldo(ec.IdEstado), 0));
            if (chkPagaAhora.Checked && montoPago > 0)
            {
                MovimientoCuenta abono = new MovimientoCuenta();
                abono.IdEstado = ec.IdEstado;
                abono.Fecha = DateTime.Now;
                abono.Tipo = "Abono";
                abono.Concepto = "Pago de medicamentos despachados";
                abono.Monto = montoPago;
                EstadoCuentaDatos.AgregarMovimiento(abono);
            }

            BitacoraDatos.RegistrarAccion("Despacho de receta", "Se despacharon productos por $" + totalCobrado.ToString("F2") + " al paciente ID " + idPaciente);

            string resultado = "Despacho registrado por $" + totalCobrado.ToString("F2") + ".";
            if (!chkPagaAhora.Checked && totalCobrado > 0) resultado += "\nEl monto quedó pendiente en el estado de cuenta del paciente.";
            if (sinStock.Count > 0) resultado += "\n\nNo se despachó por falta de stock:\n- " + string.Join("\n- ", sinStock);
            MessageBox.Show(resultado, "Despacho", MessageBoxButtons.OK, sinStock.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

            CargarPendientes();
        }
    }
}
