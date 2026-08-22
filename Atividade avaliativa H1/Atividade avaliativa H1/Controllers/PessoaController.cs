using Microsoft.AspNetCore.Mvc;

namespace Atividade_avaliativa_H1.Controllers
{
    [ApiController]
    [Route("PessoaController")]
    public class PessoaController : ControllerBase
    {
        public class PessoaDados
        {
            public string Nome { get; set; }
            public double Peso { get; set; }
            public double Altura {  get; set; }

        }

        [HttpPost("Calcular-imc")]
        public IActionResult CalcularImc([FromBody] PessoaDados pessoa)
        {
            double imc = pessoa.Peso / (pessoa.Altura * pessoa.Altura);
            return Ok(new 
            {
                Nome = pessoa.Nome,
                Imc = Math.Round(imc,2)
            });

        }

        [HttpGet("consulta-tabela-imc")]
        public IActionResult ConsultaTabelaImc([FromQuery] double imc)
        {
            string descricao;

            if (imc < 18.5)
                descricao = "Abaixo do peso";
            else if (imc < 25)
                descricao = "Peso normal";
            else if (imc < 30)
                descricao = "Sobrepeso";
            else if (imc < 35)
                descricao = "Obesidade Grau I";
            else if (imc < 40)
                descricao = "Obesidade Grau II";
            else
                descricao = "Obesidade Grau III";

            return Ok(new
            {
                Imc = imc,
                Descricao = descricao
            });
        }

    }
}
