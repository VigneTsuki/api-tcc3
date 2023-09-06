namespace PresencaAutomatizada.Application.Api.Response.Base
{
    public class ResponseBase
    {
        public bool Sucesso { get; private set; }
        public string Mensagem { get; private set; }
        public ResponseBase(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }
    }
}
