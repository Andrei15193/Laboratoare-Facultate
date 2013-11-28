using System.Windows;

namespace FamilyExpenses.Views
{
    internal partial class LoginWindow
        : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void _LoginButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void _NewPersonButtonClick(object sender, RoutedEventArgs e)
        {
            bool? createResult = new CreatePersonWindow(personNameTextBox.Text).ShowDialog();

            if (createResult == true)
                Close();
        }
    }
}
