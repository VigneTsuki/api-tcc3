using PresencaAutomatizada.Application.Domain.Models;

namespace PresencaAutomatizada.Application.Domain.Interface
{
    public interface IAlunoRepository
    {
        Task<bool> ValidarLogin(string login, string senha);
        Task<bool> JaCadastrado(string codigo);
        Task Adicionar(Aluno aluno);
        Task<Aluno> BuscarPorCodigo(string codigo);
        Task<int> BuscarIdPorCodigo(string codigo);
        Task<bool> AlunoMatriculado(int idAluno, int idMateria);
        Task<bool> AlunoJaRegistrouPresenca(int idAluno, int idCronograma, DateTime dataAtual);
        Task<bool> AlunoJaRegistrouEntrada(int idAluno, int idCronograma, DateTime dataAtual);
        Task Matricular(int idAluno, int idMateria);
        Task RegistrarPresenca(int idAluno, int idCronograma, DateTime dataAtual);
        Task<List<int>> IdsAlunoPorIdMateria(int idMateria);
        Task<string> BuscarNomeAlunoPorId(int idAluno);
        Task<List<Aluno>> Buscar();
    }
}
