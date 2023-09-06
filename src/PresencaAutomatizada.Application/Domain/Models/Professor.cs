using PresencaAutomatizada.Application.Domain.Models.Base;

namespace PresencaAutomatizada.Application.Domain.Models
{
    public class Professor : Entity
    {
        public DateTime DataCadastro { get; private set; }
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }

        public Professor(string nome)
        {
            Nome = nome;
        }

        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;
    }
}
