using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmGraficos : Form, IRecargable
    {
        public FrmGraficos()
        {
            InitializeComponent();
        }

        private void FrmGraficos_Load(object sender, EventArgs e)
        {
            // Botón de cerrar en el encabezado
            AgregarBotonCerrar();
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
            pnlGraficoCitas.Invalidate();
            pnlGraficoTratamientos.Invalidate();
            pnlGraficoStock.Invalidate();
        }

        private void tbcGraficos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Redibujamos los gráficos cuando el usuario cambia de pestaña
            pnlGraficoCitas.Invalidate();
            pnlGraficoTratamientos.Invalidate();
            pnlGraficoStock.Invalidate();
        }

        // Gráfico 1: Citas por Mes
        private void pnlGraficoCitas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Contamos cuántas citas hay en cada mes del año
            int[] citasPorMes = new int[12];
            foreach (var c in CitaDatos.citas)
            {
                int mes = c.Fecha.Month - 1;
                if (c.Fecha.Year == DateTime.Today.Year && mes >= 0 && mes < 12)
                {
                    citasPorMes[mes]++;
                }
            }

            string[] nombresMeses = { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };

            // Título del gráfico
            Font fontTitulo = new Font("Segoe UI", 12F, FontStyle.Bold);
            Brush brushTexto = new SolidBrush(Color.FromArgb(30, 41, 59));
            g.DrawString("Frecuencia de Citas por Mes (Año Actual)", fontTitulo, brushTexto, 30, 20);

            // Buscamos el valor más alto para calcular la escala de las barras
            int maxValor = 5;
            for (int i = 0; i < 12; i++)
            {
                if (citasPorMes[i] > maxValor) maxValor = citasPorMes[i];
            }

            int origenX = 60;
            int origenY = 380;
            int anchoBarra = 45;
            int espacio = 25;
            int altoMaximo = 280;

            // Líneas de los ejes X y Y
            Pen penEje = new Pen(Color.FromArgb(180, 200, 220), 2);
            g.DrawLine(penEje, origenX - 10, origenY, origenX + 12 * (anchoBarra + espacio), origenY);
            g.DrawLine(penEje, origenX - 10, origenY, origenX - 10, origenY - altoMaximo - 20);

            Font fontLabel = new Font("Segoe UI", 9F, FontStyle.Regular);
            Font fontValor = new Font("Segoe UI", 9F, FontStyle.Bold);
            Brush brushBarra = new SolidBrush(Color.FromArgb(108, 180, 234));
            Brush brushBarraConDatos = new SolidBrush(Color.FromArgb(61, 143, 203));

            // Dibujamos una barra por cada mes
            for (int i = 0; i < 12; i++)
            {
                int x = origenX + i * (anchoBarra + espacio);
                int altura = (int)((double)citasPorMes[i] / maxValor * altoMaximo);
                int y = origenY - altura;

                g.FillRectangle(citasPorMes[i] > 0 ? brushBarraConDatos : brushBarra, x, y, anchoBarra, altura);
                g.DrawRectangle(new Pen(Color.FromArgb(96, 165, 250), 1), x, y, anchoBarra, altura);

                // Mostramos el número sobre la barra
                g.DrawString(citasPorMes[i].ToString(), fontValor, brushTexto, x + 15, y - 20);

                // Nombre del mes abajo
                g.DrawString(nombresMeses[i], fontLabel, brushTexto, x + 8, origenY + 8);
            }
        }

        // Gráfico 2: Tratamientos por Estado
        private void pnlGraficoTratamientos_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Contamos los tratamientos según su estado
            int pendientes = 0, enProceso = 0, finalizados = 0, cancelados = 0;
            foreach (var t in TratamientoDatos.tratamientos)
            {
                if (t.Estado == "Pendiente") pendientes++;
                else if (t.Estado == "En proceso") enProceso++;
                else if (t.Estado == "Finalizado") finalizados++;
                else if (t.Estado == "Anulado") cancelados++;
            }

            Font fontTitulo = new Font("Segoe UI", 12F, FontStyle.Bold);
            Brush brushTexto = new SolidBrush(Color.FromArgb(30, 41, 59));
            g.DrawString("Distribución de Tratamientos por Estado", fontTitulo, brushTexto, 30, 20);

            string[] estados = { "Pendientes", "En Proceso", "Finalizados", "Anulados" };
            int[] valores = { pendientes, enProceso, finalizados, cancelados };
            Color[] colores = {
                Color.FromArgb(255, 183, 77),  // Ámbar
                Color.FromArgb(108, 180, 234), // Azul
                Color.FromArgb(102, 187, 106), // Verde
                Color.FromArgb(229, 115, 115)  // Rojo
            };

            int maxValor = 5;
            for (int i = 0; i < 4; i++)
            {
                if (valores[i] > maxValor) maxValor = valores[i];
            }

            int origenX = 100;
            int origenY = 380;
            int anchoBarra = 80;
            int espacio = 60;
            int altoMaximo = 280;

            // Ejes
            Pen penEje = new Pen(Color.FromArgb(180, 200, 220), 2);
            g.DrawLine(penEje, origenX - 20, origenY, origenX + 4 * (anchoBarra + espacio), origenY);
            g.DrawLine(penEje, origenX - 20, origenY, origenX - 20, origenY - altoMaximo - 20);

            Font fontLabel = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Font fontValor = new Font("Segoe UI", 11F, FontStyle.Bold);

            // Dibujamos las barras de cada estado
            for (int i = 0; i < 4; i++)
            {
                int x = origenX + i * (anchoBarra + espacio);
                int altura = (int)((double)valores[i] / maxValor * altoMaximo);
                int y = origenY - altura;

                Brush brushActual = new SolidBrush(colores[i]);
                g.FillRectangle(brushActual, x, y, anchoBarra, altura);
                g.DrawRectangle(new Pen(Color.FromArgb(80, 80, 80), 1), x, y, anchoBarra, altura);

                // Cantidad
                g.DrawString(valores[i].ToString(), fontValor, brushTexto, x + 30, y - 25);

                // Nombre del estado
                g.DrawString(estados[i], fontLabel, brushTexto, x, origenY + 10);
            }
        }

        // Gráfico 3: Productos Bajo Stock Mínimo
        private void pnlGraficoStock_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Font fontTitulo = new Font("Segoe UI", 12F, FontStyle.Bold);
            Brush brushTexto = new SolidBrush(Color.FromArgb(30, 41, 59));
            g.DrawString("Comparativa: Stock Actual vs Stock Mínimo Requerido", fontTitulo, brushTexto, 30, 20);

            var bajoStock = ProductoDatos.ProductosBajoStock();
            if (bajoStock.Count == 0)
            {
                g.DrawString("¡Excelente! No hay productos con stock por debajo del mínimo.", new Font("Segoe UI", 11F, FontStyle.Italic), new SolidBrush(Color.Green), 50, 100);
                return;
            }

            // Leyenda de colores
            g.FillRectangle(new SolidBrush(Color.FromArgb(229, 115, 115)), 30, 58, 20, 15);
            g.DrawString("Stock Actual", new Font("Segoe UI", 9F), brushTexto, 55, 56);
            g.FillRectangle(new SolidBrush(Color.FromArgb(61, 143, 203)), 170, 58, 20, 15);
            g.DrawString("Stock Mínimo", new Font("Segoe UI", 9F), brushTexto, 195, 56);

            int origenX = 80;
            int origenY = 380;
            int anchoBarra = 35;
            int espacioGrupo = 60;
            int altoMaximo = 260;

            int maxValor = 10;
            foreach (var p in bajoStock)
            {
                if (p.StockMinimo > maxValor) maxValor = p.StockMinimo;
                if (p.StockActual > maxValor) maxValor = p.StockActual;
            }

            Pen penEje = new Pen(Color.FromArgb(180, 200, 220), 2);
            g.DrawLine(penEje, origenX - 10, origenY, origenX + bajoStock.Count * (anchoBarra * 2 + espacioGrupo), origenY);
            g.DrawLine(penEje, origenX - 10, origenY, origenX - 10, origenY - altoMaximo - 20);

            Font fontLabel = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            Font fontValor = new Font("Segoe UI", 9F, FontStyle.Bold);

            for (int i = 0; i < bajoStock.Count; i++)
            {
                var prod = bajoStock[i];
                int xBase = origenX + i * (anchoBarra * 2 + espacioGrupo);

                // Barra Stock Actual (Rojo)
                int altActual = (int)((double)prod.StockActual / maxValor * altoMaximo);
                int yActual = origenY - altActual;
                g.FillRectangle(new SolidBrush(Color.FromArgb(229, 115, 115)), xBase, yActual, anchoBarra, altActual);
                g.DrawString(prod.StockActual.ToString(), fontValor, brushTexto, xBase + 8, yActual - 18);

                // Barra Stock Mínimo (Azul)
                int altMin = (int)((double)prod.StockMinimo / maxValor * altoMaximo);
                int yMin = origenY - altMin;
                g.FillRectangle(new SolidBrush(Color.FromArgb(61, 143, 203)), xBase + anchoBarra + 5, yMin, anchoBarra, altMin);
                g.DrawString(prod.StockMinimo.ToString(), fontValor, brushTexto, xBase + anchoBarra + 12, yMin - 18);

                // Código y nombre del producto abajo
                string etiqueta = prod.Codigo;
                float anchoEtiqueta = anchoBarra * 2 + espacioGrupo - 10;
                float xEtiqueta = xBase + anchoBarra + 2.5f - anchoEtiqueta / 2;
                StringFormat centrado = new StringFormat { Alignment = StringAlignment.Center };
                g.DrawString(etiqueta, fontLabel, brushTexto, new RectangleF(xEtiqueta, origenY + 10, anchoEtiqueta, 20), centrado);
                g.DrawString(prod.Nombre, new Font("Segoe UI", 7.5F), brushTexto, new RectangleF(xEtiqueta, origenY + 30, anchoEtiqueta, 45), centrado);
            }
        }
    }
}
