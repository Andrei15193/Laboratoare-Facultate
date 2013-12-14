using System.Windows;
namespace FamilyExpenses.Views
{
	/// <summary>
	/// Interaction logic for IncomeWindow.xaml
	/// </summary>
	public partial class IncomesWindow : Window
	{
		public IncomesWindow()
		{
			InitializeComponent();
		}

		private void _AddIncomeMenuItemClick(object sender, RoutedEventArgs e)
		{
			new CreateIncomeWindow().ShowDialog();
		}
	}
}
