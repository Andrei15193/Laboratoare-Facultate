using System.Windows;
namespace Reservations.Client.UserInterface
{
	internal partial class LoginWindow
		: Window
	{
		public LoginWindow()
		{
			InitializeComponent();
		}

		public string Username
		{
			get
			{
				return _usernameBox.Text;
			}
		}
		public string Password
		{
			get
			{
				return _passwordBox.Password;
			}
		}

		private void _LogInClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
		private void _CancelClick(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}
	}
}