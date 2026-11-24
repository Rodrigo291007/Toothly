namespace Sistema_Toothly.Forms
{
    partial class FrmLogin
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
            pnlIzquierdo = new Panel();
            lblBienvenida = new Label();
            lblDescripcion = new Label();
            pnlDerecho = new Panel();
            picLogo = new PictureBox();
            lblLogoTexto = new Label();
            lblSubtitulo = new Label();
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            lblClave = new Label();
            txtClave = new TextBox();
            btnIngresar = new Button();
            btnSalirApp = new Button();
            pnlIzquierdo.SuspendLayout();
            pnlDerecho.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // pnlIzquierdo - panel azul decorativo a la izquierda
            // 
            pnlIzquierdo.BackColor = Color.FromArgb(108, 180, 234);
            pnlIzquierdo.Controls.Add(lblBienvenida);
            pnlIzquierdo.Controls.Add(lblDescripcion);
            pnlIzquierdo.Dock = DockStyle.Left;
            pnlIzquierdo.Location = new Point(0, 0);
            pnlIzquierdo.Name = "pnlIzquierdo";
            pnlIzquierdo.Size = new Size(260, 500);
            pnlIzquierdo.TabIndex = 0;
            // 
            // lblBienvenida
            // 
            lblBienvenida.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblBienvenida.ForeColor = Color.White;
            lblBienvenida.Location = new Point(25, 140);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(210, 80);
            lblBienvenida.TabIndex = 0;
            lblBienvenida.Text = "¡Bienvenido a Toothly!";
            // 
            // lblDescripcion
            // 
            lblDescripcion.Font = new Font("Segoe UI", 9.5F);
            lblDescripcion.ForeColor = Color.FromArgb(200, 220, 240);
            lblDescripcion.Location = new Point(25, 230);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(210, 80);
            lblDescripcion.TabIndex = 1;
            lblDescripcion.Text = "Sistema de Gestión Odontológica para el control integral de su clínica dental.";
            // 
            // pnlDerecho - panel blanco con formulario de login
            // 
            pnlDerecho.BackColor = Color.White;
            pnlDerecho.Controls.Add(picLogo);
            pnlDerecho.Controls.Add(lblLogoTexto);
            pnlDerecho.Controls.Add(lblSubtitulo);
            pnlDerecho.Controls.Add(lblUsuario);
            pnlDerecho.Controls.Add(txtUsuario);
            pnlDerecho.Controls.Add(lblClave);
            pnlDerecho.Controls.Add(txtClave);
            pnlDerecho.Controls.Add(btnIngresar);
            pnlDerecho.Controls.Add(btnSalirApp);
            pnlDerecho.Dock = DockStyle.Fill;
            pnlDerecho.Location = new Point(260, 0);
            pnlDerecho.Name = "pnlDerecho";
            pnlDerecho.Padding = new Padding(40, 30, 40, 30);
            pnlDerecho.Size = new Size(380, 500);
            pnlDerecho.TabIndex = 1;
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.Image = Properties.Resources.logo_azul;
            picLogo.Location = new Point(40, 25);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(300, 75);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            picLogo.Visible = true;
            // 
            // lblLogoTexto
            // 
            lblLogoTexto.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblLogoTexto.ForeColor = Color.FromArgb(8, 86, 140);
            lblLogoTexto.Location = new Point(40, 30);
            lblLogoTexto.Name = "lblLogoTexto";
            lblLogoTexto.Size = new Size(300, 55);
            lblLogoTexto.TabIndex = 1;
            lblLogoTexto.Text = "🦷 Toothly";
            lblLogoTexto.TextAlign = ContentAlignment.MiddleCenter;
            lblLogoTexto.Visible = false;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.Font = new Font("Segoe UI", 10F);
            lblSubtitulo.ForeColor = Color.FromArgb(96, 125, 139);
            lblSubtitulo.Location = new Point(40, 95);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(300, 25);
            lblSubtitulo.TabIndex = 2;
            lblSubtitulo.Text = "Inicie sesión para continuar";
            lblSubtitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblUsuario.ForeColor = Color.FromArgb(44, 62, 80);
            lblUsuario.Location = new Point(50, 160);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(59, 17);
            lblUsuario.TabIndex = 3;
            lblUsuario.Text = "Usuario";
            // 
            // txtUsuario
            // 
            txtUsuario.BorderStyle = BorderStyle.FixedSingle;
            txtUsuario.Font = new Font("Segoe UI", 11F);
            txtUsuario.Location = new Point(50, 182);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(280, 27);
            txtUsuario.TabIndex = 4;
            // 
            // lblClave
            // 
            lblClave.AutoSize = true;
            lblClave.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblClave.ForeColor = Color.FromArgb(44, 62, 80);
            lblClave.Location = new Point(50, 225);
            lblClave.Name = "lblClave";
            lblClave.Size = new Size(81, 17);
            lblClave.TabIndex = 5;
            lblClave.Text = "Contraseña";
            // 
            // txtClave
            // 
            txtClave.BorderStyle = BorderStyle.FixedSingle;
            txtClave.Font = new Font("Segoe UI", 11F);
            txtClave.Location = new Point(50, 247);
            txtClave.Name = "txtClave";
            txtClave.PasswordChar = '●';
            txtClave.Size = new Size(280, 27);
            txtClave.TabIndex = 6;
            // 
            // btnIngresar - Centrado horizontalmente
            // 
            btnIngresar.BackColor = Color.FromArgb(108, 180, 234);
            btnIngresar.FlatAppearance.BorderSize = 0;
            btnIngresar.FlatStyle = FlatStyle.Flat;
            btnIngresar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnIngresar.ForeColor = Color.White;
            btnIngresar.Image = Properties.Resources.ico_candado;
            btnIngresar.ImageAlign = ContentAlignment.MiddleLeft;
            btnIngresar.Location = new Point(90, 300);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Padding = new Padding(10, 0, 10, 0);
            btnIngresar.Size = new Size(200, 44);
            btnIngresar.TabIndex = 7;
            btnIngresar.Text = "  Iniciar Sesión";
            btnIngresar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnIngresar.UseVisualStyleBackColor = false;
            btnIngresar.Cursor = Cursors.Hand;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // btnSalirApp - Botón de apagado/salida total
            // 
            btnSalirApp.BackColor = Color.FromArgb(220, 38, 38);
            btnSalirApp.FlatAppearance.BorderSize = 0;
            btnSalirApp.FlatStyle = FlatStyle.Flat;
            btnSalirApp.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSalirApp.ForeColor = Color.White;
            btnSalirApp.Image = Properties.Resources.ico_salir;
            btnSalirApp.ImageAlign = ContentAlignment.MiddleLeft;
            btnSalirApp.Location = new Point(120, 370);
            btnSalirApp.Name = "btnSalirApp";
            btnSalirApp.Padding = new Padding(8, 0, 8, 0);
            btnSalirApp.Size = new Size(140, 38);
            btnSalirApp.TabIndex = 9;
            btnSalirApp.Text = "  Salir";
            btnSalirApp.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSalirApp.UseVisualStyleBackColor = false;
            btnSalirApp.Cursor = Cursors.Hand;
            btnSalirApp.Click += btnSalirApp_Click;
            // 
            // FrmLogin
            // 
            AcceptButton = btnIngresar;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(41, 128, 185);
            ClientSize = new Size(640, 500);
            Controls.Add(pnlDerecho);
            Controls.Add(pnlIzquierdo);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Toothly - Iniciar Sesión";
            Load += FrmLogin_Load;
            pnlIzquierdo.ResumeLayout(false);
            pnlDerecho.ResumeLayout(false);
            pnlDerecho.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlIzquierdo;
        private Label lblBienvenida;
        private Label lblDescripcion;
        private Panel pnlDerecho;
        private PictureBox picLogo;
        private Label lblLogoTexto;
        private Label lblSubtitulo;
        private Label lblUsuario;
        private TextBox txtUsuario;
        private Label lblClave;
        private TextBox txtClave;
        private Button btnIngresar;
        private Button btnSalirApp;
    }
}
