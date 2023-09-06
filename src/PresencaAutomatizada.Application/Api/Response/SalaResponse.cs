using PresencaAutomatizada.Application.Domain.Models;

namespace PresencaAutomatizada.Application.Api.Response
{
    public class SalaResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public List<Sala> Salas { get; set; }
    }
}
