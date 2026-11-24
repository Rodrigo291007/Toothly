namespace Sistema_Toothly.Forms
{
    partial class FrmOrdenesCompra
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
            splOrdenes = new SplitContainer();
            dgvOrdenes = new DataGridView();
            pnlListaHeader = new Panel();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            pnlDetalle = new Panel();
            lblProveedor = new Label();
            cmbProveedor = new ComboBox();
            lblFecha = new Label();
            dtpFecha = new DateTimePicker();
            lblTotal = new Label();
            grpProductos = new GroupBox();
            dgvDetalle = new DataGridView();
            pnlAgregarProd = new Panel();
            lblProd = new Label();
            cmbProducto = new ComboBox();
            lblCant = new Label();
            txtCantidad = new TextBox();
            lblPrecioU = new Label();
            txtPrecioUnitario = new TextBox();
            btnAgregarProd = new Button();
            btnQuitarProd = new Button();
            pnlBotones = new FlowLayoutPanel();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnRecibirOrden = new Button();
            btnLimpiar = new Button();
            pnlEncabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splOrdenes).BeginInit();
            splOrdenes.Panel1.SuspendLayout();
            splOrdenes.Panel2.SuspendLayout();
            splOrdenes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrdenes).BeginInit();
            pnlListaHeader.SuspendLayout();
            pnlDetalle.SuspendLayout();
            grpProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            pnlAgregarProd.SuspendLayout();
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
            lblTitulo.Size = new Size(191, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Órdenes de Compra";
            // 
            // splOrdenes
            // 
            splOrdenes.Dock = DockStyle.Fill;
            splOrdenes.Location = new Point(0, 50);
            splOrdenes.Name = "splOrdenes";
            // 
            // splOrdenes.Panel1
            // 
            splOrdenes.Panel1.Controls.Add(dgvOrdenes);
            splOrdenes.Panel1.Controls.Add(pnlListaHeader);
            // 
            // splOrdenes.Panel2
            // 
            splOrdenes.Panel2.Controls.Add(pnlDetalle);
            splOrdenes.Panel2.Controls.Add(pnlBotones);
            splOrdenes.Size = new Size(1020, 570);
            splOrdenes.SplitterDistance = 420;
            splOrdenes.TabIndex = 1;
            // 
            // dgvOrdenes
            // 
            dgvOrdenes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrdenes.Dock = DockStyle.Fill;
            dgvOrdenes.Location = new Point(0, 50);
            dgvOrdenes.Name = "dgvOrdenes";
            dgvOrdenes.Size = new Size(420, 520);
            dgvOrdenes.TabIndex = 1;
            dgvOrdenes.SelectionChanged += dgvOrdenes_SelectionChanged;
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
            pnlDetalle.Controls.Add(lblProveedor);
            pnlDetalle.Controls.Add(cmbProveedor);
            pnlDetalle.Controls.Add(lblFecha);
            pnlDetalle.Controls.Add(dtpFecha);
            pnlDetalle.Controls.Add(lblTotal);
            pnlDetalle.Controls.Add(grpProductos);
            pnlDetalle.Dock = DockStyle.Fill;
            pnlDetalle.Location = new Point(0, 0);
            pnlDetalle.Name = "pnlDetalle";
            pnlDetalle.Size = new Size(596, 510);
            pnlDetalle.TabIndex = 0;
            // 
            // lblProveedor
            // 
            lblProveedor.AutoSize = true;
            lblProveedor.Location = new Point(20, 15);
            lblProveedor.Name = "lblProveedor";
            lblProveedor.Size = new Size(75, 19);
            lblProveedor.TabIndex = 0;
            lblProveedor.Text = "Proveedor:";
            // 
            // cmbProveedor
            // 
            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProveedor.FormattingEnabled = true;
            cmbProveedor.Location = new Point(20, 37);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new Size(260, 25);
            cmbProveedor.TabIndex = 1;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(300, 15);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(47, 19);
            lblFecha.TabIndex = 2;
            lblFecha.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(300, 37);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(120, 25);
            dtpFecha.TabIndex = 3;
            // 
            // lblTotal
            // 
            lblTotal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(61, 143, 203);
            lblTotal.Location = new Point(430, 20);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(150, 45);
            lblTotal.TabIndex = 4;
            lblTotal.Text = "Total: $0.00";
            lblTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // grpProductos
            // 
            grpProductos.Controls.Add(dgvDetalle);
            grpProductos.Controls.Add(pnlAgregarProd);
            grpProductos.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grpProductos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpProductos.Location = new Point(20, 80);
            grpProductos.Name = "grpProductos";
            grpProductos.Size = new Size(550, 410);
            grpProductos.TabIndex = 5;
            grpProductos.TabStop = false;
            grpProductos.Text = "Productos de la Orden";
            // 
            // dgvDetalle
            // 
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.Dock = DockStyle.Fill;
            dgvDetalle.Location = new Point(3, 20);
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.Size = new Size(544, 277);
            dgvDetalle.TabIndex = 0;
            // 
            // pnlAgregarProd
            // 
            pnlAgregarProd.BackColor = Color.FromArgb(243, 249, 253);
            pnlAgregarProd.Controls.Add(lblProd);
            pnlAgregarProd.Controls.Add(cmbProducto);
            pnlAgregarProd.Controls.Add(lblCant);
            pnlAgregarProd.Controls.Add(txtCantidad);
            pnlAgregarProd.Controls.Add(lblPrecioU);
            pnlAgregarProd.Controls.Add(txtPrecioUnitario);
            pnlAgregarProd.Controls.Add(btnAgregarProd);
            pnlAgregarProd.Controls.Add(btnQuitarProd);
            pnlAgregarProd.Dock = DockStyle.Bottom;
            pnlAgregarProd.Font = new Font("Segoe UI", 9F);
            pnlAgregarProd.Location = new Point(3, 297);
            pnlAgregarProd.Name = "pnlAgregarProd";
            pnlAgregarProd.Size = new Size(544, 110);
            pnlAgregarProd.TabIndex = 1;
            // 
            // lblProd
            // 
            lblProd.AutoSize = true;
            lblProd.Location = new Point(10, 8);
            lblProd.Name = "lblProd";
            lblProd.Size = new Size(59, 15);
            lblProd.TabIndex = 0;
            lblProd.Text = "Producto:";
            // 
            // cmbProducto
            // 
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducto.FormattingEnabled = true;
            cmbProducto.Location = new Point(10, 26);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(240, 23);
            cmbProducto.TabIndex = 1;
            cmbProducto.SelectedIndexChanged += cmbProducto_SelectedIndexChanged;
            // 
            // lblCant
            // 
            lblCant.AutoSize = true;
            lblCant.Location = new Point(260, 8);
            lblCant.Name = "lblCant";
            lblCant.Size = new Size(98, 15);
            lblCant.TabIndex = 2;
            lblCant.Text = "Cantidad (cajas):";
            // 
            // txtCantidad
            // 
            txtCantidad.Location = new Point(260, 26);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.PlaceholderText = "1";
            txtCantidad.Size = new Size(80, 23);
            txtCantidad.TabIndex = 3;
            // 
            // lblPrecioU
            // 
            lblPrecioU.AutoSize = true;
            lblPrecioU.Location = new Point(375, 8);
            lblPrecioU.Name = "lblPrecioU";
            lblPrecioU.Size = new Size(95, 15);
            lblPrecioU.TabIndex = 4;
            lblPrecioU.Text = "Precio Caja ($):";
            // 
            // txtPrecioUnitario
            // 
            txtPrecioUnitario.Location = new Point(375, 26);
            txtPrecioUnitario.Name = "txtPrecioUnitario";
            txtPrecioUnitario.PlaceholderText = "0.00";
            txtPrecioUnitario.Size = new Size(100, 23);
            txtPrecioUnitario.TabIndex = 5;
            // 
            // btnAgregarProd
            // 
            btnAgregarProd.Location = new Point(260, 65);
            btnAgregarProd.Name = "btnAgregarProd";
            btnAgregarProd.Size = new Size(125, 34);
            btnAgregarProd.TabIndex = 6;
            btnAgregarProd.Text = "+ Agregar";
            btnAgregarProd.UseVisualStyleBackColor = false;
            btnAgregarProd.Click += btnAgregarProd_Click;
            btnAgregarProd.Image = Properties.Resources.ico_nuevo;
            btnAgregarProd.ImageAlign = ContentAlignment.MiddleLeft;
            btnAgregarProd.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAgregarProd.Padding = new Padding(8, 0, 8, 0);
            btnAgregarProd.FlatStyle = FlatStyle.Flat;
            btnAgregarProd.FlatAppearance.BorderSize = 1;
            btnAgregarProd.FlatAppearance.BorderColor = Color.FromArgb(52, 211, 153);
            btnAgregarProd.BackColor = Color.FromArgb(46, 125, 50);
            btnAgregarProd.ForeColor = Color.White;
            btnAgregarProd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAgregarProd.Cursor = Cursors.Hand;
            // 
            // btnQuitarProd
            // 
            btnQuitarProd.Location = new Point(395, 65);
            btnQuitarProd.Name = "btnQuitarProd";
            btnQuitarProd.Size = new Size(125, 34);
            btnQuitarProd.TabIndex = 7;
            btnQuitarProd.Text = "- Quitar";
            btnQuitarProd.UseVisualStyleBackColor = false;
            btnQuitarProd.Click += btnQuitarProd_Click;
            btnQuitarProd.Image = Properties.Resources.ico_baja;
            btnQuitarProd.ImageAlign = ContentAlignment.MiddleLeft;
            btnQuitarProd.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnQuitarProd.Padding = new Padding(8, 0, 8, 0);
            btnQuitarProd.FlatStyle = FlatStyle.Flat;
            btnQuitarProd.FlatAppearance.BorderSize = 1;
            btnQuitarProd.FlatAppearance.BorderColor = Color.FromArgb(248, 113, 113);
            btnQuitarProd.BackColor = Color.FromArgb(220, 38, 38);
            btnQuitarProd.ForeColor = Color.White;
            btnQuitarProd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnQuitarProd.Cursor = Cursors.Hand;
            // 
            // pnlBotones
            // 
            pnlBotones.BackColor = Color.FromArgb(243, 249, 253);
            pnlBotones.Controls.Add(btnNuevo);
            pnlBotones.Controls.Add(btnGuardar);
            pnlBotones.Controls.Add(btnRecibirOrden);
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
            // btnRecibirOrden
            // 
            btnRecibirOrden.Location = new Point(235, 15);
            btnRecibirOrden.Name = "btnRecibirOrden";
            btnRecibirOrden.Size = new Size(145, 38);
            btnRecibirOrden.TabIndex = 2;
            btnRecibirOrden.Text = "Recibir Orden";
            btnRecibirOrden.UseVisualStyleBackColor = false;
            btnRecibirOrden.Click += btnRecibirOrden_Click;
            btnRecibirOrden.Image = Properties.Resources.ico_orden;
            btnRecibirOrden.ImageAlign = ContentAlignment.MiddleLeft;
            btnRecibirOrden.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRecibirOrden.Padding = new Padding(8, 0, 8, 0);
            btnRecibirOrden.FlatStyle = FlatStyle.Flat;
            btnRecibirOrden.FlatAppearance.BorderSize = 1;
            btnRecibirOrden.FlatAppearance.BorderColor = Color.FromArgb(52, 211, 153);
            btnRecibirOrden.BackColor = Color.FromArgb(0, 150, 136);
            btnRecibirOrden.ForeColor = Color.White;
            btnRecibirOrden.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRecibirOrden.Cursor = Cursors.Hand;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(361, 15);
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
            // FrmOrdenesCompra
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1020, 620);
            Controls.Add(splOrdenes);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmOrdenesCompra";
            Text = "Órdenes de Compra";
            Load += FrmOrdenesCompra_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            splOrdenes.Panel1.ResumeLayout(false);
            splOrdenes.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splOrdenes).EndInit();
            splOrdenes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOrdenes).EndInit();
            pnlListaHeader.ResumeLayout(false);
            pnlListaHeader.PerformLayout();
            pnlDetalle.ResumeLayout(false);
            pnlDetalle.PerformLayout();
            grpProductos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            pnlAgregarProd.ResumeLayout(false);
            pnlAgregarProd.PerformLayout();
            pnlBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEncabezado;
        private Label lblTitulo;
        private SplitContainer splOrdenes;
        private DataGridView dgvOrdenes;
        private Panel pnlListaHeader;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Panel pnlDetalle;
        private Label lblProveedor;
        private ComboBox cmbProveedor;
        private Label lblFecha;
        private DateTimePicker dtpFecha;
        private Label lblTotal;
        private GroupBox grpProductos;
        private DataGridView dgvDetalle;
        private Panel pnlAgregarProd;
        private Label lblProd;
        private ComboBox cmbProducto;
        private Label lblCant;
        private TextBox txtCantidad;
        private Label lblPrecioU;
        private TextBox txtPrecioUnitario;
        private Button btnAgregarProd;
        private Button btnQuitarProd;
        private FlowLayoutPanel pnlBotones;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnRecibirOrden;
        private Button btnLimpiar;
    }
}
