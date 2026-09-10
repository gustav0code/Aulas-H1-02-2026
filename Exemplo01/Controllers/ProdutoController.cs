using Exemplo01.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exemplo01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private static List<Produto> ListaProdutos = new List<Produto>();
        [HttpPost]
        public IActionResult Cadastrar(Produto produto)
        {
            var resultado = ListaProdutos
                .Where(p => p.CodigoProduto == produto.CodigoProduto).FirstOrDefault();

            if (resultado is null)
            {
                ListaProdutos.Add(produto);
                return Ok("Cadastrado com sucesso");
            }
            return BadRequest("Código de produto já cadastrado");
        }

        [HttpGet]
        [Route("ListarProdutos")]
        public IActionResult ListarProdutos()
        {
            return Ok(ListaProdutos);
        }

        [HttpGet]
        [Route("ObterPorCodigo")]
        public IActionResult ObterPorCodigo(string codigo)
        {
            var resultado = ListaProdutos.Where(p => p.CodigoProduto == codigo);

            if (resultado.Count() == 0)
            {
                return NotFound("Produto não encontrado");
            }
            return Ok(resultado);
        }

        [HttpPut]
        [Route("Atualizar")]
        public IActionResult Atualizar(Produto produto)
        {
            var resultado = ListaProdutos
                .Where(p => p.CodigoProduto == produto.CodigoProduto).FirstOrDefault();

            if (resultado is null)
                return NotFound("Código informado não existe");

            ListaProdutos.Remove(resultado);
            ListaProdutos.Add(produto);
            return Ok("Dados atualizados com sucesso");
        }

        [HttpDelete]
        [Route("Remover/{codigo}")]
        public IActionResult Remover(string codigo)
        {
            var resultado = ListaProdutos
                .Where(p => p.CodigoProduto == codigo).FirstOrDefault();

            if (resultado is null)
                return NotFound("Código informado não existe");

            ListaProdutos.Remove(resultado);
            return Ok("Produto removido com sucesso");
        }




    }
}
