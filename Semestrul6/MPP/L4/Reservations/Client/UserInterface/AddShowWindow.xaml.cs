using System;
using System.Windows;
using Reservations.Model;
namespace Reservations.UserInterface
{
	internal partial class AddShowWindow
		: Window
	{
		public AddShowWindow()
		{
			InitializeComponent();
		}

		public Show NewShow
		{
			get
			{
				try
				{
					if (_showScheduleDatePicker.SelectedDate.GetValueOrDefault().Date <= DateTime.Now.Date)
						MessageBox.Show("Spectacolul nu a poate fi adăugat deoarece data susținerii acestuia trebuie sa fie mai târzie decât data curentă!", "Eroare!", MessageBoxButton.OK, MessageBoxImage.Error);

					return new Show(_showNameTextBox.Text, _showScheduleDatePicker.SelectedDate.GetValueOrDefault());
				}
				catch (ArgumentException)
				{
					MessageBox.Show("Spectacolul nu a poate fi adăugat deoarece nu ați introdus un nume!", "Eroare!", MessageBoxButton.OK, MessageBoxImage.Error);
					return null;
				}
			}
		}

		private void AddButtonClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
		private void CancelButtonClick(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}
	}
}