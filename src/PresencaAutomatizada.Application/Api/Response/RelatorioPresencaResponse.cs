namespace PresencaAutomatizada.Application.Api.Response
{
    public class RelatorioPresencaResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public List<RelatorioPresencaAlunosResponse> Presencas { get; set; }
        public List<string> DiasAula { get; set; }
        public List<string> Alunos { get; set; }

    }

    public class RelatorioPresencaAlunosResponse
    {
        public string DiaAula { get; set; }
        public int IdAluno { get; set; }
        public int IdCronograma { get; set; }
        public string NomeAluno { get; set; }
        public bool Presente { get; set; }
        public string DataEntrada { get; set; }
        public string DataSaida { get; set; }
        public bool AulaJaRealizada { get; set; }
    }
}
