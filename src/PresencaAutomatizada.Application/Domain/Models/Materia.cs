using PresencaAutomatizada.Application.Domain.Models.Base;

namespace PresencaAutomatizada.Application.Domain.Models
{
    public class Materia : Entity
    {
        public DateTime DataCadastro { get; private set; }
        public string Nome { get; set; }
        public int QuantidadeCreditos { get; set; }
        public bool Ativo { get; set; }

        protected Materia() { }
        public Materia(string nome, int quantidadeCreditos)
        {
            Nome = nome;
            QuantidadeCreditos = quantidadeCreditos;
        }

        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;
    }
}
