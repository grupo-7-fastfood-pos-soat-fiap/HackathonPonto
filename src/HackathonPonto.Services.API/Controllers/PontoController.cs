using HackathonPonto.Application.InputModels;
using HackathonPonto.Application.Interfaces;
using HackathonPonto.Application.Services;
using HackathonPonto.Application.ViewModels;
using HackathonPonto.Services.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace HackathonPonto.Services.API.Controllers
{
    [ApiController]
    [Route("api/ponto")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class PontoController : ApiController
    {
        private readonly IPontoApp _pontoApp;

        public PontoController(IPontoApp pontoApp)
        {
            _pontoApp = pontoApp;
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

                if (cpf == null || cpf == null)
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
    }
}
