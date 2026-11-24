namespace Sistema_Toothly.Forms
{
    partial class FrmCambiarClave
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
            lblClaveActual = new Label();
            txtClaveActual = new TextBox();
            lblClaveNueva = new Label();
            txtClaveNueva = new TextBox();
            lblConfirmar = new Label();
            txtConfirmar = new TextBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            pnlEncabezado.SuspendLayout();
            SuspendLayout();
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.BackColor = Color.FromArgb(108, 180, 234);
            pnlEncabezado.Controls.Add(lblTitulo);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(384, 50);
            pnlEncabezado.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(163, 21);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Cambiar Contraseña";
            // 
            // lblClaveActual
            // 
            lblClaveActual.AutoSize = true;
            lblClaveActual.Location = new Point(30, 75);
            lblClaveActual.Name = "lblClaveActual";
            lblClaveActual.Size = new Size(125, 19);
            lblClaveActual.TabIndex = 1;
            lblClaveActual.Text = "Contraseña Actual:";
            // 
            // txtClaveActual
            // 
            txtClaveActual.Location = new Point(30, 98);
            txtClaveActual.Name = "txtClaveActual";
            txtClaveActual.PasswordChar = '●';
            txtClaveActual.Size = new Size(320, 25);
            txtClaveActual.TabIndex = 2;
            // 
            // lblClaveNueva
            // 
            lblClaveNueva.AutoSize = true;
            lblClaveNueva.Location = new Point(30, 135);
            lblClaveNueva.Name = "lblClaveNueva";
            lblClaveNueva.Size = new Size(128, 19);
            lblClaveNueva.TabIndex = 3;
            lblClaveNueva.Text = "Nueva Contraseña:";
            // 
            // txtClaveNueva
            // 
            txtClaveNueva.Location = new Point(30, 158);
            txtClaveNueva.Name = "txtClaveNueva";
            txtClaveNueva.PasswordChar = '●';
            txtClaveNueva.Size = new Size(320, 25);
            txtClaveNueva.TabIndex = 4;
            // 
            // lblConfirmar
            // 
            lblConfirmar.AutoSize = true;
            lblConfirmar.Location = new Point(30, 195);
            lblConfirmar.Name = "lblConfirmar";
            lblConfirmar.Size = new Size(149, 19);
            lblConfirmar.TabIndex = 5;
            lblConfirmar.Text = "Confirmar Contraseña:";
            // 
            // txtConfirmar
            // 
            txtConfirmar.Location = new Point(30, 218);
            txtConfirmar.Name = "txtConfirmar";
            txtConfirmar.PasswordChar = '●';
            txtConfirmar.Size = new Size(320, 25);
            txtConfirmar.TabIndex = 6;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(70, 270);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 38);
            btnGuardar.TabIndex = 7;
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
            // btnCancelar
            // 
            btnCancelar.Location = new Point(200, 270);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(125, 38);
            btnCancelar.TabIndex = 8;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            btnCancelar.Image = Properties.Resources.ico_cancelar;
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelar.Padding = new Padding(8, 0, 8, 0);
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 1;
            btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(248, 113, 113);
            btnCancelar.BackColor = Color.FromArgb(220, 38, 38);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCancelar.Cursor = Cursors.Hand;
            // 
            // FrmCambiarClave
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 249, 253);
            ClientSize = new Size(384, 330);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(txtConfirmar);
            Controls.Add(lblConfirmar);
            Controls.Add(txtClaveNueva);
            Controls.Add(lblClaveNueva);
            Controls.Add(txtClaveActual);
            Controls.Add(lblClaveActual);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmCambiarClave";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cambiar Contraseña";
            Load += FrmCambiarClave_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Label lblClaveActual;
        private TextBox txtClaveActual;
        private Label lblClaveNueva;
        private TextBox txtClaveNueva;
        private Label lblConfirmar;
        private TextBox txtConfirmar;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}
