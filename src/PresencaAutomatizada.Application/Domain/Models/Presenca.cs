using PresencaAutomatizada.Application.Domain.Models.Base;

namespace PresencaAutomatizada.Application.Domain.Models
{
    public class Presenca : Entity
    {
        public int IdAluno { get; set; }
        public int IdCronograma { get; set; }
        public DateTime HorarioEntrada { get; set; }
        public DateTime HorarioSaida { get; set; }

        public Presenca(int idAluno, int idCronograma, DateTime horarioEntrada, DateTime horarioSaida)
        {
            IdAluno = idAluno;
            IdCronograma = idCronograma;
            HorarioEntrada = horarioEntrada;
            HorarioSaida = horarioSaida;
        }
    }
}
