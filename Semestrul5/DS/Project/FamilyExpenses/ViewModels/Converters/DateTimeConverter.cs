using System;
using System.Globalization;
using System.Windows.Data;
namespace FamilyExpenses.ViewModels.Converters
{
	internal sealed class DateTimeConverter
		: IValueConverter
	{
		#region IValueConverter Members
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((DateTime)value).ToString("MMMM dd, yyyy");
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
