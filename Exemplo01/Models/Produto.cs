using System.ComponentModel.DataAnnotations;
using Exemplo01.Validations;
namespace Exemplo01.Models
{
    public class Produto
    {
        [Required(ErrorMessage = "Descrição obrigatória")]
        [StringLength(100,MinimumLength = 3,ErrorMessage = "Descrição deve ter entre 3 e 100 caracteres")]
        public string Descricao {  get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage ="Preço deve ser maior que zero")]
        public double Preco { get; set; }

        [Range(0, int.MaxValue, ErrorMessage ="Estoque não pode ser negativo")]
        public int Estoque { get; set; }

        [Required(ErrorMessage = "Código do produto é obrigatório")]
        [ProdutoValidation(ErrorMessage= "Código deve seguir o formato AAA-1234 (3 letras maiúsculas, hífen, 4 números)")]
        public string CodigoProduto {  get; set; }
    }
}
