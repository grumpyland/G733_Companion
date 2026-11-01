using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G733_Companion
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Only one instance may talk to the headset; a second launch just exits
            using (var instance = new System.Threading.Mutex(true, @"Local\G733_Companion_SingleInstance", out bool first_instance))
            {
                if (!first_instance) return;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }
    }
}
