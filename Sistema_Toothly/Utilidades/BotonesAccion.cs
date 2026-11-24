using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema_Toothly.Utilidades
{
    // Crea, con el mismo estilo de los botones del sistema, una barra de acciones
    // (Modificar / Eliminar) debajo de una lista. Se arma por código, igual que el botón "Cerrar"
    // de cada formulario, para no tocar el archivo .Designer.cs.
    public static class BotonesAccion
    {
        public static Button CrearBoton(string nombre, string texto, Color color, Image? icono, EventHandler alPulsar)
        {
            Button b = new Button();
            b.Name = nombre;
            b.Text = texto;
            b.Size = new Size(140, 36);
            b.BackColor = color;
            b.ForeColor = Color.White;
            b.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 1;
            b.Cursor = Cursors.Hand;
            b.UseVisualStyleBackColor = false;
            if (icono != null)
            {
                b.Image = icono;
                b.ImageAlign = ContentAlignment.MiddleLeft;
                b.TextImageRelation = TextImageRelation.ImageBeforeText;
            }
            b.Click += alPulsar;
            return b;
        }

        // Agrega un panel inferior al contenedor (por ejemplo, el lado izquierdo del SplitContainer)
        public static Panel AgregarBarra(Control contenedor, params Button[] botones)
        {
            Panel pnl = new Panel();
            pnl.Name = "pnlAccionesLista";
            pnl.Dock = DockStyle.Bottom;
            pnl.Height = 52;
            pnl.BackColor = Color.FromArgb(243, 249, 253);

            int x = 10;
            foreach (Button b in botones)
            {
                b.Location = new Point(x, 8);
                pnl.Controls.Add(b);
                x += b.Width + 10;
            }

            // Se agrega al final para que el DataGridView (Dock = Fill) ocupe el espacio restante
            contenedor.Controls.Add(pnl);
            return pnl;
        }
    }
}
