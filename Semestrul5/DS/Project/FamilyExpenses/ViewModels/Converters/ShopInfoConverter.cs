using System;
using System.Globalization;
using System.Windows.Data;
using FamilyExpenses.Model;
namespace FamilyExpenses.ViewModels.Converters
{
	internal sealed class ShopInfoConverter
		: IMultiValueConverter
	{
		#region IMultiValueConverter Members
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				return Tuple.Create((string)values[0], (ShopType)Enum.Parse(typeof(ShopType), values[1].ToString()), (Address)values[2]);
			}
			catch
			{
				return null;
			}
		}
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			Tuple<string, string, Address> personInfo = value as Tuple<string, string, Address>;
			if (personInfo != null)
				return new object[] { personInfo.Item1, personInfo.Item2, personInfo.Item3 };
			else
				return new object[] { string.Empty, ProductType.Electric.ToString(), null };
		}
		#endregion
	}
}