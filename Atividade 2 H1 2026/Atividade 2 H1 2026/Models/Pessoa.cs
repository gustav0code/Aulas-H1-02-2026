using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Atividade_2_H1_2026.Models
{
    public class Pessoa
    {
        public string Nome { get; set; }

        public string Cpf { get; set; }

        public double Peso { get; set; }

        public double Altura { get; set; }

        public double Imc => Altura > 0 ? Peso / (Altura * Altura) : 0;
    }
}
