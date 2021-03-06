﻿using System;
using System.Globalization;
using System.Windows.Data;
namespace FamilyExpenses.ViewModels.Converters
{
	internal sealed class AddressInfoConverter
		 : IMultiValueConverter
	{
		#region IMultiValueConverter Members
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				return Tuple.Create((string)values[0], (string)values[1], (string)values[2], (string)values[3]);
			}
			catch
			{
				return null;
			}
		}
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			Tuple<string, string, string, string> personInfo = value as Tuple<string, string, string, string>;

			if (personInfo != null)
				return new object[] { personInfo.Item1, personInfo.Item2, personInfo.Item3, personInfo.Item4 };
			else
				return new object[] { string.Empty, string.Empty, string.Empty, string.Empty };
		}
		#endregion
	}
}