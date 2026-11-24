using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sistema_Toothly.Clases;
using Sistema_Toothly.Datos;
using Sistema_Toothly.Utilidades;

namespace Sistema_Toothly.Forms
{
    public partial class FrmEstadosCuenta : Form, IRecargable
    {
        private int idPacienteActual = 0;
        private int idEstadoActual = 0;

        public FrmEstadosCuenta()
        {
            InitializeComponent();
        }

        private void FrmEstadosCuenta_Load(object sender, EventArgs e)
        {
            // Botón de cerrar en el encabezado
            AgregarBotonCerrar();

            // Colores de los botones
            btnCerrarEncabezado();
            btnCargar.Image = Properties.Resources.ico_buscar;

            btnRegistrar.Image = Properties.Resources.ico_guardar;

            // Formato de la tabla
            ConfigurarEstiloTabla();
            ConfigurarColumnas();
            LlenarComboPacientes();
            if (cmbTipo.SelectedIndex < 0) cmbTipo.SelectedIndex = 0;
        }

        public void Recargar()
        {
            Recarga.Combos(LlenarComboPacientes, cmbPaciente);
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

        private void btnCerrarEncabezado()
        {
        }

        // Colores y diseño de la tabla de movimientos
        private void ConfigurarEstiloTabla()
        {
            dgvMovimientos.BackgroundColor = Color.White;
            dgvMovimientos.BorderStyle = BorderStyle.None;
            dgvMovimientos.EnableHeadersVisualStyles = false;
            dgvMovimientos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(108, 180, 234);
            dgvMovimientos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMovimientos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvMovimientos.ColumnHeadersHeight = 35;
            dgvMovimientos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(186, 230, 253);
            dgvMovimientos.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvMovimientos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 254);
            dgvMovimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovimientos.ReadOnly = true;
            dgvMovimientos.RowHeadersVisible = false;
            dgvMovimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void SeleccionarTipo(string tipo)
        {
            if (tipo == "Cargo" || tipo == "Abono")
            {
                cmbTipo.SelectedItem = tipo;
            }
        }

        private void ConfigurarColumnas()
        {
            dgvMovimientos.Columns.Clear();
            dgvMovimientos.Columns.Add("IdMovimiento", "ID");
            dgvMovimientos.Columns.Add("Fecha", "Fecha");
            dgvMovimientos.Columns.Add("Tipo", "Tipo");
            dgvMovimientos.Columns.Add("Concepto", "Concepto");
            dgvMovimientos.Columns.Add("Monto", "Monto ($)");

            dgvMovimientos.Columns["IdMovimiento"].Width = 60;
            dgvMovimientos.Columns["Fecha"].Width = 110;
            dgvMovimientos.Columns["Tipo"].Width = 100;
            dgvMovimientos.Columns["Concepto"].Width = 400;
            dgvMovimientos.Columns["Monto"].Width = 120;
        }

        private void LlenarComboPacientes()
        {
            cmbPaciente.Items.Clear();
            foreach (var p in PacienteDatos.pacientes)
            {
                cmbPaciente.Items.Add(p.IdPaciente + " - " + p.Nombres + " " + p.Apellidos);
            }
            if (cmbPaciente.Items.Count > 0)
            {
                cmbPaciente.SelectedIndex = 0;
                CargarEstadoCuenta();
            }
        }

        private void cmbPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEstadoCuenta();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            CargarEstadoCuenta();
        }

        private void CargarEstadoCuenta()
        {
            if (cmbPaciente.SelectedIndex < 0) return;

            string item = cmbPaciente.SelectedItem.ToString();
            idPacienteActual = Convert.ToInt32(item.Split('-')[0].Trim());

            EstadoCuenta? ec = EstadoCuentaDatos.ObtenerPorPaciente(idPacienteActual);
            if (ec == null)
            {
                ec = EstadoCuentaDatos.CrearEstado(idPacienteActual);
            }
            idEstadoActual = ec.IdEstado;

            // Mostrar saldo actualizado (nunca como monto negativo)
            lblSaldoValor.Text = EstadoCuentaDatos.FormatearSaldo(ec.SaldoActual);
            if (ec.SaldoActual > 0)
            {
                lblSaldoValor.ForeColor = System.Drawing.Color.FromArgb(211, 47, 47); // Rojo suave si debe
            }
            else
            {
                lblSaldoValor.ForeColor = System.Drawing.Color.FromArgb(46, 125, 50); // Verde si al día
            }

            // Cargar movimientos
            dgvMovimientos.Rows.Clear();
            List<MovimientoCuenta> movimientos = EstadoCuentaDatos.ObtenerMovimientos(idEstadoActual);
            foreach (var m in movimientos)
            {
                dgvMovimientos.Rows.Add(m.IdMovimiento, m.Fecha.ToString("dd/MM/yyyy HH:mm"), m.Tipo, m.Concepto, "$" + m.Monto.ToString("F2"));
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (idPacienteActual == 0 || idEstadoActual == 0)
            {
                MessageBox.Show("Seleccione un paciente válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string concepto = txtConcepto.Text.Trim();
            if (string.IsNullOrWhiteSpace(concepto))
            {
                MessageBox.Show("El concepto del movimiento es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConcepto.Focus();
                return;
            }

            decimal monto;
            if (!decimal.TryParse(txtMonto.Text.Trim(), out monto) || monto <= 0)
            {
                MessageBox.Show("El monto debe ser un valor numérico mayor a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return;
            }

            string tipo = cmbTipo.SelectedItem?.ToString() ?? "Cargo";

            // REGLA: el abono no puede ser mayor a lo que el paciente debe (el saldo nunca baja de $0.00).
            // Si el paciente entrega más dinero, se registra solo lo que debe y el cambio se entrega en caja.
            if (tipo == "Abono")
            {
                decimal saldoPendiente = Math.Max(EstadoCuentaDatos.ObtenerSaldo(idEstadoActual), 0);
                if (saldoPendiente <= 0)
                {
                    MessageBox.Show("El paciente no tiene saldo pendiente; no se puede registrar un abono.", "Sin saldo pendiente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (monto > saldoPendiente)
                {
                    decimal cambio = monto - saldoPendiente;
                    bool ajustar = Mensaje.Confirmar(
                        "El abono ($" + monto.ToString("F2") + ") es mayor al saldo pendiente ($" + saldoPendiente.ToString("F2") + ").\n\n" +
                        "¿Desea registrar el pago total del saldo ($" + saldoPendiente.ToString("F2") + ")?\n" +
                        "Cambio a entregar al paciente: $" + cambio.ToString("F2"),
                        "Abono mayor al saldo");
                    if (!ajustar)
                    {
                        txtMonto.Focus();
                        return;
                    }
                    monto = saldoPendiente;
                }
            }

            MovimientoCuenta mc = new MovimientoCuenta();
            mc.IdEstado = idEstadoActual;
            mc.Fecha = DateTime.Now;
            mc.Tipo = tipo;
            mc.Concepto = concepto;
            mc.Monto = monto;

            try
            {
                EstadoCuentaDatos.AgregarMovimiento(mc);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CargarEstadoCuenta();
                return;
            }
            BitacoraDatos.RegistrarAccion("Movimiento de cuenta", "Se registró " + tipo + " de $" + monto.ToString("F2") + " para paciente ID " + idPacienteActual);

            MessageBox.Show(tipo + " registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtConcepto.Clear();
            txtMonto.Clear();
            CargarEstadoCuenta();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtConcepto.Clear();
            txtMonto.Clear();
            cmbTipo.SelectedIndex = 0;
        }
    }
}
