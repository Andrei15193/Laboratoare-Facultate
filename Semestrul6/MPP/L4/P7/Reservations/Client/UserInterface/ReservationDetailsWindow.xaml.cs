using System.Text.RegularExpressions;
using System.Windows;
namespace Reservations.Client.UserInterface
{
	internal partial class ReservationDetailsWindow :
		Window
	{
		public ReservationDetailsWindow()
		{
			InitializeComponent();
		}

		public string PersonName
		{
			get
			{
				return _personNameTextBox.Text;
			}
		}
		public string PersonPhoneNumber
		{
			get
			{
				return _phoneNumberTextBox.Text;
			}
		}

		private void _ReserverClick(object sender, RoutedEventArgs e)
		{
			if (!Regex.IsMatch(PersonName, @"\s*\w+([ -]\w+)*"))
				MessageBox.Show("Trebuie să introduceți un nume!", "Eroare!", MessageBoxButton.OK, MessageBoxImage.Error);
			else
				if (!Regex.IsMatch(PersonPhoneNumber, @"^\s*[0-9]{10}\s*$"))
					MessageBox.Show("Trebuie să introduceți un număr de telefon!", "Eroare!", MessageBoxButton.OK, MessageBoxImage.Error);
				else
					DialogResult = true;
		}
		private void _CancelClick(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}
	}
}