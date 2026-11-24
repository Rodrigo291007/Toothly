namespace Sistema_Toothly.Forms
{
    partial class FrmTratamientos
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
            pnlBusqueda = new Panel();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            dgvTratamientos = new DataGridView();
            pnlCaptura = new Panel();
            lblPaciente = new Label();
            cmbPaciente = new ComboBox();
            lblOdontologo = new Label();
            cmbOdontologo = new ComboBox();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            lblFechaInicio = new Label();
            dtpFechaInicio = new DateTimePicker();
            lblFechaFin = new Label();
            dtpFechaFin = new DateTimePicker();
            lblCosto = new Label();
            txtCosto = new TextBox();
            lblEstado = new Label();
            cmbEstado = new ComboBox();
            pnlBotones = new FlowLayoutPanel();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnModificar = new Button();
            btnCancelarTratamiento = new Button();
            btnLimpiar = new Button();
            pnlEncabezado.SuspendLayout();
            pnlBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTratamientos).BeginInit();
            pnlCaptura.SuspendLayout();
            pnlBotones.SuspendLayout();
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
            lblTitulo.Size = new Size(230, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión de Tratamientos";
            // 
            // pnlBusqueda
            // 
            pnlBusqueda.BackColor = Color.FromArgb(243, 249, 253);
            pnlBusqueda.Controls.Add(lblBuscar);
            pnlBusqueda.Controls.Add(txtBuscar);
            pnlBusqueda.Controls.Add(btnBuscar);
            pnlBusqueda.Dock = DockStyle.Top;
            pnlBusqueda.Location = new Point(0, 50);
            pnlBusqueda.Name = "pnlBusqueda";
            pnlBusqueda.Size = new Size(1000, 50);
            pnlBusqueda.TabIndex = 1;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(20, 15);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(52, 19);
            lblBuscar.TabIndex = 0;
            lblBuscar.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(78, 12);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(280, 25);
            txtBuscar.TabIndex = 1;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(370, 7);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(115, 36);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            btnBuscar.Image = Properties.Resources.ico_buscar;
            btnBuscar.ImageAlign = ContentAlignment.MiddleLeft;
            btnBuscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnBuscar.Padding = new Padding(8, 0, 8, 0);
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.FlatAppearance.BorderSize = 1;
            btnBuscar.FlatAppearance.BorderColor = Color.FromArgb(167, 139, 250);
            btnBuscar.BackColor = Color.FromArgb(61, 143, 203);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnBuscar.Cursor = Cursors.Hand;
            // 
            // dgvTratamientos
            // 
            dgvTratamientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTratamientos.Dock = DockStyle.Fill;
            dgvTratamientos.Location = new Point(0, 100);
            dgvTratamientos.Name = "dgvTratamientos";
            dgvTratamientos.Size = new Size(1000, 260);
            dgvTratamientos.TabIndex = 2;
            dgvTratamientos.SelectionChanged += dgvTratamientos_SelectionChanged;
            // 
            // pnlCaptura
            // 
            pnlCaptura.BackColor = Color.FromArgb(243, 249, 253);
            pnlCaptura.Controls.Add(lblPaciente);
            pnlCaptura.Controls.Add(cmbPaciente);
            pnlCaptura.Controls.Add(lblOdontologo);
            pnlCaptura.Controls.Add(cmbOdontologo);
            pnlCaptura.Controls.Add(lblDescripcion);
            pnlCaptura.Controls.Add(txtDescripcion);
            pnlCaptura.Controls.Add(lblFechaInicio);
            pnlCaptura.Controls.Add(dtpFechaInicio);
            pnlCaptura.Controls.Add(lblFechaFin);
            pnlCaptura.Controls.Add(dtpFechaFin);
            pnlCaptura.Controls.Add(lblCosto);
            pnlCaptura.Controls.Add(txtCosto);
            pnlCaptura.Controls.Add(lblEstado);
            pnlCaptura.Controls.Add(cmbEstado);
            pnlCaptura.Controls.Add(pnlBotones);
            pnlCaptura.Dock = DockStyle.Bottom;
            pnlCaptura.Location = new Point(0, 360);
            pnlCaptura.Name = "pnlCaptura";
            pnlCaptura.Size = new Size(1000, 240);
            pnlCaptura.TabIndex = 3;
            // 
            // lblPaciente
            // 
            lblPaciente.AutoSize = true;
            lblPaciente.Location = new Point(20, 15);
            lblPaciente.Name = "lblPaciente";
            lblPaciente.Size = new Size(63, 19);
            lblPaciente.TabIndex = 0;
            lblPaciente.Text = "Paciente:";
            // 
            // cmbPaciente
            // 
            cmbPaciente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaciente.FormattingEnabled = true;
            cmbPaciente.Location = new Point(20, 37);
            cmbPaciente.Name = "cmbPaciente";
            cmbPaciente.Size = new Size(260, 25);
            cmbPaciente.TabIndex = 1;
            // 
            // lblOdontologo
            // 
            lblOdontologo.AutoSize = true;
            lblOdontologo.Location = new Point(300, 15);
            lblOdontologo.Name = "lblOdontologo";
            lblOdontologo.Size = new Size(86, 19);
            lblOdontologo.TabIndex = 2;
            lblOdontologo.Text = "Odontólogo:";
            // 
            // cmbOdontologo
            // 
            cmbOdontologo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOdontologo.FormattingEnabled = true;
            cmbOdontologo.Location = new Point(300, 37);
            cmbOdontologo.Name = "cmbOdontologo";
            cmbOdontologo.Size = new Size(220, 25);
            cmbOdontologo.TabIndex = 3;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(540, 15);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(82, 19);
            lblDescripcion.TabIndex = 4;
            lblDescripcion.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(540, 37);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(420, 25);
            txtDescripcion.TabIndex = 5;
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Location = new Point(20, 75);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(83, 19);
            lblFechaInicio.TabIndex = 6;
            lblFechaInicio.Text = "Fecha Inicio:";
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Format = DateTimePickerFormat.Short;
            dtpFechaInicio.Location = new Point(20, 97);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(130, 25);
            dtpFechaInicio.TabIndex = 7;
            // 
            // lblFechaFin
            // 
            lblFechaFin.AutoSize = true;
            lblFechaFin.Location = new Point(170, 75);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(69, 19);
            lblFechaFin.TabIndex = 8;
            lblFechaFin.Text = "Fecha Fin:";
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Format = DateTimePickerFormat.Short;
            dtpFechaFin.Location = new Point(170, 97);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(130, 25);
            dtpFechaFin.TabIndex = 9;
            // 
            // lblCosto
            // 
            lblCosto.AutoSize = true;
            lblCosto.Location = new Point(320, 75);
            lblCosto.Name = "lblCosto";
            lblCosto.Size = new Size(68, 19);
            lblCosto.TabIndex = 10;
            lblCosto.Text = "Costo ($):";
            // 
            // txtCosto
            // 
            txtCosto.Location = new Point(320, 97);
            txtCosto.Name = "txtCosto";
            txtCosto.PlaceholderText = "0.00";
            txtCosto.Size = new Size(110, 25);
            txtCosto.TabIndex = 11;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(450, 75);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(53, 19);
            lblEstado.TabIndex = 12;
            lblEstado.Text = "Estado:";
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Items.AddRange(new object[] { "Pendiente", "En proceso", "Finalizado", "Anulado" });
            cmbEstado.Location = new Point(450, 97);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(140, 25);
            cmbEstado.TabIndex = 13;
            // 
            // pnlBotones
            // 
            pnlBotones.Controls.Add(btnNuevo);
            pnlBotones.Controls.Add(btnGuardar);
            pnlBotones.Controls.Add(btnModificar);
            pnlBotones.Controls.Add(btnCancelarTratamiento);
            pnlBotones.Controls.Add(btnLimpiar);
            pnlBotones.Dock = DockStyle.Bottom;
            pnlBotones.Location = new Point(0, 170);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Padding = new Padding(20, 10, 0, 0);
            pnlBotones.Size = new Size(1000, 70);
            pnlBotones.TabIndex = 14;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(23, 13);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(115, 38);
            btnNuevo.TabIndex = 0;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            btnNuevo.Image = Properties.Resources.ico_nuevo;
            btnNuevo.ImageAlign = ContentAlignment.MiddleLeft;
            btnNuevo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNuevo.Padding = new Padding(8, 0, 8, 0);
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.FlatAppearance.BorderSize = 1;
            btnNuevo.FlatAppearance.BorderColor = Color.FromArgb(52, 211, 153);
            btnNuevo.BackColor = Color.FromArgb(46, 125, 50);
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNuevo.Cursor = Cursors.Hand;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(129, 13);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 38);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            btnGuardar.Image = Properties.Resources.ico_guardar;
            btnGuardar.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.Padding = new Padding(8, 0, 8, 0);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.FlatAppearance.BorderSize = 1;
            btnGuardar.FlatAppearance.BorderColor = Color.FromArgb(96, 165, 250);
            btnGuardar.BackColor = Color.FromArgb(61, 143, 203);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnGuardar.Cursor = Cursors.Hand;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(235, 13);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(125, 38);
            btnModificar.TabIndex = 2;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            btnModificar.Image = Properties.Resources.ico_editar;
            btnModificar.ImageAlign = ContentAlignment.MiddleLeft;
            btnModificar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnModificar.Padding = new Padding(8, 0, 8, 0);
            btnModificar.FlatStyle = FlatStyle.Flat;
            btnModificar.FlatAppearance.BorderSize = 1;
            btnModificar.FlatAppearance.BorderColor = Color.FromArgb(251, 191, 36);
            btnModificar.BackColor = Color.FromArgb(245, 124, 0);
            btnModificar.ForeColor = Color.White;
            btnModificar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnModificar.Cursor = Cursors.Hand;
            // 
            // btnCancelarTratamiento
            // 
            btnCancelarTratamiento.Location = new Point(341, 13);
            btnCancelarTratamiento.Name = "btnCancelarTratamiento";
            btnCancelarTratamiento.Size = new Size(145, 38);
            btnCancelarTratamiento.TabIndex = 3;
            btnCancelarTratamiento.Text = "Anular Trat.";
            btnCancelarTratamiento.UseVisualStyleBackColor = false;
            btnCancelarTratamiento.Click += btnCancelarTratamiento_Click;
            btnCancelarTratamiento.Image = Properties.Resources.ico_cancelar;
            btnCancelarTratamiento.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelarTratamiento.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelarTratamiento.Padding = new Padding(8, 0, 8, 0);
            btnCancelarTratamiento.FlatStyle = FlatStyle.Flat;
            btnCancelarTratamiento.FlatAppearance.BorderSize = 1;
            btnCancelarTratamiento.FlatAppearance.BorderColor = Color.FromArgb(248, 113, 113);
            btnCancelarTratamiento.BackColor = Color.FromArgb(220, 38, 38);
            btnCancelarTratamiento.ForeColor = Color.White;
            btnCancelarTratamiento.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCancelarTratamiento.Cursor = Cursors.Hand;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(467, 13);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(115, 38);
            btnLimpiar.TabIndex = 4;
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
            // FrmTratamientos
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(dgvTratamientos);
            Controls.Add(pnlCaptura);
            Controls.Add(pnlBusqueda);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmTratamientos";
            Text = "Gestión de Tratamientos";
            Load += FrmTratamientos_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            pnlBusqueda.ResumeLayout(false);
            pnlBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTratamientos).EndInit();
            pnlCaptura.ResumeLayout(false);
            pnlCaptura.PerformLayout();
            pnlBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Panel pnlBusqueda;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private DataGridView dgvTratamientos;
        private Panel pnlCaptura;
        private Label lblPaciente;
        private ComboBox cmbPaciente;
        private Label lblOdontologo;
        private ComboBox cmbOdontologo;
        private Label lblDescripcion;
        private TextBox txtDescripcion;
        private Label lblFechaInicio;
        private DateTimePicker dtpFechaInicio;
        private Label lblFechaFin;
        private DateTimePicker dtpFechaFin;
        private Label lblCosto;
        private TextBox txtCosto;
        private Label lblEstado;
        private ComboBox cmbEstado;
        private FlowLayoutPanel pnlBotones;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnModificar;
        private Button btnCancelarTratamiento;
        private Button btnLimpiar;
    }
}
