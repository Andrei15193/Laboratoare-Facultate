using System.Globalization;
using System.IO;
using System.Windows.Controls;

namespace FamilyExpenses.ViewModels.ValidationRules
{
    internal sealed class CheckFileValidator
        : ValidationRule
    {
        public string ErrorMessage
        {
            get;
            set;
        }

        public string FileExtension
        {
            get;
            set;
        }

        public FileCheckOptions CheckOptions
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string fileName = (value as string ?? string.Empty).Trim();

            if (fileName.Length == 0)
                return new ValidationResult(false, "Must provide a value!");
            else
                if ((CheckOptions == FileCheckOptions.CheckExists && !File.Exists(fileName + FileExtension))
                    || (CheckOptions == FileCheckOptions.CheckNotExists && File.Exists(fileName + FileExtension)))
                    return new ValidationResult(false, ErrorMessage);
                else
                    return new ValidationResult(true, null);
        }
    }
}
