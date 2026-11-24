namespace Sistema_Toothly.Forms
{
    partial class FrmRolesPrivilegios
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
            lblRol = new Label();
            cmbRol = new ComboBox();
            grpPrivilegios = new GroupBox();
            chkReportes = new CheckBox();
            chkEliminar = new CheckBox();
            chkModificar = new CheckBox();
            chkConsultar = new CheckBox();
            chkInsertar = new CheckBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            pnlEncabezado.SuspendLayout();
            grpPrivilegios.SuspendLayout();
            SuspendLayout();
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.BackColor = Color.FromArgb(108, 180, 234);
            pnlEncabezado.Controls.Add(lblTitulo);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(600, 50);
            pnlEncabezado.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(20, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(244, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Roles y Privilegios";
            // 
            // lblRol
            // 
            lblRol.AutoSize = true;
            lblRol.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRol.Location = new Point(40, 75);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(130, 19);
            lblRol.TabIndex = 1;
            lblRol.Text = "Seleccione un Rol:";
            // 
            // cmbRol
            // 
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRol.FormattingEnabled = true;
            cmbRol.Location = new Point(40, 100);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(260, 25);
            cmbRol.TabIndex = 2;
            cmbRol.SelectedIndexChanged += cmbRol_SelectedIndexChanged;
            // 
            // grpPrivilegios
            // 
            grpPrivilegios.Controls.Add(chkReportes);
            grpPrivilegios.Controls.Add(chkEliminar);
            grpPrivilegios.Controls.Add(chkModificar);
            grpPrivilegios.Controls.Add(chkConsultar);
            grpPrivilegios.Controls.Add(chkInsertar);
            grpPrivilegios.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpPrivilegios.Location = new Point(40, 150);
            grpPrivilegios.Name = "grpPrivilegios";
            grpPrivilegios.Size = new Size(500, 220);
            grpPrivilegios.TabIndex = 3;
            grpPrivilegios.TabStop = false;
            grpPrivilegios.Text = "Privilegios Asignados";
            // 
            // chkReportes
            // 
            chkReportes.AutoSize = true;
            chkReportes.Font = new Font("Segoe UI", 10F);
            chkReportes.Location = new Point(30, 170);
            chkReportes.Name = "chkReportes";
            chkReportes.Size = new Size(147, 23);
            chkReportes.TabIndex = 4;
            chkReportes.Text = "Reportes (Informes)";
            chkReportes.UseVisualStyleBackColor = true;
            // 
            // chkEliminar
            // 
            chkEliminar.AutoSize = true;
            chkEliminar.Font = new Font("Segoe UI", 10F);
            chkEliminar.Location = new Point(30, 135);
            chkEliminar.Name = "chkEliminar";
            chkEliminar.Size = new Size(160, 23);
            chkEliminar.TabIndex = 3;
            chkEliminar.Text = "Eliminar / Dar de Baja";
            chkEliminar.UseVisualStyleBackColor = true;
            // 
            // chkModificar
            // 
            chkModificar.AutoSize = true;
            chkModificar.Font = new Font("Segoe UI", 10F);
            chkModificar.Location = new Point(30, 100);
            chkModificar.Name = "chkModificar";
            chkModificar.Size = new Size(143, 23);
            chkModificar.TabIndex = 2;
            chkModificar.Text = "Modificar Registros";
            chkModificar.UseVisualStyleBackColor = true;
            // 
            // chkConsultar
            // 
            chkConsultar.AutoSize = true;
            chkConsultar.Font = new Font("Segoe UI", 10F);
            chkConsultar.Location = new Point(30, 65);
            chkConsultar.Name = "chkConsultar";
            chkConsultar.Size = new Size(143, 23);
            chkConsultar.TabIndex = 1;
            chkConsultar.Text = "Consultar / Filtrar";
            chkConsultar.UseVisualStyleBackColor = true;
            // 
            // chkInsertar
            // 
            chkInsertar.AutoSize = true;
            chkInsertar.Font = new Font("Segoe UI", 10F);
            chkInsertar.Location = new Point(30, 30);
            chkInsertar.Name = "chkInsertar";
            chkInsertar.Size = new Size(174, 23);
            chkInsertar.TabIndex = 0;
            chkInsertar.Text = "Insertar Nuevos Registros";
            chkInsertar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(160, 390);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 38);
            btnGuardar.TabIndex = 4;
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
            btnCancelar.Location = new Point(300, 390);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(125, 38);
            btnCancelar.TabIndex = 5;
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
            // FrmRolesPrivilegios
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 249, 253);
            ClientSize = new Size(600, 460);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(grpPrivilegios);
            Controls.Add(cmbRol);
            Controls.Add(lblRol);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmRolesPrivilegios";
            Text = "Roles y Privilegios";
            Load += FrmRolesPrivilegios_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            grpPrivilegios.ResumeLayout(false);
            grpPrivilegios.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Label lblRol;
        private ComboBox cmbRol;
        private GroupBox grpPrivilegios;
        private CheckBox chkReportes;
        private CheckBox chkEliminar;
        private CheckBox chkModificar;
        private CheckBox chkConsultar;
        private CheckBox chkInsertar;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}
