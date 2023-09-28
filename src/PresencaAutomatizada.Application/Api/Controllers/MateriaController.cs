using Microsoft.AspNetCore.Mvc;
using PresencaAutomatizada.Application.Api.Request;
using PresencaAutomatizada.Application.Api.Response.Base;
using PresencaAutomatizada.Application.Api.Response;
using PresencaAutomatizada.Application.Data.Base;
using PresencaAutomatizada.Application.Domain.Interface;
using PresencaAutomatizada.Application.Domain.Models;

namespace PresencaAutomatizada.Application.Api.Controllers
{
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaRepository _materiaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MateriaController(IMateriaRepository materiaRepository, IUnitOfWork unitOfWork)
        {
            _materiaRepository = materiaRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Materia")]
        public async Task<IActionResult> Post([FromBody] CadastroMateriaRequest request)
        {
            var validacao = request.Validar();
            if (validacao != "")
                return BadRequest(new ResponseBase(false, validacao));

            if (await _materiaRepository.JaCadastrada(request.Nome))
                return BadRequest(new ResponseBase(false, "Matéria já cadastrada."));

            _unitOfWork.BeginTransaction();

            await _materiaRepository.Adicionar(new Materia(request.Nome, request.QuantidadeCreditos));

            _unitOfWork.Commit();

            return Ok(new ResponseBase(true, "Matéria cadastrada com sucesso."));
        }

        [HttpGet("Materias")]
        public async Task<IActionResult> Get()
        {
            var materias = await _materiaRepository.Buscar();
            return Ok(new MateriaResponse { Sucesso = true, Mensagem = "", Materias = materias });
        }
    }
}
