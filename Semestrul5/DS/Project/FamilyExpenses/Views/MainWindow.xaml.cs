using System.Windows;
using System.Windows.Input;
using FamilyExpenses.Model;
using FamilyExpenses.ViewModels;

namespace FamilyExpenses.Views
{
    internal partial class MainWindow
        : Window
    {
        public MainWindow()
        {
            Loaded += delegate
            {
                _applyFilterCommand = ((MainViewModel)FindResource("MainViewModel")).ApplyFliterCommand;
                _applyFilterCommand.CanExecuteChanged += delegate
                {
                    _applyFilterCommandCanExecute = _applyFilterCommand.CanExecute(_filtersTreeView.SelectedItem);
                };
                _applyFilterCommandCanExecute = _applyFilterCommand.CanExecute(_filtersTreeView.SelectedItem);

                _removePurchaseCommand = ((MainViewModel)FindResource("MainViewModel")).RemovePurchaseCommand;
                _removePurchaseCommand.CanExecuteChanged += delegate
                {
                    ((MainViewModel)FindResource("MainViewModel")).SelectedPurchase = _purchasesListView.SelectedItem as Purchase;
                    _removePurchaseCommandCanExecute = _removePurchaseCommand.CanExecute(null);
                };
                ((MainViewModel)FindResource("MainViewModel")).SelectedPurchase = _purchasesListView.SelectedItem as Purchase;
                _removePurchaseCommandCanExecute = _removePurchaseCommand.CanExecute(null);
            };
            InitializeComponent();
        }

        private void _LoginMenuItemClick(object sender, RoutedEventArgs e)
        {
            new LoginWindow().ShowDialog();
        }

        private void _FiltersTreeViewSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (_applyFilterCommandCanExecute)
                _applyFilterCommand.Execute(_filtersTreeView.SelectedItem);
        }

        private void _PurchsesSelectionChangedListView(object sender, RoutedEventArgs e)
        {
            if (_purchasesListView.SelectedItem == null)
            {
                _modifyPurchaseMenuItem.IsEnabled = false;
                _deletePurchaseMenuItem.IsEnabled = false;
            }
            else
            {
                _modifyPurchaseMenuItem.IsEnabled = true;
                _deletePurchaseMenuItem.IsEnabled = true;
            }
        }

        private void _CreateNewPurchaseMenuItemClick(object sender, RoutedEventArgs e)
        {
            new CreatePurchaseWindow().ShowDialog();
        }

        private void _ModifyPurchaseMenuItemClick(object sender, RoutedEventArgs e)
        {
            new ModifyPurchaseWindow(_purchasesListView.SelectedItem as Purchase).ShowDialog();
        }

        private void _PurchasesListViewDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_purchasesListView.SelectedItem != null)
                _ModifyPurchaseMenuItemClick(sender, e);
        }

        private void _DeletePurchaseMenuItemClick(object sender, RoutedEventArgs e)
        {
            if (_removePurchaseCommandCanExecute)
            {
                ((MainViewModel)FindResource("MainViewModel")).SelectedPurchase = _purchasesListView.SelectedItem as Purchase;
                _removePurchaseCommand.Execute(null);
            }
        }

        private bool _applyFilterCommandCanExecute;
        private bool _removePurchaseCommandCanExecute;
        private ICommand _applyFilterCommand;
        private ICommand _removePurchaseCommand;
    }
}
