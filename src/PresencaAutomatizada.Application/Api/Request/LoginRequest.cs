namespace PresencaAutomatizada.Application.Api.Request
{
    public class LoginRequest
    {
        public string Login { get; set; }
        public string Senha { get; set; }

        public string Validar()
        {
            if (Login == null || Login == "")
                return "Login inválido.";

            if (Senha == null || Senha == "")
                return "Senha inválido.";

            return "";
        }
    }
}
