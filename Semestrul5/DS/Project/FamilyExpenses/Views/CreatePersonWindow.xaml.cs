using System.Windows;
namespace FamilyExpenses.Views
{
	public partial class CreatePersonWindow
		: Window
	{
		public CreatePersonWindow(string personName)
		{
			InitializeComponent();
			personNameTextBox.Text = personName;
		}
		public CreatePersonWindow()
			: this(string.Empty)
		{
		}

		private void _CreateButtonClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}
