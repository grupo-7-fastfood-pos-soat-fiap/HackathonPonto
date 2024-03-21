using HackathonPonto.Application.Interfaces;
using HackathonPonto.Application.ViewModels;
using HackathonPonto.Services.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HackathonPonto.Services.API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Administrador")]
    [Route("api/ocupacao")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class OcupacaoController : ApiController
    {
        private readonly IOcupacaoApp _ocupacaoApp;

        public OcupacaoController(IOcupacaoApp ocupacaoApp)
        {
            _ocupacaoApp = ocupacaoApp;
        }

        [HttpGet]
        [SwaggerOperation(
        Summary = "Lista todos as ocupações.",
        Description = "Lista de todos as ocupações."
        )]
        [SwaggerResponse(200, "Success", typeof(List<OcupacaoViewModel>))]
        [SwaggerResponse(204, "No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(500, "Unexpected error")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var lista = await _ocupacaoApp.GetAll();
                return CustomListResponse(lista, lista.Count);
            }
            catch (Exception e)
            {
                return Problem("Há um problema com a sua requisição - " + e.Message);
            }
        }
    }
}
