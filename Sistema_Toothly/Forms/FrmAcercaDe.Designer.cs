namespace Sistema_Toothly.Forms
{
    partial class FrmAcercaDe
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
            lblTituloEncabezado = new Label();
            picLogo = new PictureBox();
            lblNombreSistema = new Label();
            lblDescripcion = new Label();
            lblVersion = new Label();
            lblCarrera = new Label();
            lblEquipo = new Label();
            lblAnio = new Label();
            lblTecnologia = new Label();
            btnCerrar = new Button();
            pnlEncabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.BackColor = Color.FromArgb(108, 180, 234);
            pnlEncabezado.Controls.Add(lblTituloEncabezado);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(440, 45);
            pnlEncabezado.TabIndex = 0;
            // 
            // lblTituloEncabezado
            // 
            lblTituloEncabezado.AutoSize = true;
            lblTituloEncabezado.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTituloEncabezado.ForeColor = Color.White;
            lblTituloEncabezado.Location = new Point(20, 12);
            lblTituloEncabezado.Name = "lblTituloEncabezado";
            lblTituloEncabezado.Size = new Size(160, 21);
            lblTituloEncabezado.TabIndex = 0;
            lblTituloEncabezado.Text = "Acerca de Toothly";
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.Image = Properties.Resources.logo_azul;
            picLogo.Location = new Point(110, 58);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(220, 60);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 1;
            picLogo.TabStop = false;
            picLogo.Visible = true;
            // 
            // lblNombreSistema
            // 
            lblNombreSistema.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblNombreSistema.ForeColor = Color.FromArgb(108, 180, 234);
            lblNombreSistema.Location = new Point(10, 130);
            lblNombreSistema.Name = "lblNombreSistema";
            lblNombreSistema.Size = new Size(420, 30);
            lblNombreSistema.TabIndex = 2;
            lblNombreSistema.Text = "Toothly";
            lblNombreSistema.TextAlign = ContentAlignment.MiddleCenter;
            lblNombreSistema.Visible = false;
            // 
            // lblDescripcion
            // 
            lblDescripcion.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDescripcion.ForeColor = Color.FromArgb(8, 86, 140);
            lblDescripcion.Location = new Point(10, 130);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(420, 26);
            lblDescripcion.TabIndex = 3;
            lblDescripcion.Text = "Sistema de Gestión Odontológica";
            lblDescripcion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblVersion
            // 
            lblVersion.Font = new Font("Segoe UI", 9.5F);
            lblVersion.ForeColor = Color.FromArgb(70, 80, 90);
            lblVersion.Location = new Point(10, 164);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(420, 22);
            lblVersion.TabIndex = 4;
            lblVersion.Text = "Versión 1.0";
            lblVersion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCarrera
            // 
            lblCarrera.Font = new Font("Segoe UI", 9.5F);
            lblCarrera.ForeColor = Color.FromArgb(70, 80, 90);
            lblCarrera.Location = new Point(10, 196);
            lblCarrera.Name = "lblCarrera";
            lblCarrera.Size = new Size(420, 22);
            lblCarrera.TabIndex = 5;
            lblCarrera.Text = "Técnico en Ingeniería de Desarrollo de Software";
            lblCarrera.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblEquipo
            // 
            lblEquipo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblEquipo.ForeColor = Color.FromArgb(44, 62, 80);
            lblEquipo.Location = new Point(10, 228);
            lblEquipo.Name = "lblEquipo";
            lblEquipo.Size = new Size(420, 22);
            lblEquipo.TabIndex = 6;
            lblEquipo.Text = "Proyecto Académico DAE";
            lblEquipo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTecnologia
            // 
            lblTecnologia.Font = new Font("Segoe UI", 9F);
            lblTecnologia.ForeColor = Color.Gray;
            lblTecnologia.Location = new Point(10, 260);
            lblTecnologia.Name = "lblTecnologia";
            lblTecnologia.Size = new Size(420, 22);
            lblTecnologia.TabIndex = 7;
            lblTecnologia.Text = "Visual Studio | C# Windows Forms | .NET 10 | GDI+";
            lblTecnologia.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblAnio
            // 
            lblAnio.Font = new Font("Segoe UI", 8.5F);
            lblAnio.ForeColor = Color.Gray;
            lblAnio.Location = new Point(10, 290);
            lblAnio.Name = "lblAnio";
            lblAnio.Size = new Size(420, 20);
            lblAnio.TabIndex = 8;
            lblAnio.Text = "© 2026 Toothly Clinic Management";
            lblAnio.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.FromArgb(220, 38, 38);
            btnCerrar.Cursor = Cursors.Hand;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Image = Properties.Resources.ico_salir;
            btnCerrar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCerrar.Location = new Point(160, 332);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Padding = new Padding(8, 0, 8, 0);
            btnCerrar.Size = new Size(120, 38);
            btnCerrar.TabIndex = 9;
            btnCerrar.Text = "Cerrar";
            btnCerrar.TextAlign = ContentAlignment.MiddleCenter;
            btnCerrar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // FrmAcercaDe
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 249, 253);
            ClientSize = new Size(440, 395);
            Controls.Add(btnCerrar);
            Controls.Add(lblAnio);
            Controls.Add(lblTecnologia);
            Controls.Add(lblEquipo);
            Controls.Add(lblCarrera);
            Controls.Add(lblVersion);
            Controls.Add(lblDescripcion);
            Controls.Add(lblNombreSistema);
            Controls.Add(picLogo);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            Name = "FrmAcercaDe";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Acerca de Toothly";
            Load += FrmAcercaDe_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEncabezado;
        private Label lblTituloEncabezado;
        private PictureBox picLogo;
        private Label lblNombreSistema;
        private Label lblDescripcion;
        private Label lblVersion;
        private Label lblCarrera;
        private Label lblEquipo;
        private Label lblTecnologia;
        private Label lblAnio;
        private Button btnCerrar;
    }
}
