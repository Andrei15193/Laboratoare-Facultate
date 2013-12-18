using System.Globalization;
using System.Windows.Controls;
namespace FamilyExpenses.ViewModels.ValidationRules
{
	internal sealed class ConstantValueValidator
		: ValidationRule
	{
		public ConstantValueValidator()
		{
			ErrorWhenEqual = true;
		}

		public object Value
		{
			get;
			set;
		}
		public string ErrorMessage
		{
			get;
			set;
		}
		public bool ErrorWhenEqual
		{
			get;
			set;
		}

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (value != null)
				if ((ErrorWhenEqual && value == Value)
					|| (!ErrorWhenEqual && value != Value))
					return new ValidationResult(false, ErrorMessage);
				else
					return new ValidationResult(true, null);
			else
				return new ValidationResult(false, "Must provide a value!");
		}

	}
}