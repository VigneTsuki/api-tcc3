namespace PresencaAutomatizada.Application.Api.Request
{
    public class RelatorioPresencaRequest
    {
        public int IdMateria { get; set; }
        public int Ano { get; set; }
        public int Semestre { get; set; }

        public string Validar()
        {
            if (IdMateria == 0)
                return "IdMateria inválido.";

            if (Ano == 0)
                return "Ano inválido.";

            if (Semestre == 0)
                return "Semestre inválido.";

            return "";
        }
    }
}
