using System;
using System.Drawing;
using System.Windows.Forms;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmPrincipal : Form
    {
        private Form? formularioActivo;

        // Submenú Reportes > Despachos (creado por código, junto a los demás reportes)
        private readonly ToolStripMenuItem mnuReportesDespachos = new ToolStripMenuItem();

        public FrmPrincipal()
        {
            InitializeComponent();
            this.MdiChildActivate += (s, e) => RecargarFormularioActivo();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            // Icono de TOOTHLY en la barra de título y en la barra de tareas
            IconoApp.Aplicar(this);

            // Logo blanco con fondo transparente integrado directamente en la barra superior celeste
            picLogoSuperior.Image = Properties.Resources.logo_blanco;
            picLogoSuperior.Visible = true;
            lblTituloClinica.Text = "|   Clínica Odontológica";
            // Dejamos un margen cómodo a la derecha del logo para evitar cualquier superposición
            lblTituloClinica.Left = picLogoSuperior.Right + 16;
            lblTituloClinica.Top = (pnlSuperior.Height - lblTituloClinica.Height) / 2;

            // Iconos para la barra de herramientas
            btnTlsPacientes.Image = Properties.Resources.ico_paciente;
            btnTlsCitas.Image = Properties.Resources.ico_cita;
            btnTlsHistoria.Image = Properties.Resources.ico_historia;
            btnTlsTratamientos.Image = Properties.Resources.ico_tratamiento;
            btnTlsRecetas.Image = Properties.Resources.ico_receta;
            btnTlsCuentas.Image = Properties.Resources.ico_cuenta;
            btnTlsInventario.Image = Properties.Resources.ico_inventario;
            btnTlsReportes.Image = Properties.Resources.ico_reporte;
            btnTlsGraficos.Image = Properties.Resources.ico_grafico;

            // Asignamos los iconos a cada opción del menú principal
            CargarIconosMenus();

            // Colores del menú superior
            mnuPrincipal.BackColor = Color.FromArgb(8, 86, 140);
            mnuPrincipal.ForeColor = Color.White;
            foreach (ToolStripItem item in mnuPrincipal.Items)
            {
                item.ForeColor = Color.White;
            }
            mnuPrincipal.Renderer = new MenuRendererToothly();

            tlsPrincipal.Visible = false;

            // Mostramos los datos del usuario logueado en la esquina superior
            lblInfoUsuarioSuperior.Text = "👤 " + Sesion.NombreCompleto + "  •  " + Sesion.NombreRol;
            lblStatusUsuario.Text = "Usuario: " + Sesion.NombreUsuario;
            lblStatusRol.Text = "Rol: " + Sesion.NombreRol;
            lblStatusFecha.Text = "Fecha: " + DateTime.Today.ToString("dd/MM/yyyy");

            // Le damos color suave al fondo MDI y dibujamos el logo en el centro
            foreach (Control c in this.Controls)
            {
                if (c is MdiClient mdiClient)
                {
                    mdiClient.BackColor = Color.FromArgb(243, 249, 253);

                    // Habilitar doble buffer para evitar parpadeos
                    typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.SetValue(mdiClient, true, null);

                    // Forzar repintado al redimensionar (maximizar) y cuando cambien ventanas hijas
                    mdiClient.Resize += (s, pe) => mdiClient.Invalidate();
                    this.Shown += (s, pe) => mdiClient.Invalidate();
                    this.MdiChildActivate += (s, pe) => mdiClient.Invalidate();

                    mdiClient.Paint += (s, pe) =>
                    {
                        // Si no hay ninguna ventana abierta mostramos el logo azul con fondo transparente centrado
                        if (this.MdiChildren.Length == 0)
                        {
                            pe.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                            // Dibujamos el logo azul celeste sin fondo en el centro
                            Image logoAzul = Properties.Resources.logo_azul;
                            int logoW = 340;
                            int logoH = (int)(logoW * (244.0 / 834.0)); // Proporción original ~99px
                            int logoX = (mdiClient.ClientSize.Width - logoW) / 2;
                            int logoY = (mdiClient.ClientSize.Height / 2) - 75;
                            pe.Graphics.DrawImage(logoAzul, logoX, logoY, logoW, logoH);

                            // Mensaje de bienvenida integrado debajo
                            string textoBienvenida = "Sistema de Gestión Odontológica";
                            using (Font fSub = new Font("Segoe UI", 13F, FontStyle.Regular))
                            using (Brush bSub = new SolidBrush(Color.FromArgb(96, 125, 139)))
                            {
                                SizeF tamSub = pe.Graphics.MeasureString(textoBienvenida, fSub);
                                float tx = (mdiClient.ClientSize.Width - tamSub.Width) / 2;
                                float ty = logoY + logoH + 15;
                                pe.Graphics.DrawString(textoBienvenida, fSub, bSub, tx, ty);
                            }
                        }
                    };
                    break;
                }
            }

            // Submenú del reporte de despachos (debajo de Recetas)
            CrearMenuReporteDespachos();

            // Configuramos los permisos según el rol del usuario
            ConfigurarMenuPorRol();

            // Si es Asistente y hay productos bajo el stock mínimo, avisar
            if (Sesion.NombreRol == "Asistente Odontológico")
            {
                var bajoStock = ProductoDatos.ProductosBajoStock();
                if (bajoStock.Count > 0)
                {
                    MessageBox.Show(
                        "Atención: Hay " + bajoStock.Count + " producto(s) por debajo del stock mínimo.\nRevise el módulo de Alertas de Stock.",
                        "Alerta de Inventario",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
        }

        // Configura visibilidad de menús y submenús según el rol del usuario conectado
        private void ConfigurarMenuPorRol()
        {
            string rol = Sesion.NombreRol;

            // Primero ocultamos los menús principales condicionales
            mnuSeguridad.Visible = false;
            mnuPacientes.Visible = false;
            mnuCitas.Visible = false;
            mnuHistoria.Visible = false;
            mnuTratamientos.Visible = false;
            mnuRecetas.Visible = false;
            mnuEstadosCuenta.Visible = false;
            mnuInventario.Visible = false;
            mnuDespacho.Visible = false;
            mnuProveedores.Visible = false;
            mnuOrdenes.Visible = false;
            mnuReportes.Visible = false;

            // Opciones de ToolStrip
            btnTlsPacientes.Visible = false;
            btnTlsCitas.Visible = false;
            btnTlsHistoria.Visible = false;
            btnTlsTratamientos.Visible = false;
            btnTlsRecetas.Visible = false;
            btnTlsCuentas.Visible = false;
            btnTlsInventario.Visible = false;
            btnTlsReportes.Visible = false;
            btnTlsGraficos.Visible = false;

            // Submenús de Pacientes
            mnuPacientesNuevo.Visible = false;
            mnuPacientesFicha.Visible = false;

            // Submenús de Citas
            mnuCitasAgendar.Visible = false;
            mnuCitasAgenda.Visible = false;
            mnuCitasReprogramar.Visible = false;
            mnuCitasCancelar.Visible = false;

            // Historia clínica, Tratamientos y Recetas ya no tienen submenús:

            // Submenús de Reportes (solo se mostrarán los autorizados para cada rol)
            mnuReportesPacientes.Visible = false;
            mnuReportesCitas.Visible = false;
            mnuReportesTratamientos.Visible = false;
            mnuReportesRecetas.Visible = false;
            mnuReportesEstadosCuenta.Visible = false;
            mnuReportesInventario.Visible = false;
            mnuReportesProveedores.Visible = false;
            mnuReportesOrdenes.Visible = false;
            mnuReportesGraficos.Visible = false;
            mnuReportesDespachos.Visible = false;

            if (rol == "Administrador")
            {
                // El Administrador tiene acceso total a todos los módulos y submenús del sistema
                mnuSeguridad.Visible = true;
                mnuPacientes.Visible = true;
                mnuCitas.Visible = true;
                mnuHistoria.Visible = true;
                mnuTratamientos.Visible = true;
                mnuRecetas.Visible = true;
                mnuEstadosCuenta.Visible = true;
                mnuInventario.Visible = true;
                mnuDespacho.Visible = true;
                mnuProveedores.Visible = true;
                mnuOrdenes.Visible = true;
                mnuReportes.Visible = true;

                // Submenús Pacientes y Citas
                mnuPacientesNuevo.Visible = true;
                mnuPacientesFicha.Visible = true;
                mnuCitasAgendar.Visible = true;
                mnuCitasAgenda.Visible = true;

                // Todos los submenús de Reportes para Administrador
                mnuReportesPacientes.Visible = true;
                mnuReportesCitas.Visible = true;
                mnuReportesTratamientos.Visible = true;
                mnuReportesRecetas.Visible = true;
                mnuReportesEstadosCuenta.Visible = true;
                mnuReportesInventario.Visible = true;
                mnuReportesProveedores.Visible = true;
                mnuReportesOrdenes.Visible = true;
                mnuReportesGraficos.Visible = true;
                mnuReportesDespachos.Visible = true;

                // Barra de herramientas completa
                btnTlsPacientes.Visible = true;
                btnTlsCitas.Visible = true;
                btnTlsHistoria.Visible = true;
                btnTlsTratamientos.Visible = true;
                btnTlsRecetas.Visible = true;
                btnTlsCuentas.Visible = true;
                btnTlsInventario.Visible = true;
                btnTlsReportes.Visible = true;
                btnTlsGraficos.Visible = true;
            }
            else if (rol == "Recepcionista")
            {
                // Recepcionista: Pacientes (todo), Citas (todo), Estados de cuenta (todo), Reportes (Citas y Estados de cuenta)
                mnuPacientes.Visible = true;
                mnuPacientesNuevo.Visible = true;
                mnuPacientesFicha.Visible = true;

                mnuCitas.Visible = true;
                mnuCitasAgendar.Visible = true;
                mnuCitasAgenda.Visible = true;

                mnuEstadosCuenta.Visible = true;
                mnuDespacho.Visible = true;

                mnuReportes.Visible = true;
                mnuReportesCitas.Visible = true;
                mnuReportesEstadosCuenta.Visible = true;
                mnuReportesDespachos.Visible = true;

                btnTlsPacientes.Visible = true;
                btnTlsCitas.Visible = true;
                btnTlsCuentas.Visible = true;
                btnTlsReportes.Visible = true;
            }
            else if (rol == "Odontólogo")
            {
                // Odontólogo: Pacientes (solo consulta/ficha), Citas (solo agenda), Historia (todo), Tratamientos (todo), Recetas (todo), Reportes (Tratamientos y Recetas)
                mnuPacientes.Visible = true;
                mnuPacientesNuevo.Visible = false; // Solo lectura
                mnuPacientesFicha.Visible = true;

                mnuCitas.Visible = true;
                mnuCitasAgendar.Visible = false; // Solo consulta agenda
                mnuCitasAgenda.Visible = true;

                mnuHistoria.Visible = true;
                mnuTratamientos.Visible = true;
                mnuRecetas.Visible = true;

                mnuReportes.Visible = true;
                mnuReportesTratamientos.Visible = true;
                mnuReportesRecetas.Visible = true;

                btnTlsPacientes.Visible = true;
                btnTlsCitas.Visible = true;
                btnTlsHistoria.Visible = true;
                btnTlsTratamientos.Visible = true;
                btnTlsRecetas.Visible = true;
                btnTlsReportes.Visible = true;
            }
            else if (rol == "Asistente Odontológico")
            {
                // Asistente: Pacientes (solo consulta/ficha), Citas (solo agenda), Historia (abrir), Inventario, Proveedores, Órdenes, Reportes (Inventario y Órdenes)
                mnuPacientes.Visible = true;
                mnuPacientesNuevo.Visible = false; // Solo lectura
                mnuPacientesFicha.Visible = true;

                mnuCitas.Visible = true;
                mnuCitasAgendar.Visible = false; // Solo consulta agenda
                mnuCitasAgenda.Visible = true;

                mnuHistoria.Visible = true;

                mnuInventario.Visible = true;
                mnuDespacho.Visible = true;
                mnuProveedores.Visible = true;
                mnuOrdenes.Visible = true;

                mnuReportes.Visible = true;
                mnuReportesInventario.Visible = true;
                mnuReportesOrdenes.Visible = true;
                mnuReportesDespachos.Visible = true;

                btnTlsPacientes.Visible = true;
                btnTlsCitas.Visible = true;
                btnTlsHistoria.Visible = true;
                btnTlsInventario.Visible = true;
                btnTlsReportes.Visible = true;
            }

            // Privilegio "Reportes" (Roles y Privilegios): sin él no se muestran reportes ni gráficos
            if (!Sesion.TienePrivilegio(Permisos.Reportes))
            {
                mnuReportes.Visible = false;
                btnTlsReportes.Visible = false;
                btnTlsGraficos.Visible = false;
            }

            // Menú Archivo: si ya está iniciada la sesión, ocultamos "Iniciar sesión"
            mnuArchivo.Visible = true;
            mnuArchivoIniciarSesion.Visible = false;
            mnuArchivoCerrarSesion.Visible = true;
            mnuArchivoCambiarClave.Visible = true;
            mnuSalirBarra.Visible = true;
            mnuAyuda.Visible = true;
        }

        // Crea el submenú Reportes > Despachos con el mismo estilo que los demás reportes
        private void CrearMenuReporteDespachos()
        {
            mnuReportesDespachos.Name = "mnuReportesDespachos";
            mnuReportesDespachos.Text = "Despachos de medicamentos";
            mnuReportesDespachos.Image = Properties.Resources.ico_reporte;
            mnuReportesDespachos.Click += (s, e) => AbrirReporte("Despachos");

            int posicion = mnuReportes.DropDownItems.IndexOf(mnuReportesRecetas);
            if (posicion >= 0)
                mnuReportes.DropDownItems.Insert(posicion + 1, mnuReportesDespachos);
            else
                mnuReportes.DropDownItems.Add(mnuReportesDespachos);
        }

        // Carga los iconos PNG directamente desde los recursos del proyecto
        private void CargarIconosMenus()
        {
            // Menús principales
            mnuArchivo.Image = Properties.Resources.ico_usuario;
            mnuSeguridad.Image = Properties.Resources.ico_candado;
            mnuPacientes.Image = Properties.Resources.ico_paciente;
            mnuCitas.Image = Properties.Resources.ico_cita;
            mnuHistoria.Image = Properties.Resources.ico_historia;
            mnuTratamientos.Image = Properties.Resources.ico_tratamiento;
            mnuRecetas.Image = Properties.Resources.ico_receta;
            mnuEstadosCuenta.Image = Properties.Resources.ico_cuenta;
            mnuInventario.Image = Properties.Resources.ico_inventario;
            mnuDespacho.Image = Properties.Resources.ico_receta;
            mnuProveedores.Image = Properties.Resources.ico_proveedor;
            mnuOrdenes.Image = Properties.Resources.ico_orden;
            mnuReportes.Image = Properties.Resources.ico_reporte;
            mnuSalirBarra.Image = Properties.Resources.ico_salir;
            mnuAyuda.Image = Properties.Resources.ico_ayuda;

            // Submenús Archivo y Seguridad
            mnuArchivoIniciarSesion.Image = Properties.Resources.ico_candado;
            mnuArchivoCerrarSesion.Image = Properties.Resources.ico_salir;
            mnuArchivoCambiarClave.Image = Properties.Resources.ico_candado;

            mnuSeguridadUsuarios.Image = Properties.Resources.ico_usuario;
            mnuSeguridadRolesPrivilegios.Image = Properties.Resources.ico_rol;
            mnuSeguridadBitacora.Image = Properties.Resources.ico_bitacora;

            // Submenús Pacientes y Citas
            mnuPacientesNuevo.Image = Properties.Resources.ico_nuevo;
            mnuPacientesFicha.Image = Properties.Resources.ico_paciente;

            mnuCitasAgendar.Image = Properties.Resources.ico_nuevo;
            mnuCitasReprogramar.Image = Properties.Resources.ico_editar;
            mnuCitasCancelar.Image = Properties.Resources.ico_cancelar;
            mnuCitasAgenda.Image = Properties.Resources.ico_cita;

            // Submenús Estados de Cuenta
            mnuEstadosCuentaAbrir.Image = Properties.Resources.ico_cuenta;
            mnuEstadosCuentaCargo.Image = Properties.Resources.ico_cuenta;
            mnuEstadosCuentaAbono.Image = Properties.Resources.ico_cuenta;
            mnuEstadosCuentaSaldo.Image = Properties.Resources.ico_cuenta;

            // Submenús Inventario, Proveedores, Órdenes, Reportes
            mnuInventarioProductos.Image = Properties.Resources.ico_inventario;
            mnuInventarioAlertas.Image = Properties.Resources.ico_alerta;

            mnuProveedoresNuevo.Image = Properties.Resources.ico_nuevo;
            mnuProveedoresConsultar.Image = Properties.Resources.ico_buscar;
            mnuProveedoresModificar.Image = Properties.Resources.ico_editar;
            mnuProveedoresBaja.Image = Properties.Resources.ico_baja;

            mnuOrdenesNueva.Image = Properties.Resources.ico_nuevo;
            mnuOrdenesConsultar.Image = Properties.Resources.ico_buscar;
            mnuOrdenesRecibir.Image = Properties.Resources.ico_orden;

            mnuReportesPacientes.Image = Properties.Resources.ico_reporte;
            mnuReportesCitas.Image = Properties.Resources.ico_reporte;
            mnuReportesTratamientos.Image = Properties.Resources.ico_reporte;
            mnuReportesRecetas.Image = Properties.Resources.ico_reporte;
            mnuReportesEstadosCuenta.Image = Properties.Resources.ico_reporte;
            mnuReportesInventario.Image = Properties.Resources.ico_reporte;
            mnuReportesProveedores.Image = Properties.Resources.ico_reporte;
            mnuReportesOrdenes.Image = Properties.Resources.ico_reporte;
            mnuReportesGraficos.Image = Properties.Resources.ico_grafico;

            mnuAyudaManual.Image = Properties.Resources.ico_ayuda;
            mnuAyudaAcercaDe.Image = Properties.Resources.ico_ayuda;
        }

        // Abre o activa un formulario hijo adaptado perfectamente al área disponible
        public void AbrirFormulario(Form frm)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == frm.GetType())
                {
                    if (f != frm) frm.Dispose();
                    formularioActivo = f;
                    // Si el formulario estaba minimizado, se restaura antes de activarlo
                    if (f.WindowState == FormWindowState.Minimized)
                        f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    f.Activate();
                    AjustarTamanoHijo(f);
                    if (f is IRecargable recargable) recargable.Recargar();
                    return;
                }
            }

            // Oculta los botones cuyo privilegio no tiene el rol (Insertar, Modificar, Eliminar, Reportes...)
            Permisos.Aplicar(frm);

            frm.FormBorderStyle = FormBorderStyle.None;
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(0, 0);
            frm.AutoScroll = true;
            formularioActivo = frm;
            frm.Show();
            AjustarTamanoHijo(frm);
            frm.BringToFront();
        }

        private void RecargarFormularioActivo()
        {
            Form? actual = this.ActiveMdiChild;
            if (actual == formularioActivo) return;
            formularioActivo = actual;
            if (actual is IRecargable recargable && !actual.IsDisposed) recargable.Recargar();
        }

        private T ObtenerFormulario<T>() where T : Form, new()
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f is T existente) return existente;
            }
            return new T();
        }

        // Ajusta el formulario para que ocupe todo el espacio del contenedor MDI
        private void AjustarTamanoHijo(Form frm)
        {
            if (frm == null || frm.IsDisposed) return;

            foreach (Control c in this.Controls)
            {
                if (c is MdiClient mdiClient)
                {
                    frm.FormBorderStyle = FormBorderStyle.None;
                    frm.Location = new Point(0, 0);
                    frm.Size = mdiClient.ClientSize;
                    break;
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            foreach (Form f in this.MdiChildren)
            {
                AjustarTamanoHijo(f);
            }
        }

        // --- Cierre de sesión: vuelve al Login sin cerrar la aplicación ---
        private void CerrarSesionYVolverLogin()
        {
            // Cerrar ventanas hijas
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }

            BitacoraDatos.RegistrarAccion("Cierre de sesión", "El usuario " + Sesion.NombreUsuario + " cerró sesión.");
            Sesion.CerrarSesion();

            FrmLogin login = new FrmLogin();
            login.Show();

            // Desactivar el evento FormClosing para no cerrar la app
            this.FormClosing -= FrmPrincipal_FormClosing;
            this.Close();
        }

        // --- Eventos Menú Archivo ---
        private void mnuArchivoIniciarSesion_Click(object sender, EventArgs e)
        {
            CerrarSesionYVolverLogin();
        }

        private void mnuArchivoCerrarSesion_Click(object sender, EventArgs e)
        {
            CerrarSesionYVolverLogin();
        }

        private void mnuArchivoCambiarClave_Click(object sender, EventArgs e)
        {
            FrmCambiarClave frm = new FrmCambiarClave();
            frm.ShowDialog(this);
        }

        // --- Evento "Salir" en la barra de menú: cierra sesión y vuelve al Login ---
        private void mnuSalirBarra_Click(object sender, EventArgs e)
        {
            CerrarSesionYVolverLogin();
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Solo cerrar si el usuario confirma o si es cierre programado
            if (e.CloseReason == CloseReason.UserClosing)
            {
                CerrarSesionYVolverLogin();
                e.Cancel = true; // Cancelar el cierre directo; CerrarSesionYVolverLogin maneja el flujo
            }
        }

        // --- Eventos Menú Seguridad ---
        private void mnuSeguridadUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmUsuarios());
        }

        private void mnuSeguridadRolesPrivilegios_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmRolesPrivilegios());
        }

        private void mnuSeguridadBitacora_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmBitacora());
        }

        // --- Eventos Menú Pacientes (Simplificado: 2 opciones) ---
        private void mnuPacientesNuevo_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmPacientes());
        }

        private void mnuPacientesFicha_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmFichaPaciente());
        }

        // --- Eventos Menú Citas ---
        private void mnuCitasAgendar_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmCitas());
        }

        private void mnuCitasReprogramar_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmCitas());
        }

        private void mnuCitasCancelar_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmCitas());
        }

        private void mnuCitasAgenda_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmAgenda());
        }

        // Verifica en la apertura que el rol actual esté autorizado
        // (ocultar el menú no basta: el formulario también puede abrirse desde la barra de herramientas)
        private bool RolAutorizado(params string[] rolesPermitidos)
        {
            foreach (string rol in rolesPermitidos)
            {
                if (Sesion.EsRol(rol)) return true;
            }
            MessageBox.Show("Su rol no tiene acceso a este módulo.", "Acceso denegado",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        // --- Accesos directos (sin submenús): Historia clínica, Tratamientos y Recetas ---
        private void mnuHistoria_Click(object sender, EventArgs e)
        {
            if (!RolAutorizado("Administrador", "Odontólogo", "Asistente Odontológico")) return;
            AbrirFormulario(ObtenerFormulario<FrmHistoriaClinica>());
        }

        private void mnuTratamientos_Click(object sender, EventArgs e)
        {
            if (!RolAutorizado("Administrador", "Odontólogo")) return;
            AbrirFormulario(ObtenerFormulario<FrmTratamientos>());
        }

        private void mnuRecetas_Click(object sender, EventArgs e)
        {
            if (!RolAutorizado("Administrador", "Odontólogo")) return;
            AbrirFormulario(ObtenerFormulario<FrmRecetas>());
        }

        // --- Eventos Menú Estados de Cuenta ---
        private void mnuEstadosCuenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmEstadosCuenta());
        }

        private void mnuEstadosCuentaAbrir_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmEstadosCuenta());
        }

        private void mnuEstadosCuentaCargo_Click(object sender, EventArgs e)
        {
            FrmEstadosCuenta frm = ObtenerFormulario<FrmEstadosCuenta>();
            frm.SeleccionarTipo("Cargo");
            AbrirFormulario(frm);
        }

        private void mnuEstadosCuentaAbono_Click(object sender, EventArgs e)
        {
            FrmEstadosCuenta frm = ObtenerFormulario<FrmEstadosCuenta>();
            frm.SeleccionarTipo("Abono");
            AbrirFormulario(frm);
        }

        private void mnuEstadosCuentaSaldo_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmEstadosCuenta());
        }

        // --- Eventos Menú Inventario ---
        private void mnuDespacho_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmDespacho());
        }

        private void mnuInventarioProductos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmProductos());
        }

        private void mnuInventarioAlertas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmAlertasStock());
        }

        // --- Eventos Menú Proveedores ---
        private void mnuProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmProveedores());
        }

        private void mnuProveedoresNuevo_Click(object sender, EventArgs e)
        {
            FrmProveedores frm = ObtenerFormulario<FrmProveedores>();
            frm.PrepararNuevo();
            AbrirFormulario(frm);
        }

        private void mnuProveedoresConsultar_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmProveedores());
        }

        private void mnuProveedoresModificar_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmProveedores());
        }

        private void mnuProveedoresBaja_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmProveedores());
        }

        // --- Eventos Menú Órdenes de Compra ---
        private void mnuOrdenes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmOrdenesCompra());
        }

        private void mnuOrdenesNueva_Click(object sender, EventArgs e)
        {
            FrmOrdenesCompra frm = ObtenerFormulario<FrmOrdenesCompra>();
            frm.PrepararNueva();
            AbrirFormulario(frm);
        }

        private void mnuOrdenesConsultar_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmOrdenesCompra());
        }

        private void mnuOrdenesRecibir_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmOrdenesCompra());
        }

        // --- Eventos Menú Reportes (Corregidos: cada submenú abre un nuevo formulario con el reporte correcto) ---
        private void AbrirReporte(string nombreReporte)
        {
            if (!Permisos.Verificar(Permisos.Reportes)) return;

            // Cerrar el reporte anterior si existe para permitir abrir con el reporte correcto
            foreach (Form f in this.MdiChildren)
            {
                if (f is FrmReportes)
                {
                    f.Close();
                    break;
                }
            }
            FrmReportes frm = new FrmReportes();
            frm.ReporteInicial = nombreReporte;
            AbrirFormulario(frm);
        }

        private void mnuReportesPacientes_Click(object sender, EventArgs e)
        {
            AbrirReporte("Pacientes");
        }

        private void mnuReportesCitas_Click(object sender, EventArgs e)
        {
            AbrirReporte("Citas");
        }

        private void mnuReportesTratamientos_Click(object sender, EventArgs e)
        {
            AbrirReporte("Tratamientos");
        }

        private void mnuReportesRecetas_Click(object sender, EventArgs e)
        {
            AbrirReporte("Recetas");
        }

        private void mnuReportesEstadosCuenta_Click(object sender, EventArgs e)
        {
            AbrirReporte("Estados de cuenta");
        }

        private void mnuReportesInventario_Click(object sender, EventArgs e)
        {
            AbrirReporte("Inventario");
        }

        private void mnuReportesProveedores_Click(object sender, EventArgs e)
        {
            AbrirReporte("Proveedores");
        }

        private void mnuReportesOrdenes_Click(object sender, EventArgs e)
        {
            AbrirReporte("Órdenes de compra");
        }

        private void mnuReportesGraficos_Click(object sender, EventArgs e)
        {
            if (!Permisos.Verificar(Permisos.Reportes)) return;
            AbrirFormulario(new FrmGraficos());
        }

        // --- Eventos Menú Ayuda ---
        // Abre el manual de usuario en PDF correspondiente al rol de la sesión
        // (archivos en Recursos\Manuales, copiados junto al ejecutable al compilar)
        private void mnuAyudaManual_Click(object sender, EventArgs e)
        {
            string archivo;
            switch (Sesion.NombreRol)
            {
                case "Administrador": archivo = "Manual_Administrador.pdf"; break;
                case "Recepcionista": archivo = "Manual_Recepcionista.pdf"; break;
                case "Odontólogo": archivo = "Manual_Odontologo.pdf"; break;
                case "Asistente Odontológico": archivo = "Manual_Asistente.pdf"; break;
                default:
                    MessageBox.Show("No hay un manual de usuario para el rol " + Sesion.NombreRol + ".", "Manual de Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
            }

            string ruta = Path.Combine(AppContext.BaseDirectory, "Recursos", "Manuales", archivo);
            if (!File.Exists(ruta))
            {
                MessageBox.Show("No se encontró el manual de usuario:\n" + ruta + "\n\nVerifique que la carpeta Recursos\\Manuales esté junto al programa.", "Manual de Usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(ruta) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir el manual: " + ex.Message + "\nAbra el archivo manualmente desde:\n" + ruta, "Manual de Usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void mnuAyudaAcercaDe_Click(object sender, EventArgs e)
        {
            FrmAcercaDe frm = new FrmAcercaDe();
            frm.ShowDialog(this);
        }

        private void pnlSuperior_Paint(object sender, PaintEventArgs e)
        {
        }
    }

    // Renderer personalizado para mejorar contraste de menús desplegables
    public class MenuRendererToothly : ToolStripProfessionalRenderer
    {
        public MenuRendererToothly() : base(new MenuColoresPersonalizados()) { }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip is MenuStrip)
            {
                using (var brush = new SolidBrush(Color.FromArgb(8, 86, 140)))
                {
                    e.Graphics.FillRectangle(brush, e.AffectedBounds);
                }
            }
            else
            {
                base.OnRenderToolStripBackground(e);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            // Items de nivel superior (en la barra): texto blanco, o azul oscuro al pasar el cursor
            if (e.Item.OwnerItem == null && e.Item.Owner is MenuStrip)
            {
                e.TextColor = e.Item.Selected ? Color.FromArgb(8, 86, 140) : Color.White;
            }
            // Items en menús desplegables: texto oscuro para buena legibilidad sobre fondo blanco
            else
            {
                e.TextColor = e.Item.Selected ? Color.FromArgb(8, 86, 140) : Color.FromArgb(30, 41, 59);
            }
            base.OnRenderItemText(e);
        }
    }

    public class MenuColoresPersonalizados : ProfessionalColorTable
    {
        // Fondo del menú desplegable
        public override Color ToolStripDropDownBackground => Color.White;
        // Selección: azul claro con buen contraste
        public override Color MenuItemSelected => Color.FromArgb(186, 230, 253);
        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(186, 230, 253);
        public override Color MenuItemSelectedGradientEnd => Color.FromArgb(186, 230, 253);
        public override Color MenuItemPressedGradientBegin => Color.FromArgb(147, 197, 253);
        public override Color MenuItemPressedGradientEnd => Color.FromArgb(147, 197, 253);
        // Bordes
        public override Color MenuItemBorder => Color.FromArgb(108, 180, 234);
        public override Color MenuBorder => Color.FromArgb(203, 213, 225);
        // Separadores
        public override Color SeparatorDark => Color.FromArgb(226, 232, 240);
        public override Color SeparatorLight => Color.White;
        // Barra lateral de iconos
        public override Color ImageMarginGradientBegin => Color.FromArgb(243, 249, 253);
        public override Color ImageMarginGradientMiddle => Color.FromArgb(243, 249, 253);
        public override Color ImageMarginGradientEnd => Color.FromArgb(243, 249, 253);
        // Barra de menú
        public override Color MenuStripGradientBegin => Color.FromArgb(8, 86, 140);
        public override Color MenuStripGradientEnd => Color.FromArgb(8, 86, 140);
    }
}
