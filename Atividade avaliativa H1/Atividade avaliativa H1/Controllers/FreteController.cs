using Microsoft.AspNetCore.Mvc;

namespace Atividade_avaliativa_H1.Controllers
{
	[ApiController]
	[Route("FreteController")]
	public class FreteController : ControllerBase
	{
		public class ProdutoDados
		{
			public string NomeProduto { get; set; }
			public float Peso { get; set; }
			public float Altura { get; set; }
			public float Largura { get; set; }
			public float Comprimento { get; set; }
			public string Uf { get; set; }
		}

		private float ObterTaxaEstado(string uf)
		{
			switch (uf.ToUpper())
			{
				case "SP":
					return 50.00f;
				case "RJ":
					return 60.00f;
				case "MG":
					return 55.00f;
				default:
					return 70.00f;
			}
		}

		[HttpPost("calcular")]
		public IActionResult CalcularFrete([FromBody] ProdutoDados produto)
		{
			float volume = produto.Altura * produto.Largura * produto.Comprimento;
			float taxaPorCm3 = 0.01f;
			float taxaEstado = ObterTaxaEstado(produto.Uf);

			float valorFrete = (volume * taxaPorCm3) + taxaEstado;

			return Ok(new
			{
				Produto = produto.NomeProduto,
				Volume = volume,
				Uf = produto.Uf.ToUpper(),
				TaxaEstado = taxaEstado,
				ValorFrete = Math.Round(valorFrete, 2)
			});
		}
	}
}