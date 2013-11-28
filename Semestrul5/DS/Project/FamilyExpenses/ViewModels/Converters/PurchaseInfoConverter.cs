using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using FamilyExpenses.Model;

namespace FamilyExpenses.ViewModels.Converters
{
    internal sealed class PurchaseInfoConverter
        : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0].ToString().Length > 0
                && values[1].ToString().Length > 0
                && values[2].ToString().Length > 0
                && values[3].ToString().Length > 0
                && values[4].ToString().Length > 0)
            {
                DateTime dateOfPurchase = DateTime.Parse((string)values[2]);

                dateOfPurchase = dateOfPurchase.AddHours(int.Parse(values[3].ToString()))
                                               .AddMinutes(int.Parse(values[4].ToString()));

                return Tuple.Create(int.Parse((string)values[0]),
                                    int.Parse((string)values[1]),
                                    dateOfPurchase,
                                    (Product)values[5],
                                    (Shop)values[6]);
            }
            else
                return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
