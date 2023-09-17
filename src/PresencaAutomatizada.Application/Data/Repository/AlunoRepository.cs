using Dapper;
using PresencaAutomatizada.Application.Api.Dto;
using PresencaAutomatizada.Application.Data.Base;
using PresencaAutomatizada.Application.Domain.Interface;
using PresencaAutomatizada.Application.Domain.Models;

namespace PresencaAutomatizada.Application.Data.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private DbSession _session;

        public AlunoRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<bool> ValidarLogin(string login, string senha)
        {
            var quantidade = await _session.Connection.QueryFirstAsync<int>("SELECT COUNT(*) FROM aluno " +
                "WHERE codigo=@Codigo AND senha=@Senha", new { Codigo = login, Senha = senha }, _session.Transaction);

            return quantidade != 0;
        }

        public async Task<bool> JaCadastrado(string codigo)
        {
            var quantidade = await _session.Connection.QueryFirstAsync<int>("SELECT COUNT(*) FROM aluno " +
                "WHERE codigo = @Codigo", new { Codigo = codigo }, _session.Transaction);

            return quantidade != 0;
        }

        public async Task Adicionar(Aluno aluno)
        {
            await _session.Connection.ExecuteAsync("INSERT INTO aluno(codigo, senha, email, telefone, nome, datacadastro, ativo)" +
                "VALUES (@Codigo, @Senha, @Email, @Telefone, @Nome, @DataCadastro, 1)", new
                {
                    aluno.Codigo,
                    aluno.Senha,
                    aluno.Email,
                    aluno.Telefone,
                    aluno.Nome,
                    DataCadastro = DateTime.UtcNow
                }, _session.Transaction);
        }

        public async Task<Aluno> BuscarPorCodigo(string codigo)
        {
            var aluno = await _session.Connection.QueryFirstOrDefaultAsync<Aluno>("SELECT Codigo, Email, Telefone, Nome, Ativo FROM aluno " +
                "WHERE codigo = @Codigo LIMIT 1;", new { Codigo = codigo }, _session.Transaction);

            return aluno;
        }

        public async Task<int> BuscarIdPorCodigo(string codigo)
        {
            var idAluno = await _session.Connection.QueryFirstOrDefaultAsync<int>("SELECT Id FROM aluno " +
                "WHERE codigo = @Codigo LIMIT 1;", new { Codigo = codigo }, _session.Transaction);

            return idAluno;
        }

        public async Task<bool> AlunoMatriculado(int idAluno, int idMateria)
        {
            var quantidade = await _session.Connection.QueryFirstAsync<int>("SELECT COUNT(*) FROM alunomateria " +
                "WHERE idaluno = @IdAluno and idmateria = @IdMateria", new { IdAluno = idAluno, IdMateria = idMateria }, _session.Transaction);

            return quantidade != 0;
        }

        public async Task<bool> AlunoJaRegistrouPresenca(int idAluno, int idCronograma, DateTime dataAtual)
        {
            var horarioSaida = await _session.Connection.QueryFirstOrDefaultAsync<DateTime?>("SELECT HorarioSaida FROM presenca " +
                "WHERE idaluno = @IdAluno and idcronograma = @IdCronograma LIMIT 1", new 
                { 
                    IdAluno = idAluno,
                    IdCronograma = idCronograma
                }, _session.Transaction);

            return horarioSaida != null;
        }

        public async Task<bool> AlunoJaRegistrouEntrada(int idAluno, int idCronograma, DateTime dataAtual)
        {
            var horarioEntrada = await _session.Connection.QueryFirstOrDefaultAsync<DateTime?>("SELECT HorarioEntrada FROM presenca " +
                "WHERE idaluno = @IdAluno and idcronograma = @IdCronograma LIMIT 1", new
                {
                    IdAluno = idAluno,
                    IdCronograma = idCronograma
                }, _session.Transaction);

            return horarioEntrada != null;
        }

        public async Task Matricular(int idAluno, int idMateria)
        {
            await _session.Connection.ExecuteAsync("INSERT INTO alunomateria(idaluno, idmateria) " +
                    "VALUES (@IdAluno, @IdMateria)", new
                    {
                        IdAluno = idAluno,
                        IdMateria = idMateria
                    }, _session.Transaction);
        }

        public async Task RegistrarPresenca(int idAluno, int idCronograma, DateTime dataAtual)
        {
            var alunoJaRegistrouEntrada = await AlunoJaRegistrouEntrada(idAluno, idCronograma, dataAtual);
            if (!alunoJaRegistrouEntrada)
                await _session.Connection.ExecuteAsync("INSERT INTO presenca(idaluno, idcronograma, horarioentrada)" +
                        "VALUES (@IdAluno, @IdCronograma, @DataAtual)", new
                {
                    IdAluno = idAluno,
                    IdCronograma = idCronograma,
                    DataAtual = dataAtual.ToString("yyyy-MM-dd HH:mm:ss")
                }, _session.Transaction);
            else
                await _session.Connection.ExecuteAsync("UPDATE presenca SET horariosaida = @HorarioSaida " +
                        "WHERE idaluno = @IdAluno AND idcronograma = @IdCronograma", new
                {
                    IdAluno = idAluno,
                    IdCronograma = idCronograma,
                    HorarioSaida = dataAtual.ToString("yyyy-MM-dd HH:mm:ss")
                }, _session.Transaction);
        }

        public async Task<List<int>> IdsAlunoPorIdMateria(int idMateria)
        {
            var idsAlunos = await _session.Connection.QueryAsync<int>("SELECT IdAluno FROM alunomateria " +
                    "WHERE idmateria = @IdMateria", new
                    {
                        IdMateria = idMateria,
                    }, _session.Transaction);

            return idsAlunos.ToList();
        }

        public async Task<string> BuscarNomeAlunoPorId(int idAluno)
        {
            var nome = await _session.Connection.QueryFirstOrDefaultAsync<string>("SELECT Nome FROM aluno " +
                    "WHERE id = @IdAluno", new
                    {
                        IdAluno = idAluno,
                    }, _session.Transaction);

            return nome;
        }

        public async Task<List<Aluno>> Buscar()
        {
            var alunos = await _session.Connection.QueryAsync<Aluno>("SELECT * FROM aluno", null, _session.Transaction);

            return alunos.ToList();
        }

        public async Task<HorarioEntradaSaidaPresencaDto> HorarioEntradaESaidaAluno(int idAluno, int idCronograma)
        {
            var horarios = await _session.Connection.QueryFirstOrDefaultAsync<HorarioEntradaSaidaPresencaDto?>("SELECT HorarioEntrada, HorarioSaida FROM presenca " +
                "WHERE idaluno = @IdAluno and idcronograma = @IdCronograma LIMIT 1", new
                {
                    IdAluno = idAluno,
                    IdCronograma = idCronograma
                }, _session.Transaction);

            return horarios;
        }
    }
}
