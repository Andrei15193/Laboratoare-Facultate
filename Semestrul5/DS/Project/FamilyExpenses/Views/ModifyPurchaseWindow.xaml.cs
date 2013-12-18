using System;
using System.Windows;
using FamilyExpenses.Model;
using FamilyExpenses.ViewModels;
namespace FamilyExpenses.Views
{
	public partial class ModifyPurchaseWindow
		: Window
	{
		public ModifyPurchaseWindow(Purchase prototypePurchase)
		{
			if (prototypePurchase == null)
				throw new ArgumentNullException("purchase");
			InitializeComponent();
			_initialPurchase = prototypePurchase;
			_priceTextBox.Text = _initialPurchase.Price.ToString();
			_quantityTextBox.Text = _initialPurchase.Quantity.ToString();
			_datePurchasedDatePicker.Text = _initialPurchase.PurchaseDate.ToString("dd/MM/yyyy");
			_hoursTextBox.Text = _initialPurchase.PurchaseDate.Hour.ToString();
			_minutesTextBox.Text = _initialPurchase.PurchaseDate.Minute.ToString();
			_productComboBox.SelectedItem = _initialPurchase.Product;
			_shopComboBox.SelectedItem = _initialPurchase.Shop;
			Loaded += delegate
			{
				((MainViewModel)FindResource("MainViewModel")).SelectedPurchase = _initialPurchase;
			};
		}

		private void _NewProductButtonClick(object sender, RoutedEventArgs e)
		{
			new CreateProductWindow().ShowDialog();
		}
		private void _CreateShopButtonClick(object sender, RoutedEventArgs e)
		{
			new CreateShopWindow().ShowDialog();
		}
		private void _ModifyPurachseButtonClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}

		private readonly Purchase _initialPurchase;
	}
}