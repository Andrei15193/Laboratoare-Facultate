using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISSApp
{
    class IssApplication : ApplicationContext
    {
        public IssApplication(string connectionString)
        {
            library = new Library(connectionString);
            AuthenticationView authenticationView = new AuthenticationView();
            if (authenticationView.ShowDialog() == DialogResult.OK)
            {
                library.Authenticate(authenticationView.Code);
                if (library.User != null)
                {
                    if (library.User.Type == UserType.Librarian)
                        mainForm = new LibrarianView(library);
                    else
                        mainForm = new ConsumerView(library);
                    mainForm.ShowDialog();
                }
            }
            Environment.Exit(0);
        }

        private Form mainForm;
        private Library library;
    }
}
