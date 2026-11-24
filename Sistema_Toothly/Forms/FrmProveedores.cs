using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmProveedores : Form, IRecargable
    {
        private int idProveedorSeleccionado = 0;

        public FrmProveedores()
        {
            InitializeComponent();
        }

        private void FrmProveedores_Load(object sender, EventArgs e)
        {
            // Botón de cerrar en el encabezado
            AgregarBotonCerrar();

            // Colores de los botones
            btnBuscar.Image = Properties.Resources.ico_buscar;

            btnNuevo.Image = Properties.Resources.ico_nuevo;

            btnGuardar.Image = Properties.Resources.ico_guardar;

            btnModificar.Image = Properties.Resources.ico_editar;

            btnDarDeBaja.Image = Properties.Resources.ico_baja;

            // Formato de la tabla
            ConfigurarEstiloTabla();

            ConfigurarColumnas();
            CargarProveedores();
            cmbEstado.SelectedIndex = 0;
            // Solo quien puede dar de baja puede cambiar el estado
            cmbEstado.Enabled = Sesion.TienePrivilegio(Permisos.Eliminar);
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

        // Colores y diseño de la tabla de proveedores
        private void ConfigurarEstiloTabla()
        {
            dgvProveedores.BackgroundColor = Color.White;
            dgvProveedores.BorderStyle = BorderStyle.None;
            dgvProveedores.EnableHeadersVisualStyles = false;
            dgvProveedores.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(108, 180, 234);
            dgvProveedores.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProveedores.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvProveedores.ColumnHeadersHeight = 35;
            dgvProveedores.DefaultCellStyle.SelectionBackColor = Color.FromArgb(186, 230, 253);
            dgvProveedores.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvProveedores.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 254);
            dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedores.ReadOnly = true;
            dgvProveedores.RowHeadersVisible = false;
            dgvProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void PrepararNuevo()
        {
            btnNuevo_Click(this, EventArgs.Empty);
        }

        private void ConfigurarColumnas()
        {
            dgvProveedores.Columns.Clear();
            dgvProveedores.Columns.Add("IdProveedor", "ID");
            dgvProveedores.Columns.Add("Nombre", "Nombre de Empresa");
            dgvProveedores.Columns.Add("Telefono", "Teléfono");
            dgvProveedores.Columns.Add("Correo", "Correo");
            dgvProveedores.Columns.Add("Direccion", "Dirección");
            dgvProveedores.Columns.Add("Estado", "Estado");

            dgvProveedores.Columns["IdProveedor"].Width = 50;
            dgvProveedores.Columns["Nombre"].Width = 220;
            dgvProveedores.Columns["Telefono"].Width = 110;
            dgvProveedores.Columns["Correo"].Width = 200;
            dgvProveedores.Columns["Direccion"].Width = 260;
            dgvProveedores.Columns["Estado"].Width = 90;
        }

        private void CargarProveedores()
        {
            dgvProveedores.Rows.Clear();
            List<Proveedor> lista = ProveedorDatos.Listar();
            foreach (var p in lista)
            {
                string estadoTexto = (p.Estado == "A") ? "Activo" : "Inactivo";
                dgvProveedores.Rows.Add(p.IdProveedor, p.Nombre, p.Telefono, p.Correo, p.Direccion, estadoTexto);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();
            dgvProveedores.Rows.Clear();
            List<Proveedor> lista = ProveedorDatos.Buscar(texto);
            foreach (var p in lista)
            {
                string estadoTexto = (p.Estado == "A") ? "Activo" : "Inactivo";
                dgvProveedores.Rows.Add(p.IdProveedor, p.Nombre, p.Telefono, p.Correo, p.Direccion, estadoTexto);
            }
        }

        public void Recargar()
        {
            Recarga.Grid(dgvProveedores, () =>
            {
                if (txtBuscar.Text.Trim() == "") CargarProveedores();
                else btnBuscar_Click(this, EventArgs.Empty);
            });
        }

        private void dgvProveedores_SelectionChanged(object sender, EventArgs e)
        {
            if (Recarga.EnCurso) return;
            if (dgvProveedores.CurrentRow != null && dgvProveedores.CurrentRow.Index >= 0)
            {
                var celdaId = dgvProveedores.CurrentRow.Cells["IdProveedor"].Value;
                if (celdaId != null)
                {
                    idProveedorSeleccionado = Convert.ToInt32(celdaId);
                    foreach (var p in ProveedorDatos.proveedores)
                    {
                        if (p.IdProveedor == idProveedorSeleccionado)
                        {
                            txtNombre.Text = p.Nombre;
                            txtTelefono.Text = p.Telefono;
                            txtCorreo.Text = p.Correo;
                            txtDireccion.Text = p.Direccion;
                            cmbEstado.SelectedItem = (p.Estado == "A") ? "Activo" : "Inactivo";
                            break;
                        }
                    }
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            idProveedorSeleccionado = 0;
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Evita crear un duplicado al pulsar Guardar con un registro ya seleccionado
            if (idProveedorSeleccionado != 0 && !Mensaje.Confirmar(
                    "Tiene seleccionado un registro existente.\n¿Desea guardarlo como un NUEVO proveedor?\n\n" +
                    "Si solo quiere actualizar el registro seleccionado, pulse \"No\" y use el botón Modificar.",
                    "Guardar como nuevo"))
                return;
            if (!ValidarCampos()) return;
            if (NombreEnUso(txtNombre.Text.Trim(), 0)) return;

            Proveedor p = new Proveedor();
            p.Nombre = txtNombre.Text.Trim();
            p.Telefono = txtTelefono.Text.Trim();
            p.Correo = txtCorreo.Text.Trim();
            p.Direccion = txtDireccion.Text.Trim();
            p.Estado = (cmbEstado.SelectedItem?.ToString() == "Activo") ? "A" : "I";

            ProveedorDatos.Agregar(p);
            BitacoraDatos.RegistrarAccion("Registro de proveedor", "Se registró al proveedor: " + p.Nombre);

            MessageBox.Show("Proveedor guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarProveedores();
            if (!Recarga.SeleccionarFila(dgvProveedores, p.IdProveedor)) LimpiarCampos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idProveedorSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un proveedor de la tabla para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos()) return;
            if (NombreEnUso(txtNombre.Text.Trim(), idProveedorSeleccionado)) return;

            foreach (var p in ProveedorDatos.proveedores)
            {
                if (p.IdProveedor == idProveedorSeleccionado)
                {
                    p.Nombre = txtNombre.Text.Trim();
                    p.Telefono = txtTelefono.Text.Trim();
                    p.Correo = txtCorreo.Text.Trim();
                    p.Direccion = txtDireccion.Text.Trim();
                    p.Estado = (cmbEstado.SelectedItem?.ToString() == "Activo") ? "A" : "I";
                    ProveedorDatos.Modificar(p);
                    break;
                }
            }

            BitacoraDatos.RegistrarAccion("Modificación de proveedor", "Se actualizó proveedor ID " + idProveedorSeleccionado);
            MessageBox.Show("Proveedor modificado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            int id = idProveedorSeleccionado;
            CargarProveedores();
            if (!Recarga.SeleccionarFila(dgvProveedores, id)) LimpiarCampos();
        }

        private void btnDarDeBaja_Click(object sender, EventArgs e)
        {
            if (idProveedorSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un proveedor para dar de baja.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Mensaje.Confirmar("¿Está seguro de dar de baja al proveedor seleccionado?", "Confirmación"))
            {
                ProveedorDatos.DarDeBaja(idProveedorSeleccionado);
                BitacoraDatos.RegistrarAccion("Baja de proveedor", "Se dio de baja al proveedor ID " + idProveedorSeleccionado);
                MessageBox.Show("Proveedor dado de baja (Inactivo).", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                int id = idProveedorSeleccionado;
                CargarProveedores();
                if (!Recarga.SeleccionarFila(dgvProveedores, id)) LimpiarCampos();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            dgvProveedores.CurrentCell = null;
            dgvProveedores.ClearSelection();
            idProveedorSeleccionado = 0;
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            cmbEstado.SelectedIndex = 0;
        }

        // Evita registrar dos proveedores con el mismo nombre
        private bool NombreEnUso(string nombre, int idExcluir)
        {
            foreach (var p in ProveedorDatos.proveedores)
            {
                if (p.IdProveedor != idExcluir && p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Ya existe un proveedor con el nombre \"" + nombre + "\".", "Registro Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return true;
                }
            }
            return false;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del proveedor es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("El teléfono es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCorreo.Text) || !txtCorreo.Text.Contains("@"))
            {
                MessageBox.Show("El correo es obligatorio y debe contener '@'.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return false;
            }
            return true;
        }
    }
}
