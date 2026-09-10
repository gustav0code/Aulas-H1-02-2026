using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Exemplo01.Validations
{
    public class RaValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            
            string ra = value.ToString();

            return Regex.IsMatch(ra, @"^RA\d{6}$");
        }
    }
}
