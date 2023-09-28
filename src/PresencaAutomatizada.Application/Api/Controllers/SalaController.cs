using Microsoft.AspNetCore.Mvc;
using PresencaAutomatizada.Application.Api.Request;
using PresencaAutomatizada.Application.Api.Response;
using PresencaAutomatizada.Application.Api.Response.Base;
using PresencaAutomatizada.Application.Data.Base;
using PresencaAutomatizada.Application.Domain.Interface;
using PresencaAutomatizada.Application.Domain.Models;

namespace PresencaAutomatizada.Application.Api.Controllers
{
    public class SalaController : ControllerBase
    {
        private readonly ISalaRepository _salaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SalaController(ISalaRepository salaRepository, IUnitOfWork unitOfWork)
        {
            _salaRepository = salaRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Sala")]
        public async Task<IActionResult> Post([FromBody] CadastroSalaRequest request)
        {
            var validacao = request.Validar();
            if (validacao != "")
                return BadRequest(new ResponseBase(false, validacao));

            if (await _salaRepository.JaCadastrada(request.Bloco,  request.Numeracao))
                return BadRequest(new ResponseBase(false, "Sala já cadastrada."));

            _unitOfWork.BeginTransaction();

            await _salaRepository.Adicionar(new Sala(request.Bloco, request.Numeracao));

            _unitOfWork.Commit();

            return Ok(new ResponseBase(true, "Sala cadastrada com sucesso."));
        }

        [HttpGet("Salas")]
        public async Task<IActionResult> Get()
        {
            var salas = await _salaRepository.Buscar();
            return Ok(new SalaResponse { Sucesso = true, Mensagem = "", Salas = salas});
        }
    }
}
