using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;
namespace Reservations.UserInterface.Converters
{
	internal sealed class DescriptionConverter
		: IValueConverter
	{
		#region IValueConverter Members
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return null;

			FieldInfo fieldInfo = value.GetType().GetField(value.ToString(), BindingFlags.Static | BindingFlags.Public);
			if (fieldInfo == null)
				return null;
			else
			{
				DescriptionAttribute descriptionAttribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
				if (descriptionAttribute != null)
					return descriptionAttribute.Description;
				else
					return fieldInfo.Name;
			}
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}