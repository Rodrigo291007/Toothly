namespace Sistema_Toothly.Forms
{
    partial class FrmPrincipal
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
            pnlSuperior = new Panel();
            picLogoSuperior = new PictureBox();
            lblTituloClinica = new Label();
            pnlUsuarioCard = new Panel();
            lblInfoUsuarioSuperior = new Label();
            pnlLogoCard = new Panel();
            mnuPrincipal = new MenuStrip();
            mnuArchivo = new ToolStripMenuItem();
            mnuArchivoIniciarSesion = new ToolStripMenuItem();
            mnuArchivoCerrarSesion = new ToolStripMenuItem();
            mnuArchivoCambiarClave = new ToolStripMenuItem();
            mnuSeguridad = new ToolStripMenuItem();
            mnuSeguridadUsuarios = new ToolStripMenuItem();
            mnuSeguridadRolesPrivilegios = new ToolStripMenuItem();
            mnuSeguridadBitacora = new ToolStripMenuItem();
            mnuPacientes = new ToolStripMenuItem();
            mnuPacientesNuevo = new ToolStripMenuItem();
            mnuPacientesFicha = new ToolStripMenuItem();
            mnuCitas = new ToolStripMenuItem();
            mnuCitasAgendar = new ToolStripMenuItem();
            mnuCitasReprogramar = new ToolStripMenuItem();
            mnuCitasCancelar = new ToolStripMenuItem();
            mnuCitasAgenda = new ToolStripMenuItem();
            mnuHistoria = new ToolStripMenuItem();
            mnuTratamientos = new ToolStripMenuItem();
            mnuRecetas = new ToolStripMenuItem();
            mnuEstadosCuenta = new ToolStripMenuItem();
            mnuInventario = new ToolStripMenuItem();
            mnuDespacho = new ToolStripMenuItem();
            mnuProveedores = new ToolStripMenuItem();
            mnuOrdenes = new ToolStripMenuItem();
            mnuReportes = new ToolStripMenuItem();
            mnuReportesPacientes = new ToolStripMenuItem();
            mnuReportesCitas = new ToolStripMenuItem();
            mnuReportesTratamientos = new ToolStripMenuItem();
            mnuReportesRecetas = new ToolStripMenuItem();
            mnuReportesEstadosCuenta = new ToolStripMenuItem();
            mnuReportesInventario = new ToolStripMenuItem();
            mnuReportesProveedores = new ToolStripMenuItem();
            mnuReportesOrdenes = new ToolStripMenuItem();
            mnuReportesGraficos = new ToolStripMenuItem();
            mnuSalirBarra = new ToolStripMenuItem();
            mnuAyuda = new ToolStripMenuItem();
            mnuAyudaManual = new ToolStripMenuItem();
            mnuAyudaAcercaDe = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            mnuArchivoSalir = new ToolStripMenuItem();
            mnuPacientesConsultar = new ToolStripMenuItem();
            mnuPacientesFiltrados = new ToolStripMenuItem();
            mnuPacientesModificar = new ToolStripMenuItem();
            mnuPacientesBaja = new ToolStripMenuItem();
            mnuEstadosCuentaAbrir = new ToolStripMenuItem();
            mnuEstadosCuentaCargo = new ToolStripMenuItem();
            mnuEstadosCuentaAbono = new ToolStripMenuItem();
            mnuEstadosCuentaSaldo = new ToolStripMenuItem();
            mnuInventarioProductos = new ToolStripMenuItem();
            mnuInventarioAlertas = new ToolStripMenuItem();
            mnuProveedoresNuevo = new ToolStripMenuItem();
            mnuProveedoresConsultar = new ToolStripMenuItem();
            mnuProveedoresModificar = new ToolStripMenuItem();
            mnuProveedoresBaja = new ToolStripMenuItem();
            mnuOrdenesNueva = new ToolStripMenuItem();
            mnuOrdenesConsultar = new ToolStripMenuItem();
            mnuOrdenesRecibir = new ToolStripMenuItem();
            tlsPrincipal = new ToolStrip();
            btnTlsPacientes = new ToolStripButton();
            btnTlsCitas = new ToolStripButton();
            btnTlsHistoria = new ToolStripButton();
            btnTlsTratamientos = new ToolStripButton();
            btnTlsRecetas = new ToolStripButton();
            btnTlsCuentas = new ToolStripButton();
            btnTlsInventario = new ToolStripButton();
            btnTlsReportes = new ToolStripButton();
            btnTlsGraficos = new ToolStripButton();
            btnTlsSalir = new ToolStripButton();
            stsPrincipal = new StatusStrip();
            lblStatusUsuario = new ToolStripStatusLabel();
            lblStatusRol = new ToolStripStatusLabel();
            lblStatusFecha = new ToolStripStatusLabel();
            pnlSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogoSuperior).BeginInit();
            pnlUsuarioCard.SuspendLayout();
            mnuPrincipal.SuspendLayout();
            tlsPrincipal.SuspendLayout();
            stsPrincipal.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSuperior
            // 
            pnlSuperior.BackColor = Color.FromArgb(108, 180, 234);
            pnlSuperior.Controls.Add(picLogoSuperior);
            pnlSuperior.Controls.Add(lblTituloClinica);
            pnlSuperior.Controls.Add(pnlUsuarioCard);
            pnlSuperior.Dock = DockStyle.Top;
            pnlSuperior.Location = new Point(0, 0);
            pnlSuperior.Name = "pnlSuperior";
            pnlSuperior.Size = new Size(1264, 58);
            pnlSuperior.TabIndex = 0;
            pnlSuperior.Paint += pnlSuperior_Paint;
            // 
            // picLogoSuperior
            // 
            picLogoSuperior.BackColor = Color.Transparent;
            picLogoSuperior.Image = Properties.Resources.logo_blanco;
            picLogoSuperior.Location = new Point(14, 11);
            picLogoSuperior.Name = "picLogoSuperior";
            picLogoSuperior.Size = new Size(130, 42);
            picLogoSuperior.SizeMode = PictureBoxSizeMode.Zoom;
            picLogoSuperior.TabIndex = 0;
            picLogoSuperior.TabStop = false;
            // 
            // lblTituloClinica
            // 
            lblTituloClinica.AutoSize = true;
            lblTituloClinica.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTituloClinica.ForeColor = Color.White;
            lblTituloClinica.Location = new Point(172, 15);
            lblTituloClinica.Name = "lblTituloClinica";
            lblTituloClinica.Size = new Size(228, 30);
            lblTituloClinica.TabIndex = 1;
            lblTituloClinica.Text = "Clínica Odontológica";
            // 
            // pnlUsuarioCard
            // 
            pnlUsuarioCard.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlUsuarioCard.BackColor = Color.FromArgb(61, 143, 203);
            pnlUsuarioCard.Controls.Add(lblInfoUsuarioSuperior);
            pnlUsuarioCard.Location = new Point(880, 10);
            pnlUsuarioCard.Name = "pnlUsuarioCard";
            pnlUsuarioCard.Padding = new Padding(12, 0, 12, 0);
            pnlUsuarioCard.Size = new Size(370, 36);
            pnlUsuarioCard.TabIndex = 2;
            // 
            // lblInfoUsuarioSuperior
            // 
            lblInfoUsuarioSuperior.Dock = DockStyle.Fill;
            lblInfoUsuarioSuperior.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblInfoUsuarioSuperior.ForeColor = Color.White;
            lblInfoUsuarioSuperior.Location = new Point(12, 0);
            lblInfoUsuarioSuperior.Name = "lblInfoUsuarioSuperior";
            lblInfoUsuarioSuperior.Size = new Size(346, 36);
            lblInfoUsuarioSuperior.TabIndex = 0;
            lblInfoUsuarioSuperior.Text = "👤 Usuario: Activo";
            lblInfoUsuarioSuperior.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlLogoCard
            // 
            pnlLogoCard.Location = new Point(0, 0);
            pnlLogoCard.Name = "pnlLogoCard";
            pnlLogoCard.Size = new Size(200, 100);
            pnlLogoCard.TabIndex = 0;
            // 
            // mnuPrincipal
            // 
            mnuPrincipal.BackColor = Color.FromArgb(8, 86, 140);
            mnuPrincipal.ForeColor = Color.White;
            mnuPrincipal.Font = new Font("Segoe UI", 10F);
            mnuPrincipal.ImageScalingSize = new Size(20, 20);
            mnuPrincipal.Items.AddRange(new ToolStripItem[] { mnuArchivo, mnuSeguridad, mnuPacientes, mnuCitas, mnuHistoria, mnuTratamientos, mnuRecetas, mnuEstadosCuenta, mnuInventario, mnuDespacho, mnuProveedores, mnuOrdenes, mnuReportes, mnuSalirBarra, mnuAyuda });
            mnuPrincipal.Location = new Point(0, 58);
            mnuPrincipal.Name = "mnuPrincipal";
            mnuPrincipal.Size = new Size(1264, 31);
            mnuPrincipal.TabIndex = 1;
            mnuPrincipal.Text = "menuStrip1";
            // 
            // mnuArchivo
            // 
            mnuArchivo.DropDownItems.AddRange(new ToolStripItem[] { mnuArchivoIniciarSesion, mnuArchivoCerrarSesion, mnuArchivoCambiarClave });
            mnuArchivo.Image = Properties.Resources.ico_usuario;
            mnuArchivo.Name = "mnuArchivo";
            mnuArchivo.Size = new Size(101, 27);
            mnuArchivo.Text = "Archivo";
            // 
            // mnuArchivoIniciarSesion
            // 
            mnuArchivoIniciarSesion.Name = "mnuArchivoIniciarSesion";
            mnuArchivoIniciarSesion.Size = new Size(247, 28);
            mnuArchivoIniciarSesion.Text = "Iniciar sesión";
            mnuArchivoIniciarSesion.Click += mnuArchivoIniciarSesion_Click;
            // 
            // mnuArchivoCerrarSesion
            // 
            mnuArchivoCerrarSesion.Image = Properties.Resources.ico_salir;
            mnuArchivoCerrarSesion.Name = "mnuArchivoCerrarSesion";
            mnuArchivoCerrarSesion.Size = new Size(247, 28);
            mnuArchivoCerrarSesion.Text = "Cerrar sesión";
            mnuArchivoCerrarSesion.Click += mnuArchivoCerrarSesion_Click;
            // 
            // mnuArchivoCambiarClave
            // 
            mnuArchivoCambiarClave.Image = Properties.Resources.ico_candado;
            mnuArchivoCambiarClave.Name = "mnuArchivoCambiarClave";
            mnuArchivoCambiarClave.Size = new Size(247, 28);
            mnuArchivoCambiarClave.Text = "Cambiar contraseña";
            mnuArchivoCambiarClave.Click += mnuArchivoCambiarClave_Click;
            // 
            // mnuSeguridad
            // 
            mnuSeguridad.DropDownItems.AddRange(new ToolStripItem[] { mnuSeguridadUsuarios, mnuSeguridadRolesPrivilegios, mnuSeguridadBitacora });
            mnuSeguridad.Image = Properties.Resources.ico_candado;
            mnuSeguridad.Name = "mnuSeguridad";
            mnuSeguridad.Size = new Size(121, 27);
            mnuSeguridad.Text = "Seguridad";
            // 
            // mnuSeguridadUsuarios
            // 
            mnuSeguridadUsuarios.Image = Properties.Resources.ico_usuario;
            mnuSeguridadUsuarios.Name = "mnuSeguridadUsuarios";
            mnuSeguridadUsuarios.Size = new Size(243, 28);
            mnuSeguridadUsuarios.Text = "Usuarios";
            mnuSeguridadUsuarios.Click += mnuSeguridadUsuarios_Click;
            // 
            // mnuSeguridadRolesPrivilegios
            // 
            mnuSeguridadRolesPrivilegios.Image = Properties.Resources.ico_rol;
            mnuSeguridadRolesPrivilegios.Name = "mnuSeguridadRolesPrivilegios";
            mnuSeguridadRolesPrivilegios.Size = new Size(243, 28);
            mnuSeguridadRolesPrivilegios.Text = "Roles y Privilegios";
            mnuSeguridadRolesPrivilegios.Click += mnuSeguridadRolesPrivilegios_Click;
            // 
            // mnuSeguridadBitacora
            // 
            mnuSeguridadBitacora.Image = Properties.Resources.ico_bitacora;
            mnuSeguridadBitacora.Name = "mnuSeguridadBitacora";
            mnuSeguridadBitacora.Size = new Size(243, 28);
            mnuSeguridadBitacora.Text = "Bitácora de accesos";
            mnuSeguridadBitacora.Click += mnuSeguridadBitacora_Click;
            // 
            // mnuPacientes
            // 
            mnuPacientes.DropDownItems.AddRange(new ToolStripItem[] { mnuPacientesNuevo, mnuPacientesFicha });
            mnuPacientes.Image = Properties.Resources.ico_paciente;
            mnuPacientes.Name = "mnuPacientes";
            mnuPacientes.Size = new Size(115, 27);
            mnuPacientes.Text = "Pacientes";
            // 
            // mnuPacientesNuevo
            // 
            mnuPacientesNuevo.Image = Properties.Resources.ico_nuevo;
            mnuPacientesNuevo.Name = "mnuPacientesNuevo";
            mnuPacientesNuevo.Size = new Size(231, 28);
            mnuPacientesNuevo.Text = "Nuevo paciente";
            mnuPacientesNuevo.Click += mnuPacientesNuevo_Click;
            // 
            // mnuPacientesFicha
            // 
            mnuPacientesFicha.Image = Properties.Resources.ico_paciente;
            mnuPacientesFicha.Name = "mnuPacientesFicha";
            mnuPacientesFicha.Size = new Size(231, 28);
            mnuPacientesFicha.Text = "Ficha del paciente";
            mnuPacientesFicha.Click += mnuPacientesFicha_Click;
            // 
            // mnuCitas
            // 
            mnuCitas.DropDownItems.AddRange(new ToolStripItem[] { mnuCitasAgendar, mnuCitasReprogramar, mnuCitasCancelar, mnuCitasAgenda });
            mnuCitas.Image = Properties.Resources.ico_cita;
            mnuCitas.Name = "mnuCitas";
            mnuCitas.Size = new Size(81, 27);
            mnuCitas.Text = "Citas";
            // 
            // mnuCitasAgendar
            // 
            mnuCitasAgendar.Image = Properties.Resources.ico_nuevo;
            mnuCitasAgendar.Name = "mnuCitasAgendar";
            mnuCitasAgendar.Size = new Size(229, 28);
            mnuCitasAgendar.Text = "Agendar cita";
            mnuCitasAgendar.Click += mnuCitasAgendar_Click;
            // 
            // mnuCitasReprogramar
            // 
            mnuCitasReprogramar.Image = Properties.Resources.ico_editar;
            mnuCitasReprogramar.Name = "mnuCitasReprogramar";
            mnuCitasReprogramar.Size = new Size(229, 28);
            mnuCitasReprogramar.Text = "Reprogramar cita";
            mnuCitasReprogramar.Click += mnuCitasReprogramar_Click;
            // 
            // mnuCitasCancelar
            // 
            mnuCitasCancelar.Image = Properties.Resources.ico_cancelar;
            mnuCitasCancelar.Name = "mnuCitasCancelar";
            mnuCitasCancelar.Size = new Size(229, 28);
            mnuCitasCancelar.Text = "Cancelar cita";
            mnuCitasCancelar.Click += mnuCitasCancelar_Click;
            // 
            // mnuCitasAgenda
            // 
            mnuCitasAgenda.Image = Properties.Resources.ico_cita;
            mnuCitasAgenda.Name = "mnuCitasAgenda";
            mnuCitasAgenda.Size = new Size(229, 28);
            mnuCitasAgenda.Text = "Consultar agenda";
            mnuCitasAgenda.Click += mnuCitasAgenda_Click;
            // 
            // mnuHistoria
            // 
            mnuHistoria.Image = Properties.Resources.ico_historia;
            mnuHistoria.Name = "mnuHistoria";
            mnuHistoria.Size = new Size(154, 27);
            mnuHistoria.Text = "Historia clínica";
            mnuHistoria.Click += mnuHistoria_Click;
            // 
            // mnuTratamientos
            // 
            mnuTratamientos.Image = Properties.Resources.ico_tratamiento;
            mnuTratamientos.Name = "mnuTratamientos";
            mnuTratamientos.Size = new Size(143, 27);
            mnuTratamientos.Text = "Tratamientos";
            mnuTratamientos.Click += mnuTratamientos_Click;
            // 
            // mnuRecetas
            // 
            mnuRecetas.Image = Properties.Resources.ico_receta;
            mnuRecetas.Name = "mnuRecetas";
            mnuRecetas.Size = new Size(102, 27);
            mnuRecetas.Text = "Recetas";
            mnuRecetas.Click += mnuRecetas_Click;
            // 
            // mnuEstadosCuenta
            // 
            mnuEstadosCuenta.Image = Properties.Resources.ico_cuenta;
            mnuEstadosCuenta.Name = "mnuEstadosCuenta";
            mnuEstadosCuenta.Size = new Size(183, 27);
            mnuEstadosCuenta.Text = "Estados de cuenta";
            mnuEstadosCuenta.Click += mnuEstadosCuenta_Click;
            // 
            // mnuInventario
            // 
            mnuInventario.DropDownItems.AddRange(new ToolStripItem[] { mnuInventarioProductos, mnuInventarioAlertas });
            mnuInventario.Image = Properties.Resources.ico_inventario;
            mnuInventario.Name = "mnuInventario";
            mnuInventario.Size = new Size(121, 27);
            mnuInventario.Text = "Inventario";
            //
            // mnuDespacho
            //
            mnuDespacho.Image = Properties.Resources.ico_receta;
            mnuDespacho.Name = "mnuDespacho";
            mnuDespacho.Size = new Size(121, 27);
            mnuDespacho.Text = "Despacho";
            mnuDespacho.Click += mnuDespacho_Click;
            //
            // mnuProveedores
            // 
            mnuProveedores.Image = Properties.Resources.ico_proveedor;
            mnuProveedores.Name = "mnuProveedores";
            mnuProveedores.Size = new Size(138, 27);
            mnuProveedores.Text = "Proveedores";
            mnuProveedores.Click += mnuProveedores_Click;
            // 
            // mnuOrdenes
            // 
            mnuOrdenes.Image = Properties.Resources.ico_orden;
            mnuOrdenes.Name = "mnuOrdenes";
            mnuOrdenes.Size = new Size(195, 27);
            mnuOrdenes.Text = "Órdenes de compra";
            mnuOrdenes.Click += mnuOrdenes_Click;
            // 
            // mnuReportes
            // 
            mnuReportes.DropDownItems.AddRange(new ToolStripItem[] { mnuReportesPacientes, mnuReportesCitas, mnuReportesTratamientos, mnuReportesRecetas, mnuReportesEstadosCuenta, mnuReportesInventario, mnuReportesProveedores, mnuReportesOrdenes, mnuReportesGraficos });
            mnuReportes.Image = Properties.Resources.ico_reporte;
            mnuReportes.Name = "mnuReportes";
            mnuReportes.Size = new Size(111, 27);
            mnuReportes.Text = "Reportes";
            // 
            // mnuReportesPacientes
            // 
            mnuReportesPacientes.Image = Properties.Resources.ico_reporte;
            mnuReportesPacientes.Name = "mnuReportesPacientes";
            mnuReportesPacientes.Size = new Size(247, 28);
            mnuReportesPacientes.Text = "Pacientes";
            mnuReportesPacientes.Click += mnuReportesPacientes_Click;
            // 
            // mnuReportesCitas
            // 
            mnuReportesCitas.Image = Properties.Resources.ico_reporte;
            mnuReportesCitas.Name = "mnuReportesCitas";
            mnuReportesCitas.Size = new Size(247, 28);
            mnuReportesCitas.Text = "Citas";
            mnuReportesCitas.Click += mnuReportesCitas_Click;
            // 
            // mnuReportesTratamientos
            // 
            mnuReportesTratamientos.Image = Properties.Resources.ico_reporte;
            mnuReportesTratamientos.Name = "mnuReportesTratamientos";
            mnuReportesTratamientos.Size = new Size(247, 28);
            mnuReportesTratamientos.Text = "Tratamientos";
            mnuReportesTratamientos.Click += mnuReportesTratamientos_Click;
            // 
            // mnuReportesRecetas
            // 
            mnuReportesRecetas.Image = Properties.Resources.ico_reporte;
            mnuReportesRecetas.Name = "mnuReportesRecetas";
            mnuReportesRecetas.Size = new Size(247, 28);
            mnuReportesRecetas.Text = "Recetas";
            mnuReportesRecetas.Click += mnuReportesRecetas_Click;
            // 
            // mnuReportesEstadosCuenta
            // 
            mnuReportesEstadosCuenta.Image = Properties.Resources.ico_reporte;
            mnuReportesEstadosCuenta.Name = "mnuReportesEstadosCuenta";
            mnuReportesEstadosCuenta.Size = new Size(247, 28);
            mnuReportesEstadosCuenta.Text = "Estados de cuenta";
            mnuReportesEstadosCuenta.Click += mnuReportesEstadosCuenta_Click;
            // 
            // mnuReportesInventario
            // 
            mnuReportesInventario.Image = Properties.Resources.ico_reporte;
            mnuReportesInventario.Name = "mnuReportesInventario";
            mnuReportesInventario.Size = new Size(247, 28);
            mnuReportesInventario.Text = "Inventario";
            mnuReportesInventario.Click += mnuReportesInventario_Click;
            // 
            // mnuReportesProveedores
            // 
            mnuReportesProveedores.Image = Properties.Resources.ico_reporte;
            mnuReportesProveedores.Name = "mnuReportesProveedores";
            mnuReportesProveedores.Size = new Size(247, 28);
            mnuReportesProveedores.Text = "Proveedores";
            mnuReportesProveedores.Click += mnuReportesProveedores_Click;
            // 
            // mnuReportesOrdenes
            // 
            mnuReportesOrdenes.Image = Properties.Resources.ico_reporte;
            mnuReportesOrdenes.Name = "mnuReportesOrdenes";
            mnuReportesOrdenes.Size = new Size(247, 28);
            mnuReportesOrdenes.Text = "Órdenes de compra";
            mnuReportesOrdenes.Click += mnuReportesOrdenes_Click;
            // 
            // mnuReportesGraficos
            // 
            mnuReportesGraficos.Image = Properties.Resources.ico_grafico;
            mnuReportesGraficos.Name = "mnuReportesGraficos";
            mnuReportesGraficos.Size = new Size(247, 28);
            mnuReportesGraficos.Text = "Gráficos estadísticos";
            mnuReportesGraficos.Click += mnuReportesGraficos_Click;
            // 
            // mnuSalirBarra
            // 
            mnuSalirBarra.Image = Properties.Resources.ico_salir;
            mnuSalirBarra.Name = "mnuSalirBarra";
            mnuSalirBarra.Size = new Size(76, 27);
            mnuSalirBarra.Text = "Salir";
            mnuSalirBarra.Click += mnuSalirBarra_Click;
            // 
            // mnuAyuda
            // 
            mnuAyuda.DropDownItems.AddRange(new ToolStripItem[] { mnuAyudaManual, mnuAyudaAcercaDe });
            mnuAyuda.Image = Properties.Resources.ico_ayuda;
            mnuAyuda.Name = "mnuAyuda";
            mnuAyuda.Size = new Size(92, 27);
            mnuAyuda.Text = "Ayuda";
            // 
            // mnuAyudaManual
            // 
            mnuAyudaManual.Image = Properties.Resources.ico_ayuda;
            mnuAyudaManual.Name = "mnuAyudaManual";
            mnuAyudaManual.Size = new Size(236, 28);
            mnuAyudaManual.Text = "Manual de usuario";
            mnuAyudaManual.Click += mnuAyudaManual_Click;
            // 
            // mnuAyudaAcercaDe
            // 
            mnuAyudaAcercaDe.Image = Properties.Resources.ico_ayuda;
            mnuAyudaAcercaDe.Name = "mnuAyudaAcercaDe";
            mnuAyudaAcercaDe.Size = new Size(236, 28);
            mnuAyudaAcercaDe.Text = "Acerca de";
            mnuAyudaAcercaDe.Click += mnuAyudaAcercaDe_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(197, 6);
            // 
            // mnuArchivoSalir
            // 
            mnuArchivoSalir.Image = Properties.Resources.ico_salir;
            mnuArchivoSalir.Name = "mnuArchivoSalir";
            mnuArchivoSalir.Size = new Size(200, 24);
            mnuArchivoSalir.Text = "Salir";
            // 
            // mnuPacientesConsultar
            // 
            mnuPacientesConsultar.Image = Properties.Resources.ico_buscar;
            mnuPacientesConsultar.Name = "mnuPacientesConsultar";
            mnuPacientesConsultar.Size = new Size(198, 24);
            mnuPacientesConsultar.Text = "Consultar pacientes";
            // 
            // mnuPacientesFiltrados
            // 
            mnuPacientesFiltrados.Image = Properties.Resources.ico_buscar;
            mnuPacientesFiltrados.Name = "mnuPacientesFiltrados";
            mnuPacientesFiltrados.Size = new Size(198, 24);
            mnuPacientesFiltrados.Text = "Consultas filtradas";
            // 
            // mnuPacientesModificar
            // 
            mnuPacientesModificar.Image = Properties.Resources.ico_editar;
            mnuPacientesModificar.Name = "mnuPacientesModificar";
            mnuPacientesModificar.Size = new Size(198, 24);
            mnuPacientesModificar.Text = "Modificar paciente";
            // 
            // mnuPacientesBaja
            // 
            mnuPacientesBaja.Image = Properties.Resources.ico_baja;
            mnuPacientesBaja.Name = "mnuPacientesBaja";
            mnuPacientesBaja.Size = new Size(198, 24);
            mnuPacientesBaja.Text = "Dar de baja";
            // 
            // mnuEstadosCuentaAbrir
            // 
            mnuEstadosCuentaAbrir.Image = Properties.Resources.ico_cuenta;
            mnuEstadosCuentaAbrir.Name = "mnuEstadosCuentaAbrir";
            mnuEstadosCuentaAbrir.Size = new Size(218, 24);
            mnuEstadosCuentaAbrir.Text = "Abrir estado de cuenta";
            mnuEstadosCuentaAbrir.Click += mnuEstadosCuentaAbrir_Click;
            // 
            // mnuEstadosCuentaCargo
            // 
            mnuEstadosCuentaCargo.Image = Properties.Resources.ico_cuenta;
            mnuEstadosCuentaCargo.Name = "mnuEstadosCuentaCargo";
            mnuEstadosCuentaCargo.Size = new Size(218, 24);
            mnuEstadosCuentaCargo.Text = "Registrar cargo";
            mnuEstadosCuentaCargo.Click += mnuEstadosCuentaCargo_Click;
            // 
            // mnuEstadosCuentaAbono
            // 
            mnuEstadosCuentaAbono.Image = Properties.Resources.ico_cuenta;
            mnuEstadosCuentaAbono.Name = "mnuEstadosCuentaAbono";
            mnuEstadosCuentaAbono.Size = new Size(218, 24);
            mnuEstadosCuentaAbono.Text = "Registrar abono";
            mnuEstadosCuentaAbono.Click += mnuEstadosCuentaAbono_Click;
            // 
            // mnuEstadosCuentaSaldo
            // 
            mnuEstadosCuentaSaldo.Image = Properties.Resources.ico_cuenta;
            mnuEstadosCuentaSaldo.Name = "mnuEstadosCuentaSaldo";
            mnuEstadosCuentaSaldo.Size = new Size(218, 24);
            mnuEstadosCuentaSaldo.Text = "Consultar saldo";
            mnuEstadosCuentaSaldo.Click += mnuEstadosCuentaSaldo_Click;
            // 
            // mnuInventarioProductos
            // 
            mnuInventarioProductos.Image = Properties.Resources.ico_inventario;
            mnuInventarioProductos.Name = "mnuInventarioProductos";
            mnuInventarioProductos.Size = new Size(224, 24);
            mnuInventarioProductos.Text = "Productos";
            mnuInventarioProductos.Click += mnuInventarioProductos_Click;
            // 
            // mnuInventarioAlertas
            // 
            mnuInventarioAlertas.Image = Properties.Resources.ico_alerta;
            mnuInventarioAlertas.Name = "mnuInventarioAlertas";
            mnuInventarioAlertas.Size = new Size(224, 24);
            mnuInventarioAlertas.Text = "Alertas de stock mínimo";
            mnuInventarioAlertas.Click += mnuInventarioAlertas_Click;
            // 
            // mnuProveedoresNuevo
            // 
            mnuProveedoresNuevo.Image = Properties.Resources.ico_nuevo;
            mnuProveedoresNuevo.Name = "mnuProveedoresNuevo";
            mnuProveedoresNuevo.Size = new Size(147, 24);
            mnuProveedoresNuevo.Text = "Nuevo";
            mnuProveedoresNuevo.Click += mnuProveedoresNuevo_Click;
            // 
            // mnuProveedoresConsultar
            // 
            mnuProveedoresConsultar.Image = Properties.Resources.ico_buscar;
            mnuProveedoresConsultar.Name = "mnuProveedoresConsultar";
            mnuProveedoresConsultar.Size = new Size(147, 24);
            mnuProveedoresConsultar.Text = "Consultar";
            mnuProveedoresConsultar.Click += mnuProveedoresConsultar_Click;
            // 
            // mnuProveedoresModificar
            // 
            mnuProveedoresModificar.Image = Properties.Resources.ico_editar;
            mnuProveedoresModificar.Name = "mnuProveedoresModificar";
            mnuProveedoresModificar.Size = new Size(147, 24);
            mnuProveedoresModificar.Text = "Modificar";
            mnuProveedoresModificar.Click += mnuProveedoresModificar_Click;
            // 
            // mnuProveedoresBaja
            // 
            mnuProveedoresBaja.Image = Properties.Resources.ico_baja;
            mnuProveedoresBaja.Name = "mnuProveedoresBaja";
            mnuProveedoresBaja.Size = new Size(147, 24);
            mnuProveedoresBaja.Text = "Dar de baja";
            mnuProveedoresBaja.Click += mnuProveedoresBaja_Click;
            // 
            // mnuOrdenesNueva
            // 
            mnuOrdenesNueva.Image = Properties.Resources.ico_nuevo;
            mnuOrdenesNueva.Name = "mnuOrdenesNueva";
            mnuOrdenesNueva.Size = new Size(187, 24);
            mnuOrdenesNueva.Text = "Nueva orden";
            mnuOrdenesNueva.Click += mnuOrdenesNueva_Click;
            // 
            // mnuOrdenesConsultar
            // 
            mnuOrdenesConsultar.Image = Properties.Resources.ico_buscar;
            mnuOrdenesConsultar.Name = "mnuOrdenesConsultar";
            mnuOrdenesConsultar.Size = new Size(187, 24);
            mnuOrdenesConsultar.Text = "Consultar órdenes";
            mnuOrdenesConsultar.Click += mnuOrdenesConsultar_Click;
            // 
            // mnuOrdenesRecibir
            // 
            mnuOrdenesRecibir.Image = Properties.Resources.ico_orden;
            mnuOrdenesRecibir.Name = "mnuOrdenesRecibir";
            mnuOrdenesRecibir.Size = new Size(187, 24);
            mnuOrdenesRecibir.Text = "Recibir orden";
            mnuOrdenesRecibir.Click += mnuOrdenesRecibir_Click;
            // 
            // tlsPrincipal
            // 
            tlsPrincipal.BackColor = Color.FromArgb(244, 247, 250);
            tlsPrincipal.Font = new Font("Segoe UI", 9.5F);
            tlsPrincipal.ImageScalingSize = new Size(24, 24);
            tlsPrincipal.Items.AddRange(new ToolStripItem[] { btnTlsPacientes, btnTlsCitas, btnTlsHistoria, btnTlsTratamientos, btnTlsRecetas, btnTlsCuentas, btnTlsInventario, btnTlsReportes, btnTlsGraficos, btnTlsSalir });
            tlsPrincipal.Location = new Point(0, 89);
            tlsPrincipal.Name = "tlsPrincipal";
            tlsPrincipal.Size = new Size(1264, 31);
            tlsPrincipal.TabIndex = 2;
            tlsPrincipal.Text = "toolStrip1";
            tlsPrincipal.Visible = false;
            tlsPrincipal.GripStyle = ToolStripGripStyle.Hidden;
            // 
            // btnTlsPacientes
            // 
            btnTlsPacientes.Image = Properties.Resources.ico_paciente;
            btnTlsPacientes.ImageTransparentColor = Color.Magenta;
            btnTlsPacientes.Name = "btnTlsPacientes";
            btnTlsPacientes.Size = new Size(102, 28);
            btnTlsPacientes.Text = "Pacientes";
            btnTlsPacientes.Click += mnuPacientesNuevo_Click;
            // 
            // btnTlsCitas
            // 
            btnTlsCitas.Image = Properties.Resources.ico_cita;
            btnTlsCitas.ImageTransparentColor = Color.Magenta;
            btnTlsCitas.Name = "btnTlsCitas";
            btnTlsCitas.Size = new Size(72, 28);
            btnTlsCitas.Text = "Citas";
            btnTlsCitas.Click += mnuCitasAgenda_Click;
            // 
            // btnTlsHistoria
            // 
            btnTlsHistoria.Image = Properties.Resources.ico_historia;
            btnTlsHistoria.ImageTransparentColor = Color.Magenta;
            btnTlsHistoria.Name = "btnTlsHistoria";
            btnTlsHistoria.Size = new Size(142, 28);
            btnTlsHistoria.Text = "Historia Clínica";
            btnTlsHistoria.Click += mnuHistoria_Click;
            // 
            // btnTlsTratamientos
            // 
            btnTlsTratamientos.Image = Properties.Resources.ico_tratamiento;
            btnTlsTratamientos.ImageTransparentColor = Color.Magenta;
            btnTlsTratamientos.Name = "btnTlsTratamientos";
            btnTlsTratamientos.Size = new Size(128, 28);
            btnTlsTratamientos.Text = "Tratamientos";
            btnTlsTratamientos.Click += mnuTratamientos_Click;
            // 
            // btnTlsRecetas
            // 
            btnTlsRecetas.Image = Properties.Resources.ico_receta;
            btnTlsRecetas.ImageTransparentColor = Color.Magenta;
            btnTlsRecetas.Name = "btnTlsRecetas";
            btnTlsRecetas.Size = new Size(91, 28);
            btnTlsRecetas.Text = "Recetas";
            btnTlsRecetas.Click += mnuRecetas_Click;
            // 
            // btnTlsCuentas
            // 
            btnTlsCuentas.Image = Properties.Resources.ico_cuenta;
            btnTlsCuentas.ImageTransparentColor = Color.Magenta;
            btnTlsCuentas.Name = "btnTlsCuentas";
            btnTlsCuentas.Size = new Size(94, 28);
            btnTlsCuentas.Text = "Cuentas";
            btnTlsCuentas.Click += mnuEstadosCuentaAbrir_Click;
            // 
            // btnTlsInventario
            // 
            btnTlsInventario.Image = Properties.Resources.ico_inventario;
            btnTlsInventario.ImageTransparentColor = Color.Magenta;
            btnTlsInventario.Name = "btnTlsInventario";
            btnTlsInventario.Size = new Size(108, 28);
            btnTlsInventario.Text = "Inventario";
            btnTlsInventario.Click += mnuInventarioProductos_Click;
            // 
            // btnTlsReportes
            // 
            btnTlsReportes.Image = Properties.Resources.ico_reporte;
            btnTlsReportes.ImageTransparentColor = Color.Magenta;
            btnTlsReportes.Name = "btnTlsReportes";
            btnTlsReportes.Size = new Size(100, 28);
            btnTlsReportes.Text = "Reportes";
            btnTlsReportes.Click += mnuReportesPacientes_Click;
            // 
            // btnTlsGraficos
            // 
            btnTlsGraficos.Image = Properties.Resources.ico_grafico;
            btnTlsGraficos.ImageTransparentColor = Color.Magenta;
            btnTlsGraficos.Name = "btnTlsGraficos";
            btnTlsGraficos.Size = new Size(95, 28);
            btnTlsGraficos.Text = "Gráficos";
            btnTlsGraficos.Click += mnuReportesGraficos_Click;
            // 
            // btnTlsSalir
            // 
            btnTlsSalir.Image = Properties.Resources.ico_salir;
            btnTlsSalir.ImageTransparentColor = Color.Magenta;
            btnTlsSalir.Name = "btnTlsSalir";
            btnTlsSalir.Size = new Size(69, 28);
            btnTlsSalir.Text = "Salir";
            btnTlsSalir.Click += mnuSalirBarra_Click;
            // 
            // stsPrincipal
            // 
            stsPrincipal.BackColor = Color.White;
            stsPrincipal.Font = new Font("Segoe UI", 9.5F);
            stsPrincipal.ImageScalingSize = new Size(20, 20);
            stsPrincipal.Items.AddRange(new ToolStripItem[] { lblStatusUsuario, lblStatusRol, lblStatusFecha });
            stsPrincipal.Location = new Point(0, 708);
            stsPrincipal.Name = "stsPrincipal";
            stsPrincipal.Size = new Size(1264, 31);
            stsPrincipal.TabIndex = 3;
            stsPrincipal.Text = "statusStrip1";
            // 
            // lblStatusUsuario
            // 
            lblStatusUsuario.BorderSides = ToolStripStatusLabelBorderSides.Right;
            lblStatusUsuario.Name = "lblStatusUsuario";
            lblStatusUsuario.Size = new Size(119, 25);
            lblStatusUsuario.Text = "Usuario: admin";
            // 
            // lblStatusRol
            // 
            lblStatusRol.BorderSides = ToolStripStatusLabelBorderSides.Right;
            lblStatusRol.Name = "lblStatusRol";
            lblStatusRol.Size = new Size(144, 25);
            lblStatusRol.Text = "Rol: Administrador";
            // 
            // lblStatusFecha
            // 
            lblStatusFecha.Name = "lblStatusFecha";
            lblStatusFecha.Size = new Size(986, 25);
            lblStatusFecha.Spring = true;
            lblStatusFecha.Text = "Fecha: ";
            lblStatusFecha.TextAlign = ContentAlignment.MiddleRight;
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 739);
            Controls.Add(tlsPrincipal);
            Controls.Add(mnuPrincipal);
            Controls.Add(pnlSuperior);
            Controls.Add(stsPrincipal);
            Font = new Font("Segoe UI", 10F);
            IsMdiContainer = true;
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Toothly - Sistema de Gestión Odontológica";
            WindowState = FormWindowState.Maximized;
            FormClosing += FrmPrincipal_FormClosing;
            Load += FrmPrincipal_Load;
            pnlSuperior.ResumeLayout(false);
            pnlSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogoSuperior).EndInit();
            pnlUsuarioCard.ResumeLayout(false);
            mnuPrincipal.ResumeLayout(false);
            mnuPrincipal.PerformLayout();
            tlsPrincipal.ResumeLayout(false);
            tlsPrincipal.PerformLayout();
            stsPrincipal.ResumeLayout(false);
            stsPrincipal.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlSuperior;
        private Panel pnlLogoCard;
        private PictureBox picLogoSuperior;
        private Label lblTituloClinica;
        private Panel pnlUsuarioCard;
        private Label lblInfoUsuarioSuperior;
        private MenuStrip mnuPrincipal;
        private ToolStripMenuItem mnuArchivo;
        private ToolStripMenuItem mnuArchivoIniciarSesion;
        private ToolStripMenuItem mnuArchivoCerrarSesion;
        private ToolStripMenuItem mnuArchivoCambiarClave;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem mnuArchivoSalir;
        private ToolStripMenuItem mnuSeguridad;
        private ToolStripMenuItem mnuSeguridadUsuarios;
        private ToolStripMenuItem mnuSeguridadRolesPrivilegios;
        private ToolStripMenuItem mnuSeguridadBitacora;
        private ToolStripMenuItem mnuPacientes;
        private ToolStripMenuItem mnuPacientesNuevo;
        private ToolStripMenuItem mnuPacientesConsultar;
        private ToolStripMenuItem mnuPacientesFiltrados;
        private ToolStripMenuItem mnuPacientesModificar;
        private ToolStripMenuItem mnuPacientesBaja;
        private ToolStripMenuItem mnuPacientesFicha;
        private ToolStripMenuItem mnuCitas;
        private ToolStripMenuItem mnuCitasAgendar;
        private ToolStripMenuItem mnuCitasReprogramar;
        private ToolStripMenuItem mnuCitasCancelar;
        private ToolStripMenuItem mnuCitasAgenda;
        private ToolStripMenuItem mnuHistoria;
        private ToolStripMenuItem mnuTratamientos;
        private ToolStripMenuItem mnuRecetas;
        private ToolStripMenuItem mnuEstadosCuenta;
        private ToolStripMenuItem mnuEstadosCuentaAbrir;
        private ToolStripMenuItem mnuEstadosCuentaCargo;
        private ToolStripMenuItem mnuEstadosCuentaAbono;
        private ToolStripMenuItem mnuEstadosCuentaSaldo;
        private ToolStripMenuItem mnuInventario;
        private ToolStripMenuItem mnuDespacho;
        private ToolStripMenuItem mnuInventarioProductos;
        private ToolStripMenuItem mnuInventarioAlertas;
        private ToolStripMenuItem mnuProveedores;
        private ToolStripMenuItem mnuProveedoresNuevo;
        private ToolStripMenuItem mnuProveedoresConsultar;
        private ToolStripMenuItem mnuProveedoresModificar;
        private ToolStripMenuItem mnuProveedoresBaja;
        private ToolStripMenuItem mnuOrdenes;
        private ToolStripMenuItem mnuOrdenesNueva;
        private ToolStripMenuItem mnuOrdenesConsultar;
        private ToolStripMenuItem mnuOrdenesRecibir;
        private ToolStripMenuItem mnuReportes;
        private ToolStripMenuItem mnuReportesPacientes;
        private ToolStripMenuItem mnuReportesCitas;
        private ToolStripMenuItem mnuReportesTratamientos;
        private ToolStripMenuItem mnuReportesRecetas;
        private ToolStripMenuItem mnuReportesEstadosCuenta;
        private ToolStripMenuItem mnuReportesInventario;
        private ToolStripMenuItem mnuReportesProveedores;
        private ToolStripMenuItem mnuReportesOrdenes;
        private ToolStripMenuItem mnuReportesGraficos;
        private ToolStripMenuItem mnuSalirBarra;
        private ToolStripMenuItem mnuAyuda;
        private ToolStripMenuItem mnuAyudaManual;
        private ToolStripMenuItem mnuAyudaAcercaDe;
        private ToolStrip tlsPrincipal;
        private ToolStripButton btnTlsPacientes;
        private ToolStripButton btnTlsCitas;
        private ToolStripButton btnTlsHistoria;
        private ToolStripButton btnTlsTratamientos;
        private ToolStripButton btnTlsRecetas;
        private ToolStripButton btnTlsCuentas;
        private ToolStripButton btnTlsInventario;
        private ToolStripButton btnTlsReportes;
        private ToolStripButton btnTlsGraficos;
        private ToolStripButton btnTlsSalir;
        private StatusStrip stsPrincipal;
        private ToolStripStatusLabel lblStatusUsuario;
        private ToolStripStatusLabel lblStatusRol;
        private ToolStripStatusLabel lblStatusFecha;
    }
}
