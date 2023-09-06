using PresencaAutomatizada.Application.Domain.Models;

namespace PresencaAutomatizada.Application.Domain.Interface
{
    public interface IMateriaRepository
    {
        Task<bool> JaCadastrada(string nome);
        Task Adicionar(Materia materia);
        Task<List<Materia>> Buscar();
    }
}
