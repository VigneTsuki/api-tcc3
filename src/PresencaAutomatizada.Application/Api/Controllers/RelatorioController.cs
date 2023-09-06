using Microsoft.AspNetCore.Mvc;
using PresencaAutomatizada.Application.Api.Request;
using PresencaAutomatizada.Application.Api.Response;
using PresencaAutomatizada.Application.Api.Response.Base;
using PresencaAutomatizada.Application.Domain.Interface;
using PresencaAutomatizada.Application.Domain.Models;

namespace PresencaAutomatizada.Application.Api.Controllers
{
    public class RelatorioController : ControllerBase
    {
        private readonly ICronogramaRepository _cronogramaRepository;
        private readonly IAlunoRepository _alunoRepository;

        public RelatorioController(ICronogramaRepository cronogramaRepository, IAlunoRepository alunoRepository)
        {
            _cronogramaRepository = cronogramaRepository;
            _alunoRepository = alunoRepository;
        }

        [HttpGet("Relatorio/Presenca")]
        public async Task<IActionResult> Presenca([FromQuery] RelatorioPresencaRequest request)
        {
            var validacao = request.Validar();
            if (validacao != "")
                return BadRequest(new ResponseBase(false, validacao));

            var idsCronograma = await _cronogramaRepository.BuscarIdCronogramaRelatorioPresenca(request.IdMateria, request.Ano, request.Semestre);
            var idsMatriculados = await _alunoRepository.IdsAlunoPorIdMateria(request.IdMateria);

            var response = new RelatorioPresencaResponse { Sucesso = true, Mensagem = "" };
            response.Presencas = new List<RelatorioPresencaAlunosResponse>();

            foreach (var idCronograma in idsCronograma)
            {
                foreach (var idAluno in idsMatriculados)
                {
                    var alunoPresente = await _cronogramaRepository.AlunoPresentePorIdCronograma(idCronograma, idAluno);
                    var nomeAluno = await _alunoRepository.BuscarNomeAlunoPorId(idAluno);
                    var dataAula = await _cronogramaRepository.BuscarDiaAulaPorId(idCronograma);
                    response.Presencas.Add(new RelatorioPresencaAlunosResponse
                    {
                        IdAluno = idAluno,
                        IdCronograma = idCronograma,
                        Presente = alunoPresente,
                        NomeAluno = nomeAluno,
                        DiaAula = dataAula
                    });
                }
            }

            var diasAula = new List<string>();

            foreach(var presenca in response.Presencas)
                if(!diasAula.Contains(presenca.DiaAula))
                    diasAula.Add(presenca.DiaAula);

            response.DiasAula = diasAula.OrderBy(DateTime.Parse).ToList();

            var nomeAlunos = new List<string>();

            foreach (var presenca in response.Presencas)
                if (!nomeAlunos.Contains(presenca.NomeAluno))
                    nomeAlunos.Add(presenca.NomeAluno);

            nomeAlunos.Sort();
            response.Alunos = nomeAlunos;

            response.Presencas = response.Presencas.OrderBy(p => DateTime.Parse(p.DiaAula)).ToList();

            return Ok(response);
        }
    }
}
