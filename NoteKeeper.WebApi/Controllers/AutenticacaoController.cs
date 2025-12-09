using Microsoft.AspNetCore.Mvc;
using NoteKeeper.Aplicacao.ModuloAutenticacao;

namespace NoteKeeper.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AutenticacaoController(AutenticacaoAppService autenticacaoAppService) : ControllerBase
    {
        [HttpPost("registrar")]
        public async Task<ActionResult<AccessToken?>> Registrar([FromBody] RegistrarUsuarioCommand command)
        {
            var accessToken = await autenticacaoAppService.Registrar(command);

            if (accessToken is null)
                return BadRequest("Não foi possível registrar o usuário! Tente novamente mais tarde.");

            return Ok(accessToken); // HTTP Sucesso
        }

        [HttpPost("autenticar")]
        public async Task<ActionResult<AccessToken?>> Autenticar([FromBody] AutenticarUsuarioCommand command)
        {
            var accessToken = await autenticacaoAppService.Autenticar(command);

            if (accessToken is null)
                return BadRequest("Não foi possível autenticar o usuário! Tente novamente mais tarde.");

            return Ok(accessToken); // HTTP Sucesso
        }
    }
}
