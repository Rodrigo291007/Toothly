namespace Sistema_Toothly.Forms
{
    partial class FrmAgenda
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
            pnlEncabezado = new System.Windows.Forms.Panel();
            lblTitulo = new System.Windows.Forms.Label();
            splPrincipal = new System.Windows.Forms.SplitContainer();
            CalendarMes = new System.Windows.Forms.MonthCalendar();
            pnlBotonesCalendario = new System.Windows.Forms.Panel();
            btnHoy = new System.Windows.Forms.Button();
            btnActualizar = new System.Windows.Forms.Button();
            pnlDerecha = new System.Windows.Forms.Panel();
            lblFechaSeleccionada = new System.Windows.Forms.Label();
            dgvCitasDia = new System.Windows.Forms.DataGridView();
            pnlResumen = new System.Windows.Forms.Panel();
            lblResumenDia = new System.Windows.Forms.Label();
            lblDetalle = new System.Windows.Forms.Label();
            pnlEncabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splPrincipal).BeginInit();
            splPrincipal.Panel1.SuspendLayout();
            splPrincipal.Panel2.SuspendLayout();
            splPrincipal.SuspendLayout();
            pnlBotonesCalendario.SuspendLayout();
            pnlDerecha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCitasDia).BeginInit();
            pnlResumen.SuspendLayout();
            SuspendLayout();
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(108, 180, 234);
            pnlEncabezado.Controls.Add(lblTitulo);
            pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new System.Drawing.Size(1200, 50);
            pnlEncabezado.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblTitulo.ForeColor = System.Drawing.Color.White;
            lblTitulo.Location = new System.Drawing.Point(20, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(240, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Consulta de Agenda";
            // 
            // splPrincipal
            // 
            splPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            splPrincipal.Location = new System.Drawing.Point(0, 50);
            splPrincipal.Name = "splPrincipal";
            splPrincipal.Size = new System.Drawing.Size(1200, 570);
            splPrincipal.SplitterDistance = 270;
            splPrincipal.TabIndex = 1;
            // 
            // splPrincipal.Panel1
            // 
            splPrincipal.Panel1.Controls.Add(CalendarMes);
            splPrincipal.Panel1.Controls.Add(pnlBotonesCalendario);
            splPrincipal.Panel1.BackColor = System.Drawing.Color.FromArgb(243, 249, 253);
            // 
            // splPrincipal.Panel2
            // 
            splPrincipal.Panel2.Controls.Add(pnlDerecha);
            // 
            // CalendarMes
            // 
            CalendarMes.Dock = System.Windows.Forms.DockStyle.Fill;
            CalendarMes.Location = new System.Drawing.Point(0, 0);
            CalendarMes.MaxSelectionCount = 1;
            CalendarMes.Name = "CalendarMes";
            CalendarMes.TabIndex = 0;
            CalendarMes.DateChanged += CalendarMes_DateChanged;
            // 
            // pnlBotonesCalendario
            // 
            pnlBotonesCalendario.Controls.Add(btnHoy);
            pnlBotonesCalendario.Controls.Add(btnActualizar);
            pnlBotonesCalendario.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlBotonesCalendario.Location = new System.Drawing.Point(0, 510);
            pnlBotonesCalendario.Name = "pnlBotonesCalendario";
            pnlBotonesCalendario.Size = new System.Drawing.Size(270, 60);
            pnlBotonesCalendario.TabIndex = 1;
            pnlBotonesCalendario.BackColor = System.Drawing.Color.FromArgb(243, 249, 253);
            // 
            // btnHoy
            // 
            btnHoy.Location = new System.Drawing.Point(10, 12);
            btnHoy.Name = "btnHoy";
            btnHoy.Size = new System.Drawing.Size(115, 36);
            btnHoy.TabIndex = 0;
            btnHoy.Text = "Hoy";
            btnHoy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnHoy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            btnHoy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnHoy.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            btnHoy.BackColor = Color.FromArgb(61, 143, 203);
            btnHoy.ForeColor = Color.White;
            btnHoy.FlatStyle = FlatStyle.Flat;
            btnHoy.FlatAppearance.BorderSize = 0;
            btnHoy.UseVisualStyleBackColor = false;
            btnHoy.Click += btnHoy_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new System.Drawing.Point(135, 12);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new System.Drawing.Size(125, 36);
            btnActualizar.TabIndex = 1;
            btnActualizar.Text = "Actualizar";
            btnActualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnActualizar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            btnActualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnActualizar.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            btnActualizar.BackColor = Color.FromArgb(96, 125, 139);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // pnlDerecha
            // 
            pnlDerecha.Controls.Add(dgvCitasDia);
            pnlDerecha.Controls.Add(lblFechaSeleccionada);
            pnlDerecha.Controls.Add(pnlResumen);
            pnlDerecha.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlDerecha.Location = new System.Drawing.Point(0, 0);
            pnlDerecha.Name = "pnlDerecha";
            pnlDerecha.Size = new System.Drawing.Size(926, 570);
            pnlDerecha.TabIndex = 0;
            // 
            // lblFechaSeleccionada
            // 
            lblFechaSeleccionada.Dock = System.Windows.Forms.DockStyle.Top;
            lblFechaSeleccionada.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblFechaSeleccionada.ForeColor = System.Drawing.Color.FromArgb(8, 86, 140);
            lblFechaSeleccionada.Location = new System.Drawing.Point(0, 0);
            lblFechaSeleccionada.Name = "lblFechaSeleccionada";
            lblFechaSeleccionada.Size = new System.Drawing.Size(926, 36);
            lblFechaSeleccionada.TabIndex = 0;
            lblFechaSeleccionada.Text = "Seleccione una fecha en el calendario";
            lblFechaSeleccionada.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblFechaSeleccionada.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            lblFechaSeleccionada.BackColor = System.Drawing.Color.FromArgb(236, 246, 255);
            // 
            // dgvCitasDia
            // 
            dgvCitasDia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCitasDia.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvCitasDia.Location = new System.Drawing.Point(0, 36);
            dgvCitasDia.Name = "dgvCitasDia";
            dgvCitasDia.ReadOnly = true;
            dgvCitasDia.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvCitasDia.MultiSelect = false;
            dgvCitasDia.AllowUserToAddRows = false;
            dgvCitasDia.Size = new System.Drawing.Size(926, 464);
            dgvCitasDia.TabIndex = 1;
            dgvCitasDia.SelectionChanged += dgvCitasDia_SelectionChanged;
            // 
            // pnlResumen
            // 
            pnlResumen.Controls.Add(lblResumenDia);
            pnlResumen.Controls.Add(lblDetalle);
            pnlResumen.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlResumen.Location = new System.Drawing.Point(0, 500);
            pnlResumen.Name = "pnlResumen";
            pnlResumen.Size = new System.Drawing.Size(926, 70);
            pnlResumen.TabIndex = 2;
            pnlResumen.BackColor = System.Drawing.Color.FromArgb(243, 249, 253);
            // 
            // lblResumenDia
            // 
            lblResumenDia.Dock = System.Windows.Forms.DockStyle.Top;
            lblResumenDia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblResumenDia.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblResumenDia.Location = new System.Drawing.Point(0, 0);
            lblResumenDia.Name = "lblResumenDia";
            lblResumenDia.Size = new System.Drawing.Size(926, 30);
            lblResumenDia.TabIndex = 0;
            lblResumenDia.Text = "Seleccione una fecha para ver las citas.";
            lblResumenDia.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            lblResumenDia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDetalle
            // 
            lblDetalle.Dock = System.Windows.Forms.DockStyle.Bottom;
            lblDetalle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            lblDetalle.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblDetalle.Location = new System.Drawing.Point(0, 40);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new System.Drawing.Size(926, 30);
            lblDetalle.TabIndex = 1;
            lblDetalle.Text = "";
            lblDetalle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            lblDetalle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmAgenda
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 249, 253);
            ClientSize = new System.Drawing.Size(1200, 620);
            Controls.Add(splPrincipal);
            Controls.Add(pnlEncabezado);
            Font = new System.Drawing.Font("Segoe UI", 10F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "FrmAgenda";
            Text = "Consulta de Agenda";
            Load += FrmAgenda_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            splPrincipal.Panel1.ResumeLayout(false);
            splPrincipal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splPrincipal).EndInit();
            splPrincipal.ResumeLayout(false);
            pnlBotonesCalendario.ResumeLayout(false);
            pnlDerecha.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCitasDia).EndInit();
            pnlResumen.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.SplitContainer splPrincipal;
        private System.Windows.Forms.MonthCalendar CalendarMes;
        private System.Windows.Forms.Panel pnlBotonesCalendario;
        private System.Windows.Forms.Button btnHoy;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Panel pnlDerecha;
        private System.Windows.Forms.Label lblFechaSeleccionada;
        private System.Windows.Forms.DataGridView dgvCitasDia;
        private System.Windows.Forms.Panel pnlResumen;
        private System.Windows.Forms.Label lblResumenDia;
        private System.Windows.Forms.Label lblDetalle;
    }
}
