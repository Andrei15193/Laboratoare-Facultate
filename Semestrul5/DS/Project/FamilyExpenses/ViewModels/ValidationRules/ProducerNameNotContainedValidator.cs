using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using FamilyExpenses.Model;

namespace FamilyExpenses.ViewModels.ValidationRules
{
    internal sealed class ProducerNameNotContainedValidator
         : ValidationRule
    {
        public string ErrorMessage
        {
            get;
            set;
        }

        public IReadOnlyList<Producer> Producers
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
                if (Producers.FirstOrDefault(producer => producer.Name == value.ToString()) == null)
                    return new ValidationResult(true, null);
                else
                    return new ValidationResult(false, ErrorMessage);
            else
                return new ValidationResult(false, "Must provide a value!");
        }
    }
}
