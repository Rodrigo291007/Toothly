namespace Sistema_Toothly.Forms
{
    partial class FrmRecetas
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
            splPrincipal = new SplitContainer();
            dgvRecetas = new DataGridView();
            pnlListaHeader = new Panel();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            pnlDetalle = new Panel();
            lblPaciente = new Label();
            cmbPaciente = new ComboBox();
            lblTratamiento = new Label();
            cmbTratamiento = new ComboBox();
            lblFecha = new Label();
            dtpFecha = new DateTimePicker();
            lblIndicaciones = new Label();
            txtIndicaciones = new TextBox();
            grpMedicamentos = new GroupBox();
            dgvMedicamentos = new DataGridView();
            pnlAgregarMed = new Panel();
            lblMedicamento = new Label();
            cmbMedicamento = new ComboBox();
            lblCantidadMed = new Label();
            txtCantidadMed = new TextBox();
            lblDosis = new Label();
            txtDosis = new TextBox();
            lblFrecuencia = new Label();
            txtFrecuencia = new TextBox();
            lblDuracion = new Label();
            txtDuracion = new TextBox();
            btnAgregarMed = new Button();
            btnQuitarMed = new Button();
            pnlBotones = new FlowLayoutPanel();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnImprimir = new Button();
            btnLimpiar = new Button();
            pnlEncabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splPrincipal).BeginInit();
            splPrincipal.Panel1.SuspendLayout();
            splPrincipal.Panel2.SuspendLayout();
            splPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecetas).BeginInit();
            pnlListaHeader.SuspendLayout();
            pnlDetalle.SuspendLayout();
            grpMedicamentos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMedicamentos).BeginInit();
            pnlAgregarMed.SuspendLayout();
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
            pnlEncabezado.Size = new Size(1020, 50);
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
            lblTitulo.Text = "Gestión de Recetas";
            // 
            // splPrincipal
            // 
            splPrincipal.Dock = DockStyle.Fill;
            splPrincipal.Location = new Point(0, 50);
            splPrincipal.Name = "splPrincipal";
            // 
            // splPrincipal.Panel1
            // 
            splPrincipal.Panel1.Controls.Add(dgvRecetas);
            splPrincipal.Panel1.Controls.Add(pnlListaHeader);
            // 
            // splPrincipal.Panel2
            // 
            splPrincipal.Panel2.Controls.Add(pnlDetalle);
            splPrincipal.Panel2.Controls.Add(pnlBotones);
            splPrincipal.Size = new Size(1020, 570);
            splPrincipal.SplitterDistance = 420;
            splPrincipal.TabIndex = 1;
            // 
            // dgvRecetas
            // 
            dgvRecetas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRecetas.Dock = DockStyle.Fill;
            dgvRecetas.Location = new Point(0, 50);
            dgvRecetas.Name = "dgvRecetas";
            dgvRecetas.Size = new Size(420, 520);
            dgvRecetas.TabIndex = 1;
            dgvRecetas.SelectionChanged += dgvRecetas_SelectionChanged;
            // 
            // pnlListaHeader
            // 
            pnlListaHeader.BackColor = Color.FromArgb(243, 249, 253);
            pnlListaHeader.Controls.Add(lblBuscar);
            pnlListaHeader.Controls.Add(txtBuscar);
            pnlListaHeader.Controls.Add(btnBuscar);
            pnlListaHeader.Dock = DockStyle.Top;
            pnlListaHeader.Location = new Point(0, 0);
            pnlListaHeader.Name = "pnlListaHeader";
            pnlListaHeader.Size = new Size(420, 50);
            pnlListaHeader.TabIndex = 0;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(10, 15);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(52, 19);
            lblBuscar.TabIndex = 0;
            lblBuscar.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(65, 12);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(160, 25);
            txtBuscar.TabIndex = 1;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(235, 8);
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
            // pnlDetalle
            // 
            pnlDetalle.AutoScroll = true;
            pnlDetalle.BackColor = Color.White;
            pnlDetalle.Controls.Add(lblPaciente);
            pnlDetalle.Controls.Add(cmbPaciente);
            pnlDetalle.Controls.Add(lblTratamiento);
            pnlDetalle.Controls.Add(cmbTratamiento);
            pnlDetalle.Controls.Add(lblFecha);
            pnlDetalle.Controls.Add(dtpFecha);
            pnlDetalle.Controls.Add(lblIndicaciones);
            pnlDetalle.Controls.Add(txtIndicaciones);
            pnlDetalle.Controls.Add(grpMedicamentos);
            pnlDetalle.Dock = DockStyle.Fill;
            pnlDetalle.Location = new Point(0, 0);
            pnlDetalle.Name = "pnlDetalle";
            pnlDetalle.Size = new Size(596, 510);
            pnlDetalle.TabIndex = 0;
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
            cmbPaciente.Size = new Size(240, 25);
            cmbPaciente.TabIndex = 1;
            cmbPaciente.SelectedIndexChanged += cmbPaciente_SelectedIndexChanged;
            // 
            // lblTratamiento
            // 
            lblTratamiento.AutoSize = true;
            lblTratamiento.Location = new Point(280, 15);
            lblTratamiento.Name = "lblTratamiento";
            lblTratamiento.Size = new Size(84, 19);
            lblTratamiento.TabIndex = 2;
            lblTratamiento.Text = "Tratamiento:";
            // 
            // cmbTratamiento
            // 
            cmbTratamiento.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTratamiento.FormattingEnabled = true;
            cmbTratamiento.Location = new Point(280, 37);
            cmbTratamiento.Name = "cmbTratamiento";
            cmbTratamiento.Size = new Size(280, 25);
            cmbTratamiento.TabIndex = 3;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(20, 75);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(47, 19);
            lblFecha.TabIndex = 4;
            lblFecha.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(20, 97);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(130, 25);
            dtpFecha.TabIndex = 5;
            // 
            // lblIndicaciones
            // 
            lblIndicaciones.AutoSize = true;
            lblIndicaciones.Location = new Point(170, 75);
            lblIndicaciones.Name = "lblIndicaciones";
            lblIndicaciones.Size = new Size(160, 19);
            lblIndicaciones.TabIndex = 6;
            lblIndicaciones.Text = "Indicaciones Generales:";
            // 
            // txtIndicaciones
            // 
            txtIndicaciones.Location = new Point(170, 97);
            txtIndicaciones.Name = "txtIndicaciones";
            txtIndicaciones.Size = new Size(390, 25);
            txtIndicaciones.TabIndex = 7;
            // 
            // grpMedicamentos
            // 
            grpMedicamentos.Controls.Add(dgvMedicamentos);
            grpMedicamentos.Controls.Add(pnlAgregarMed);
            grpMedicamentos.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grpMedicamentos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpMedicamentos.Location = new Point(20, 140);
            grpMedicamentos.Name = "grpMedicamentos";
            grpMedicamentos.Size = new Size(540, 350);
            grpMedicamentos.TabIndex = 8;
            grpMedicamentos.TabStop = false;
            grpMedicamentos.Text = "Medicamentos Prescritos";
            // 
            // dgvMedicamentos
            // 
            dgvMedicamentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMedicamentos.Dock = DockStyle.Fill;
            dgvMedicamentos.Location = new Point(3, 20);
            dgvMedicamentos.Name = "dgvMedicamentos";
            dgvMedicamentos.Size = new Size(534, 217);
            dgvMedicamentos.TabIndex = 0;
            // 
            // pnlAgregarMed
            // 
            pnlAgregarMed.BackColor = Color.FromArgb(243, 249, 253);
            pnlAgregarMed.Controls.Add(lblMedicamento);
            pnlAgregarMed.Controls.Add(cmbMedicamento);
            pnlAgregarMed.Controls.Add(lblCantidadMed);
            pnlAgregarMed.Controls.Add(txtCantidadMed);
            pnlAgregarMed.Controls.Add(lblDosis);
            pnlAgregarMed.Controls.Add(txtDosis);
            pnlAgregarMed.Controls.Add(lblFrecuencia);
            pnlAgregarMed.Controls.Add(txtFrecuencia);
            pnlAgregarMed.Controls.Add(lblDuracion);
            pnlAgregarMed.Controls.Add(txtDuracion);
            pnlAgregarMed.Controls.Add(btnAgregarMed);
            pnlAgregarMed.Controls.Add(btnQuitarMed);
            pnlAgregarMed.Dock = DockStyle.Bottom;
            pnlAgregarMed.Font = new Font("Segoe UI", 9F);
            pnlAgregarMed.Location = new Point(3, 237);
            pnlAgregarMed.Name = "pnlAgregarMed";
            pnlAgregarMed.Size = new Size(534, 110);
            pnlAgregarMed.TabIndex = 1;
            // 
            // lblMedicamento
            // 
            lblMedicamento.AutoSize = true;
            lblMedicamento.Location = new Point(10, 8);
            lblMedicamento.Name = "lblMedicamento";
            lblMedicamento.Size = new Size(84, 15);
            lblMedicamento.TabIndex = 0;
            lblMedicamento.Text = "Medicamento:";
            // 
            // cmbMedicamento
            //
            cmbMedicamento.DropDownStyle = ComboBoxStyle.DropDown;
            cmbMedicamento.FormattingEnabled = true;
            cmbMedicamento.Location = new Point(10, 26);
            cmbMedicamento.Name = "cmbMedicamento";
            cmbMedicamento.Size = new Size(180, 23);
            cmbMedicamento.TabIndex = 1;
            //
            // lblCantidadMed
            //
            lblCantidadMed.AutoSize = true;
            lblCantidadMed.Location = new Point(10, 57);
            lblCantidadMed.Name = "lblCantidadMed";
            lblCantidadMed.Size = new Size(93, 15);
            lblCantidadMed.TabIndex = 10;
            lblCantidadMed.Text = "Cantidad (und):";
            //
            // txtCantidadMed
            //
            txtCantidadMed.Location = new Point(10, 75);
            txtCantidadMed.Name = "txtCantidadMed";
            txtCantidadMed.Size = new Size(80, 23);
            txtCantidadMed.TabIndex = 11;
            // 
            // lblDosis
            // 
            lblDosis.AutoSize = true;
            lblDosis.Location = new Point(200, 8);
            lblDosis.Name = "lblDosis";
            lblDosis.Size = new Size(38, 15);
            lblDosis.TabIndex = 2;
            lblDosis.Text = "Dosis:";
            // 
            // txtDosis
            // 
            txtDosis.Location = new Point(200, 26);
            txtDosis.Name = "txtDosis";
            txtDosis.PlaceholderText = "1 tableta";
            txtDosis.Size = new Size(100, 23);
            txtDosis.TabIndex = 3;
            // 
            // lblFrecuencia
            // 
            lblFrecuencia.AutoSize = true;
            lblFrecuencia.Location = new Point(310, 8);
            lblFrecuencia.Name = "lblFrecuencia";
            lblFrecuencia.Size = new Size(68, 15);
            lblFrecuencia.TabIndex = 4;
            lblFrecuencia.Text = "Frecuencia:";
            // 
            // txtFrecuencia
            // 
            txtFrecuencia.Location = new Point(310, 26);
            txtFrecuencia.Name = "txtFrecuencia";
            txtFrecuencia.PlaceholderText = "c/8 hrs";
            txtFrecuencia.Size = new Size(100, 23);
            txtFrecuencia.TabIndex = 5;
            // 
            // lblDuracion
            // 
            lblDuracion.AutoSize = true;
            lblDuracion.Location = new Point(420, 8);
            lblDuracion.Name = "lblDuracion";
            lblDuracion.Size = new Size(58, 15);
            lblDuracion.TabIndex = 6;
            lblDuracion.Text = "Duración:";
            // 
            // txtDuracion
            // 
            txtDuracion.Location = new Point(420, 26);
            txtDuracion.Name = "txtDuracion";
            txtDuracion.PlaceholderText = "5 días";
            txtDuracion.Size = new Size(100, 23);
            txtDuracion.TabIndex = 7;
            // 
            // btnAgregarMed
            // 
            btnAgregarMed.Location = new Point(280, 65);
            btnAgregarMed.Name = "btnAgregarMed";
            btnAgregarMed.Size = new Size(125, 34);
            btnAgregarMed.TabIndex = 8;
            btnAgregarMed.Text = "+ Agregar";
            btnAgregarMed.UseVisualStyleBackColor = false;
            btnAgregarMed.Click += btnAgregarMed_Click;
            btnAgregarMed.Image = Properties.Resources.ico_nuevo;
            btnAgregarMed.ImageAlign = ContentAlignment.MiddleLeft;
            btnAgregarMed.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAgregarMed.Padding = new Padding(8, 0, 8, 0);
            btnAgregarMed.FlatStyle = FlatStyle.Flat;
            btnAgregarMed.FlatAppearance.BorderSize = 1;
            btnAgregarMed.FlatAppearance.BorderColor = Color.FromArgb(52, 211, 153);
            btnAgregarMed.BackColor = Color.FromArgb(46, 125, 50);
            btnAgregarMed.ForeColor = Color.White;
            btnAgregarMed.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAgregarMed.Cursor = Cursors.Hand;
            // 
            // btnQuitarMed
            // 
            btnQuitarMed.Location = new Point(410, 65);
            btnQuitarMed.Name = "btnQuitarMed";
            btnQuitarMed.Size = new Size(125, 34);
            btnQuitarMed.TabIndex = 9;
            btnQuitarMed.Text = "- Quitar";
            btnQuitarMed.UseVisualStyleBackColor = false;
            btnQuitarMed.Click += btnQuitarMed_Click;
            btnQuitarMed.Image = Properties.Resources.ico_baja;
            btnQuitarMed.ImageAlign = ContentAlignment.MiddleLeft;
            btnQuitarMed.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnQuitarMed.Padding = new Padding(8, 0, 8, 0);
            btnQuitarMed.FlatStyle = FlatStyle.Flat;
            btnQuitarMed.FlatAppearance.BorderSize = 1;
            btnQuitarMed.FlatAppearance.BorderColor = Color.FromArgb(248, 113, 113);
            btnQuitarMed.BackColor = Color.FromArgb(220, 38, 38);
            btnQuitarMed.ForeColor = Color.White;
            btnQuitarMed.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnQuitarMed.Cursor = Cursors.Hand;
            // 
            // pnlBotones
            // 
            pnlBotones.BackColor = Color.FromArgb(243, 249, 253);
            pnlBotones.Controls.Add(btnNuevo);
            pnlBotones.Controls.Add(btnGuardar);
            pnlBotones.Controls.Add(btnImprimir);
            pnlBotones.Controls.Add(btnLimpiar);
            pnlBotones.Dock = DockStyle.Bottom;
            pnlBotones.Location = new Point(0, 510);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Padding = new Padding(20, 12, 0, 0);
            pnlBotones.Size = new Size(596, 60);
            pnlBotones.TabIndex = 1;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(23, 15);
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
            btnGuardar.Location = new Point(129, 15);
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
            // btnImprimir
            // 
            btnImprimir.Location = new Point(235, 15);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(145, 38);
            btnImprimir.TabIndex = 2;
            btnImprimir.Text = "Imprimir";
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
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(341, 15);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(115, 38);
            btnLimpiar.TabIndex = 3;
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
            // FrmRecetas
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1020, 620);
            Controls.Add(splPrincipal);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmRecetas";
            Text = "Gestión de Recetas";
            Load += FrmRecetas_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            splPrincipal.Panel1.ResumeLayout(false);
            splPrincipal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splPrincipal).EndInit();
            splPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRecetas).EndInit();
            pnlListaHeader.ResumeLayout(false);
            pnlListaHeader.PerformLayout();
            pnlDetalle.ResumeLayout(false);
            pnlDetalle.PerformLayout();
            grpMedicamentos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMedicamentos).EndInit();
            pnlAgregarMed.ResumeLayout(false);
            pnlAgregarMed.PerformLayout();
            pnlBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEncabezado;
        private Label lblTitulo;
        private SplitContainer splPrincipal;
        private DataGridView dgvRecetas;
        private Panel pnlListaHeader;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Panel pnlDetalle;
        private Label lblPaciente;
        private ComboBox cmbPaciente;
        private Label lblTratamiento;
        private ComboBox cmbTratamiento;
        private Label lblFecha;
        private DateTimePicker dtpFecha;
        private Label lblIndicaciones;
        private TextBox txtIndicaciones;
        private GroupBox grpMedicamentos;
        private DataGridView dgvMedicamentos;
        private Panel pnlAgregarMed;
        private Label lblMedicamento;
        private ComboBox cmbMedicamento;
        private Label lblCantidadMed;
        private TextBox txtCantidadMed;
        private Label lblDosis;
        private TextBox txtDosis;
        private Label lblFrecuencia;
        private TextBox txtFrecuencia;
        private Label lblDuracion;
        private TextBox txtDuracion;
        private Button btnAgregarMed;
        private Button btnQuitarMed;
        private FlowLayoutPanel pnlBotones;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnImprimir;
        private Button btnLimpiar;
    }
}
