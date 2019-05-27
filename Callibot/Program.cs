using System;
using System.Windows.Forms;

namespace Callibot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            //DataCut wash = new DataCut("t");
            //wash.Correctmode();
            //int i = 1;
        }
    }
}
