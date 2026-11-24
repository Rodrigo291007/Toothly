namespace Sistema_Toothly.Forms
{
    partial class FrmBitacora
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
            lblUsuario = new Label();
            cmbUsuario = new ComboBox();
            lblDesde = new Label();
            dtpDesde = new DateTimePicker();
            lblHasta = new Label();
            dtpHasta = new DateTimePicker();
            btnFiltrar = new Button();
            btnLimpiarFiltro = new Button();
            dgvBitacora = new DataGridView();
            pnlEncabezado.SuspendLayout();
            pnlFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).BeginInit();
            SuspendLayout();
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.BackColor = Color.FromArgb(108, 180, 234);
            pnlEncabezado.Controls.Add(lblTitulo);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(980, 50);
            pnlEncabezado.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(20, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(189, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Bitácora de Accesos";
            // 
            // pnlFiltros
            // 
            pnlFiltros.BackColor = Color.FromArgb(243, 249, 253);
            pnlFiltros.Controls.Add(lblUsuario);
            pnlFiltros.Controls.Add(cmbUsuario);
            pnlFiltros.Controls.Add(lblDesde);
            pnlFiltros.Controls.Add(dtpDesde);
            pnlFiltros.Controls.Add(lblHasta);
            pnlFiltros.Controls.Add(dtpHasta);
            pnlFiltros.Controls.Add(btnFiltrar);
            pnlFiltros.Controls.Add(btnLimpiarFiltro);
            pnlFiltros.Dock = DockStyle.Top;
            pnlFiltros.Location = new Point(0, 50);
            pnlFiltros.Name = "pnlFiltros";
            pnlFiltros.Size = new Size(980, 60);
            pnlFiltros.TabIndex = 1;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(20, 20);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(59, 19);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "Usuario:";
            // 
            // cmbUsuario
            // 
            cmbUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUsuario.FormattingEnabled = true;
            cmbUsuario.Location = new Point(80, 17);
            cmbUsuario.Name = "cmbUsuario";
            cmbUsuario.Size = new Size(170, 25);
            cmbUsuario.TabIndex = 1;
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.Location = new Point(270, 20);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(50, 19);
            lblDesde.TabIndex = 2;
            lblDesde.Text = "Desde:";
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(325, 17);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(120, 25);
            dtpDesde.TabIndex = 3;
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.Location = new Point(460, 20);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(47, 19);
            lblHasta.TabIndex = 4;
            lblHasta.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(510, 17);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(120, 25);
            dtpHasta.TabIndex = 5;
            // 
            // btnFiltrar
            // 
            btnFiltrar.Location = new Point(650, 12);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(115, 34);
            btnFiltrar.TabIndex = 6;
            btnFiltrar.Text = "Filtrar";
            btnFiltrar.UseVisualStyleBackColor = false;
            btnFiltrar.Click += btnFiltrar_Click;
            btnFiltrar.Image = Properties.Resources.ico_buscar;
            btnFiltrar.ImageAlign = ContentAlignment.MiddleLeft;
            btnFiltrar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnFiltrar.Padding = new Padding(8, 0, 8, 0);
            btnFiltrar.FlatStyle = FlatStyle.Flat;
            btnFiltrar.FlatAppearance.BorderSize = 1;
            btnFiltrar.FlatAppearance.BorderColor = Color.FromArgb(167, 139, 250);
            btnFiltrar.BackColor = Color.FromArgb(61, 143, 203);
            btnFiltrar.ForeColor = Color.White;
            btnFiltrar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnFiltrar.Cursor = Cursors.Hand;
            // 
            // btnLimpiarFiltro
            // 
            btnLimpiarFiltro.Location = new Point(775, 12);
            btnLimpiarFiltro.Name = "btnLimpiarFiltro";
            btnLimpiarFiltro.Size = new Size(115, 34);
            btnLimpiarFiltro.TabIndex = 7;
            btnLimpiarFiltro.Text = "Todos";
            btnLimpiarFiltro.UseVisualStyleBackColor = false;
            btnLimpiarFiltro.Click += btnLimpiarFiltro_Click;
            btnLimpiarFiltro.Image = Properties.Resources.ico_cancelar;
            btnLimpiarFiltro.ImageAlign = ContentAlignment.MiddleLeft;
            btnLimpiarFiltro.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLimpiarFiltro.Padding = new Padding(8, 0, 8, 0);
            btnLimpiarFiltro.FlatStyle = FlatStyle.Flat;
            btnLimpiarFiltro.FlatAppearance.BorderSize = 1;
            btnLimpiarFiltro.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnLimpiarFiltro.BackColor = Color.FromArgb(120, 144, 156);
            btnLimpiarFiltro.ForeColor = Color.White;
            btnLimpiarFiltro.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnLimpiarFiltro.Cursor = Cursors.Hand;
            // 
            // dgvBitacora
            // 
            dgvBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBitacora.Dock = DockStyle.Fill;
            dgvBitacora.Location = new Point(0, 110);
            dgvBitacora.Name = "dgvBitacora";
            dgvBitacora.Size = new Size(980, 490);
            dgvBitacora.TabIndex = 2;
            // 
            // FrmBitacora
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 600);
            Controls.Add(dgvBitacora);
            Controls.Add(pnlFiltros);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmBitacora";
            Text = "Bitácora de Accesos";
            Load += FrmBitacora_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            pnlFiltros.ResumeLayout(false);
            pnlFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Panel pnlFiltros;
        private Label lblUsuario;
        private ComboBox cmbUsuario;
        private Label lblDesde;
        private DateTimePicker dtpDesde;
        private Label lblHasta;
        private DateTimePicker dtpHasta;
        private Button btnFiltrar;
        private Button btnLimpiarFiltro;
        private DataGridView dgvBitacora;
    }
}
