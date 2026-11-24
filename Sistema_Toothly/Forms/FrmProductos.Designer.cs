namespace Sistema_Toothly.Forms
{
    partial class FrmProductos
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
            dgvProductos = new DataGridView();
            pnlCaptura = new Panel();
            lblCodigo = new Label();
            txtCodigo = new TextBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblCategoria = new Label();
            cmbCategoria = new ComboBox();
            lblStockActual = new Label();
            txtStockActual = new TextBox();
            lblStockMinimo = new Label();
            txtStockMinimo = new TextBox();
            lblPrecio = new Label();
            txtPrecio = new TextBox();
            lblUnidadesCaja = new Label();
            txtUnidadesCaja = new TextBox();
            lblPrecioVenta = new Label();
            txtPrecioVenta = new TextBox();
            lblVencimiento = new Label();
            dtpVencimiento = new DateTimePicker();
            pnlBotones = new FlowLayoutPanel();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnModificar = new Button();
            btnLimpiar = new Button();
            pnlEncabezado.SuspendLayout();
            pnlBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
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
            lblTitulo.Size = new Size(202, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión de Productos";
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
            pnlBusqueda.Size = new Size(1000, 50);
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
            // dgvProductos
            // 
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Dock = DockStyle.Fill;
            dgvProductos.Location = new Point(0, 100);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(1000, 270);
            dgvProductos.TabIndex = 2;
            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
            // 
            // pnlCaptura
            // 
            pnlCaptura.BackColor = Color.FromArgb(243, 249, 253);
            pnlCaptura.Controls.Add(lblCodigo);
            pnlCaptura.Controls.Add(txtCodigo);
            pnlCaptura.Controls.Add(lblNombre);
            pnlCaptura.Controls.Add(txtNombre);
            pnlCaptura.Controls.Add(lblCategoria);
            pnlCaptura.Controls.Add(cmbCategoria);
            pnlCaptura.Controls.Add(lblStockActual);
            pnlCaptura.Controls.Add(txtStockActual);
            pnlCaptura.Controls.Add(lblStockMinimo);
            pnlCaptura.Controls.Add(txtStockMinimo);
            pnlCaptura.Controls.Add(lblPrecio);
            pnlCaptura.Controls.Add(txtPrecio);
            pnlCaptura.Controls.Add(lblUnidadesCaja);
            pnlCaptura.Controls.Add(txtUnidadesCaja);
            pnlCaptura.Controls.Add(lblPrecioVenta);
            pnlCaptura.Controls.Add(txtPrecioVenta);
            pnlCaptura.Controls.Add(lblVencimiento);
            pnlCaptura.Controls.Add(dtpVencimiento);
            pnlCaptura.Controls.Add(pnlBotones);
            pnlCaptura.Dock = DockStyle.Bottom;
            pnlCaptura.Location = new Point(0, 370);
            pnlCaptura.Name = "pnlCaptura";
            pnlCaptura.Size = new Size(1000, 230);
            pnlCaptura.TabIndex = 3;
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Location = new Point(20, 15);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(56, 19);
            lblCodigo.TabIndex = 0;
            lblCodigo.Text = "Código:";
            // 
            // txtCodigo
            // 
            txtCodigo.Location = new Point(20, 37);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(120, 25);
            txtCodigo.TabIndex = 1;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(160, 15);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(146, 19);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre del Producto:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(160, 37);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(320, 25);
            txtNombre.TabIndex = 3;
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.Location = new Point(500, 15);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(71, 19);
            lblCategoria.TabIndex = 4;
            lblCategoria.Text = "Categoría:";
            // 
            // cmbCategoria
            // 
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.FormattingEnabled = true;
            cmbCategoria.Location = new Point(500, 37);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(200, 25);
            cmbCategoria.TabIndex = 5;
            // 
            // lblStockActual
            // 
            lblStockActual.AutoSize = true;
            lblStockActual.Location = new Point(20, 75);
            lblStockActual.Name = "lblStockActual";
            lblStockActual.Size = new Size(128, 19);
            lblStockActual.TabIndex = 6;
            lblStockActual.Text = "Stock Actual (und):";
            //
            // txtStockActual
            //
            txtStockActual.Location = new Point(20, 97);
            txtStockActual.Name = "txtStockActual";
            txtStockActual.Size = new Size(100, 25);
            txtStockActual.TabIndex = 7;
            //
            // lblStockMinimo
            //
            lblStockMinimo.AutoSize = true;
            lblStockMinimo.Location = new Point(165, 75);
            lblStockMinimo.Name = "lblStockMinimo";
            lblStockMinimo.Size = new Size(117, 19);
            lblStockMinimo.TabIndex = 8;
            lblStockMinimo.Text = "Stock Mín. (und):";
            //
            // txtStockMinimo
            //
            txtStockMinimo.Location = new Point(165, 97);
            txtStockMinimo.Name = "txtStockMinimo";
            txtStockMinimo.Size = new Size(100, 25);
            txtStockMinimo.TabIndex = 9;
            //
            // lblPrecio
            //
            lblPrecio.AutoSize = true;
            lblPrecio.Location = new Point(310, 75);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(105, 19);
            lblPrecio.TabIndex = 10;
            lblPrecio.Text = "Precio Caja ($):";
            //
            // txtPrecio
            //
            txtPrecio.Location = new Point(310, 97);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(110, 25);
            txtPrecio.TabIndex = 11;
            //
            // lblUnidadesCaja
            //
            lblUnidadesCaja.AutoSize = true;
            lblUnidadesCaja.Location = new Point(440, 75);
            lblUnidadesCaja.Name = "lblUnidadesCaja";
            lblUnidadesCaja.Size = new Size(95, 19);
            lblUnidadesCaja.TabIndex = 15;
            lblUnidadesCaja.Text = "Und. por Caja:";
            //
            // txtUnidadesCaja
            //
            txtUnidadesCaja.Location = new Point(440, 97);
            txtUnidadesCaja.Name = "txtUnidadesCaja";
            txtUnidadesCaja.Size = new Size(95, 25);
            txtUnidadesCaja.TabIndex = 16;
            txtUnidadesCaja.Text = "1";
            //
            // lblPrecioVenta
            //
            lblPrecioVenta.AutoSize = true;
            lblPrecioVenta.Location = new Point(555, 75);
            lblPrecioVenta.Name = "lblPrecioVenta";
            lblPrecioVenta.Size = new Size(148, 19);
            lblPrecioVenta.TabIndex = 17;
            lblPrecioVenta.Text = "Precio Venta Und. ($):";
            //
            // txtPrecioVenta
            //
            txtPrecioVenta.Location = new Point(555, 97);
            txtPrecioVenta.Name = "txtPrecioVenta";
            txtPrecioVenta.Size = new Size(110, 25);
            txtPrecioVenta.TabIndex = 18;
            txtPrecioVenta.Text = "0.00";
            //
            // lblVencimiento
            //
            lblVencimiento.AutoSize = true;
            lblVencimiento.Location = new Point(725, 75);
            lblVencimiento.Name = "lblVencimiento";
            lblVencimiento.Size = new Size(142, 19);
            lblVencimiento.TabIndex = 12;
            lblVencimiento.Text = "Fecha de Vencimiento:";
            //
            // dtpVencimiento
            //
            dtpVencimiento.Format = DateTimePickerFormat.Short;
            dtpVencimiento.Location = new Point(725, 97);
            dtpVencimiento.Name = "dtpVencimiento";
            dtpVencimiento.Size = new Size(140, 25);
            dtpVencimiento.TabIndex = 13;
            // 
            // pnlBotones
            // 
            pnlBotones.Controls.Add(btnNuevo);
            pnlBotones.Controls.Add(btnGuardar);
            pnlBotones.Controls.Add(btnModificar);
            pnlBotones.Controls.Add(btnLimpiar);
            pnlBotones.Dock = DockStyle.Bottom;
            pnlBotones.Location = new Point(0, 160);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Padding = new Padding(20, 10, 0, 0);
            pnlBotones.Size = new Size(1000, 70);
            pnlBotones.TabIndex = 14;
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
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(341, 13);
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
            // FrmProductos
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(dgvProductos);
            Controls.Add(pnlCaptura);
            Controls.Add(pnlBusqueda);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmProductos";
            Text = "Gestión de Productos";
            Load += FrmProductos_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            pnlBusqueda.ResumeLayout(false);
            pnlBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
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
        private DataGridView dgvProductos;
        private Panel pnlCaptura;
        private Label lblCodigo;
        private TextBox txtCodigo;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblCategoria;
        private ComboBox cmbCategoria;
        private Label lblStockActual;
        private TextBox txtStockActual;
        private Label lblStockMinimo;
        private TextBox txtStockMinimo;
        private Label lblPrecio;
        private TextBox txtPrecio;
        private Label lblUnidadesCaja;
        private TextBox txtUnidadesCaja;
        private Label lblPrecioVenta;
        private TextBox txtPrecioVenta;
        private Label lblVencimiento;
        private DateTimePicker dtpVencimiento;
        private FlowLayoutPanel pnlBotones;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnModificar;
        private Button btnLimpiar;
    }
}
