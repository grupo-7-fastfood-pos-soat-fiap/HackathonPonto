using HackathonPonto.Application.InputModels;
using HackathonPonto.Application.Interfaces;
using HackathonPonto.Application.ViewModels;
using HackathonPonto.Services.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HackathonPonto.Services.API.Controllers
{
    [ApiController]
    [Route("api/login")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class LoginController : ApiController
    {
        private readonly ILoginApp _loginoApp;

        public LoginController(ILoginApp loginoApp)
        {
            _loginoApp = loginoApp;
        }

        [HttpPost]
        [SwaggerOperation(
           Summary = "Obter o token de autenticação (JWT).",
           Description = "Obter o token de autenticação (JWT)."
           )]
        [SwaggerResponse(201, "Success", typeof(TokenViewModel))]
        [SwaggerResponse(400, "Bad Request")]        
        [SwaggerResponse(500, "Unexpected error")]
        public async Task<IActionResult> Authenticate([FromBody] UsuarioInputModel usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResponse(ModelState);

                var result = await _loginoApp.Autenticar(usuario);
                
                return CustomCreateResponse(result);
            }
            catch (Exception e)
            {
                return Problem("Há um problema com a sua requisição - " + e.Message);
            }

        }        
    }
}
