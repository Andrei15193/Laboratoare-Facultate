using System;
using System.Globalization;
using System.Windows.Data;
using FamilyExpenses.Model;

namespace FamilyExpenses.ViewModels.Converters
{
    internal sealed class ProductInfoConverter
          : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return Tuple.Create((string)values[0], (ProductType)Enum.Parse(typeof(ProductType), values[1].ToString()), (Producer)values[2]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            Tuple<string, string, Producer> personInfo = value as Tuple<string, string, Producer>;

            if (personInfo != null)
                return new object[] { personInfo.Item1, personInfo.Item2.ToString(), personInfo.Item3 };
            else
                return new object[] { string.Empty, ProductType.Electronic.ToString(), null };
        }

        #endregion
    }
}
