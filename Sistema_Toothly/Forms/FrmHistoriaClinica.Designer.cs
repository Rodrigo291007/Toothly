namespace Sistema_Toothly.Forms
{
    partial class FrmHistoriaClinica
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
            txtBuscarPaciente = new TextBox();
            btnBuscarPaciente = new Button();
            lblPacienteActual = new Label();
            pnlCampos = new Panel();
            lblOdontologo = new Label();
            cmbOdontologo = new ComboBox();
            lblFechaApertura = new Label();
            dtpFechaApertura = new DateTimePicker();
            lblMotivo = new Label();
            txtMotivo = new TextBox();
            lblAntecedentes = new Label();
            txtAntecedentes = new TextBox();
            lblObservacionesGenerales = new Label();
            txtObservacionesGenerales = new TextBox();
            lblDiagnostico = new Label();
            txtDiagnostico = new TextBox();
            lblPiezaDental = new Label();
            txtObservacionesClinicas = new TextBox();
            pnlBotones = new FlowLayoutPanel();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnModificar = new Button();
            btnLimpiar = new Button();
            btnImprimir = new Button();
            pnlEncabezado.SuspendLayout();
            pnlBusqueda.SuspendLayout();
            pnlCampos.SuspendLayout();
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
            lblTitulo.Size = new Size(150, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Historia Clínica";
            // 
            // pnlBusqueda
            // 
            pnlBusqueda.BackColor = Color.FromArgb(243, 249, 253);
            pnlBusqueda.Controls.Add(lblBuscar);
            pnlBusqueda.Controls.Add(txtBuscarPaciente);
            pnlBusqueda.Controls.Add(btnBuscarPaciente);
            pnlBusqueda.Controls.Add(lblPacienteActual);
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
            lblBuscar.Size = new Size(139, 19);
            lblBuscar.TabIndex = 0;
            lblBuscar.Text = "Buscar por Nom/DUI:";
            // 
            // txtBuscarPaciente
            // 
            txtBuscarPaciente.Location = new Point(165, 12);
            txtBuscarPaciente.Name = "txtBuscarPaciente";
            txtBuscarPaciente.Size = new Size(220, 25);
            txtBuscarPaciente.TabIndex = 1;
            // 
            // btnBuscarPaciente
            // 
            btnBuscarPaciente.Location = new Point(395, 7);
            btnBuscarPaciente.Name = "btnBuscarPaciente";
            btnBuscarPaciente.Size = new Size(115, 34);
            btnBuscarPaciente.TabIndex = 2;
            btnBuscarPaciente.Text = "Buscar";
            btnBuscarPaciente.UseVisualStyleBackColor = false;
            btnBuscarPaciente.Click += btnBuscarPaciente_Click;
            btnBuscarPaciente.Image = Properties.Resources.ico_buscar;
            btnBuscarPaciente.ImageAlign = ContentAlignment.MiddleLeft;
            btnBuscarPaciente.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnBuscarPaciente.Padding = new Padding(8, 0, 8, 0);
            btnBuscarPaciente.FlatStyle = FlatStyle.Flat;
            btnBuscarPaciente.FlatAppearance.BorderSize = 1;
            btnBuscarPaciente.FlatAppearance.BorderColor = Color.FromArgb(167, 139, 250);
            btnBuscarPaciente.BackColor = Color.FromArgb(61, 143, 203);
            btnBuscarPaciente.ForeColor = Color.White;
            btnBuscarPaciente.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnBuscarPaciente.Cursor = Cursors.Hand;
            // 
            // lblPacienteActual
            // 
            lblPacienteActual.AutoSize = true;
            lblPacienteActual.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblPacienteActual.ForeColor = Color.FromArgb(47, 59, 69);
            lblPacienteActual.Location = new Point(520, 15);
            lblPacienteActual.Name = "lblPacienteActual";
            lblPacienteActual.Size = new Size(230, 19);
            lblPacienteActual.TabIndex = 3;
            lblPacienteActual.Text = "Paciente: Ninguno seleccionado";
            // 
            // pnlCampos
            // 
            pnlCampos.AutoScroll = true;
            pnlCampos.BackColor = Color.White;
            pnlCampos.Controls.Add(lblOdontologo);
            pnlCampos.Controls.Add(cmbOdontologo);
            pnlCampos.Controls.Add(lblFechaApertura);
            pnlCampos.Controls.Add(dtpFechaApertura);
            pnlCampos.Controls.Add(lblMotivo);
            pnlCampos.Controls.Add(txtMotivo);
            pnlCampos.Controls.Add(lblAntecedentes);
            pnlCampos.Controls.Add(txtAntecedentes);
            pnlCampos.Controls.Add(lblObservacionesGenerales);
            pnlCampos.Controls.Add(txtObservacionesGenerales);
            pnlCampos.Controls.Add(lblDiagnostico);
            pnlCampos.Controls.Add(txtDiagnostico);
            pnlCampos.Controls.Add(lblPiezaDental);
            pnlCampos.Controls.Add(txtObservacionesClinicas);
            pnlCampos.Dock = DockStyle.Fill;
            pnlCampos.Location = new Point(0, 100);
            pnlCampos.Name = "pnlCampos";
            pnlCampos.Size = new Size(1000, 480);
            pnlCampos.TabIndex = 2;
            // 
            // lblOdontologo
            // 
            lblOdontologo.AutoSize = true;
            lblOdontologo.Location = new Point(20, 15);
            lblOdontologo.Name = "lblOdontologo";
            lblOdontologo.Size = new Size(140, 19);
            lblOdontologo.TabIndex = 0;
            lblOdontologo.Text = "Odontólogo Tratante:";
            // 
            // cmbOdontologo
            // 
            cmbOdontologo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOdontologo.FormattingEnabled = true;
            cmbOdontologo.Location = new Point(20, 37);
            cmbOdontologo.Name = "cmbOdontologo";
            cmbOdontologo.Size = new Size(260, 25);
            cmbOdontologo.TabIndex = 1;
            // 
            // lblFechaApertura
            // 
            lblFechaApertura.AutoSize = true;
            lblFechaApertura.Location = new Point(310, 15);
            lblFechaApertura.Name = "lblFechaApertura";
            lblFechaApertura.Size = new Size(125, 19);
            lblFechaApertura.TabIndex = 2;
            lblFechaApertura.Text = "Fecha de Apertura:";
            // 
            // dtpFechaApertura
            // 
            dtpFechaApertura.Format = DateTimePickerFormat.Short;
            dtpFechaApertura.Location = new Point(310, 37);
            dtpFechaApertura.Name = "dtpFechaApertura";
            dtpFechaApertura.Size = new Size(130, 25);
            dtpFechaApertura.TabIndex = 3;
            // 
            // lblMotivo
            // 
            lblMotivo.AutoSize = true;
            lblMotivo.Location = new Point(470, 15);
            lblMotivo.Name = "lblMotivo";
            lblMotivo.Size = new Size(130, 19);
            lblMotivo.TabIndex = 4;
            lblMotivo.Text = "Motivo de Consulta:";
            // 
            // txtMotivo
            // 
            txtMotivo.Location = new Point(470, 37);
            txtMotivo.Name = "txtMotivo";
            txtMotivo.Size = new Size(490, 25);
            txtMotivo.TabIndex = 5;
            // 
            // lblAntecedentes
            // 
            lblAntecedentes.AutoSize = true;
            lblAntecedentes.Location = new Point(20, 75);
            lblAntecedentes.Name = "lblAntecedentes";
            lblAntecedentes.Size = new Size(157, 19);
            lblAntecedentes.TabIndex = 6;
            lblAntecedentes.Text = "Antecedentes Médicos:";
            // 
            // txtAntecedentes
            // 
            txtAntecedentes.Location = new Point(20, 97);
            txtAntecedentes.Multiline = true;
            txtAntecedentes.Name = "txtAntecedentes";
            txtAntecedentes.ScrollBars = ScrollBars.Vertical;
            txtAntecedentes.Size = new Size(450, 60);
            txtAntecedentes.TabIndex = 7;
            // 
            // lblObservacionesGenerales
            // 
            lblObservacionesGenerales.AutoSize = true;
            lblObservacionesGenerales.Location = new Point(500, 75);
            lblObservacionesGenerales.Name = "lblObservacionesGenerales";
            lblObservacionesGenerales.Size = new Size(166, 19);
            lblObservacionesGenerales.TabIndex = 8;
            lblObservacionesGenerales.Text = "Observaciones Generales:";
            // 
            // txtObservacionesGenerales
            // 
            txtObservacionesGenerales.Location = new Point(500, 97);
            txtObservacionesGenerales.Multiline = true;
            txtObservacionesGenerales.Name = "txtObservacionesGenerales";
            txtObservacionesGenerales.ScrollBars = ScrollBars.Vertical;
            txtObservacionesGenerales.Size = new Size(460, 60);
            txtObservacionesGenerales.TabIndex = 9;
            // 
            // lblDiagnostico
            // 
            lblDiagnostico.AutoSize = true;
            lblDiagnostico.Location = new Point(20, 170);
            lblDiagnostico.Name = "lblDiagnostico";
            lblDiagnostico.Size = new Size(204, 19);
            lblDiagnostico.TabIndex = 10;
            lblDiagnostico.Text = "Diagnóstico (Solo Odontólogo):";
            // 
            // txtDiagnostico
            // 
            txtDiagnostico.Location = new Point(20, 192);
            txtDiagnostico.Multiline = true;
            txtDiagnostico.Name = "txtDiagnostico";
            txtDiagnostico.ScrollBars = ScrollBars.Vertical;
            txtDiagnostico.Size = new Size(940, 60);
            txtDiagnostico.TabIndex = 11;
            // 
            // lblPiezaDental
            // 
            lblPiezaDental.AutoSize = true;
            lblPiezaDental.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPiezaDental.Location = new Point(20, 265);
            lblPiezaDental.Name = "lblPiezaDental";
            lblPiezaDental.Size = new Size(330, 19);
            lblPiezaDental.TabIndex = 12;
            lblPiezaDental.Text = "Observaciones Clínicas por Pieza Dental / Trabajos:";
            // 
            // txtObservacionesClinicas
            // 
            txtObservacionesClinicas.Location = new Point(20, 287);
            txtObservacionesClinicas.Multiline = true;
            txtObservacionesClinicas.Name = "txtObservacionesClinicas";
            txtObservacionesClinicas.ScrollBars = ScrollBars.Vertical;
            txtObservacionesClinicas.Size = new Size(940, 120);
            txtObservacionesClinicas.TabIndex = 13;
            // 
            // pnlBotones
            // 
            pnlBotones.BackColor = Color.FromArgb(243, 249, 253);
            pnlBotones.Controls.Add(btnNuevo);
            pnlBotones.Controls.Add(btnGuardar);
            pnlBotones.Controls.Add(btnModificar);
            pnlBotones.Controls.Add(btnLimpiar);
            pnlBotones.Controls.Add(btnImprimir);
            pnlBotones.Dock = DockStyle.Bottom;
            pnlBotones.Location = new Point(0, 580);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Padding = new Padding(20, 12, 0, 0);
            pnlBotones.Size = new Size(1000, 60);
            pnlBotones.TabIndex = 3;
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
            // btnModificar
            // 
            btnModificar.Location = new Point(235, 15);
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
            // btnImprimir
            // 
            btnImprimir.Location = new Point(462, 15);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(130, 38);
            btnImprimir.TabIndex = 4;
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
            // FrmHistoriaClinica
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 640);
            Controls.Add(pnlCampos);
            Controls.Add(pnlBotones);
            Controls.Add(pnlBusqueda);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmHistoriaClinica";
            Text = "Historia Clínica";
            Load += FrmHistoriaClinica_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            pnlBusqueda.ResumeLayout(false);
            pnlBusqueda.PerformLayout();
            pnlCampos.ResumeLayout(false);
            pnlCampos.PerformLayout();
            pnlBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Panel pnlBusqueda;
        private Label lblBuscar;
        private TextBox txtBuscarPaciente;
        private Button btnBuscarPaciente;
        private Label lblPacienteActual;
        private Panel pnlCampos;
        private Label lblOdontologo;
        private ComboBox cmbOdontologo;
        private Label lblFechaApertura;
        private DateTimePicker dtpFechaApertura;
        private Label lblMotivo;
        private TextBox txtMotivo;
        private Label lblAntecedentes;
        private TextBox txtAntecedentes;
        private Label lblObservacionesGenerales;
        private TextBox txtObservacionesGenerales;
        private Label lblDiagnostico;
        private TextBox txtDiagnostico;
        private Label lblPiezaDental;
        private TextBox txtObservacionesClinicas;
        private FlowLayoutPanel pnlBotones;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnModificar;
        private Button btnLimpiar;
        private Button btnImprimir;
    }
}
