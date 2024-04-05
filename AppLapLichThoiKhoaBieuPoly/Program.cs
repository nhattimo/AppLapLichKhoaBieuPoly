using System;
using System.Text;
using System.Windows.Forms;

namespace AppLapLichThoiKhoaBieuPoly
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            Console.WriteLine("Genetic Algorithm completed.");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin());
        }
    }
}
