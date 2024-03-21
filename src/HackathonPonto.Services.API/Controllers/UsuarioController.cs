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
    [Route("api/usuario")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioApp _usuarioApp;

        public UsuarioController(IUsuarioApp usuarioApp)
        {
            _usuarioApp = usuarioApp;
        }

        [HttpGet]
        [SwaggerOperation(
        Summary = "Lista todos os usuários.",
        Description = "Lista de todos os usuários."
        )]
        [SwaggerResponse(200, "Success", typeof(List<UsuarioViewModel>))]
        [SwaggerResponse(204, "No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(500, "Unexpected error")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var lista = await _usuarioApp.GetAll();
                return CustomListResponse(lista, lista.Count);
            }
            catch (Exception e)
            {
                return Problem("Há um problema com a sua requisição - " + e.Message);
            }
        }
    }
}
