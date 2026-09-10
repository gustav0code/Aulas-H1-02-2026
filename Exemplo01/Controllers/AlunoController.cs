using Exemplo01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Exemplo01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunoController : ControllerBase
    { 
        private static List<Aluno> ListaAlunos = new List<Aluno>();

        //sintaxe de uma funcao
        //visibilidade da funcao: public, private, protected
        //Tipo do retorno 
        //nome da funcao
        //parametros
        private static List<Aluno> alunos = new List<Aluno>();

        #region Métodos GET


        [HttpGet]
        [Route("Saudacao")]

        public IActionResult Saudacao(string nome)
        {
            return Ok("Oi " + nome);
        }

        [HttpGet]
        [Route("OutraSaudacao")]
        public IActionResult OutraSaudacao(string nome) 
        {
            return Ok("Fala comigooo " + nome);
        }
       

        [HttpGet]
        [Route("ListarAlunos")]
        public IActionResult ListarAlunos()
        {
            return Ok(ListaAlunos);
        }

        [HttpGet]
        [Route("obterPorRa")]
        public IActionResult obterporRa(string ra)
        {
            var resultado = ListaAlunos.Where( a=> a.RA == ra);
            if(resultado.Count() == 0)
            {
                return NotFound("Aluno não encontrado");
            }
            return Ok(resultado);
        }

        #endregion
        [HttpPost]
        public IActionResult Cadastrar(Aluno aluno) 
        {
            //verificar se RA ja existe na lista
            //isto é uma regra de negocio
            var resultado = ListaAlunos
          .Where(a => a.RA == aluno.RA).FirstOrDefault();

            if (resultado is null)
            {
                ListaAlunos.Add(aluno);
                return Ok("Cadastrado com sucesso");
            }
            return BadRequest("RA já cadastrado");

        }

        [HttpPut]
        [Route("Atualizar")]
        public IActionResult Atualizar(Aluno aluno) 
        {

            var resultado = ListaAlunos
           .Where(a => a.RA == aluno.RA).FirstOrDefault();

            if (resultado is null)
                return NotFound("Ra informado não existe");

            ListaAlunos.Remove(resultado);
            ListaAlunos.Add(aluno);
            return Ok("Dados atualizados com sucesso");
        }

        [HttpDelete()]
        [Route("Remover/{ra}")]
        public IActionResult Remover(string ra)
        {
            var resultado = ListaAlunos
                .Where( a => a.RA == ra).FirstOrDefault();

            if ( resultado is null)
                return NotFound("Ra informado não existe");

            ListaAlunos.Remove(resultado);           
            return Ok("Aluno removido com sucesso");
        }

    }
}
