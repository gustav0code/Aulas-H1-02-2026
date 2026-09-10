using Exemplo01.Validations;
using Microsoft.OpenApi.MicrosoftExtensions;
using System.ComponentModel.DataAnnotations;

namespace Exemplo01.Models
{
    //classe anemica
    public class Aluno
    {

        //if (string.IsNullOrEmpty(aluno.Nome))
        //    return BadRequest("Nome do aluno é obrigatório");
        [StringLength(50, ErrorMessage = "Nome deve ter no máximo 50 caracteres")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        
        [Required(ErrorMessage = "RA é obrigatório")]
        [RaValidation(ErrorMessage = "Ra deve começar com 'RA' seguido de 6 digitios número (ex: RA123456)")]
        public string RA { get; set; }

        [Required(ErrorMessage ="Cpf é obrigatório")]
        [CpfValidation(ErrorMessage = "Cpf deve ter 11 dígitos")]
        public string Cpf {  get; set; }

        public bool Ativo { get; set; }
        
        [IdadeValidation(ErrorMessage = "Idade invalidade")]
        public int idade { get; set; }
    }
}
