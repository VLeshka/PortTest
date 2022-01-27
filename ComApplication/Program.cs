using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ComApplication
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainUnit.OnProgramStart();
            
            Application.Run(new MainForm());

            MainUnit.OnProgramClose();
        }
    }
}
