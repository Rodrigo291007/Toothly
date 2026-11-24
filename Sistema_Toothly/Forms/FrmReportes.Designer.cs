namespace Sistema_Toothly.Forms
{
    partial class FrmReportes
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
            pnlFiltros = new Panel();
            lblTipoReporte = new Label();
            cmbTipoReporte = new ComboBox();
            lblFiltroTexto = new Label();
            txtFiltroTexto = new TextBox();
            lblDesde = new Label();
            dtpDesde = new DateTimePicker();
            lblHasta = new Label();
            dtpHasta = new DateTimePicker();
            btnImprimir = new Button();
            dgvReporte = new DataGridView();
            pnlEncabezado.SuspendLayout();
            pnlFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReporte).BeginInit();
            SuspendLayout();
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.BackColor = Color.FromArgb(108, 180, 234);
            pnlEncabezado.Controls.Add(lblTitulo);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(1000, 50);
            pnlEncabezado.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(20, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(207, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Informes del Sistema";
            // 
            // pnlFiltros
            // 
            pnlFiltros.BackColor = Color.FromArgb(243, 249, 253);
            pnlFiltros.Controls.Add(lblTipoReporte);
            pnlFiltros.Controls.Add(cmbTipoReporte);
            pnlFiltros.Controls.Add(lblFiltroTexto);
            pnlFiltros.Controls.Add(txtFiltroTexto);
            pnlFiltros.Controls.Add(lblDesde);
            pnlFiltros.Controls.Add(dtpDesde);
            pnlFiltros.Controls.Add(lblHasta);
            pnlFiltros.Controls.Add(dtpHasta);
            pnlFiltros.Controls.Add(btnImprimir);
            pnlFiltros.Dock = DockStyle.Top;
            pnlFiltros.Location = new Point(0, 50);
            pnlFiltros.Name = "pnlFiltros";
            pnlFiltros.Size = new Size(1000, 100);
            pnlFiltros.TabIndex = 1;
            // 
            // lblTipoReporte
            // 
            lblTipoReporte.AutoSize = true;
            lblTipoReporte.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTipoReporte.Location = new Point(20, 15);
            lblTipoReporte.Name = "lblTipoReporte";
            lblTipoReporte.Size = new Size(109, 17);
            lblTipoReporte.TabIndex = 0;
            lblTipoReporte.Text = "Tipo de Informe:";
            // 
            // cmbTipoReporte
            // 
            cmbTipoReporte.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoReporte.FormattingEnabled = true;
            cmbTipoReporte.Location = new Point(20, 37);
            cmbTipoReporte.Name = "cmbTipoReporte";
            cmbTipoReporte.Size = new Size(220, 25);
            cmbTipoReporte.TabIndex = 1;
            cmbTipoReporte.SelectedIndexChanged += cmbTipoReporte_SelectedIndexChanged;
            // 
            // lblFiltroTexto
            // 
            lblFiltroTexto.AutoSize = true;
            lblFiltroTexto.Location = new Point(260, 15);
            lblFiltroTexto.Name = "lblFiltroTexto";
            lblFiltroTexto.Size = new Size(104, 19);
            lblFiltroTexto.TabIndex = 2;
            lblFiltroTexto.Text = "Filtrar por texto:";
            // 
            // txtFiltroTexto
            // 
            txtFiltroTexto.Location = new Point(260, 37);
            txtFiltroTexto.Name = "txtFiltroTexto";
            txtFiltroTexto.PlaceholderText = "Nombre, DUI o texto...";
            txtFiltroTexto.Size = new Size(190, 25);
            txtFiltroTexto.TabIndex = 3;
            txtFiltroTexto.TextChanged += Filtros_Changed;
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.Location = new Point(465, 15);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(50, 19);
            lblDesde.TabIndex = 4;
            lblDesde.Text = "Desde:";
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(465, 37);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(115, 25);
            dtpDesde.TabIndex = 5;
            dtpDesde.ValueChanged += Filtros_Changed;
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.Location = new Point(595, 15);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(47, 19);
            lblHasta.TabIndex = 6;
            lblHasta.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(595, 37);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(115, 25);
            dtpHasta.TabIndex = 7;
            dtpHasta.ValueChanged += Filtros_Changed;
            //
            // btnImprimir
            //
            btnImprimir.Location = new Point(730, 31);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(145, 38);
            btnImprimir.TabIndex = 9;
            btnImprimir.Text = "Imprimir PDF";
            btnImprimir.UseVisualStyleBackColor = false;
            btnImprimir.Click += btnImprimir_Click;
            btnImprimir.Image = Properties.Resources.ico_imprimir;
            btnImprimir.ImageAlign = ContentAlignment.MiddleLeft;
            btnImprimir.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnImprimir.Padding = new Padding(8, 0, 8, 0);
            btnImprimir.FlatStyle = FlatStyle.Flat;
            btnImprimir.FlatAppearance.BorderSize = 1;
            btnImprimir.FlatAppearance.BorderColor = Color.FromArgb(45, 212, 191);
            btnImprimir.BackColor = Color.FromArgb(0, 150, 136);
            btnImprimir.ForeColor = Color.White;
            btnImprimir.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnImprimir.Cursor = Cursors.Hand;
            // 
            // dgvReporte
            // 
            dgvReporte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReporte.Dock = DockStyle.Fill;
            dgvReporte.Location = new Point(0, 150);
            dgvReporte.Name = "dgvReporte";
            dgvReporte.Size = new Size(1000, 450);
            dgvReporte.TabIndex = 2;
            // 
            // FrmReportes
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(dgvReporte);
            Controls.Add(pnlFiltros);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmReportes";
            Text = "Reportes";
            Load += FrmReportes_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            pnlFiltros.ResumeLayout(false);
            pnlFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReporte).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Panel pnlFiltros;
        private Label lblTipoReporte;
        private ComboBox cmbTipoReporte;
        private Label lblFiltroTexto;
        private TextBox txtFiltroTexto;
        private Label lblDesde;
        private DateTimePicker dtpDesde;
        private Label lblHasta;
        private DateTimePicker dtpHasta;
        private Button btnImprimir;
        private DataGridView dgvReporte;
    }
}
