using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BDLab4
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
            Repository.SqlDataBaseRepository repository = new Repository.SqlDataBaseRepository(new Repository.DataBaseConnectionParameters("(local)\\SQLEXPRESS", "LocalDb"), true);
            Application.Run(new UserInterface.MainForm(new Controller.ApplicationController(repository, repository)));
        }
    }
}
