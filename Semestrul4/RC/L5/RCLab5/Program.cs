using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCLab5
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ViewModel viewModel = new ViewModel();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (new NodeSettingsForm(viewModel).ShowDialog() == DialogResult.OK)
            {
                Application.Run(new MainForm(viewModel));
            }
        }
    }
}
