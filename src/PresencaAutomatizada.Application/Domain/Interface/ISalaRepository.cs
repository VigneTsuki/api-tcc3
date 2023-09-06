using PresencaAutomatizada.Application.Domain.Models;

namespace PresencaAutomatizada.Application.Domain.Interface
{
    public interface ISalaRepository
    {
        Task<int> BuscarIdSalaPorNumeracaoECodigo(int numeracao, string bloco);
        Task<bool> JaCadastrada(string bloco, int numeracao);
        Task Adicionar(Sala sala);
        Task<List<Sala>> Buscar();
    }
}
