using System.Windows;
namespace FamilyExpenses.Views
{
	/// <summary>
	/// Interaction logic for CreateIncomeWindow.xaml
	/// </summary>
	public partial class CreateIncomeWindow : Window
	{
		public CreateIncomeWindow()
		{
			InitializeComponent();
		}

		private void _AddButtonClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}
