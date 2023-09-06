using PresencaAutomatizada.Application.Domain.Models.Base;

namespace PresencaAutomatizada.Application.Domain.Models
{
    public class Sala : Entity
    {
        public DateTime DataCadastro { get; private set; }
        public string Bloco { get; set; }
        public int Numeracao { get; set; }

        protected Sala() { }
        public Sala(string bloco, int numeracao)
        {
            Bloco = bloco;
            Numeracao = numeracao;
        }
    }
}
