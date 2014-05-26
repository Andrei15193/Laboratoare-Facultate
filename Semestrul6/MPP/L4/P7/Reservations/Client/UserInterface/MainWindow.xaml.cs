using System.Windows;
using System.Windows.Controls;
using Reservations.ViewModels;
namespace Reservations
{
	internal partial class MainWindow
		: Window
	{
		internal MainWindow()
		{
			InitializeComponent();

			if (!((MainViewModel)App.Current.Resources["MainViewModel"]).IsConnected)
			{
				MessageBox.Show("Nu s-a putut realiza o conexiune la server! Incercati mai tarziu.", "Eroare!", MessageBoxButton.OK, MessageBoxImage.Error);
				Close();
			}
		}

		private void _ListBoxSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			ListBox listBox = sender as ListBox;

			if (listBox != null)
			{
				if (listBox.SelectedItem != null)
					listBox.SelectedItem = null;
			}
		}
	}
}