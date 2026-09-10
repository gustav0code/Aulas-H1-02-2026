using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Exemplo01.Validations
{
    public class CpfValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
                return false;

            string cpf = value.ToString();


            return Regex.IsMatch(cpf, @"^\d{11}$");
        }
       
    }
}
