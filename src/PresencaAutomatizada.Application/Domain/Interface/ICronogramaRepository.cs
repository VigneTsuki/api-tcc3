using PresencaAutomatizada.Application.Api.Dto;
using PresencaAutomatizada.Application.Domain.Models;

namespace PresencaAutomatizada.Application.Domain.Interface
{
    public interface ICronogramaRepository
    {
        Task Adicionar(Cronograma cronograma);
        Task<IdMateriaCronogramaDto?> BuscarIdMateriaCronogramaPorSala(int idSala, DateTime dataAtual);
        Task<List<int>> BuscarIdCronogramaRelatorioPresenca(int idMateria, int ano, int semestre);
        Task<bool> AlunoPresentePorIdCronograma(int idCronograma, int idAluno);
        Task<string> BuscarDiaAulaPorId(int idCronograma);
        Task<HorarioAulaDto?> BuscarDataInicioFimAula(int idCronograma);
    }
}
