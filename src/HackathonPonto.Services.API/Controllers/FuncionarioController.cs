using HackathonPonto.Application.InputModels;
using HackathonPonto.Application.Interfaces;
using HackathonPonto.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HackathonPonto.Services.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Administrador")]
    [Route("api/funcionario")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class FuncionarioController: ApiController
    {
        private readonly IFuncionarioApp _funcionarioApp;        

        public FuncionarioController(IFuncionarioApp funcionarioApp)
        {
            _funcionarioApp = funcionarioApp;            
        }

        [HttpGet]
        [SwaggerOperation(
        Summary = "Lista todos os funcionários.",
        Description = "Lista de todos os funcionários ordenada por nome."
        )]
        [SwaggerResponse(200, "Success", typeof(List<FuncionarioViewModel>))]
        [SwaggerResponse(204, "No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(500, "Unexpected error")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var lista = await _funcionarioApp.GetAll();
                return CustomListResponse(lista, lista.Count);
            }
            catch (Exception e)
            {
                return Problem("Há um problema com a sua requisição - " + e.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(
       Summary = "Cadastra um novo funcionário.",
       Description = "Cadastra um novo funcionário."
       )]
        [SwaggerResponse(201, "Success", typeof(FuncionarioViewModel))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(500, "Unexpected error")]
        public async Task<IActionResult> Add([FromBody] FuncionarioInputModel funcionario)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResponse(ModelState);

                var result = await _funcionarioApp.Add(funcionario);
                if (result.Id != null)
                    return CustomResponse(await _funcionarioApp.GetById((Guid)result.Id));
                else
                    return CustomCreateResponse(result);
            }
            catch (Exception e)
            {
                return Problem("Há um problema com a sua requisição - " + e.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
        Summary = "Localiza um funcionario pelo seu ID",
        Description = "Localiza um funcionario pelo seu ID."
        )]
        [SwaggerResponse(200, "Success", typeof(FuncionarioViewModel))]
        [SwaggerResponse(204, "No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(500, "Unexpected error")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                return CustomResponse(await _funcionarioApp.GetById(id));
            }
            catch (Exception e)
            {
                return Problem("Há um problema com a sua requisição - " + e.Message);
            }
        }

        [HttpGet("cpf/{cpf}")]
        [SwaggerOperation(
        Summary = "Localiza um funcionario pelo seu CPF",
        Description = "Localiza um funcionario pelo seu CPF."
        )]
        [SwaggerResponse(200, "Success", typeof(FuncionarioViewModel))]
        [SwaggerResponse(204, "No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(500, "Unexpected error")]
        public async Task<IActionResult> GetByCpf([FromRoute] string cpf)
        {
            try
            {
                return CustomResponse(await _funcionarioApp.GetByCpf(cpf));
            }
            catch (Exception e)
            {
                return Problem("Há um problema com a sua requisição - " + e.Message);
            }
        }

        [HttpGet("email/{email}")]
        [SwaggerOperation(
        Summary = "Localiza um funcionário pelo seu endereço de email.",
        Description = "Localiza um funcionário pelo seu endereço de email."
        )]
        [SwaggerResponse(200, "Success", typeof(FuncionarioViewModel))]
        [SwaggerResponse(204, "No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(500, "Unexpected error")]
        public async Task<IActionResult> GetByEmail([FromRoute] string email)
        {
            try
            {
                return CustomResponse(await _funcionarioApp.GetByEmail(email));
            }
            catch (Exception e)
            {
                return Problem("Há um problema com a sua requisição - " + e.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
        Summary = "Atualiza o cadastro de um funcionario.",
        Description = "Atualiza o cadastro de um funcionario."
        )]
        [SwaggerResponse(204, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(500, "Unexpected error")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] FuncionarioInputModel funcionario)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResponse(ModelState);

                return CustomNoContentResponse(await _funcionarioApp.Update(id, funcionario));
            }
            catch (Exception e)
            {
                return Problem("Há um problema com a sua requisição - " + e.Message);
            }

        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
        Summary = "Exclui o cadastro de um funcionario.",
        Description = "Exclui o cadastro de um funcionario."
        )]
        [SwaggerResponse(204, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Unexpected error")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                return CustomNoContentResponse(await _funcionarioApp.Remove(id));
            }
            catch (Exception e)
            {
                return Problem("Há um problema com a sua requisição - " + e.Message);
            }

        }

    }
}
