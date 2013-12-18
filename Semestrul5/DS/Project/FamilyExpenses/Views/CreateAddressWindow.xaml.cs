using System.Windows;
namespace FamilyExpenses.Views
{
	public partial class CreateAddressWindow : Window
	{
		public CreateAddressWindow()
		{
			InitializeComponent();
		}

		private void _AddButtonClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}