using HackathonPonto.Application.InputModels;
using HackathonPonto.Application.Interfaces;
using HackathonPonto.Application.Services;
using HackathonPonto.Application.ViewModels;
using HackathonPonto.Services.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using System.Security.Cryptography;

namespace HackathonPonto.Services.API.Controllers
{
    [ApiController]
    [Route("api/ponto")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class PontoController : ApiController
    {
        private readonly IPontoApp _pontoApp;
        private readonly IFuncionarioApp _funcionarioApp;

        public PontoController(IPontoApp pontoApp, IFuncionarioApp funcionarioApp)
        {
            _pontoApp = pontoApp;
            _funcionarioApp = funcionarioApp;
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Colaborador")]
        [SwaggerOperation(
           Summary = "Registra o ponto.",
           Description = "Registra o ponto."
           )]
        [SwaggerResponse(201, "Success", typeof(PontoViewModel))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(500, "Unexpected error")]
        public async Task<IActionResult> Add()
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var cpf = claimsIdentity?.Claims.FirstOrDefault(x => x.Type == "Cpf");

                if (cpf == null)
                    return StatusCode(StatusCodes.Status401Unauthorized);   

                var result = await _pontoApp.Add(cpf.Value);

                if (result.Id != null)
                    return CustomResponse(await _pontoApp.GetById((Guid)result.Id));
                else
                    return CustomCreateResponse(result);
            }
            catch (Exception e)
            {
                return Problem("Há um problema com a sua requisição - " + e.Message);
            }
        }

        [HttpGet("{cpf}/ano/{ano}/mes/{mes}/dia/{dia}")]
        [Authorize(Roles = "Administrador,Colaborador")]
        [SwaggerOperation(
        Summary = "Ponto diário por funcionário.",
        Description = "Ponto diário por funcionário. Usuário 'Colaborador' visualiza apenas o seu registro de ponto."
        )]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(204, "No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(500, "Unexpected error")]
        public async Task<IActionResult> GetDayByUser([FromRoute] string cpf,int ano, int mes, int dia)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResponse(ModelState);

                if (DateOnly.TryParse($"{ano}-{mes}-{dia}", out DateOnly data))
                {

                    var claimsIdentity = User.Identity as ClaimsIdentity;
                    var cpfLogado = claimsIdentity?.Claims.FirstOrDefault(x => x.Type == "Cpf");
                    var perfilLogado = claimsIdentity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

                    if (cpfLogado == null)
                        return StatusCode(StatusCodes.Status401Unauthorized);

                    if (perfilLogado!.Value == "Colaborador" && cpfLogado.Value != cpf)
                        return StatusCode(StatusCodes.Status403Forbidden);

                    return CustomResponse(await _pontoApp.GetDayByUser(data, cpf));
                }

                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                return Problem("Há um problema com a sua requisição - " + e.Message);
            }
        }

        [HttpGet("{cpf}/ano/{ano}/mes/{mes}")]
        [Authorize(Roles = "Administrador,Colaborador")]
        [SwaggerOperation(
        Summary = "Ponto mensal por funcionário.",
        Description = "Ponto mensal por funcionário. Usuário 'Colaborador' visualiza apenas o seu registro de ponto."
        )]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(204, "No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(500, "Unexpected error")]
        public async Task<IActionResult> GetMonthYearByUser([FromRoute] string cpf, int ano, int mes)
        {
            try
            {
                if (!DateOnly.TryParse($"{ano}-{mes}-01", out DateOnly data))
                    return BadRequest(ModelState);

                var claimsIdentity = User.Identity as ClaimsIdentity;
                var cpfLogado = claimsIdentity?.Claims.FirstOrDefault(x => x.Type == "Cpf");
                var perfilLogado = claimsIdentity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

                if (cpfLogado == null)
                    return StatusCode(StatusCodes.Status401Unauthorized);

                if (perfilLogado!.Value == "Colaborador" && cpfLogado.Value != cpf)
                    return StatusCode(StatusCodes.Status403Forbidden);

                return CustomResponse(await _pontoApp.GetMonthYearByUser(mes, ano, cpf));
            }
            catch (Exception e)
            {
                return Problem("Há um problema com a sua requisição - " + e.Message);
            }
        }

        [HttpPost("{cpf}/ano/{ano}/mes/{mes}")]
        [Authorize(Roles = "Administrador,Colaborador")]
        [SwaggerOperation(
           Summary = "Solicita relatório de ponto.",
           Description = "Solicita relatório de ponto."
           )]
        [SwaggerResponse(201, "Success", typeof(PontoViewModel))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(500, "Unexpected error")]
        public async Task<IActionResult> SolicitaRelatorio([FromRoute] string cpf, int ano, int mes)
        {
            try
            {
                if (!DateOnly.TryParse($"{ano}-{mes}-01", out DateOnly data))
                    return BadRequest(ModelState);

                var claimsIdentity = User.Identity as ClaimsIdentity;
                var cpfLogado = claimsIdentity?.Claims.FirstOrDefault(x => x.Type == "Cpf");
                var perfilLogado = claimsIdentity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

                if (cpfLogado == null)
                    return StatusCode(StatusCodes.Status401Unauthorized);

                if (perfilLogado!.Value == "Colaborador" && cpfLogado.Value != cpf)
                    return StatusCode(StatusCodes.Status403Forbidden);

                var funcionario = await _funcionarioApp.GetByCpf(cpf);

                _pontoApp.SendReport(mes, ano, cpf, funcionario.Email, funcionario.Nome);

                return Ok("Seu relatório foi solicitado, verifique seu e-mail.");
            }
            catch (Exception e)
            {
                return Problem("Há um problema com a sua requisição - " + e.Message);
            }
        }
    }
}
