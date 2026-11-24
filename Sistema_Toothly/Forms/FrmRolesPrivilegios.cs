using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmRolesPrivilegios : Form
    {
        public FrmRolesPrivilegios()
        {
            InitializeComponent();
        }

        private void FrmRolesPrivilegios_Load(object sender, EventArgs e)
        {
            // Botón de cerrar en el encabezado
            AgregarBotonCerrar();

            // Colores de los botones
            btnGuardar.Image = Properties.Resources.ico_guardar;

            btnCancelar.Image = Properties.Resources.ico_cancelar;

            // Llenar roles
            cmbRol.Items.Clear();
            foreach (var r in UsuarioDatos.roles)
            {
                cmbRol.Items.Add(r.NombreRol);
            }
            if (cmbRol.Items.Count > 0)
                cmbRol.SelectedIndex = 0;
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

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idRol = ObtenerIdRolSeleccionado();

            // Cargar estado de los checkboxes según RolPrivilegio
            chkInsertar.Checked = UsuarioDatos.TienePrivilegio(idRol, 1);
            chkConsultar.Checked = UsuarioDatos.TienePrivilegio(idRol, 2);
            chkModificar.Checked = UsuarioDatos.TienePrivilegio(idRol, 3);
            chkEliminar.Checked = UsuarioDatos.TienePrivilegio(idRol, 4);
            chkReportes.Checked = UsuarioDatos.TienePrivilegio(idRol, 5);

            // El Administrador conserva siempre todos los privilegios (evita que el sistema quede sin administrador)
            bool esAdministrador = (cmbRol.SelectedItem?.ToString() == "Administrador");
            if (esAdministrador)
            {
                chkInsertar.Checked = true;
                chkConsultar.Checked = true;
                chkModificar.Checked = true;
                chkEliminar.Checked = true;
                chkReportes.Checked = true;
            }
            chkInsertar.Enabled = !esAdministrador;
            chkConsultar.Enabled = !esAdministrador;
            chkModificar.Enabled = !esAdministrador;
            chkEliminar.Enabled = !esAdministrador;
            chkReportes.Enabled = !esAdministrador;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int idRol = ObtenerIdRolSeleccionado();

            // Eliminar privilegios existentes del rol
            List<RolPrivilegio> nuevaLista = new List<RolPrivilegio>();
            foreach (var rp in UsuarioDatos.rolPrivilegios)
            {
                if (rp.IdRol != idRol)
                    nuevaLista.Add(rp);
            }

            // Agregar privilegios seleccionados
            if (chkInsertar.Checked) nuevaLista.Add(new RolPrivilegio { IdRol = idRol, IdPrivilegio = 1 });
            if (chkConsultar.Checked) nuevaLista.Add(new RolPrivilegio { IdRol = idRol, IdPrivilegio = 2 });
            if (chkModificar.Checked) nuevaLista.Add(new RolPrivilegio { IdRol = idRol, IdPrivilegio = 3 });
            if (chkEliminar.Checked) nuevaLista.Add(new RolPrivilegio { IdRol = idRol, IdPrivilegio = 4 });
            if (chkReportes.Checked) nuevaLista.Add(new RolPrivilegio { IdRol = idRol, IdPrivilegio = 5 });

            UsuarioDatos.rolPrivilegios = nuevaLista;
            BitacoraDatos.RegistrarAccion("Actualización de privilegios", "Se actualizaron los privilegios para el rol " + cmbRol.SelectedItem?.ToString());

            // Si se modificó el rol del usuario conectado, se actualiza su sesión
            if (idRol == Sesion.IdRol)
            {
                Sesion.CargarPrivilegios(UsuarioDatos.ObtenerPrivilegiosRol(idRol));
            }

            MessageBox.Show("Privilegios guardados exitosamente.\nLos usuarios de este rol verán los cambios la próxima vez que inicien sesión.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cmbRol_SelectedIndexChanged(sender, e);
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
