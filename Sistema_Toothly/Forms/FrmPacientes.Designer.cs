namespace Sistema_Toothly.Forms
{
    partial class FrmPacientes
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
            lblEstadoFiltro = new Label();
            cmbEstadoFiltro = new ComboBox();
            btnBuscar = new Button();
            dgvPacientes = new DataGridView();
            pnlCaptura = new Panel();
            lblNombres = new Label();
            txtNombres = new TextBox();
            lblApellidos = new Label();
            txtApellidos = new TextBox();
            lblDui = new Label();
            txtDui = new TextBox();
            lblFechaNac = new Label();
            dtpFechaNac = new DateTimePicker();
            lblSexo = new Label();
            cmbSexo = new ComboBox();
            lblTelefono = new Label();
            txtTelefono = new TextBox();
            lblCorreo = new Label();
            txtCorreo = new TextBox();
            lblDireccion = new Label();
            txtDireccion = new TextBox();
            lblAlergias = new Label();
            txtAlergias = new TextBox();
            lblAntecedentes = new Label();
            txtAntecedentes = new TextBox();
            lblEstado = new Label();
            cmbEstado = new ComboBox();
            pnlBotones = new FlowLayoutPanel();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnModificar = new Button();
            btnDarDeBaja = new Button();
            btnLimpiar = new Button();
            btnCancelar = new Button();
            pnlEncabezado.SuspendLayout();
            pnlBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPacientes).BeginInit();
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
            pnlEncabezado.Size = new Size(1080, 50);
            pnlEncabezado.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(20, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(250, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión de Pacientes";
            // 
            // pnlBusqueda
            // 
            pnlBusqueda.BackColor = Color.FromArgb(243, 249, 253);
            pnlBusqueda.Controls.Add(lblBuscar);
            pnlBusqueda.Controls.Add(txtBuscar);
            pnlBusqueda.Controls.Add(lblEstadoFiltro);
            pnlBusqueda.Controls.Add(cmbEstadoFiltro);
            pnlBusqueda.Controls.Add(btnBuscar);
            pnlBusqueda.Dock = DockStyle.Top;
            pnlBusqueda.Location = new Point(0, 50);
            pnlBusqueda.Name = "pnlBusqueda";
            pnlBusqueda.Size = new Size(1080, 50);
            pnlBusqueda.TabIndex = 1;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(20, 15);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(153, 23);
            lblBuscar.TabIndex = 0;
            lblBuscar.Text = "Buscar (Nom/DUI):";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(185, 12);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(220, 30);
            txtBuscar.TabIndex = 1;
            // 
            // lblEstadoFiltro
            // 
            lblEstadoFiltro.AutoSize = true;
            lblEstadoFiltro.Location = new Point(425, 15);
            lblEstadoFiltro.Name = "lblEstadoFiltro";
            lblEstadoFiltro.Size = new Size(65, 23);
            lblEstadoFiltro.TabIndex = 2;
            lblEstadoFiltro.Text = "Estado:";
            // 
            // cmbEstadoFiltro
            // 
            cmbEstadoFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstadoFiltro.FormattingEnabled = true;
            cmbEstadoFiltro.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" });
            cmbEstadoFiltro.Location = new Point(500, 12);
            cmbEstadoFiltro.Name = "cmbEstadoFiltro";
            cmbEstadoFiltro.Size = new Size(130, 31);
            cmbEstadoFiltro.TabIndex = 3;
            cmbEstadoFiltro.SelectedIndexChanged += cmbEstadoFiltro_SelectedIndexChanged;
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.FromArgb(61, 143, 203);
            btnBuscar.Cursor = Cursors.Hand;
            btnBuscar.FlatAppearance.BorderColor = Color.FromArgb(167, 139, 250);
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Image = Properties.Resources.ico_buscar;
            btnBuscar.ImageAlign = ContentAlignment.MiddleLeft;
            btnBuscar.Location = new Point(650, 7);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Padding = new Padding(8, 0, 8, 0);
            btnBuscar.Size = new Size(115, 36);
            btnBuscar.TabIndex = 4;
            btnBuscar.Text = "Buscar";
            btnBuscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // dgvPacientes
            // 
            dgvPacientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPacientes.Dock = DockStyle.Fill;
            dgvPacientes.Location = new Point(0, 100);
            dgvPacientes.Name = "dgvPacientes";
            dgvPacientes.RowHeadersWidth = 51;
            dgvPacientes.Size = new Size(1080, 240);
            dgvPacientes.TabIndex = 2;
            dgvPacientes.SelectionChanged += dgvPacientes_SelectionChanged;
            // 
            // pnlCaptura
            // 
            pnlCaptura.BackColor = Color.FromArgb(243, 249, 253);
            pnlCaptura.Controls.Add(lblNombres);
            pnlCaptura.Controls.Add(txtNombres);
            pnlCaptura.Controls.Add(lblApellidos);
            pnlCaptura.Controls.Add(txtApellidos);
            pnlCaptura.Controls.Add(lblDui);
            pnlCaptura.Controls.Add(txtDui);
            pnlCaptura.Controls.Add(lblFechaNac);
            pnlCaptura.Controls.Add(dtpFechaNac);
            pnlCaptura.Controls.Add(lblSexo);
            pnlCaptura.Controls.Add(cmbSexo);
            pnlCaptura.Controls.Add(lblTelefono);
            pnlCaptura.Controls.Add(txtTelefono);
            pnlCaptura.Controls.Add(lblCorreo);
            pnlCaptura.Controls.Add(txtCorreo);
            pnlCaptura.Controls.Add(lblDireccion);
            pnlCaptura.Controls.Add(txtDireccion);
            pnlCaptura.Controls.Add(lblAlergias);
            pnlCaptura.Controls.Add(txtAlergias);
            pnlCaptura.Controls.Add(lblAntecedentes);
            pnlCaptura.Controls.Add(txtAntecedentes);
            pnlCaptura.Controls.Add(lblEstado);
            pnlCaptura.Controls.Add(cmbEstado);
            pnlCaptura.Controls.Add(pnlBotones);
            pnlCaptura.Dock = DockStyle.Bottom;
            pnlCaptura.Location = new Point(0, 340);
            pnlCaptura.Name = "pnlCaptura";
            pnlCaptura.Size = new Size(1080, 310);
            pnlCaptura.TabIndex = 3;
            // 
            // lblNombres
            // 
            lblNombres.AutoSize = true;
            lblNombres.Location = new Point(20, 10);
            lblNombres.Name = "lblNombres";
            lblNombres.Size = new Size(84, 23);
            lblNombres.TabIndex = 0;
            lblNombres.Text = "Nombres:";
            // 
            // txtNombres
            // 
            txtNombres.Location = new Point(20, 30);
            txtNombres.Name = "txtNombres";
            txtNombres.Size = new Size(240, 30);
            txtNombres.TabIndex = 1;
            // 
            // lblApellidos
            // 
            lblApellidos.AutoSize = true;
            lblApellidos.Location = new Point(280, 10);
            lblApellidos.Name = "lblApellidos";
            lblApellidos.Size = new Size(83, 23);
            lblApellidos.TabIndex = 2;
            lblApellidos.Text = "Apellidos:";
            // 
            // txtApellidos
            // 
            txtApellidos.Location = new Point(280, 30);
            txtApellidos.Name = "txtApellidos";
            txtApellidos.Size = new Size(240, 30);
            txtApellidos.TabIndex = 3;
            // 
            // lblDui
            // 
            lblDui.AutoSize = true;
            lblDui.Location = new Point(540, 10);
            lblDui.Name = "lblDui";
            lblDui.Size = new Size(43, 23);
            lblDui.TabIndex = 4;
            lblDui.Text = "DUI:";
            // 
            // txtDui
            // 
            txtDui.Location = new Point(540, 30);
            txtDui.Name = "txtDui";
            txtDui.PlaceholderText = "12345678-9";
            txtDui.Size = new Size(130, 30);
            txtDui.TabIndex = 5;
            // 
            // lblFechaNac
            // 
            lblFechaNac.AutoSize = true;
            lblFechaNac.Location = new Point(690, 10);
            lblFechaNac.Name = "lblFechaNac";
            lblFechaNac.Size = new Size(151, 23);
            lblFechaNac.TabIndex = 6;
            lblFechaNac.Text = "Fecha Nacimiento:";
            // 
            // dtpFechaNac
            // 
            dtpFechaNac.Format = DateTimePickerFormat.Short;
            dtpFechaNac.Location = new Point(690, 30);
            dtpFechaNac.Name = "dtpFechaNac";
            dtpFechaNac.Size = new Size(130, 30);
            dtpFechaNac.TabIndex = 7;
            // 
            // lblSexo
            // 
            lblSexo.AutoSize = true;
            lblSexo.Location = new Point(855, 10);
            lblSexo.Name = "lblSexo";
            lblSexo.Size = new Size(50, 23);
            lblSexo.TabIndex = 8;
            lblSexo.Text = "Sexo:";
            // 
            // cmbSexo
            // 
            cmbSexo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSexo.FormattingEnabled = true;
            cmbSexo.Items.AddRange(new object[] { "Masculino", "Femenino" });
            cmbSexo.Location = new Point(855, 30);
            cmbSexo.Name = "cmbSexo";
            cmbSexo.Size = new Size(120, 31);
            cmbSexo.TabIndex = 9;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(20, 65);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(78, 23);
            lblTelefono.TabIndex = 10;
            lblTelefono.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(20, 85);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.PlaceholderText = "7890-1234";
            txtTelefono.Size = new Size(150, 30);
            txtTelefono.TabIndex = 11;
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Location = new Point(190, 65);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(66, 23);
            lblCorreo.TabIndex = 12;
            lblCorreo.Text = "Correo:";
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(190, 85);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(240, 30);
            txtCorreo.TabIndex = 13;
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Location = new Point(450, 65);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(85, 23);
            lblDireccion.TabIndex = 14;
            lblDireccion.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(450, 85);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(370, 30);
            txtDireccion.TabIndex = 15;
            // 
            // lblAlergias
            // 
            lblAlergias.AutoSize = true;
            lblAlergias.Location = new Point(20, 120);
            lblAlergias.Name = "lblAlergias";
            lblAlergias.Size = new Size(74, 23);
            lblAlergias.TabIndex = 18;
            lblAlergias.Text = "Alergias:";
            // 
            // txtAlergias
            // 
            txtAlergias.Location = new Point(20, 140);
            txtAlergias.Name = "txtAlergias";
            txtAlergias.Size = new Size(450, 30);
            txtAlergias.TabIndex = 19;
            // 
            // lblAntecedentes
            // 
            lblAntecedentes.AutoSize = true;
            lblAntecedentes.Location = new Point(490, 120);
            lblAntecedentes.Name = "lblAntecedentes";
            lblAntecedentes.Size = new Size(118, 23);
            lblAntecedentes.TabIndex = 20;
            lblAntecedentes.Text = "Antecedentes:";
            // 
            // txtAntecedentes
            // 
            txtAntecedentes.Location = new Point(490, 140);
            txtAntecedentes.Name = "txtAntecedentes";
            txtAntecedentes.Size = new Size(470, 30);
            txtAntecedentes.TabIndex = 21;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(855, 65);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(65, 23);
            lblEstado.TabIndex = 16;
            lblEstado.Text = "Estado:";
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Items.AddRange(new object[] { "Activo", "Inactivo" });
            cmbEstado.Location = new Point(855, 85);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(120, 31);
            cmbEstado.TabIndex = 17;
            // 
            // pnlBotones
            // 
            pnlBotones.Controls.Add(btnNuevo);
            pnlBotones.Controls.Add(btnGuardar);
            pnlBotones.Controls.Add(btnModificar);
            pnlBotones.Controls.Add(btnDarDeBaja);
            pnlBotones.Controls.Add(btnLimpiar);
            pnlBotones.Dock = DockStyle.Bottom;
            pnlBotones.Location = new Point(0, 240);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Padding = new Padding(20, 15, 0, 0);
            pnlBotones.Size = new Size(1080, 70);
            pnlBotones.TabIndex = 22;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = Color.FromArgb(46, 125, 50);
            btnNuevo.Cursor = Cursors.Hand;
            btnNuevo.FlatAppearance.BorderColor = Color.FromArgb(52, 211, 153);
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Image = Properties.Resources.ico_nuevo;
            btnNuevo.ImageAlign = ContentAlignment.MiddleLeft;
            btnNuevo.Location = new Point(23, 18);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Padding = new Padding(8, 0, 8, 0);
            btnNuevo.Size = new Size(115, 38);
            btnNuevo.TabIndex = 0;
            btnNuevo.Text = "Nuevo";
            btnNuevo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(61, 143, 203);
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatAppearance.BorderColor = Color.FromArgb(96, 165, 250);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Image = Properties.Resources.ico_guardar;
            btnGuardar.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardar.Location = new Point(144, 18);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Padding = new Padding(8, 0, 8, 0);
            btnGuardar.Size = new Size(126, 38);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.FromArgb(245, 124, 0);
            btnModificar.Cursor = Cursors.Hand;
            btnModificar.FlatAppearance.BorderColor = Color.FromArgb(251, 191, 36);
            btnModificar.FlatStyle = FlatStyle.Flat;
            btnModificar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnModificar.ForeColor = Color.White;
            btnModificar.Image = Properties.Resources.ico_editar;
            btnModificar.ImageAlign = ContentAlignment.MiddleLeft;
            btnModificar.Location = new Point(276, 18);
            btnModificar.Name = "btnModificar";
            btnModificar.Padding = new Padding(8, 0, 8, 0);
            btnModificar.Size = new Size(160, 38);
            btnModificar.TabIndex = 2;
            btnModificar.Text = "Modificar";
            btnModificar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnDarDeBaja
            // 
            btnDarDeBaja.BackColor = Color.FromArgb(220, 38, 38);
            btnDarDeBaja.Cursor = Cursors.Hand;
            btnDarDeBaja.FlatAppearance.BorderColor = Color.FromArgb(248, 113, 113);
            btnDarDeBaja.FlatStyle = FlatStyle.Flat;
            btnDarDeBaja.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDarDeBaja.ForeColor = Color.White;
            btnDarDeBaja.Image = Properties.Resources.ico_baja;
            btnDarDeBaja.ImageAlign = ContentAlignment.MiddleLeft;
            btnDarDeBaja.Location = new Point(442, 18);
            btnDarDeBaja.Name = "btnDarDeBaja";
            btnDarDeBaja.Padding = new Padding(8, 0, 8, 0);
            btnDarDeBaja.Size = new Size(164, 38);
            btnDarDeBaja.TabIndex = 3;
            btnDarDeBaja.Text = "Dar de Baja";
            btnDarDeBaja.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDarDeBaja.UseVisualStyleBackColor = false;
            btnDarDeBaja.Click += btnDarDeBaja_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.FromArgb(120, 144, 156);
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Image = Properties.Resources.ico_cancelar;
            btnLimpiar.ImageAlign = ContentAlignment.MiddleLeft;
            btnLimpiar.Location = new Point(612, 18);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Padding = new Padding(8, 0, 8, 0);
            btnLimpiar.Size = new Size(124, 38);
            btnLimpiar.TabIndex = 4;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(0, 0);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 0;
            btnCancelar.BackColor = Color.FromArgb(220, 38, 38);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 0;
            // 
            // FrmPacientes
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1080, 650);
            Controls.Add(dgvPacientes);
            Controls.Add(pnlCaptura);
            Controls.Add(pnlBusqueda);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmPacientes";
            Text = "Gestión de Pacientes";
            Load += FrmPacientes_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            pnlBusqueda.ResumeLayout(false);
            pnlBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPacientes).EndInit();
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
        private Label lblEstadoFiltro;
        private ComboBox cmbEstadoFiltro;
        private Button btnBuscar;
        private DataGridView dgvPacientes;
        private Panel pnlCaptura;
        private Label lblNombres;
        private TextBox txtNombres;
        private Label lblApellidos;
        private TextBox txtApellidos;
        private Label lblDui;
        private TextBox txtDui;
        private Label lblFechaNac;
        private DateTimePicker dtpFechaNac;
        private Label lblSexo;
        private ComboBox cmbSexo;
        private Label lblTelefono;
        private TextBox txtTelefono;
        private Label lblCorreo;
        private TextBox txtCorreo;
        private Label lblDireccion;
        private TextBox txtDireccion;
        private Label lblAlergias;
        private TextBox txtAlergias;
        private Label lblAntecedentes;
        private TextBox txtAntecedentes;
        private Label lblEstado;
        private ComboBox cmbEstado;
        private FlowLayoutPanel pnlBotones;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnModificar;
        private Button btnDarDeBaja;
        private Button btnCancelar;
        private Button btnLimpiar;
    }
}
