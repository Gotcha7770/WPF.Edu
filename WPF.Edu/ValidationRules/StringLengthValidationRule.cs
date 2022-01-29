using System.Globalization;
using System.Windows.Controls;

namespace WPF.Edu.ValidationRules
{
    public class StringLengthValidationRule : ValidationRule
    {
        public int MaxLength { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return value switch
            {
                string s when s.Length > MaxLength => new ValidationResult(false, $"Value length is greater then {MaxLength}"),
                string => ValidationResult.ValidResult,
                _ => new ValidationResult(false, "Value is not string ")
            };
        }
    }
}