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
    /// Interaction logic for CreatePurchaseWindow.xaml
    /// </summary>
    public partial class CreatePurchaseWindow : Window
    {
        public CreatePurchaseWindow()
        {
            InitializeComponent();
        }

        private void _NewProductButtonClick(object sender, RoutedEventArgs e)
        {
            new CreateProductWindow().ShowDialog();
        }

        private void _CreateShopButtonClick(object sender, RoutedEventArgs e)
        {
            new CreateShopWindow().ShowDialog();
        }

        private void _AddPurachseButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
