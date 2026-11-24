using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmUsuarios : Form, IRecargable
    {
        private int idUsuarioSeleccionado = 0;
        cls_encriptar objEncriptar = new cls_encriptar();

        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            // Botón de cerrar en el encabezado
            AgregarBotonCerrar();

            btnBuscar.Image = Properties.Resources.ico_buscar;

            btnNuevo.Image = Properties.Resources.ico_nuevo;

            btnGuardar.Image = Properties.Resources.ico_guardar;

            btnModificar.Image = Properties.Resources.ico_editar;

            btnDarDeBaja.Image = Properties.Resources.ico_baja;

            btnCancelar.Image = Properties.Resources.ico_cancelar;

            // Formato de la tabla
            ConfigurarEstiloTabla();

            // Configurar columnas de la tabla
            ConfigurarColumnas();

            // Cargar roles en el ComboBox
            LlenarComboRoles();

            // Cargar datos
            CargarUsuarios();
            cmbEstado.SelectedIndex = 0;
        }

        // Agregamos un botón para cerrar la ventana en la esquina superior
        private void AgregarBotonCerrar()
        {
            Button btnCerrar = new Button();
            btnCerrar.Text = "✕ Cerrar";
            btnCerrar.Size = new Size(85, 30);
            btnCerrar.Location = new Point(pnlEncabezado.Width - 95, 10);
            btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrar.Click += (s, e) => this.Close();
            pnlEncabezado.Controls.Add(btnCerrar);
            btnCerrar.BackColor = Color.FromArgb(220, 38, 38);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Cursor = Cursors.Hand;
        }

        // Colores y diseño de la tabla de usuarios
        private void ConfigurarEstiloTabla()
        {
            dgvUsuarios.BackgroundColor = Color.White;
            dgvUsuarios.BorderStyle = BorderStyle.None;
            dgvUsuarios.EnableHeadersVisualStyles = false;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(108, 180, 234);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvUsuarios.ColumnHeadersHeight = 35;
            dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.FromArgb(186, 230, 253);
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvUsuarios.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 254);
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ConfigurarColumnas()
        {
            dgvUsuarios.Columns.Clear();
            dgvUsuarios.Columns.Add("IdUsuario", "ID");
            dgvUsuarios.Columns.Add("NombreCompleto", "Nombre Completo");
            dgvUsuarios.Columns.Add("NombreUsuario", "Usuario");
            dgvUsuarios.Columns.Add("Correo", "Correo");
            dgvUsuarios.Columns.Add("Rol", "Rol");
            dgvUsuarios.Columns.Add("Estado", "Estado");

            dgvUsuarios.Columns["IdUsuario"].Width = 60;
            dgvUsuarios.Columns["NombreCompleto"].Width = 220;
            dgvUsuarios.Columns["NombreUsuario"].Width = 120;
            dgvUsuarios.Columns["Correo"].Width = 180;
            dgvUsuarios.Columns["Rol"].Width = 140;
            dgvUsuarios.Columns["Estado"].Width = 90;
        }

        private void LlenarComboRoles()
        {
            cmbRol.Items.Clear();
            foreach (var r in UsuarioDatos.roles)
            {
                cmbRol.Items.Add(r.NombreRol);
            }
            if (cmbRol.Items.Count > 0)
                cmbRol.SelectedIndex = 0;
        }

        private void CargarUsuarios()
        {
            dgvUsuarios.Rows.Clear();
            List<Usuario> lista = UsuarioDatos.Listar();
            foreach (var u in lista)
            {
                string estadoTexto = (u.Estado == "A") ? "Activo" : "Inactivo";
                string rolTexto = UsuarioDatos.ObtenerNombreRol(u.IdRol);
                dgvUsuarios.Rows.Add(u.IdUsuario, u.NombreCompleto, u.NombreUsuario, u.Correo, rolTexto, estadoTexto);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();
            dgvUsuarios.Rows.Clear();
            List<Usuario> lista = UsuarioDatos.Buscar(texto);
            foreach (var u in lista)
            {
                string estadoTexto = (u.Estado == "A") ? "Activo" : "Inactivo";
                string rolTexto = UsuarioDatos.ObtenerNombreRol(u.IdRol);
                dgvUsuarios.Rows.Add(u.IdUsuario, u.NombreCompleto, u.NombreUsuario, u.Correo, rolTexto, estadoTexto);
            }
        }

        public void Recargar()
        {
            Recarga.Combos(LlenarComboRoles, cmbRol);
            Recarga.Grid(dgvUsuarios, () =>
            {
                if (txtBuscar.Text.Trim() == "") CargarUsuarios();
                else btnBuscar_Click(this, EventArgs.Empty);
            });
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (Recarga.EnCurso) return;
            if (dgvUsuarios.CurrentRow != null && dgvUsuarios.CurrentRow.Index >= 0)
            {
                var celdaId = dgvUsuarios.CurrentRow.Cells["IdUsuario"].Value;
                if (celdaId != null)
                {
                    idUsuarioSeleccionado = Convert.ToInt32(celdaId);
                    foreach (var u in UsuarioDatos.usuarios)
                    {
                        if (u.IdUsuario == idUsuarioSeleccionado)
                        {
                            txtNombreCompleto.Text = u.NombreCompleto;
                            txtNombreUsuario.Text = u.NombreUsuario;
                            txtCorreo.Text = u.Correo;
                            txtClave.Text = objEncriptar.desencriptar(u.Clave, cls_encriptar.Llave);
                            cmbRol.SelectedItem = UsuarioDatos.ObtenerNombreRol(u.IdRol);
                            cmbEstado.SelectedItem = (u.Estado == "A") ? "Activo" : "Inactivo";
                            break;
                        }
                    }
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            idUsuarioSeleccionado = 0;
            txtNombreCompleto.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (!ValidarCampos()) return;

            // Verificar si el usuario ya existe
            foreach (var existing in UsuarioDatos.usuarios)
            {
                if (existing.NombreUsuario.ToLower() == txtNombreUsuario.Text.Trim().ToLower())
                {
                    MessageBox.Show("El nombre de usuario ya está registrado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            Usuario u = new Usuario();
            u.NombreCompleto = txtNombreCompleto.Text.Trim();
            u.NombreUsuario = txtNombreUsuario.Text.Trim();
            u.Correo = txtCorreo.Text.Trim();
            u.Clave = objEncriptar.encriptar(txtClave.Text.Trim(), cls_encriptar.Llave);
            u.IdRol = ObtenerIdRolSeleccionado();
            u.Estado = (cmbEstado.SelectedItem?.ToString() == "Activo") ? "A" : "I";

            UsuarioDatos.Agregar(u);
            BitacoraDatos.RegistrarAccion("Registro de usuario", "Se creó el usuario " + u.NombreUsuario);

            MessageBox.Show("Usuario registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarUsuarios();
            if (!Recarga.SeleccionarFila(dgvUsuarios, u.IdUsuario)) LimpiarCampos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un usuario de la tabla para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos()) return;

            Usuario? u = null;
            foreach (var user in UsuarioDatos.usuarios)
            {
                if (user.IdUsuario == idUsuarioSeleccionado)
                {
                    u = user;
                    break;
                }
            }

            // El nombre de usuario no puede repetirse con otra cuenta
            foreach (var otro in UsuarioDatos.usuarios)
            {
                if (otro.IdUsuario != idUsuarioSeleccionado &&
                    otro.NombreUsuario.Equals(txtNombreUsuario.Text.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("El nombre de usuario ya pertenece a otra cuenta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombreUsuario.Focus();
                    return;
                }
            }

            // La cuenta principal y la cuenta en sesión no pueden inactivarse ni cambiar de rol
            if (u != null && (u.IdUsuario == 1 || u.IdUsuario == Sesion.IdUsuario))
            {
                bool inactiva = cmbEstado.SelectedItem?.ToString() != "Activo";
                if (inactiva || ObtenerIdRolSeleccionado() != u.IdRol)
                {
                    MessageBox.Show("No puede inactivar ni cambiar el rol de la cuenta principal o de la cuenta con sesión activa.", "Acción Denegada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (u != null)
            {
                u.NombreCompleto = txtNombreCompleto.Text.Trim();
                u.NombreUsuario = txtNombreUsuario.Text.Trim();
                u.Correo = txtCorreo.Text.Trim();
                u.Clave = objEncriptar.encriptar(txtClave.Text.Trim(), cls_encriptar.Llave);
                u.IdRol = ObtenerIdRolSeleccionado();
                u.Estado = (cmbEstado.SelectedItem?.ToString() == "Activo") ? "A" : "I";

                UsuarioDatos.Modificar(u);
                BitacoraDatos.RegistrarAccion("Modificación de usuario", "Se actualizó el usuario " + u.NombreUsuario);

                MessageBox.Show("Usuario modificado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
                if (!Recarga.SeleccionarFila(dgvUsuarios, u.IdUsuario)) LimpiarCampos();
            }
        }

        private void btnDarDeBaja_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un usuario de la tabla para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Regla de seguridad: No permitir eliminar al usuario principal 'admin' ni a la cuenta con sesión activa
            if (idUsuarioSeleccionado == 1)
            {
                MessageBox.Show("No se puede eliminar la cuenta principal de administrador del sistema.", "Acción Denegada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (idUsuarioSeleccionado == Sesion.IdUsuario)
            {
                MessageBox.Show("No puede eliminar la cuenta con la que ha iniciado sesión actualmente.", "Acción Denegada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Mensaje.Confirmar("¿Está seguro de eliminar al usuario seleccionado?", "Confirmación de Eliminación"))
            {
                UsuarioDatos.DarDeBaja(idUsuarioSeleccionado);
                BitacoraDatos.RegistrarAccion("Eliminación de usuario", "Se dio de baja / eliminó al usuario ID " + idUsuarioSeleccionado);

                MessageBox.Show("Usuario eliminado exitosamente (estado Inactivo).", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                int id = idUsuarioSeleccionado;
                CargarUsuarios();
                if (!Recarga.SeleccionarFila(dgvUsuarios, id)) LimpiarCampos();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            dgvUsuarios.CurrentCell = null;
            dgvUsuarios.ClearSelection();
            idUsuarioSeleccionado = 0;
            txtNombreCompleto.Clear();
            txtNombreUsuario.Clear();
            txtCorreo.Clear();
            txtClave.Clear();
            txtBuscar.Clear();
            if (cmbRol.Items.Count > 0) cmbRol.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text))
            {
                MessageBox.Show("El nombre completo es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCompleto.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
            {
                MessageBox.Show("El nombre de usuario es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreUsuario.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCorreo.Text) || !txtCorreo.Text.Contains("@"))
            {
                MessageBox.Show("El correo es obligatorio y debe contener un formato válido con '@'.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtClave.Text))
            {
                MessageBox.Show("La contraseña es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtClave.Focus();
                return false;
            }
            return true;
        }

        private int ObtenerIdRolSeleccionado()
        {
            string nombre = cmbRol.SelectedItem?.ToString() ?? "";
            foreach (var r in UsuarioDatos.roles)
            {
                if (r.NombreRol == nombre) return r.IdRol;
            }
            return 1;
        }
    }
}
