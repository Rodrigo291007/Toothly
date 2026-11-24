namespace Sistema_Toothly.Forms
{
    partial class FrmGraficos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlEncabezado = new Panel();
            lblTitulo = new Label();
            tbcGraficos = new TabControl();
            tbpCitas = new TabPage();
            pnlGraficoCitas = new Panel();
            tbpTratamientos = new TabPage();
            pnlGraficoTratamientos = new Panel();
            tbpStock = new TabPage();
            pnlGraficoStock = new Panel();
            pnlEncabezado.SuspendLayout();
            tbcGraficos.SuspendLayout();
            tbpCitas.SuspendLayout();
            tbpTratamientos.SuspendLayout();
            tbpStock.SuspendLayout();
            SuspendLayout();
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.BackColor = Color.FromArgb(108, 180, 234);
            pnlEncabezado.Controls.Add(lblTitulo);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(950, 50);
            pnlEncabezado.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(20, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(198, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gráficos Estadísticos";
            // 
            // tbcGraficos
            // 
            tbcGraficos.Controls.Add(tbpCitas);
            tbcGraficos.Controls.Add(tbpTratamientos);
            tbcGraficos.Controls.Add(tbpStock);
            tbcGraficos.Dock = DockStyle.Fill;
            tbcGraficos.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            tbcGraficos.Location = new Point(0, 50);
            tbcGraficos.Name = "tbcGraficos";
            tbcGraficos.SelectedIndex = 0;
            tbcGraficos.Size = new Size(950, 550);
            tbcGraficos.TabIndex = 1;
            tbcGraficos.SelectedIndexChanged += tbcGraficos_SelectedIndexChanged;
            // 
            // tbpCitas
            // 
            tbpCitas.Controls.Add(pnlGraficoCitas);
            tbpCitas.Location = new Point(4, 26);
            tbpCitas.Name = "tbpCitas";
            tbpCitas.Padding = new Padding(15);
            tbpCitas.Size = new Size(942, 520);
            tbpCitas.TabIndex = 0;
            tbpCitas.Text = "📅 Citas por Mes";
            tbpCitas.UseVisualStyleBackColor = true;
            // 
            // pnlGraficoCitas
            // 
            pnlGraficoCitas.BackColor = Color.White;
            pnlGraficoCitas.BorderStyle = BorderStyle.FixedSingle;
            pnlGraficoCitas.Dock = DockStyle.Fill;
            pnlGraficoCitas.Location = new Point(15, 15);
            pnlGraficoCitas.Name = "pnlGraficoCitas";
            pnlGraficoCitas.Size = new Size(912, 490);
            pnlGraficoCitas.TabIndex = 0;
            pnlGraficoCitas.Paint += pnlGraficoCitas_Paint;
            // 
            // tbpTratamientos
            // 
            tbpTratamientos.Controls.Add(pnlGraficoTratamientos);
            tbpTratamientos.Location = new Point(4, 26);
            tbpTratamientos.Name = "tbpTratamientos";
            tbpTratamientos.Padding = new Padding(15);
            tbpTratamientos.Size = new Size(942, 520);
            tbpTratamientos.TabIndex = 1;
            tbpTratamientos.Text = "🩺 Tratamientos por Estado";
            tbpTratamientos.UseVisualStyleBackColor = true;
            // 
            // pnlGraficoTratamientos
            // 
            pnlGraficoTratamientos.BackColor = Color.White;
            pnlGraficoTratamientos.BorderStyle = BorderStyle.FixedSingle;
            pnlGraficoTratamientos.Dock = DockStyle.Fill;
            pnlGraficoTratamientos.Location = new Point(15, 15);
            pnlGraficoTratamientos.Name = "pnlGraficoTratamientos";
            pnlGraficoTratamientos.Size = new Size(912, 490);
            pnlGraficoTratamientos.TabIndex = 0;
            pnlGraficoTratamientos.Paint += pnlGraficoTratamientos_Paint;
            // 
            // tbpStock
            // 
            tbpStock.Controls.Add(pnlGraficoStock);
            tbpStock.Location = new Point(4, 26);
            tbpStock.Name = "tbpStock";
            tbpStock.Padding = new Padding(15);
            tbpStock.Size = new Size(942, 520);
            tbpStock.TabIndex = 2;
            tbpStock.Text = "📦 Productos Bajo Stock Mínimo";
            tbpStock.UseVisualStyleBackColor = true;
            // 
            // pnlGraficoStock
            // 
            pnlGraficoStock.BackColor = Color.White;
            pnlGraficoStock.BorderStyle = BorderStyle.FixedSingle;
            pnlGraficoStock.Dock = DockStyle.Fill;
            pnlGraficoStock.Location = new Point(15, 15);
            pnlGraficoStock.Name = "pnlGraficoStock";
            pnlGraficoStock.Size = new Size(912, 490);
            pnlGraficoStock.TabIndex = 0;
            pnlGraficoStock.Paint += pnlGraficoStock_Paint;
            // 
            // FrmGraficos
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 249, 253);
            ClientSize = new Size(950, 600);
            Controls.Add(tbcGraficos);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmGraficos";
            Text = "Gráficos Estadísticos";
            Load += FrmGraficos_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            tbcGraficos.ResumeLayout(false);
            tbpCitas.ResumeLayout(false);
            tbpTratamientos.ResumeLayout(false);
            tbpStock.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEncabezado;
        private Label lblTitulo;
        private TabControl tbcGraficos;
        private TabPage tbpCitas;
        private Panel pnlGraficoCitas;
        private TabPage tbpTratamientos;
        private Panel pnlGraficoTratamientos;
        private TabPage tbpStock;
        private Panel pnlGraficoStock;
    }
}
