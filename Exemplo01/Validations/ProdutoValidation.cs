using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Exemplo01.Validations
{
    public class ProdutoValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            
            string codigo = value.ToString();

            return Regex.IsMatch(codigo, @"^[A-Z]{3}-\d{4}$");
        }

    }
}
