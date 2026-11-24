namespace Sistema_Toothly.Forms
{
    partial class FrmFichaPaciente
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
            pnlSeleccion = new Panel();
            lblSeleccionar = new Label();
            cmbPacientes = new ComboBox();
            btnCargar = new Button();
            pnlDatos = new Panel();
            lblNombre = new Label();
            lblDui = new Label();
            lblTelefono = new Label();
            lblCorreo = new Label();
            lblAlergias = new Label();
            lblSaldo = new Label();
            tbcDetalles = new TabControl();
            tbpCitas = new TabPage();
            dgvCitasFicha = new DataGridView();
            tbpTratamientos = new TabPage();
            dgvTratamientosFicha = new DataGridView();
            pnlEncabezado.SuspendLayout();
            pnlSeleccion.SuspendLayout();
            pnlDatos.SuspendLayout();
            tbcDetalles.SuspendLayout();
            tbpCitas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCitasFicha).BeginInit();
            tbpTratamientos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTratamientosFicha).BeginInit();
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
            lblTitulo.Size = new Size(181, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Ficha del Paciente";
            // 
            // pnlSeleccion
            // 
            pnlSeleccion.BackColor = Color.FromArgb(243, 249, 253);
            pnlSeleccion.Controls.Add(lblSeleccionar);
            pnlSeleccion.Controls.Add(cmbPacientes);
            pnlSeleccion.Controls.Add(btnCargar);
            pnlSeleccion.Dock = DockStyle.Top;
            pnlSeleccion.Location = new Point(0, 50);
            pnlSeleccion.Name = "pnlSeleccion";
            pnlSeleccion.Size = new Size(950, 50);
            pnlSeleccion.TabIndex = 1;
            // 
            // lblSeleccionar
            // 
            lblSeleccionar.AutoSize = true;
            lblSeleccionar.Location = new Point(20, 15);
            lblSeleccionar.Name = "lblSeleccionar";
            lblSeleccionar.Size = new Size(133, 19);
            lblSeleccionar.TabIndex = 0;
            lblSeleccionar.Text = "Seleccionar Paciente:";
            // 
            // cmbPacientes
            // 
            cmbPacientes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPacientes.FormattingEnabled = true;
            cmbPacientes.Location = new Point(160, 12);
            cmbPacientes.Name = "cmbPacientes";
            cmbPacientes.Size = new Size(320, 25);
            cmbPacientes.TabIndex = 1;
            cmbPacientes.SelectedIndexChanged += cmbPacientes_SelectedIndexChanged;
            // 
            // btnCargar
            // 
            btnCargar.Location = new Point(495, 7);
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
            // pnlDatos
            // 
            pnlDatos.BackColor = Color.White;
            pnlDatos.BorderStyle = BorderStyle.FixedSingle;
            pnlDatos.Controls.Add(lblNombre);
            pnlDatos.Controls.Add(lblDui);
            pnlDatos.Controls.Add(lblTelefono);
            pnlDatos.Controls.Add(lblCorreo);
            pnlDatos.Controls.Add(lblAlergias);
            pnlDatos.Controls.Add(lblSaldo);
            pnlDatos.Dock = DockStyle.Top;
            pnlDatos.Location = new Point(0, 100);
            pnlDatos.Name = "pnlDatos";
            pnlDatos.Size = new Size(950, 110);
            pnlDatos.TabIndex = 2;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblNombre.ForeColor = Color.FromArgb(47, 59, 69);
            lblNombre.Location = new Point(20, 15);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(160, 20);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Paciente: [Seleccione]";
            // 
            // lblDui
            // 
            lblDui.AutoSize = true;
            lblDui.Location = new Point(20, 45);
            lblDui.Name = "lblDui";
            lblDui.Size = new Size(47, 19);
            lblDui.TabIndex = 1;
            lblDui.Text = "DUI: --";
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(220, 45);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(74, 19);
            lblTelefono.TabIndex = 2;
            lblTelefono.Text = "Teléfono: --";
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Location = new Point(420, 45);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(65, 19);
            lblCorreo.TabIndex = 3;
            lblCorreo.Text = "Correo: --";
            // 
            // lblAlergias
            // 
            lblAlergias.AutoSize = true;
            lblAlergias.Location = new Point(20, 75);
            lblAlergias.Name = "lblAlergias";
            lblAlergias.Size = new Size(71, 19);
            lblAlergias.TabIndex = 4;
            lblAlergias.Text = "Alergias: --";
            // 
            // lblSaldo
            // 
            lblSaldo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSaldo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSaldo.ForeColor = Color.FromArgb(61, 143, 203);
            lblSaldo.Location = new Point(680, 20);
            lblSaldo.Name = "lblSaldo";
            lblSaldo.Size = new Size(250, 40);
            lblSaldo.TabIndex = 5;
            lblSaldo.Text = "Saldo Actual: $0.00";
            lblSaldo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tbcDetalles
            // 
            tbcDetalles.Controls.Add(tbpCitas);
            tbcDetalles.Controls.Add(tbpTratamientos);
            tbcDetalles.Dock = DockStyle.Fill;
            tbcDetalles.Location = new Point(0, 210);
            tbcDetalles.Name = "tbcDetalles";
            tbcDetalles.SelectedIndex = 0;
            tbcDetalles.Size = new Size(950, 390);
            tbcDetalles.TabIndex = 3;
            // 
            // tbpCitas
            // 
            tbpCitas.Controls.Add(dgvCitasFicha);
            tbpCitas.Location = new Point(4, 26);
            tbpCitas.Name = "tbpCitas";
            tbpCitas.Padding = new Padding(3);
            tbpCitas.Size = new Size(942, 360);
            tbpCitas.TabIndex = 0;
            tbpCitas.Text = "Historial de Citas";
            tbpCitas.UseVisualStyleBackColor = true;
            // 
            // dgvCitasFicha
            // 
            dgvCitasFicha.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCitasFicha.Dock = DockStyle.Fill;
            dgvCitasFicha.Location = new Point(3, 3);
            dgvCitasFicha.Name = "dgvCitasFicha";
            dgvCitasFicha.Size = new Size(936, 354);
            dgvCitasFicha.TabIndex = 0;
            // 
            // tbpTratamientos
            // 
            tbpTratamientos.Controls.Add(dgvTratamientosFicha);
            tbpTratamientos.Location = new Point(4, 26);
            tbpTratamientos.Name = "tbpTratamientos";
            tbpTratamientos.Padding = new Padding(3);
            tbpTratamientos.Size = new Size(942, 360);
            tbpTratamientos.TabIndex = 1;
            tbpTratamientos.Text = "Tratamientos Realizados";
            tbpTratamientos.UseVisualStyleBackColor = true;
            // 
            // dgvTratamientosFicha
            // 
            dgvTratamientosFicha.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTratamientosFicha.Dock = DockStyle.Fill;
            dgvTratamientosFicha.Location = new Point(3, 3);
            dgvTratamientosFicha.Name = "dgvTratamientosFicha";
            dgvTratamientosFicha.Size = new Size(936, 354);
            dgvTratamientosFicha.TabIndex = 0;
            // 
            // FrmFichaPaciente
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(950, 600);
            Controls.Add(tbcDetalles);
            Controls.Add(pnlDatos);
            Controls.Add(pnlSeleccion);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmFichaPaciente";
            Text = "Ficha del Paciente";
            Load += FrmFichaPaciente_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            pnlSeleccion.ResumeLayout(false);
            pnlSeleccion.PerformLayout();
            pnlDatos.ResumeLayout(false);
            pnlDatos.PerformLayout();
            tbcDetalles.ResumeLayout(false);
            tbpCitas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCitasFicha).EndInit();
            tbpTratamientos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTratamientosFicha).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Panel pnlSeleccion;
        private Label lblSeleccionar;
        private ComboBox cmbPacientes;
        private Button btnCargar;
        private Panel pnlDatos;
        private Label lblNombre;
        private Label lblDui;
        private Label lblTelefono;
        private Label lblCorreo;
        private Label lblAlergias;
        private Label lblSaldo;
        private TabControl tbcDetalles;
        private TabPage tbpCitas;
        private DataGridView dgvCitasFicha;
        private TabPage tbpTratamientos;
        private DataGridView dgvTratamientosFicha;
    }
}
