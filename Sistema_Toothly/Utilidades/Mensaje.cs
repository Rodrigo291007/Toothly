using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema_Toothly.Utilidades
{
    // Cuadros de confirmación con botones "Sí" / "No" en español.
    // MessageBox usa el idioma de Windows (por eso aparecía "Yes / No" en equipos configurados en inglés),
    // así que construimos un diálogo propio con el mismo aspecto sencillo.
    public static class Mensaje
    {
        public static bool Confirmar(string texto, string titulo)
        {
            return Confirmar(texto, titulo, SystemIcons.Question);
        }

        public static bool ConfirmarAdvertencia(string texto, string titulo)
        {
            return Confirmar(texto, titulo, SystemIcons.Warning);
        }

        private static bool Confirmar(string texto, string titulo, Icon icono)
        {
            using (Form dlg = new Form())
            {
                dlg.Text = titulo;
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;
                dlg.ShowInTaskbar = false;
                IconoApp.Aplicar(dlg);
                dlg.BackColor = Color.White;
                dlg.Font = new Font("Segoe UI", 9.5F);
                dlg.AutoScaleMode = AutoScaleMode.Font;

                PictureBox pic = new PictureBox();
                pic.Image = icono.ToBitmap();
                pic.SizeMode = PictureBoxSizeMode.AutoSize;
                pic.Location = new Point(20, 22);
                dlg.Controls.Add(pic);

                Label lbl = new Label();
                lbl.Text = texto;
                lbl.AutoSize = true;
                lbl.MaximumSize = new Size(380, 0);
                lbl.Location = new Point(68, 22);
                dlg.Controls.Add(lbl);

                // Calculamos el alto del diálogo según el texto
                Size tamTexto = lbl.GetPreferredSize(new Size(380, 0));
                int altoContenido = Math.Max(tamTexto.Height, 40);

                Panel pnlBotones = new Panel();
                pnlBotones.Dock = DockStyle.Bottom;
                pnlBotones.Height = 52;
                pnlBotones.BackColor = Color.FromArgb(240, 240, 240);
                dlg.Controls.Add(pnlBotones);

                Button btnSi = new Button();
                btnSi.Text = "Sí";
                btnSi.DialogResult = DialogResult.Yes;
                btnSi.Size = new Size(90, 30);
                btnSi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                pnlBotones.Controls.Add(btnSi);

                Button btnNo = new Button();
                btnNo.Text = "No";
                btnNo.DialogResult = DialogResult.No;
                btnNo.Size = new Size(90, 30);
                btnNo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                pnlBotones.Controls.Add(btnNo);

                int anchoCliente = Math.Max(340, 68 + tamTexto.Width + 30);
                dlg.ClientSize = new Size(anchoCliente, 22 + altoContenido + 22 + pnlBotones.Height);
                btnNo.Location = new Point(anchoCliente - btnNo.Width - 14, 11);
                btnSi.Location = new Point(btnNo.Left - btnSi.Width - 8, 11);

                dlg.AcceptButton = btnSi;
                dlg.CancelButton = btnNo;

                Form? propietario = Form.ActiveForm;
                DialogResult r = propietario != null && !propietario.IsDisposed && propietario != dlg
                    ? dlg.ShowDialog(propietario)
                    : dlg.ShowDialog();
                return r == DialogResult.Yes;
            }
        }
    }
}
