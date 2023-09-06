namespace PresencaAutomatizada.Application.Api.Request
{
    public class RegistroPresencaRequest
    {
        public string CodigoAluno { get; set; }
        public int Numeracao { get; set; }
        public string BlocoSala { get; set; }

        public string Validar()
        {
            if (CodigoAluno == null || CodigoAluno == "")
                return "Código do aluno inválido.";

            if (Numeracao == 0)
                return "Numeração da sala inválida.";

            if (BlocoSala == null || BlocoSala == "")
                return "Bloco da sala inválida.";

            return "";
        }
    }
}
