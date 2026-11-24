namespace Sistema_Toothly.Forms
{
    partial class FrmDespacho
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
            lblAyuda = new Label();
            dgvPendientes = new DataGridView();
            pnlAcciones = new Panel();
            lblTotal = new Label();
            chkPagaAhora = new CheckBox();
            btnDespachar = new Button();
            pnlEncabezado.SuspendLayout();
            pnlPaciente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPendientes).BeginInit();
            pnlAcciones.SuspendLayout();
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
            lblTitulo.Size = new Size(196, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Despacho de Recetas";
            //
            // pnlPaciente
            //
            pnlPaciente.BackColor = Color.FromArgb(243, 249, 253);
            pnlPaciente.Controls.Add(lblPaciente);
            pnlPaciente.Controls.Add(cmbPaciente);
            pnlPaciente.Controls.Add(lblAyuda);
            pnlPaciente.Dock = DockStyle.Top;
            pnlPaciente.Location = new Point(0, 50);
            pnlPaciente.Name = "pnlPaciente";
            pnlPaciente.Size = new Size(1000, 85);
            pnlPaciente.TabIndex = 1;
            //
            // lblPaciente
            //
            lblPaciente.AutoSize = true;
            lblPaciente.Location = new Point(20, 18);
            lblPaciente.Name = "lblPaciente";
            lblPaciente.Size = new Size(63, 19);
            lblPaciente.TabIndex = 0;
            lblPaciente.Text = "Paciente:";
            //
            // cmbPaciente
            //
            cmbPaciente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaciente.FormattingEnabled = true;
            cmbPaciente.Location = new Point(95, 15);
            cmbPaciente.Name = "cmbPaciente";
            cmbPaciente.Size = new Size(320, 25);
            cmbPaciente.TabIndex = 1;
            cmbPaciente.SelectedIndexChanged += cmbPaciente_SelectedIndexChanged;
            //
            // lblAyuda
            //
            lblAyuda.AutoSize = true;
            lblAyuda.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblAyuda.ForeColor = Color.FromArgb(96, 125, 139);
            lblAyuda.Location = new Point(20, 52);
            lblAyuda.Name = "lblAyuda";
            lblAyuda.Size = new Size(620, 15);
            lblAyuda.TabIndex = 2;
            lblAyuda.Text = "Escriba en \"A despachar\" las unidades que el paciente compra. Lo que no compre queda pendiente en su receta.";
            //
            // dgvPendientes
            //
            dgvPendientes.AllowUserToAddRows = false;
            dgvPendientes.AllowUserToDeleteRows = false;
            dgvPendientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPendientes.Dock = DockStyle.Fill;
            dgvPendientes.Location = new Point(0, 135);
            dgvPendientes.Name = "dgvPendientes";
            dgvPendientes.Size = new Size(1000, 395);
            dgvPendientes.TabIndex = 2;
            dgvPendientes.CellValidating += dgvPendientes_CellValidating;
            dgvPendientes.CellEndEdit += dgvPendientes_CellEndEdit;
            //
            // pnlAcciones
            //
            pnlAcciones.BackColor = Color.FromArgb(243, 249, 253);
            pnlAcciones.Controls.Add(lblTotal);
            pnlAcciones.Controls.Add(chkPagaAhora);
            pnlAcciones.Controls.Add(btnDespachar);
            pnlAcciones.Dock = DockStyle.Bottom;
            pnlAcciones.Location = new Point(0, 530);
            pnlAcciones.Name = "pnlAcciones";
            pnlAcciones.Size = new Size(1000, 70);
            pnlAcciones.TabIndex = 3;
            //
            // lblTotal
            //
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(61, 143, 203);
            lblTotal.Location = new Point(20, 22);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(118, 21);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "Total: $0.00";
            //
            // chkPagaAhora
            //
            chkPagaAhora.AutoSize = true;
            chkPagaAhora.Checked = true;
            chkPagaAhora.CheckState = CheckState.Checked;
            chkPagaAhora.Location = new Point(250, 24);
            chkPagaAhora.Name = "chkPagaAhora";
            chkPagaAhora.Size = new Size(300, 23);
            chkPagaAhora.TabIndex = 1;
            chkPagaAhora.Text = "El paciente paga ahora (registrar el abono)";
            chkPagaAhora.UseVisualStyleBackColor = true;
            //
            // btnDespachar
            //
            btnDespachar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDespachar.BackColor = Color.FromArgb(46, 125, 50);
            btnDespachar.Cursor = Cursors.Hand;
            btnDespachar.FlatAppearance.BorderColor = Color.FromArgb(52, 211, 153);
            btnDespachar.FlatStyle = FlatStyle.Flat;
            btnDespachar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDespachar.ForeColor = Color.White;
            btnDespachar.ImageAlign = ContentAlignment.MiddleLeft;
            btnDespachar.Location = new Point(790, 15);
            btnDespachar.Name = "btnDespachar";
            btnDespachar.Padding = new Padding(8, 0, 8, 0);
            btnDespachar.Size = new Size(190, 40);
            btnDespachar.TabIndex = 2;
            btnDespachar.Text = "Despachar";
            btnDespachar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDespachar.UseVisualStyleBackColor = false;
            btnDespachar.Click += btnDespachar_Click;
            //
            // FrmDespacho
            //
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 249, 253);
            ClientSize = new Size(1000, 600);
            Controls.Add(dgvPendientes);
            Controls.Add(pnlAcciones);
            Controls.Add(pnlPaciente);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmDespacho";
            Text = "Despacho de Recetas";
            Load += FrmDespacho_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            pnlPaciente.ResumeLayout(false);
            pnlPaciente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPendientes).EndInit();
            pnlAcciones.ResumeLayout(false);
            pnlAcciones.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Panel pnlPaciente;
        private Label lblPaciente;
        private ComboBox cmbPaciente;
        private Label lblAyuda;
        private DataGridView dgvPendientes;
        private Panel pnlAcciones;
        private Label lblTotal;
        private CheckBox chkPagaAhora;
        private Button btnDespachar;
    }
}
