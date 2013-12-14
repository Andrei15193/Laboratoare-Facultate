using System;
using System.Globalization;
using System.Windows.Data;
namespace FamilyExpenses.ViewModels.Converters
{
	internal sealed class IncomeInfoConverter
		: IMultiValueConverter
	{
		#region IMultiValueConverter Members
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				return Tuple.Create(int.Parse((string)values[0]), DateTime.Parse((string)values[1]));
			}
			catch
			{
				return null;
			}
		}
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
