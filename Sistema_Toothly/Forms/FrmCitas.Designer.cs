namespace Sistema_Toothly.Forms
{
    partial class FrmCitas
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
            lblFiltroFecha = new Label();
            dtpFiltroFecha = new DateTimePicker();
            lblFiltroOdontologo = new Label();
            cmbFiltroOdontologo = new ComboBox();
            btnFiltrar = new Button();
            btnVerTodas = new Button();
            dgvCitas = new DataGridView();
            pnlCaptura = new Panel();
            lblPaciente = new Label();
            cmbPaciente = new ComboBox();
            lblOdontologo = new Label();
            cmbOdontologo = new ComboBox();
            lblFecha = new Label();
            dtpFecha = new DateTimePicker();
            lblHora = new Label();
            cmbHora = new ComboBox();
            lblMotivo = new Label();
            txtMotivo = new TextBox();
            lblEstado = new Label();
            cmbEstado = new ComboBox();
            pnlBotones = new FlowLayoutPanel();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnReprogramar = new Button();
            btnCancelarCita = new Button();
            btnLimpiar = new Button();
            pnlEncabezado.SuspendLayout();
            pnlFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCitas).BeginInit();
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
            lblTitulo.Size = new Size(260, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión y Agenda de Citas";
            // 
            // pnlFiltros
            // 
            pnlFiltros.BackColor = Color.FromArgb(243, 249, 253);
            pnlFiltros.Controls.Add(lblFiltroFecha);
            pnlFiltros.Controls.Add(dtpFiltroFecha);
            pnlFiltros.Controls.Add(lblFiltroOdontologo);
            pnlFiltros.Controls.Add(cmbFiltroOdontologo);
            pnlFiltros.Controls.Add(btnFiltrar);
            pnlFiltros.Controls.Add(btnVerTodas);
            pnlFiltros.Dock = DockStyle.Top;
            pnlFiltros.Location = new Point(0, 50);
            pnlFiltros.Name = "pnlFiltros";
            pnlFiltros.Size = new Size(1000, 50);
            pnlFiltros.TabIndex = 1;
            // 
            // lblFiltroFecha
            // 
            lblFiltroFecha.AutoSize = true;
            lblFiltroFecha.Location = new Point(20, 15);
            lblFiltroFecha.Name = "lblFiltroFecha";
            lblFiltroFecha.Size = new Size(47, 19);
            lblFiltroFecha.TabIndex = 0;
            lblFiltroFecha.Text = "Fecha:";
            // 
            // dtpFiltroFecha
            // 
            dtpFiltroFecha.Format = DateTimePickerFormat.Short;
            dtpFiltroFecha.Location = new Point(70, 12);
            dtpFiltroFecha.Name = "dtpFiltroFecha";
            dtpFiltroFecha.Size = new Size(120, 25);
            dtpFiltroFecha.TabIndex = 1;
            // 
            // lblFiltroOdontologo
            // 
            lblFiltroOdontologo.AutoSize = true;
            lblFiltroOdontologo.Location = new Point(210, 15);
            lblFiltroOdontologo.Name = "lblFiltroOdontologo";
            lblFiltroOdontologo.Size = new Size(86, 19);
            lblFiltroOdontologo.TabIndex = 2;
            lblFiltroOdontologo.Text = "Odontólogo:";
            // 
            // cmbFiltroOdontologo
            // 
            cmbFiltroOdontologo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroOdontologo.FormattingEnabled = true;
            cmbFiltroOdontologo.Location = new Point(300, 12);
            cmbFiltroOdontologo.Name = "cmbFiltroOdontologo";
            cmbFiltroOdontologo.Size = new Size(200, 25);
            cmbFiltroOdontologo.TabIndex = 3;
            // 
            // btnFiltrar
            // 
            btnFiltrar.Location = new Point(520, 7);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(115, 34);
            btnFiltrar.TabIndex = 4;
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
            // btnVerTodas
            // 
            btnVerTodas.Location = new Point(645, 7);
            btnVerTodas.Name = "btnVerTodas";
            btnVerTodas.Size = new Size(130, 34);
            btnVerTodas.TabIndex = 5;
            btnVerTodas.Text = "Ver Todas";
            btnVerTodas.UseVisualStyleBackColor = false;
            btnVerTodas.Click += btnVerTodas_Click;
            btnVerTodas.Image = Properties.Resources.ico_buscar;
            btnVerTodas.ImageAlign = ContentAlignment.MiddleLeft;
            btnVerTodas.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnVerTodas.Padding = new Padding(8, 0, 8, 0);
            btnVerTodas.FlatStyle = FlatStyle.Flat;
            btnVerTodas.FlatAppearance.BorderSize = 1;
            btnVerTodas.FlatAppearance.BorderColor = Color.FromArgb(167, 139, 250);
            btnVerTodas.BackColor = Color.FromArgb(96, 125, 139);
            btnVerTodas.ForeColor = Color.White;
            btnVerTodas.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnVerTodas.Cursor = Cursors.Hand;
            // 
            // dgvCitas
            // 
            dgvCitas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCitas.Dock = DockStyle.Fill;
            dgvCitas.Location = new Point(0, 100);
            dgvCitas.Name = "dgvCitas";
            dgvCitas.Size = new Size(1000, 270);
            dgvCitas.TabIndex = 2;
            dgvCitas.SelectionChanged += dgvCitas_SelectionChanged;
            // 
            // pnlCaptura
            // 
            pnlCaptura.BackColor = Color.FromArgb(243, 249, 253);
            pnlCaptura.Controls.Add(lblPaciente);
            pnlCaptura.Controls.Add(cmbPaciente);
            pnlCaptura.Controls.Add(lblOdontologo);
            pnlCaptura.Controls.Add(cmbOdontologo);
            pnlCaptura.Controls.Add(lblFecha);
            pnlCaptura.Controls.Add(dtpFecha);
            pnlCaptura.Controls.Add(lblHora);
            pnlCaptura.Controls.Add(cmbHora);
            pnlCaptura.Controls.Add(lblMotivo);
            pnlCaptura.Controls.Add(txtMotivo);
            pnlCaptura.Controls.Add(lblEstado);
            pnlCaptura.Controls.Add(cmbEstado);
            pnlCaptura.Controls.Add(pnlBotones);
            pnlCaptura.Dock = DockStyle.Bottom;
            pnlCaptura.Location = new Point(0, 370);
            pnlCaptura.Name = "pnlCaptura";
            pnlCaptura.Size = new Size(1000, 230);
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
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(540, 15);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(47, 19);
            lblFecha.TabIndex = 4;
            lblFecha.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(540, 37);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(130, 25);
            dtpFecha.TabIndex = 5;
            // 
            // lblHora
            // 
            lblHora.AutoSize = true;
            lblHora.Location = new Point(690, 15);
            lblHora.Name = "lblHora";
            lblHora.Size = new Size(42, 19);
            lblHora.TabIndex = 6;
            lblHora.Text = "Hora:";
            // 
            // cmbHora
            // 
            cmbHora.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHora.FormattingEnabled = true;
            cmbHora.Location = new Point(690, 37);
            cmbHora.Name = "cmbHora";
            cmbHora.Size = new Size(110, 25);
            cmbHora.TabIndex = 7;
            // 
            // lblMotivo
            // 
            lblMotivo.AutoSize = true;
            lblMotivo.Location = new Point(20, 75);
            lblMotivo.Name = "lblMotivo";
            lblMotivo.Size = new Size(55, 19);
            lblMotivo.TabIndex = 8;
            lblMotivo.Text = "Motivo:";
            // 
            // txtMotivo
            // 
            txtMotivo.Location = new Point(20, 97);
            txtMotivo.Name = "txtMotivo";
            txtMotivo.Size = new Size(650, 25);
            txtMotivo.TabIndex = 9;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(690, 75);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(53, 19);
            lblEstado.TabIndex = 10;
            lblEstado.Text = "Estado:";
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Items.AddRange(new object[] { "Programada", "Atendida", "Cancelada", "Reprogramada" });
            cmbEstado.Location = new Point(690, 97);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(140, 25);
            cmbEstado.TabIndex = 11;
            // 
            // pnlBotones
            // 
            pnlBotones.Controls.Add(btnNuevo);
            pnlBotones.Controls.Add(btnGuardar);
            pnlBotones.Controls.Add(btnReprogramar);
            pnlBotones.Controls.Add(btnCancelarCita);
            pnlBotones.Controls.Add(btnLimpiar);
            pnlBotones.Dock = DockStyle.Bottom;
            pnlBotones.Location = new Point(0, 160);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Padding = new Padding(20, 10, 0, 0);
            pnlBotones.Size = new Size(1000, 70);
            pnlBotones.TabIndex = 12;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(23, 13);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(115, 38);
            btnNuevo.TabIndex = 0;
            btnNuevo.Text = "Nueva";
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
            btnGuardar.Text = "Agendar";
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
            // btnReprogramar
            // 
            btnReprogramar.Location = new Point(235, 13);
            btnReprogramar.Name = "btnReprogramar";
            btnReprogramar.Size = new Size(150, 38);
            btnReprogramar.TabIndex = 2;
            btnReprogramar.Text = "Reprogramar";
            btnReprogramar.UseVisualStyleBackColor = false;
            btnReprogramar.Click += btnReprogramar_Click;
            btnReprogramar.Image = Properties.Resources.ico_editar;
            btnReprogramar.ImageAlign = ContentAlignment.MiddleLeft;
            btnReprogramar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReprogramar.Padding = new Padding(8, 0, 8, 0);
            btnReprogramar.FlatStyle = FlatStyle.Flat;
            btnReprogramar.FlatAppearance.BorderSize = 1;
            btnReprogramar.FlatAppearance.BorderColor = Color.FromArgb(251, 191, 36);
            btnReprogramar.BackColor = Color.FromArgb(245, 124, 0);
            btnReprogramar.ForeColor = Color.White;
            btnReprogramar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnReprogramar.Cursor = Cursors.Hand;
            // 
            // btnCancelarCita
            // 
            btnCancelarCita.Location = new Point(361, 13);
            btnCancelarCita.Name = "btnCancelarCita";
            btnCancelarCita.Size = new Size(145, 38);
            btnCancelarCita.TabIndex = 3;
            btnCancelarCita.Text = "Cancelar Cita";
            btnCancelarCita.UseVisualStyleBackColor = false;
            btnCancelarCita.Click += btnCancelarCita_Click;
            btnCancelarCita.Image = Properties.Resources.ico_cancelar;
            btnCancelarCita.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelarCita.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelarCita.Padding = new Padding(8, 0, 8, 0);
            btnCancelarCita.FlatStyle = FlatStyle.Flat;
            btnCancelarCita.FlatAppearance.BorderSize = 1;
            btnCancelarCita.FlatAppearance.BorderColor = Color.FromArgb(248, 113, 113);
            btnCancelarCita.BackColor = Color.FromArgb(220, 38, 38);
            btnCancelarCita.ForeColor = Color.White;
            btnCancelarCita.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCancelarCita.Cursor = Cursors.Hand;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(487, 13);
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
            // FrmCitas
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(dgvCitas);
            Controls.Add(pnlCaptura);
            Controls.Add(pnlFiltros);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmCitas";
            Text = "Gestión de Citas";
            Load += FrmCitas_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            pnlFiltros.ResumeLayout(false);
            pnlFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCitas).EndInit();
            pnlCaptura.ResumeLayout(false);
            pnlCaptura.PerformLayout();
            pnlBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Panel pnlFiltros;
        private Label lblFiltroFecha;
        private DateTimePicker dtpFiltroFecha;
        private Label lblFiltroOdontologo;
        private ComboBox cmbFiltroOdontologo;
        private Button btnFiltrar;
        private Button btnVerTodas;
        private DataGridView dgvCitas;
        private Panel pnlCaptura;
        private Label lblPaciente;
        private ComboBox cmbPaciente;
        private Label lblOdontologo;
        private ComboBox cmbOdontologo;
        private Label lblFecha;
        private DateTimePicker dtpFecha;
        private Label lblHora;
        private ComboBox cmbHora;
        private Label lblMotivo;
        private TextBox txtMotivo;
        private Label lblEstado;
        private ComboBox cmbEstado;
        private FlowLayoutPanel pnlBotones;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnReprogramar;
        private Button btnCancelarCita;
        private Button btnLimpiar;
    }
}
