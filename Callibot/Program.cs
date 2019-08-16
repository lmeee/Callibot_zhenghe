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

            //int j = 0;
            //for (int i = 326; i <= 557; i++)
            //{
            //    DataCut wash = new DataCut(Convert.ToString(i));
            //    wash.Washmode();
            //    //wash.ErrorCorrect();
            //    if (wash.ErrorStringExist != 0)
            //    {
            //        j++;
            //    }
            //}


        }
    }
}
