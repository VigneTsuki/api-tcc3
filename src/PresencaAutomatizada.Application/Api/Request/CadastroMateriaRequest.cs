namespace PresencaAutomatizada.Application.Api.Request
{
    public class CadastroMateriaRequest
    {
        public string Nome { get; set; }
        public int QuantidadeCreditos { get; set; }
        public string Validar()
        {
            if (QuantidadeCreditos == 0)
                return "Quantidade de créditos inválida.";

            if (Nome == null || Nome == "")
                return "Nome da matéria inválido.";

            return "";
        }
    }
}
