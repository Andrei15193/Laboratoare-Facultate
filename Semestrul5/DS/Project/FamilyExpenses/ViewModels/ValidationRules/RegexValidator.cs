using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace FamilyExpenses.ViewModels.ValidationRules
{
    internal sealed class RegexValidator
        : ValidationRule
    {
        public string RegexPattern
        {
            get;
            set;
        }

        public string ErrorMessage
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = (value as string ?? string.Empty).Trim();

            if (input.Length == 0)
                return new ValidationResult(false, "Must provide a value!");
            else
                if (!Regex.IsMatch(input, RegexPattern, RegexOptions.Compiled))
                    return new ValidationResult(false, ErrorMessage);
                else
                    return new ValidationResult(true, null);
        }
    }
}
