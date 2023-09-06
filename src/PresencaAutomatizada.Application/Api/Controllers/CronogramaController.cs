using Microsoft.AspNetCore.Mvc;
using PresencaAutomatizada.Application.Api.Request;
using PresencaAutomatizada.Application.Api.Response.Base;
using PresencaAutomatizada.Application.Data.Base;
using PresencaAutomatizada.Application.Domain.Interface;
using PresencaAutomatizada.Application.Domain.Models;
using System.Globalization;

namespace PresencaAutomatizada.Application.Api.Controllers
{
    public class CronogramaController : ControllerBase
    {
        private readonly ICronogramaRepository _cronogramaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CronogramaController(ICronogramaRepository cronogramaRepository, IUnitOfWork unitOfWork)
        {
            _cronogramaRepository = cronogramaRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Cronograma")]
        public async Task<IActionResult> Post([FromBody] CadastroCronogramaRequest request)
        {
            var validacao = request.Validar();
            if (validacao != "")
                return BadRequest(new ResponseBase(false, validacao));

            _unitOfWork.BeginTransaction();

            await _cronogramaRepository.Adicionar(new Cronograma(
                request.IdMateria, 
                request.IdSala, 
                DateTime.ParseExact(request.DataInicioAula, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), 
                DateTime.ParseExact(request.DataFimAula, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                request.Ano,
                request.Semestre
            ));

            _unitOfWork.Commit();

            return Ok(new ResponseBase(true, "Dia e hora da aula cadastrada com sucesso."));
        }
    }
}
