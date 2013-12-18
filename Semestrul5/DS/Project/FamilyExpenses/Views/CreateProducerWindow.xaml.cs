using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace FamilyExpenses.Views
{
	/// <summary>
	/// Interaction logic for CreateProducerWindow.xaml
	/// </summary>
	public partial class CreateProducerWindow : Window
	{
		public CreateProducerWindow()
		{
			InitializeComponent();
		}

		private void _AddProducerButtonClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}