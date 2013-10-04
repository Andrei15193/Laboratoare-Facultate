using DateSemistructurate.Laborator1.Model;
using DateSemistructurate.Laborator1.ViewModels;
using System.Windows;

namespace DateSemistructurate.Laborator1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void _TreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            (Resources["MainViewModel"] as MainViewModel).SelectedCategory = (e.NewValue as Category);
        }
    }
}
