using System;
using System.Globalization;
using System.Windows.Data;
namespace FamilyExpenses.ViewModels.Converters
{
	internal sealed class ProducerInfoConverter
		 : IMultiValueConverter
	{
		#region IMultiValueConverter Members
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				return Tuple.Create((string)values[0], (string)values[1]);
			}
			catch
			{
				return null;
			}
		}
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			Tuple<string, string> personInfo = value as Tuple<string, string>;
			if (personInfo != null)
				return new object[] { personInfo.Item1, personInfo.Item2 };
			else
				return new object[] { string.Empty, string.Empty };
		}
		#endregion
	}
}