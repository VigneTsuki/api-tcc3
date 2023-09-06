using Dapper;
using PresencaAutomatizada.Application.Api.Dto;
using PresencaAutomatizada.Application.Data.Base;
using PresencaAutomatizada.Application.Domain.Interface;
using PresencaAutomatizada.Application.Domain.Models;

namespace PresencaAutomatizada.Application.Data.Repository
{
    public class CronogramaRepository : ICronogramaRepository
    {
        private DbSession _session;

        public CronogramaRepository(DbSession session)
        {
            _session = session;
        }

        public async Task Adicionar(Cronograma cronograma)
        {
            await _session.Connection.ExecuteAsync("INSERT INTO cronograma(idmateria, idsala, datainicioaula, datafimaula, ano, semestre)" +
                "VALUES (@IdMateria, @IdSala, @DataInicioAula, @DataFimAula, @Ano, @Semestre)", new
                {
                    cronograma.IdMateria,
                    cronograma.IdSala,
                    cronograma.DataInicioAula,
                    cronograma.DataFimAula,
                    cronograma.Ano,
                    cronograma.Semestre
                }, _session.Transaction);
        }

        public async Task<IdMateriaCronogramaDto?> BuscarIdMateriaCronogramaPorSala(int idSala, DateTime dataAtual)
        {
            var ids = await _session.Connection.QueryFirstOrDefaultAsync<IdMateriaCronogramaDto?>("SELECT IdMateria, Id FROM cronograma " +
                    "WHERE idsala = @IdSala AND datainicioaula <= @DataInicioAula and datafimaula >= @DataFimAula LIMIT 1;", new 
                    { 
                        IdSala = idSala, 
                        DataInicioAula = dataAtual.ToString("yyyy-MM-dd HH:mm:ss"),
                        DataFimAula = dataAtual.ToString("yyyy-MM-dd HH:mm:ss")
                    }, _session.Transaction);

            return ids;
        }

        public async Task<List<int>> BuscarIdCronogramaRelatorioPresenca(int idMateria, int ano, int semestre)
        {
            var ids = await _session.Connection.QueryAsync<int>("SELECT Id FROM cronograma " +
                    "WHERE idMateria = @IdMateria AND ano = @Ano and semestre = @Semestre;", new
        {
            IdMateria = idMateria,
            Ano = ano,
            Semestre = semestre
        }, _session.Transaction);

            return ids.ToList();
        }

        public async Task<bool> AlunoPresentePorIdCronograma(int idCronograma, int idAluno)
        {
            var presente = await _session.Connection.QueryFirstOrDefaultAsync<int?>("SELECT COUNT(*) FROM presenca " +
                    "WHERE idcronograma = @IdCronograma AND idaluno = @IdAluno;", new
                    {
                        IdCronograma = idCronograma,
                        IdAluno = idAluno
                    }, _session.Transaction);

            return presente > 0;
        }

        public async Task<string> BuscarDiaAulaPorId(int idCronograma)
        {
            var dataInicioAula = await _session.Connection.QueryFirstOrDefaultAsync<DateTime>("SELECT datainicioaula FROM cronograma " +
                    "WHERE id = @IdCronograma", new
                    {
                        IdCronograma = idCronograma
                    }, _session.Transaction);

            return dataInicioAula.ToString("yyyy-MM-dd");
        }
    }
}
