using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmAlertasStock : Form, IRecargable
    {
        public FrmAlertasStock()
        {
            InitializeComponent();
        }

        private void FrmAlertasStock_Load(object sender, EventArgs e)
        {
            // Botón de cerrar en la esquina del encabezado
            AgregarBotonCerrar();

            // Configuramos los colores y bordes de las dos tablas
            EstilizarTabla(dgvBajoStock);
            EstilizarTabla(dgvPorVencer);

            ConfigurarColumnas();
            CargarAlertas();
        }

        public void Recargar()
        {
            CargarAlertas();
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

        // Le da estilo visual a la tabla
        private void EstilizarTabla(DataGridView dgv)
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
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ConfigurarColumnas()
        {
            // Grid Bajo Stock
            dgvBajoStock.Columns.Clear();
            dgvBajoStock.Columns.Add("Codigo", "Código");
            dgvBajoStock.Columns.Add("Nombre", "Producto");
            dgvBajoStock.Columns.Add("Categoria", "Categoría");
            dgvBajoStock.Columns.Add("StockActual", "Stock Actual");
            dgvBajoStock.Columns.Add("StockMinimo", "Stock Mínimo");
            dgvBajoStock.Columns.Add("Deficit", "Déficit");

            dgvBajoStock.Columns["Codigo"].Width = 100;
            dgvBajoStock.Columns["Nombre"].Width = 260;
            dgvBajoStock.Columns["Categoria"].Width = 160;
            dgvBajoStock.Columns["StockActual"].Width = 110;
            dgvBajoStock.Columns["StockMinimo"].Width = 110;
            dgvBajoStock.Columns["Deficit"].Width = 90;

            // Grid Por Vencer
            dgvPorVencer.Columns.Clear();
            dgvPorVencer.Columns.Add("Codigo", "Código");
            dgvPorVencer.Columns.Add("Nombre", "Producto");
            dgvPorVencer.Columns.Add("Categoria", "Categoría");
            dgvPorVencer.Columns.Add("StockActual", "Stock Actual");
            dgvPorVencer.Columns.Add("Vencimiento", "Fecha Vencimiento");
            dgvPorVencer.Columns.Add("DiasRestantes", "Días Restantes");

            dgvPorVencer.Columns["Codigo"].Width = 100;
            dgvPorVencer.Columns["Nombre"].Width = 260;
            dgvPorVencer.Columns["Categoria"].Width = 160;
            dgvPorVencer.Columns["StockActual"].Width = 110;
            dgvPorVencer.Columns["Vencimiento"].Width = 130;
            dgvPorVencer.Columns["DiasRestantes"].Width = 110;
        }

        private void CargarAlertas()
        {
            // 1. Productos bajo stock mínimo (filas en rojo suave)
            dgvBajoStock.Rows.Clear();
            var bajoStock = ProductoDatos.ProductosBajoStock();
            foreach (var p in bajoStock)
            {
                string cat = ProductoDatos.ObtenerNombreCategoria(p.IdCategoria);
                int deficit = p.StockMinimo - p.StockActual;
                int rowIdx = dgvBajoStock.Rows.Add(p.Codigo, p.Nombre, cat, p.StockActual, p.StockMinimo, deficit);

                // Coloreamos la fila en rojo suave para que resalte la alerta
                dgvBajoStock.Rows[rowIdx].DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 238);
                dgvBajoStock.Rows[rowIdx].DefaultCellStyle.ForeColor = Color.FromArgb(183, 28, 28);
            }

            // 2. Productos por vencer en menos de 30 días (filas en ámbar suave)
            dgvPorVencer.Rows.Clear();
            var porVencer = ProductoDatos.ProductosPorVencer();
            foreach (var p in porVencer)
            {
                string cat = ProductoDatos.ObtenerNombreCategoria(p.IdCategoria);
                int dias = (p.FechaVencimiento.Date - DateTime.Today.Date).Days;
                string textoDias = dias < 0 ? "¡VENCIDO!" : dias + " días";
                int rowIdx = dgvPorVencer.Rows.Add(p.Codigo, p.Nombre, cat, p.StockActual, p.FechaVencimiento.ToString("dd/MM/yyyy"), textoDias);

                // Coloreamos la fila en ámbar suave para advertir el vencimiento
                dgvPorVencer.Rows[rowIdx].DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 225);
                dgvPorVencer.Rows[rowIdx].DefaultCellStyle.ForeColor = Color.FromArgb(230, 81, 0);
            }
        }
    }
}
