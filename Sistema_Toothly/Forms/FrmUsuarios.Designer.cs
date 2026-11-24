namespace Sistema_Toothly.Forms
{
    partial class FrmUsuarios
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
            dgvUsuarios = new DataGridView();
            pnlCaptura = new Panel();
            lblNombreCompleto = new Label();
            txtNombreCompleto = new TextBox();
            lblNombreUsuario = new Label();
            txtNombreUsuario = new TextBox();
            lblCorreo = new Label();
            txtCorreo = new TextBox();
            lblClave = new Label();
            txtClave = new TextBox();
            lblRol = new Label();
            cmbRol = new ComboBox();
            lblEstado = new Label();
            cmbEstado = new ComboBox();
            pnlBotones = new FlowLayoutPanel();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnModificar = new Button();
            btnDarDeBaja = new Button();
            btnCancelar = new Button();
            btnLimpiar = new Button();
            pnlEncabezado.SuspendLayout();
            pnlBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
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
            lblTitulo.Size = new Size(198, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión de Usuarios";
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
            pnlBusqueda.Size = new Size(980, 50);
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
            // dgvUsuarios
            // 
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Dock = DockStyle.Fill;
            dgvUsuarios.Location = new Point(0, 100);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.Size = new Size(980, 290);
            dgvUsuarios.TabIndex = 2;
            dgvUsuarios.SelectionChanged += dgvUsuarios_SelectionChanged;
            // 
            // pnlCaptura
            // 
            pnlCaptura.BackColor = Color.FromArgb(243, 249, 253);
            pnlCaptura.Controls.Add(lblNombreCompleto);
            pnlCaptura.Controls.Add(txtNombreCompleto);
            pnlCaptura.Controls.Add(lblNombreUsuario);
            pnlCaptura.Controls.Add(txtNombreUsuario);
            pnlCaptura.Controls.Add(lblCorreo);
            pnlCaptura.Controls.Add(txtCorreo);
            pnlCaptura.Controls.Add(lblClave);
            pnlCaptura.Controls.Add(txtClave);
            pnlCaptura.Controls.Add(lblRol);
            pnlCaptura.Controls.Add(cmbRol);
            pnlCaptura.Controls.Add(lblEstado);
            pnlCaptura.Controls.Add(cmbEstado);
            pnlCaptura.Controls.Add(pnlBotones);
            pnlCaptura.Dock = DockStyle.Bottom;
            pnlCaptura.Location = new Point(0, 390);
            pnlCaptura.Name = "pnlCaptura";
            pnlCaptura.Size = new Size(980, 230);
            pnlCaptura.TabIndex = 3;
            // 
            // lblNombreCompleto
            // 
            lblNombreCompleto.AutoSize = true;
            lblNombreCompleto.Location = new Point(20, 15);
            lblNombreCompleto.Name = "lblNombreCompleto";
            lblNombreCompleto.Size = new Size(126, 19);
            lblNombreCompleto.TabIndex = 0;
            lblNombreCompleto.Text = "Nombre Completo:";
            // 
            // txtNombreCompleto
            // 
            txtNombreCompleto.Location = new Point(20, 37);
            txtNombreCompleto.Name = "txtNombreCompleto";
            txtNombreCompleto.Size = new Size(280, 25);
            txtNombreCompleto.TabIndex = 1;
            // 
            // lblNombreUsuario
            // 
            lblNombreUsuario.AutoSize = true;
            lblNombreUsuario.Location = new Point(320, 15);
            lblNombreUsuario.Name = "lblNombreUsuario";
            lblNombreUsuario.Size = new Size(113, 19);
            lblNombreUsuario.TabIndex = 2;
            lblNombreUsuario.Text = "Nombre Usuario:";
            // 
            // txtNombreUsuario
            // 
            txtNombreUsuario.Location = new Point(320, 37);
            txtNombreUsuario.Name = "txtNombreUsuario";
            txtNombreUsuario.Size = new Size(180, 25);
            txtNombreUsuario.TabIndex = 3;
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Location = new Point(520, 15);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(54, 19);
            lblCorreo.TabIndex = 4;
            lblCorreo.Text = "Correo:";
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(520, 37);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(240, 25);
            txtCorreo.TabIndex = 5;
            // 
            // lblClave
            // 
            lblClave.AutoSize = true;
            lblClave.Location = new Point(20, 75);
            lblClave.Name = "lblClave";
            lblClave.Size = new Size(82, 19);
            lblClave.TabIndex = 6;
            lblClave.Text = "Contraseña:";
            // 
            // txtClave
            // 
            txtClave.Location = new Point(20, 97);
            txtClave.Name = "txtClave";
            txtClave.PasswordChar = '●';
            txtClave.Size = new Size(280, 25);
            txtClave.TabIndex = 7;
            // 
            // lblRol
            // 
            lblRol.AutoSize = true;
            lblRol.Location = new Point(320, 75);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(31, 19);
            lblRol.TabIndex = 8;
            lblRol.Text = "Rol:";
            // 
            // cmbRol
            // 
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRol.FormattingEnabled = true;
            cmbRol.Location = new Point(320, 97);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(180, 25);
            cmbRol.TabIndex = 9;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(520, 75);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(53, 19);
            lblEstado.TabIndex = 10;
            lblEstado.Text = "Estado:";
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Items.AddRange(new object[] { "Activo", "Inactivo" });
            cmbEstado.Location = new Point(520, 97);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(140, 25);
            cmbEstado.TabIndex = 11;
            // 
            // pnlBotones
            // 
            pnlBotones.Controls.Add(btnNuevo);
            pnlBotones.Controls.Add(btnGuardar);
            pnlBotones.Controls.Add(btnModificar);
            pnlBotones.Controls.Add(btnDarDeBaja);
            pnlBotones.Controls.Add(btnLimpiar);
            pnlBotones.Dock = DockStyle.Bottom;
            pnlBotones.Location = new Point(0, 160);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Padding = new Padding(20, 10, 0, 0);
            pnlBotones.Size = new Size(980, 70);
            pnlBotones.TabIndex = 12;
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
            // btnDarDeBaja
            // 
            btnDarDeBaja.Location = new Point(341, 13);
            btnDarDeBaja.Name = "btnDarDeBaja";
            btnDarDeBaja.Size = new Size(145, 38);
            btnDarDeBaja.TabIndex = 3;
            btnDarDeBaja.Text = "Eliminar";
            btnDarDeBaja.UseVisualStyleBackColor = false;
            btnDarDeBaja.Click += btnDarDeBaja_Click;
            btnDarDeBaja.Image = Properties.Resources.ico_baja;
            btnDarDeBaja.ImageAlign = ContentAlignment.MiddleLeft;
            btnDarDeBaja.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDarDeBaja.Padding = new Padding(8, 0, 8, 0);
            btnDarDeBaja.FlatStyle = FlatStyle.Flat;
            btnDarDeBaja.FlatAppearance.BorderSize = 1;
            btnDarDeBaja.FlatAppearance.BorderColor = Color.FromArgb(248, 113, 113);
            btnDarDeBaja.BackColor = Color.FromArgb(220, 38, 38);
            btnDarDeBaja.ForeColor = Color.White;
            btnDarDeBaja.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDarDeBaja.Cursor = Cursors.Hand;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(457, 13);
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
            // FrmUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 620);
            Controls.Add(dgvUsuarios);
            Controls.Add(pnlCaptura);
            Controls.Add(pnlBusqueda);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmUsuarios";
            Text = "Gestión de Usuarios";
            Load += FrmUsuarios_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            pnlBusqueda.ResumeLayout(false);
            pnlBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
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
        private DataGridView dgvUsuarios;
        private Panel pnlCaptura;
        private Label lblNombreCompleto;
        private TextBox txtNombreCompleto;
        private Label lblNombreUsuario;
        private TextBox txtNombreUsuario;
        private Label lblCorreo;
        private TextBox txtCorreo;
        private Label lblClave;
        private TextBox txtClave;
        private Label lblRol;
        private ComboBox cmbRol;
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
