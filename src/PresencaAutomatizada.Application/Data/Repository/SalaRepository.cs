using Dapper;
using PresencaAutomatizada.Application.Data.Base;
using PresencaAutomatizada.Application.Domain.Interface;
using PresencaAutomatizada.Application.Domain.Models;

namespace PresencaAutomatizada.Application.Data.Repository
{
    public class SalaRepository : ISalaRepository
    {
        private DbSession _session;

        public SalaRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<bool> JaCadastrada(string bloco, int numeracao)
        {
            var quantidade = await _session.Connection.QueryFirstAsync<int>("SELECT COUNT(*) FROM sala " +
                "WHERE bloco = @Bloco AND numeracao = @Numeracao", new { Bloco = bloco, Numeracao = numeracao }, _session.Transaction);

            return quantidade != 0;
        }

        public async Task Adicionar(Sala sala)
        {
            await _session.Connection.ExecuteAsync("INSERT INTO sala(datacadastro, bloco, numeracao)" +
                "VALUES (@DataCadastro, @Bloco, @Numeracao)", new
                {
                    sala.Bloco,
                    sala.Numeracao,
                    DataCadastro = DateTime.Now
                }, _session.Transaction);
        }

        public async Task<int> BuscarIdSalaPorNumeracaoECodigo(int numeracao, string bloco)
        {
            var idSala = await _session.Connection.QueryFirstOrDefaultAsync<int?>("SELECT Id FROM sala " +
                    "WHERE bloco = @Bloco AND numeracao = @Numeracao LIMIT 1;", new { Bloco = bloco, Numeracao = numeracao }, _session.Transaction);

            return idSala ?? 0;
        }

        public async Task<List<Sala>> Buscar()
        {
            var salas = await _session.Connection.QueryAsync<Sala>("SELECT * FROM sala", null, _session.Transaction);

            return salas.ToList();
        }
    }
}
