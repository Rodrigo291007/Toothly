namespace Sistema_Toothly.Forms
{
    partial class FrmEstadosCuenta
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
            pnlPaciente = new Panel();
            lblPaciente = new Label();
            cmbPaciente = new ComboBox();
            btnCargar = new Button();
            lblSaldoTitulo = new Label();
            lblSaldoValor = new Label();
            dgvMovimientos = new DataGridView();
            pnlRegistro = new Panel();
            lblTipo = new Label();
            cmbTipo = new ComboBox();
            lblConcepto = new Label();
            txtConcepto = new TextBox();
            lblMonto = new Label();
            txtMonto = new TextBox();
            btnRegistrar = new Button();
            btnLimpiar = new Button();
            pnlEncabezado.SuspendLayout();
            pnlPaciente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMovimientos).BeginInit();
            pnlRegistro.SuspendLayout();
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
            lblTitulo.Size = new Size(174, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Estados de Cuenta";
            // 
            // pnlPaciente
            // 
            pnlPaciente.BackColor = Color.FromArgb(243, 249, 253);
            pnlPaciente.Controls.Add(lblPaciente);
            pnlPaciente.Controls.Add(cmbPaciente);
            pnlPaciente.Controls.Add(btnCargar);
            pnlPaciente.Controls.Add(lblSaldoTitulo);
            pnlPaciente.Controls.Add(lblSaldoValor);
            pnlPaciente.Dock = DockStyle.Top;
            pnlPaciente.Location = new Point(0, 50);
            pnlPaciente.Name = "pnlPaciente";
            pnlPaciente.Size = new Size(950, 70);
            pnlPaciente.TabIndex = 1;
            // 
            // lblPaciente
            // 
            lblPaciente.AutoSize = true;
            lblPaciente.Location = new Point(20, 25);
            lblPaciente.Name = "lblPaciente";
            lblPaciente.Size = new Size(63, 19);
            lblPaciente.TabIndex = 0;
            lblPaciente.Text = "Paciente:";
            // 
            // cmbPaciente
            // 
            cmbPaciente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaciente.FormattingEnabled = true;
            cmbPaciente.Location = new Point(90, 22);
            cmbPaciente.Name = "cmbPaciente";
            cmbPaciente.Size = new Size(300, 25);
            cmbPaciente.TabIndex = 1;
            cmbPaciente.SelectedIndexChanged += cmbPaciente_SelectedIndexChanged;
            // 
            // btnCargar
            // 
            btnCargar.Location = new Point(400, 18);
            btnCargar.Name = "btnCargar";
            btnCargar.Size = new Size(115, 34);
            btnCargar.TabIndex = 2;
            btnCargar.Text = "Cargar";
            btnCargar.UseVisualStyleBackColor = false;
            btnCargar.Click += btnCargar_Click;
            btnCargar.Image = Properties.Resources.ico_cuenta;
            btnCargar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCargar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCargar.Padding = new Padding(8, 0, 8, 0);
            btnCargar.FlatStyle = FlatStyle.Flat;
            btnCargar.FlatAppearance.BorderSize = 1;
            btnCargar.FlatAppearance.BorderColor = Color.FromArgb(96, 165, 250);
            btnCargar.BackColor = Color.FromArgb(61, 143, 203);
            btnCargar.ForeColor = Color.White;
            btnCargar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCargar.Cursor = Cursors.Hand;
            // 
            // lblSaldoTitulo
            // 
            lblSaldoTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSaldoTitulo.AutoSize = true;
            lblSaldoTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSaldoTitulo.Location = new Point(710, 25);
            lblSaldoTitulo.Name = "lblSaldoTitulo";
            lblSaldoTitulo.Size = new Size(97, 19);
            lblSaldoTitulo.TabIndex = 3;
            lblSaldoTitulo.Text = "Saldo Actual:";
            // 
            // lblSaldoValor
            // 
            lblSaldoValor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSaldoValor.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblSaldoValor.ForeColor = Color.FromArgb(61, 143, 203);
            lblSaldoValor.Location = new Point(810, 20);
            lblSaldoValor.Name = "lblSaldoValor";
            lblSaldoValor.Size = new Size(125, 28);
            lblSaldoValor.TabIndex = 4;
            lblSaldoValor.Text = "$0.00";
            lblSaldoValor.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvMovimientos
            // 
            dgvMovimientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMovimientos.Dock = DockStyle.Fill;
            dgvMovimientos.Location = new Point(0, 120);
            dgvMovimientos.Name = "dgvMovimientos";
            dgvMovimientos.Size = new Size(950, 380);
            dgvMovimientos.TabIndex = 2;
            // 
            // pnlRegistro
            // 
            pnlRegistro.BackColor = Color.FromArgb(243, 249, 253);
            pnlRegistro.BorderStyle = BorderStyle.FixedSingle;
            pnlRegistro.Controls.Add(lblTipo);
            pnlRegistro.Controls.Add(cmbTipo);
            pnlRegistro.Controls.Add(lblConcepto);
            pnlRegistro.Controls.Add(txtConcepto);
            pnlRegistro.Controls.Add(lblMonto);
            pnlRegistro.Controls.Add(txtMonto);
            pnlRegistro.Controls.Add(btnRegistrar);
            pnlRegistro.Controls.Add(btnLimpiar);
            pnlRegistro.Dock = DockStyle.Bottom;
            pnlRegistro.Location = new Point(0, 500);
            pnlRegistro.Name = "pnlRegistro";
            pnlRegistro.Size = new Size(950, 100);
            pnlRegistro.TabIndex = 3;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTipo.Location = new Point(20, 15);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(120, 17);
            lblTipo.TabIndex = 0;
            lblTipo.Text = "Tipo Movimiento:";
            // 
            // cmbTipo
            // 
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.FormattingEnabled = true;
            cmbTipo.Items.AddRange(new object[] { "Cargo", "Abono" });
            cmbTipo.Location = new Point(20, 40);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(130, 25);
            cmbTipo.TabIndex = 1;
            // 
            // lblConcepto
            // 
            lblConcepto.AutoSize = true;
            lblConcepto.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblConcepto.Location = new Point(170, 15);
            lblConcepto.Name = "lblConcepto";
            lblConcepto.Size = new Size(71, 17);
            lblConcepto.TabIndex = 2;
            lblConcepto.Text = "Concepto:";
            // 
            // txtConcepto
            // 
            txtConcepto.Location = new Point(170, 40);
            txtConcepto.Name = "txtConcepto";
            txtConcepto.PlaceholderText = "Motivo del cargo o pago";
            txtConcepto.Size = new Size(380, 25);
            txtConcepto.TabIndex = 3;
            // 
            // lblMonto
            // 
            lblMonto.AutoSize = true;
            lblMonto.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMonto.Location = new Point(570, 15);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(73, 17);
            lblMonto.TabIndex = 4;
            lblMonto.Text = "Monto ($):";
            // 
            // txtMonto
            // 
            txtMonto.Location = new Point(570, 40);
            txtMonto.Name = "txtMonto";
            txtMonto.PlaceholderText = "0.00";
            txtMonto.Size = new Size(120, 25);
            txtMonto.TabIndex = 5;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(700, 33);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(125, 36);
            btnRegistrar.TabIndex = 6;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = false;
            btnRegistrar.Click += btnRegistrar_Click;
            btnRegistrar.Image = Properties.Resources.ico_cuenta;
            btnRegistrar.ImageAlign = ContentAlignment.MiddleLeft;
            btnRegistrar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRegistrar.Padding = new Padding(8, 0, 8, 0);
            btnRegistrar.FlatStyle = FlatStyle.Flat;
            btnRegistrar.FlatAppearance.BorderSize = 1;
            btnRegistrar.FlatAppearance.BorderColor = Color.FromArgb(96, 165, 250);
            btnRegistrar.BackColor = Color.FromArgb(46, 125, 50);
            btnRegistrar.ForeColor = Color.White;
            btnRegistrar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRegistrar.Cursor = Cursors.Hand;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(835, 33);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(115, 38);
            btnLimpiar.TabIndex = 7;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            btnLimpiar.Image = Properties.Resources.ico_cancelar;
            btnLimpiar.ImageAlign = ContentAlignment.MiddleLeft;
            btnLimpiar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLimpiar.Padding = new Padding(8, 0, 8, 0);
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.FlatAppearance.BorderSize = 1;
            btnLimpiar.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnLimpiar.BackColor = Color.FromArgb(120, 144, 156);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnLimpiar.Cursor = Cursors.Hand;
            // 
            // FrmEstadosCuenta
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(950, 600);
            Controls.Add(dgvMovimientos);
            Controls.Add(pnlRegistro);
            Controls.Add(pnlPaciente);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmEstadosCuenta";
            Text = "Estados de Cuenta";
            Load += FrmEstadosCuenta_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            pnlPaciente.ResumeLayout(false);
            pnlPaciente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMovimientos).EndInit();
            pnlRegistro.ResumeLayout(false);
            pnlRegistro.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Panel pnlPaciente;
        private Label lblPaciente;
        private ComboBox cmbPaciente;
        private Button btnCargar;
        private Label lblSaldoTitulo;
        private Label lblSaldoValor;
        private DataGridView dgvMovimientos;
        private Panel pnlRegistro;
        private Label lblTipo;
        private ComboBox cmbTipo;
        private Label lblConcepto;
        private TextBox txtConcepto;
        private Label lblMonto;
        private TextBox txtMonto;
        private Button btnRegistrar;
        private Button btnLimpiar;
    }
}
