namespace PresencaAutomatizada.Application.Api.Request
{
    public class CadastroAlunoRequest
    {
        public string Codigo { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string ReSenha { get; set; }

        public string Validar()
        {
            if (Codigo == null || Codigo == "")
                return "Código inválido.";

            if (Email == null || Email == "")
                return "Email inválido.";

            if (Telefone == null || Telefone == "")
                return "Telefone inválido.";

            if (Nome == null || Nome == "")
                return "Nome inválido.";

            if (Senha == null || Senha == "")
                return "Senha inválido.";

            if (ReSenha == null || ReSenha == "")
                return "ReSenha inválido.";

            return "";
        }

        public bool SenhasIguais() => Senha.Equals(ReSenha);
    }
}
