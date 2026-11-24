using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmReportes : Form, IRecargable
    {
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string ReporteInicial { get; set; } = "";

        public FrmReportes()
        {
            InitializeComponent();
        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {
            // Botón de cerrar en el encabezado
            AgregarBotonCerrar();

            // Colores de los botones
            btnImprimir.Image = Properties.Resources.ico_imprimir;

            // Formato de la tabla
            ConfigurarEstiloTabla();

            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today.AddDays(30);

            CargarTiposReportePorRol();

            if (!string.IsNullOrEmpty(ReporteInicial))
            {
                SeleccionarReporte(ReporteInicial);
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

        // Colores y diseño de la tabla del reporte
        private void ConfigurarEstiloTabla()
        {
            dgvReporte.BackgroundColor = Color.White;
            dgvReporte.BorderStyle = BorderStyle.None;
            dgvReporte.EnableHeadersVisualStyles = false;
            dgvReporte.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(108, 180, 234);
            dgvReporte.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReporte.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvReporte.ColumnHeadersHeight = 35;
            dgvReporte.DefaultCellStyle.SelectionBackColor = Color.FromArgb(186, 230, 253);
            dgvReporte.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvReporte.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 254);
            dgvReporte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReporte.ReadOnly = true;
            dgvReporte.RowHeadersVisible = false;
            dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void SeleccionarReporte(string nombre)
        {
            for (int i = 0; i < cmbTipoReporte.Items.Count; i++)
            {
                if (cmbTipoReporte.Items[i].ToString().ToLower().Contains(nombre.ToLower()))
                {
                    cmbTipoReporte.SelectedIndex = i;
                    GenerarReporte();
                    break;
                }
            }
        }

        private void CargarTiposReportePorRol()
        {
            cmbTipoReporte.Items.Clear();
            string rol = Sesion.NombreRol;

            // Según ANEXO D
            if (rol == "Administrador")
            {
                cmbTipoReporte.Items.Add("Pacientes");
                cmbTipoReporte.Items.Add("Citas");
                cmbTipoReporte.Items.Add("Tratamientos");
                cmbTipoReporte.Items.Add("Recetas");
                cmbTipoReporte.Items.Add("Despachos");
                cmbTipoReporte.Items.Add("Estados de cuenta");
                cmbTipoReporte.Items.Add("Inventario");
                cmbTipoReporte.Items.Add("Proveedores");
                cmbTipoReporte.Items.Add("Órdenes de compra");
            }
            else if (rol == "Recepcionista")
            {
                cmbTipoReporte.Items.Add("Citas");
                cmbTipoReporte.Items.Add("Estados de cuenta");
                cmbTipoReporte.Items.Add("Despachos");
            }
            else if (rol == "Odontólogo")
            {
                cmbTipoReporte.Items.Add("Tratamientos");
                cmbTipoReporte.Items.Add("Recetas");
            }
            else if (rol == "Asistente Odontológico")
            {
                cmbTipoReporte.Items.Add("Inventario");
                cmbTipoReporte.Items.Add("Órdenes de compra");
                cmbTipoReporte.Items.Add("Despachos");
            }
            else
            {
                // Todos por defecto
                cmbTipoReporte.Items.Add("Pacientes");
                cmbTipoReporte.Items.Add("Citas");
                cmbTipoReporte.Items.Add("Tratamientos");
            }

            if (cmbTipoReporte.Items.Count > 0)
            {
                cmbTipoReporte.SelectedIndex = 0;
                GenerarReporte();
            }
        }

        private void cmbTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerarReporte();
        }

        private void Filtros_Changed(object sender, EventArgs e)
        {
            GenerarReporte();
        }

        private bool EnRango(DateTime fecha)
        {
            return fecha.Date >= dtpDesde.Value.Date && fecha.Date <= dtpHasta.Value.Date;
        }

        public void Recargar()
        {
            GenerarReporte();
        }

        private void GenerarReporte()
        {
            if (cmbTipoReporte.SelectedIndex < 0) return;
            string tipo = cmbTipoReporte.SelectedItem.ToString();
            string filtro = txtFiltroTexto.Text.Trim().ToLower();

            bool usaFechas = tipo == "Citas" || tipo == "Tratamientos" || tipo == "Recetas" || tipo == "Órdenes de compra" || tipo == "Despachos";
            dtpDesde.Enabled = usaFechas;
            dtpHasta.Enabled = usaFechas;

            dgvReporte.Columns.Clear();
            dgvReporte.Rows.Clear();
            dgvReporte.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            if (tipo == "Pacientes")
            {
                dgvReporte.Columns.Add("Id", "ID");
                dgvReporte.Columns.Add("Nombres", "Nombres");
                dgvReporte.Columns.Add("Apellidos", "Apellidos");
                dgvReporte.Columns.Add("Dui", "DUI");
                dgvReporte.Columns.Add("Telefono", "Teléfono");
                dgvReporte.Columns.Add("Estado", "Estado");
                dgvReporte.Columns.Add("FechaRegistro", "Fecha Registro");

                foreach (var p in PacienteDatos.pacientes)
                {
                    if (string.IsNullOrEmpty(filtro) || p.Nombres.ToLower().Contains(filtro) || p.Apellidos.ToLower().Contains(filtro) || p.Dui.Contains(filtro))
                    {
                        string est = p.Estado == "A" ? "Activo" : "Inactivo";
                        dgvReporte.Rows.Add(p.IdPaciente, p.Nombres, p.Apellidos, p.Dui, p.Telefono, est, p.FechaRegistro.ToString("dd/MM/yyyy"));
                    }
                }
            }
            else if (tipo == "Citas")
            {
                dgvReporte.Columns.Add("Id", "ID");
                dgvReporte.Columns.Add("Fecha", "Fecha");
                dgvReporte.Columns.Add("Hora", "Hora");
                dgvReporte.Columns.Add("Paciente", "Paciente");
                dgvReporte.Columns.Add("Odontologo", "Odontólogo");
                dgvReporte.Columns.Add("Motivo", "Motivo");
                dgvReporte.Columns.Add("Estado", "Estado");

                foreach (var c in CitaDatos.citas)
                {
                    string pac = PacienteDatos.ObtenerNombre(c.IdPaciente);
                    string odonto = "Dr. Asignado";
                    foreach (var u in UsuarioDatos.usuarios)
                    {
                        if (u.IdUsuario == c.IdOdontologo) { odonto = u.NombreCompleto; break; }
                    }

                    if (EnRango(c.Fecha) && (string.IsNullOrEmpty(filtro) || pac.ToLower().Contains(filtro) || odonto.ToLower().Contains(filtro)))
                    {
                        dgvReporte.Rows.Add(c.IdCita, c.Fecha.ToString("dd/MM/yyyy"), c.Hora, pac, odonto, c.Motivo, c.Estado);
                    }
                }
            }
            else if (tipo == "Tratamientos")
            {
                dgvReporte.Columns.Add("Id", "ID");
                dgvReporte.Columns.Add("Paciente", "Paciente");
                dgvReporte.Columns.Add("Odontologo", "Odontólogo");
                dgvReporte.Columns.Add("Descripcion", "Descripción");
                dgvReporte.Columns.Add("Inicio", "Inicio");
                dgvReporte.Columns.Add("Fin", "Fin");
                dgvReporte.Columns.Add("Costo", "Costo ($)");
                dgvReporte.Columns.Add("Estado", "Estado");

                foreach (var t in TratamientoDatos.tratamientos)
                {
                    string pac = PacienteDatos.ObtenerNombre(t.IdPaciente);
                    string odonto = "Dr. Asignado";
                    foreach (var u in UsuarioDatos.usuarios)
                    {
                        if (u.IdUsuario == t.IdOdontologo) { odonto = u.NombreCompleto; break; }
                    }

                    if (EnRango(t.FechaInicio) && (string.IsNullOrEmpty(filtro) || pac.ToLower().Contains(filtro) || t.Descripcion.ToLower().Contains(filtro)))
                    {
                        dgvReporte.Rows.Add(t.IdTratamiento, pac, odonto, t.Descripcion, t.FechaInicio.ToString("dd/MM/yyyy"), t.FechaFin.ToString("dd/MM/yyyy"), "$" + t.Costo.ToString("F2"), t.Estado);
                    }
                }
            }
            else if (tipo == "Recetas")
            {
                dgvReporte.Columns.Add("Id", "ID");
                dgvReporte.Columns.Add("Paciente", "Paciente");
                dgvReporte.Columns.Add("Fecha", "Fecha");
                dgvReporte.Columns.Add("Indicaciones", "Indicaciones");
                dgvReporte.Columns.Add("Medicamentos", "Medicamentos Prescritos");

                foreach (var r in RecetaDatos.recetas)
                {
                    string pac = PacienteDatos.ObtenerNombre(r.IdPaciente);
                    var meds = RecetaDatos.ObtenerDetalles(r.IdReceta);
                    string resumenMeds = "";
                    foreach (var m in meds)
                    {
                        // Un medicamento por línea: el PDF ajusta el alto de la fila al contenido
                        if (resumenMeds.Length > 0) resumenMeds += "\n";
                        resumenMeds += "- " + m.Medicamento + (m.Cantidad > 0 ? " x" + m.Cantidad : "") + " (" + m.Dosis + " - " + m.Frecuencia + " - " + m.Duracion + ")";
                    }

                    if (EnRango(r.Fecha) && (string.IsNullOrEmpty(filtro) || pac.ToLower().Contains(filtro)))
                    {
                        dgvReporte.Rows.Add(r.IdReceta, pac, r.Fecha.ToString("dd/MM/yyyy"), r.Indicaciones, resumenMeds);
                    }
                }
                // En pantalla también se ven todas las líneas de medicamentos
                dgvReporte.Columns["Medicamentos"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvReporte.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
            else if (tipo == "Estados de cuenta")
            {
                dgvReporte.Columns.Add("Id", "ID");
                dgvReporte.Columns.Add("Paciente", "Paciente");
                dgvReporte.Columns.Add("Dui", "DUI");
                dgvReporte.Columns.Add("SaldoActual", "Saldo Actual ($)");

                foreach (var ec in EstadoCuentaDatos.estados)
                {
                    Paciente? p = PacienteDatos.ObtenerPorId(ec.IdPaciente);
                    string pac = p != null ? p.Nombres + " " + p.Apellidos : "Desconocido";
                    string dui = p != null ? p.Dui : "--";

                    if (string.IsNullOrEmpty(filtro) || pac.ToLower().Contains(filtro))
                    {
                        dgvReporte.Rows.Add(ec.IdEstado, pac, dui, EstadoCuentaDatos.FormatearSaldo(ec.SaldoActual));
                    }
                }
            }
            else if (tipo == "Despachos")
            {
                // Ventas de medicamentos realizadas desde el módulo Despacho (lo que el paciente compró)
                dgvReporte.Columns.Add("Fecha", "Fecha");
                dgvReporte.Columns.Add("Paciente", "Paciente");
                dgvReporte.Columns.Add("Receta", "Receta");
                dgvReporte.Columns.Add("Producto", "Producto");
                dgvReporte.Columns.Add("Cantidad", "Cant.");
                dgvReporte.Columns.Add("PrecioUnd", "Precio Und.");
                dgvReporte.Columns.Add("Total", "Total ($)");

                decimal granTotal = 0;
                int unidades = 0;
                foreach (var venta in EstadoCuentaDatos.ListarVentasDespacho())
                {
                    if (!EnRango(venta.Movimiento.Fecha)) continue;

                    string pac = PacienteDatos.ObtenerNombre(venta.IdPaciente);
                    string producto, receta;
                    int cantidad;
                    InterpretarConceptoVenta(venta.Movimiento.Concepto, out producto, out cantidad, out receta);

                    if (!string.IsNullOrEmpty(filtro) && !pac.ToLower().Contains(filtro) && !producto.ToLower().Contains(filtro)) continue;

                    decimal precioUnd = cantidad > 0 ? venta.Movimiento.Monto / cantidad : venta.Movimiento.Monto;
                    dgvReporte.Rows.Add(venta.Movimiento.Fecha.ToString("dd/MM/yyyy HH:mm"), pac, receta, producto,
                        cantidad > 0 ? cantidad.ToString() : "-", "$" + precioUnd.ToString("F2"), "$" + venta.Movimiento.Monto.ToString("F2"));
                    granTotal += venta.Movimiento.Monto;
                    unidades += cantidad;
                }

                if (dgvReporte.Rows.Count > 0)
                {
                    dgvReporte.Rows.Add("", "TOTAL VENDIDO", "", "", unidades.ToString(), "", "$" + granTotal.ToString("F2"));
                    DataGridViewRow filaTotal = dgvReporte.Rows[dgvReporte.Rows.Count - 1];
                    filaTotal.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }
            else if (tipo == "Inventario")
            {
                dgvReporte.Columns.Add("Codigo", "Código");
                dgvReporte.Columns.Add("Nombre", "Producto");
                dgvReporte.Columns.Add("Categoria", "Categoría");
                dgvReporte.Columns.Add("StockActual", "Stock (und)");
                dgvReporte.Columns.Add("StockMinimo", "Stock Mínimo");
                dgvReporte.Columns.Add("Precio", "Precio Caja");
                dgvReporte.Columns.Add("UnidadesCaja", "Und/Caja");
                dgvReporte.Columns.Add("PrecioVenta", "Venta Und.");
                dgvReporte.Columns.Add("Vencimiento", "Vencimiento");

                foreach (var prod in ProductoDatos.productos)
                {
                    string cat = ProductoDatos.ObtenerNombreCategoria(prod.IdCategoria);
                    if (string.IsNullOrEmpty(filtro) || prod.Nombre.ToLower().Contains(filtro) || prod.Codigo.ToLower().Contains(filtro))
                    {
                        dgvReporte.Rows.Add(prod.Codigo, prod.Nombre, cat, prod.StockActual, prod.StockMinimo, "$" + prod.PrecioCompra.ToString("F2"), prod.UnidadesPorCaja, "$" + prod.PrecioVenta.ToString("F2"), prod.FechaVencimiento.ToString("dd/MM/yyyy"));
                    }
                }
            }
            else if (tipo == "Proveedores")
            {
                dgvReporte.Columns.Add("Id", "ID");
                dgvReporte.Columns.Add("Nombre", "Proveedor");
                dgvReporte.Columns.Add("Telefono", "Teléfono");
                dgvReporte.Columns.Add("Correo", "Correo");
                dgvReporte.Columns.Add("Direccion", "Dirección");
                dgvReporte.Columns.Add("Estado", "Estado");

                foreach (var prov in ProveedorDatos.proveedores)
                {
                    string est = prov.Estado == "A" ? "Activo" : "Inactivo";
                    if (string.IsNullOrEmpty(filtro) || prov.Nombre.ToLower().Contains(filtro))
                    {
                        dgvReporte.Rows.Add(prov.IdProveedor, prov.Nombre, prov.Telefono, prov.Correo, prov.Direccion, est);
                    }
                }
            }
            else if (tipo == "Órdenes de compra")
            {
                dgvReporte.Columns.Add("Id", "ID");
                dgvReporte.Columns.Add("Proveedor", "Proveedor");
                dgvReporte.Columns.Add("Fecha", "Fecha");
                dgvReporte.Columns.Add("Total", "Total ($)");
                dgvReporte.Columns.Add("Estado", "Estado");

                foreach (var o in OrdenCompraDatos.ordenes)
                {
                    string prov = ProveedorDatos.ObtenerNombre(o.IdProveedor);
                    if (EnRango(o.Fecha) && (string.IsNullOrEmpty(filtro) || prov.ToLower().Contains(filtro)))
                    {
                        dgvReporte.Rows.Add(o.IdOrden, prov, o.Fecha.ToString("dd/MM/yyyy"), "$" + o.Total.ToString("F2"), o.Estado);
                    }
                }
            }
        }

        // El despacho guarda el cargo como: "Venta: Producto (COD) x 3 und (Receta #5)"
        private static void InterpretarConceptoVenta(string concepto, out string producto, out int cantidad, out string receta)
        {
            producto = concepto.StartsWith("Venta: ") ? concepto.Substring(7) : concepto;
            cantidad = 0;
            receta = "-";

            int posReceta = producto.LastIndexOf(" (Receta ");
            if (posReceta >= 0)
            {
                receta = producto.Substring(posReceta + 9).TrimEnd(')').Trim();
                producto = producto.Substring(0, posReceta);
            }

            int posCant = producto.LastIndexOf(" x ");
            if (posCant >= 0)
            {
                string textoCant = producto.Substring(posCant + 3).Replace("und", "").Trim();
                if (int.TryParse(textoCant, out int n))
                {
                    cantidad = n;
                    producto = producto.Substring(0, posCant);
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (!Permisos.Verificar(Permisos.Reportes)) return;
            string tipo = (cmbTipoReporte.SelectedItem != null) ? cmbTipoReporte.SelectedItem.ToString()! : "Informe";
            GeneradorPDF.GenerarDesdeGrid(tipo, dgvReporte);
        }
    }
}
