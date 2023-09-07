using Microsoft.AspNetCore.Mvc;
using PresencaAutomatizada.Application.Api.Request;
using PresencaAutomatizada.Application.Api.Response;
using PresencaAutomatizada.Application.Api.Response.Base;
using PresencaAutomatizada.Application.Data.Base;
using PresencaAutomatizada.Application.Data.Repository;
using PresencaAutomatizada.Application.Domain.Interface;
using PresencaAutomatizada.Application.Domain.Models;

namespace PresencaAutomatizada.Application.Api.Controllers
{
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly ISalaRepository _salaRepository;
        private readonly ICronogramaRepository _cronogramaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AlunoController(IAlunoRepository alunoRepository, ISalaRepository salaRepository, IUnitOfWork unitOfWork, ICronogramaRepository cronogramaRepository)
        {
            _alunoRepository = alunoRepository;
            _salaRepository = salaRepository;
            _unitOfWork = unitOfWork;
            _cronogramaRepository = cronogramaRepository;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var validacao = loginRequest.Validar();
            if (validacao != "")
                return BadRequest(new ResponseBase(false, validacao));

            var loginValido = await _alunoRepository.ValidarLogin(loginRequest.Login, loginRequest.Senha);

            return loginValido
                ? Ok(new ResponseBase(true, "Login realizado com sucesso."))
                : BadRequest(new ResponseBase (false, "Dados de acesso incorretos."));
        }

        [HttpPost("Aluno")]
        public async Task<IActionResult> Post([FromBody] CadastroAlunoRequest alunoRequest)
        {
            var validacao = alunoRequest.Validar();
            if (validacao != "")
                return BadRequest(new ResponseBase(false, validacao));

            if (!alunoRequest.SenhasIguais())
                return BadRequest(new ResponseBase(false, "As senhas não coincidem."));

            if (await _alunoRepository.JaCadastrado(alunoRequest.Codigo))
                return BadRequest(new ResponseBase (false, "Código de estudante já registrado."));

            _unitOfWork.BeginTransaction();

            await _alunoRepository.Adicionar(new Aluno(alunoRequest.Codigo, alunoRequest.Email, alunoRequest.Nome, alunoRequest.Senha, alunoRequest.Telefone));

            _unitOfWork.Commit();

            return Ok(new ResponseBase(true, "Usuário cadastrado com sucesso."));
        }

        [HttpGet("Aluno")]
        public async Task<IActionResult> Get([FromQuery] string codigo)
        {
            if (codigo == null || codigo == "")
                return BadRequest(new ResponseBase(false, "Código inválido."));

            var usuario = await _alunoRepository.BuscarPorCodigo(codigo);

            return usuario != null
                ? Ok(new AlunoResponse
                {
                    Sucesso = true,
                    Mensagem = "",
                    Codigo = usuario.Codigo,
                    Email = usuario.Email,
                    Nome = usuario.Nome,
                    Telefone = usuario.Telefone
                })
                : BadRequest(new ResponseBase(false, "Usuário não encontrado."));
        }

        [HttpPost("Matricular")]
        public async Task<IActionResult> Matricular([FromQuery] int idAluno, int idMateria)
        {
            if(await _alunoRepository.AlunoMatriculado(idAluno, idMateria))
                return BadRequest(new ResponseBase(false, "O aluno já está matriculado nesta matéria."));

            await _alunoRepository.Matricular(idAluno, idMateria);
            return Ok(new ResponseBase(true, "Matricula realizada com sucesso."));
        }

        [HttpPost("RegistrarPresenca")]
        public async Task<IActionResult> RegistrarPresenca([FromBody] RegistroPresencaRequest request)
        {
            var validacao = request.Validar();
            if (validacao != "")
                return BadRequest(new ResponseBase(false, validacao));

            var dataAtual = DateTime.Now;

            var idSala = await _salaRepository.BuscarIdSalaPorNumeracaoECodigo(request.Numeracao, request.BlocoSala);
            if (idSala == 0)
                return BadRequest(new ResponseBase(false, "Sala não encontrada, verifique o campo Numeracao e BlocoSala."));

            var idAluno = await _alunoRepository.BuscarIdPorCodigo(request.CodigoAluno);
            if (idAluno == 0)
                return BadRequest(new ResponseBase(false, "Aluno não encontrado, verifique o campo Código."));

            var ids = await _cronogramaRepository.BuscarIdMateriaCronogramaPorSala(idSala, dataAtual);
            if (ids == null)
                return BadRequest(new ResponseBase(false, "Não há nenhuma matéria cadastrada para este dia e horário na sala informada."));

            var alunoMatriculado = await _alunoRepository.AlunoMatriculado(idAluno, ids.IdMateria);
            if (!alunoMatriculado)
                return BadRequest(new ResponseBase(false, "O aluno não está matriculado nesta matéria."));

            var alunoJaRegistrouPresenca = await _alunoRepository.AlunoJaRegistrouPresenca(idAluno, ids.Id, dataAtual);
            if (alunoJaRegistrouPresenca)
                return BadRequest(new ResponseBase(false, "O aluno já registrou a presença nesta matéria."));

            await _alunoRepository.RegistrarPresenca(idAluno, ids.Id, dataAtual);

            return Ok(new ResponseBase(true, "Presença registrada com sucesso."));

        }

        [HttpGet("Alunos")]
        public async Task<IActionResult> Get()
        {
            var alunos = await _alunoRepository.Buscar();
            return Ok(new AlunosResponse { Sucesso = true, Mensagem = "", Alunos = alunos });
        }
    }
}
