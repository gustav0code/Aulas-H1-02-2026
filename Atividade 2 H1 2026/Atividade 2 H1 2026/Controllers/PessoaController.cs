using Microsoft.AspNetCore.Mvc;
using Atividade_2_H1_2026.Models;

namespace Atividade_2_H1_2026.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private static readonly List<Pessoa> listaPessoas = new List<Pessoa>();

        
        [HttpPost]
        public IActionResult Adicionar([FromBody] Pessoa novaPessoa)
        {
            if (novaPessoa == null)
                return BadRequest("Dados inválidos.");

            if (listaPessoas.Any(p => p.Cpf == novaPessoa.Cpf))
                return BadRequest("Já existe uma pessoa cadastrada com este CPF.");

            listaPessoas.Add(novaPessoa);
            return CreatedAtAction(nameof(BuscarPorCpf), new { cpf = novaPessoa.Cpf }, novaPessoa);
        }

       
        [HttpPut("{cpf}")]
        public IActionResult Atualizar(string cpf, [FromBody] Pessoa dadosAtualizados)
        {
            var pessoa = listaPessoas.FirstOrDefault(p => p.Cpf == cpf);

            if (pessoa == null)
                return NotFound("Pessoa não encontrada.");

            pessoa.Nome = dadosAtualizados.Nome;
            pessoa.Peso = dadosAtualizados.Peso;
            pessoa.Altura = dadosAtualizados.Altura;

            return Ok(pessoa);
        }

        
        [HttpDelete("{cpf}")]
        public IActionResult Remover(string cpf)
        {
            var pessoa = listaPessoas.FirstOrDefault(p => p.Cpf == cpf);

            if (pessoa == null)
                return NotFound("Pessoa não encontrada.");

            listaPessoas.Remove(pessoa);
            return NoContent();
        }

        
        [HttpGet]
        public IActionResult BuscarTodas()
        {
            return Ok(listaPessoas);
        }

        
        [HttpGet("{cpf}")]
        public IActionResult BuscarPorCpf(string cpf)
        {
            var pessoa = listaPessoas.FirstOrDefault(p => p.Cpf == cpf);

            if (pessoa == null)
                return NotFound("Pessoa não encontrada.");

            return Ok(pessoa);
        }

        
        [HttpGet("imc-bom")]
        public IActionResult BuscarPorImcBom()
        {
            var resultado = listaPessoas
                .Where(p => p.Imc >= 18 && p.Imc <= 24)
                .ToList();

            return Ok(resultado);
        }

        
        [HttpGet("buscar-por-nome")]
        public IActionResult BuscarPorNome([FromQuery] string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return BadRequest("Informe um termo para busca.");

            var resultado = listaPessoas
                .Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(resultado);
        }
    }
}