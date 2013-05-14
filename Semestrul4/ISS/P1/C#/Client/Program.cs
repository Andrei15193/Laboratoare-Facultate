using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISSApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ConsumerView consumerView;
            LibrarianView librarianView;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            librarianView = new LibrarianView();
            consumerView = new ConsumerView();
            if (librarianView.CanRun)
                if (!consumerView.CanRun)
                    Application.Run(librarianView);
                else
                    librarianView.Show();
            else
                consumerView.Dispose();
            if (consumerView.CanRun)
                Application.Run(consumerView);
            else
                consumerView.Dispose();
        }
    }
}
