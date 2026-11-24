using System;
using System.Windows.Forms;

namespace Sistema_Toothly
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Forms.FrmLogin());
        }
    }
}
