using System;
using System.Windows.Forms;

namespace Sistema_Toothly.Utilidades
{
    public interface IRecargable
    {
        void Recargar();
    }

    public static class Recarga
    {
        public static bool EnCurso { get; private set; }

        public static void Combos(Action llenar, params ComboBox[] combos)
        {
            string?[] seleccion = new string?[combos.Length];
            for (int i = 0; i < combos.Length; i++)
            {
                seleccion[i] = combos[i].SelectedItem?.ToString();
            }

            bool previo = EnCurso;
            EnCurso = true;
            try
            {
                llenar();
                for (int i = 0; i < combos.Length; i++)
                {
                    Seleccionar(combos[i], seleccion[i]);
                }
            }
            finally
            {
                EnCurso = previo;
            }
        }

        public static void Grid(DataGridView dgv, Action cargar, bool actualizarCampos = true)
        {
            object? id = dgv.CurrentRow?.Cells[0].Value;

            bool previo = EnCurso;
            EnCurso = true;
            try
            {
                cargar();
                dgv.CurrentCell = null;
                dgv.ClearSelection();
                if (!actualizarCampos) SeleccionarFila(dgv, id);
            }
            finally
            {
                EnCurso = previo;
            }

            if (actualizarCampos) SeleccionarFila(dgv, id);
        }

        public static bool SeleccionarFila(DataGridView dgv, object? id)
        {
            if (id == null) return false;
            foreach (DataGridViewRow fila in dgv.Rows)
            {
                if (Equals(fila.Cells[0].Value, id))
                {
                    dgv.CurrentCell = fila.Cells[0];
                    return true;
                }
            }
            return false;
        }

        private static void Seleccionar(ComboBox cmb, string? texto)
        {
            if (texto == null) return;

            int indice = cmb.FindStringExact(texto);
            if (indice < 0 && texto.Contains(" - "))
            {
                string prefijo = texto.Split('-')[0].Trim() + " -";
                for (int i = 0; i < cmb.Items.Count; i++)
                {
                    if (cmb.Items[i]?.ToString()?.StartsWith(prefijo) == true)
                    {
                        indice = i;
                        break;
                    }
                }
            }

            if (indice >= 0 && cmb.SelectedIndex != indice)
            {
                cmb.SelectedIndex = indice;
            }
        }
    }
}
