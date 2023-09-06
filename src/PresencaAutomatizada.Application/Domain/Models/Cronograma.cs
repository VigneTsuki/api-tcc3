using PresencaAutomatizada.Application.Domain.Models.Base;

namespace PresencaAutomatizada.Application.Domain.Models
{
    public class Cronograma : Entity
    {
        public int IdMateria { get; set; }
        public int IdSala { get; set; }
        public DateTime DataInicioAula { get; set; }
        public DateTime DataFimAula { get; set; }
        public int Ano { get; set; }
        public int Semestre { get; set; }

        public Cronograma(int idMateria, int idSala, DateTime dataInicioAula, DateTime dataFimAula, int ano, int semestre)
        {
            IdMateria = idMateria;
            IdSala = idSala;
            DataInicioAula = dataInicioAula;
            DataFimAula = dataFimAula;
            Ano = ano;
            Semestre = semestre;
        }
    }
}
