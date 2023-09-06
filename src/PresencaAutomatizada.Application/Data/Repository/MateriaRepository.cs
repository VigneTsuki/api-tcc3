using Dapper;
using PresencaAutomatizada.Application.Data.Base;
using PresencaAutomatizada.Application.Domain.Interface;
using PresencaAutomatizada.Application.Domain.Models;

namespace PresencaAutomatizada.Application.Data.Repository
{
    public class MateriaRepository : IMateriaRepository
    {
        private DbSession _session;

        public MateriaRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<bool> JaCadastrada(string nome)
        {
            var quantidade = await _session.Connection.QueryFirstAsync<int>("SELECT COUNT(*) FROM materia " +
                "WHERE nome = @Nome AND ativo = 1", new { Nome = nome }, _session.Transaction);

            return quantidade != 0;
        }

        public async Task Adicionar(Materia materia)
        {
            await _session.Connection.ExecuteAsync("INSERT INTO materia(datacadastro, nome, quantidadecreditos, ativo)" +
                "VALUES (@DataCadastro, @Nome, @QuantidadeCreditos, 1)", new
                {
                    DataCadastro = DateTime.Now,
                    materia.Nome,
                    materia.QuantidadeCreditos
                }, _session.Transaction);
        }

        public async Task<List<Materia>> Buscar()
        {
            var materias = await _session.Connection.QueryAsync<Materia>("SELECT * FROM materia", null, _session.Transaction);

            return materias.ToList();
        }
    }
}
