using PresencaAutomatizada.Application.Domain.Models;

namespace PresencaAutomatizada.Application.Api.Response
{
    public class MateriaResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public List<Materia> Materias { get; set; }
    }
}
