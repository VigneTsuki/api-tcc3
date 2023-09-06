namespace PresencaAutomatizada.Application.Api.Request
{
    public class CadastroCronogramaRequest
    {
        public int IdMateria { get; set; }
        public int IdSala { get; set; }
        public string? DataInicioAula { get; set; }
        public string? DataFimAula { get; set; }
        public int Ano { get; set; }
        public int Semestre { get; set; }

        public string Validar()
        {
            if (IdMateria == 0)
                return "IdMateria inválido.";

            if (IdSala == 0)
                return "IdMateria inválido.";

            if (DataInicioAula == null)
                return "DataInicioAula inválido.";

            if (DataFimAula == null)
                return "DataFimAula inválido.";

            if (Ano == 0)
                return "Ano inválido.";

            if (Semestre == 0)
                return "Semestre inválido.";

            return "";
        }
    }
}
