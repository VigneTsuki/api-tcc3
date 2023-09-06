using PresencaAutomatizada.Application.Domain.Models.Base;

namespace PresencaAutomatizada.Application.Domain.Models
{
    public class Aluno : Entity
    {
        public DateTime DataCadastro { get; private set; }
        public string Codigo { get; private set; }
        public string Email { get; private set; }
        public string Nome { get; private set; }
        public string Senha { get; private set; }
        public string Telefone { get; private set; }
        public bool Ativo { get; private set; }

        // Dapper
        protected Aluno() { }
        public Aluno (string codigo, string email, string nome, string senha, string telefone)
        {
            Codigo = codigo;
            Email = email;
            Nome = nome;
            Senha = senha;
            Telefone = telefone;
        }

        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;
    }
}
