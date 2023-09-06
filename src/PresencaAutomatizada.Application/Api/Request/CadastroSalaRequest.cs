namespace PresencaAutomatizada.Application.Api.Request
{
    public class CadastroSalaRequest
    {
        public string Bloco { get; set; }
        public int Numeracao { get; set; }
        public string Validar()
        {
            if (Numeracao == 0)
                return "Numeração da sala inválida.";

            if (Bloco == null || Bloco == "")
                return "Bloco da sala inválida.";

            return "";
        }
    }
}
