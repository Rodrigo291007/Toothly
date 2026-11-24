namespace Sistema_Toothly.Forms
{
    partial class FrmAlertasStock
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
            splAlertas = new SplitContainer();
            grpBajoStock = new GroupBox();
            dgvBajoStock = new DataGridView();
            grpPorVencer = new GroupBox();
            dgvPorVencer = new DataGridView();
            pnlEncabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splAlertas).BeginInit();
            splAlertas.Panel1.SuspendLayout();
            splAlertas.Panel2.SuspendLayout();
            splAlertas.SuspendLayout();
            grpBajoStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBajoStock).BeginInit();
            grpPorVencer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPorVencer).BeginInit();
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
            lblTitulo.Size = new Size(385, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Alertas de Inventario (Stock y Vencimiento)";
            // 
            // splAlertas
            // 
            splAlertas.Dock = DockStyle.Fill;
            splAlertas.Location = new Point(0, 50);
            splAlertas.Name = "splAlertas";
            splAlertas.Orientation = Orientation.Horizontal;
            // 
            // splAlertas.Panel1
            // 
            splAlertas.Panel1.Controls.Add(grpBajoStock);
            splAlertas.Panel1.Padding = new Padding(15);
            // 
            // splAlertas.Panel2
            // 
            splAlertas.Panel2.Controls.Add(grpPorVencer);
            splAlertas.Panel2.Padding = new Padding(15);
            splAlertas.Size = new Size(950, 550);
            splAlertas.SplitterDistance = 270;
            splAlertas.TabIndex = 1;
            // 
            // grpBajoStock
            // 
            grpBajoStock.Controls.Add(dgvBajoStock);
            grpBajoStock.Dock = DockStyle.Fill;
            grpBajoStock.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpBajoStock.ForeColor = Color.FromArgb(198, 40, 40);
            grpBajoStock.Location = new Point(15, 15);
            grpBajoStock.Name = "grpBajoStock";
            grpBajoStock.Size = new Size(920, 240);
            grpBajoStock.TabIndex = 0;
            grpBajoStock.TabStop = false;
            grpBajoStock.Text = "⚠️ Productos con Stock Bajo el Mínimo Requerido (Urgente)";
            // 
            // dgvBajoStock
            // 
            dgvBajoStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBajoStock.Dock = DockStyle.Fill;
            dgvBajoStock.Location = new Point(3, 21);
            dgvBajoStock.Name = "dgvBajoStock";
            dgvBajoStock.Size = new Size(914, 216);
            dgvBajoStock.TabIndex = 0;
            // 
            // grpPorVencer
            // 
            grpPorVencer.Controls.Add(dgvPorVencer);
            grpPorVencer.Dock = DockStyle.Fill;
            grpPorVencer.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpPorVencer.ForeColor = Color.FromArgb(239, 108, 0);
            grpPorVencer.Location = new Point(15, 15);
            grpPorVencer.Name = "grpPorVencer";
            grpPorVencer.Size = new Size(920, 246);
            grpPorVencer.TabIndex = 0;
            grpPorVencer.TabStop = false;
            grpPorVencer.Text = "⏰ Productos Próximos a Vencer (30 Días o Menos)";
            // 
            // dgvPorVencer
            // 
            dgvPorVencer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPorVencer.Dock = DockStyle.Fill;
            dgvPorVencer.Location = new Point(3, 21);
            dgvPorVencer.Name = "dgvPorVencer";
            dgvPorVencer.Size = new Size(914, 222);
            dgvPorVencer.TabIndex = 0;
            // 
            // FrmAlertasStock
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 249, 253);
            ClientSize = new Size(950, 600);
            Controls.Add(splAlertas);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmAlertasStock";
            Text = "Alertas de Inventario";
            Load += FrmAlertasStock_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            splAlertas.Panel1.ResumeLayout(false);
            splAlertas.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splAlertas).EndInit();
            splAlertas.ResumeLayout(false);
            grpBajoStock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBajoStock).EndInit();
            grpPorVencer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPorVencer).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEncabezado;
        private Label lblTitulo;
        private SplitContainer splAlertas;
        private GroupBox grpBajoStock;
        private DataGridView dgvBajoStock;
        private GroupBox grpPorVencer;
        private DataGridView dgvPorVencer;
    }
}
