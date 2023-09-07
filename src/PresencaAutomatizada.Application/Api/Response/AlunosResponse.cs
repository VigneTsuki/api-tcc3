using PresencaAutomatizada.Application.Domain.Models;

namespace PresencaAutomatizada.Application.Api.Response
{
    public class AlunosResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public List<Aluno> Alunos { get; set; }
    }
}
