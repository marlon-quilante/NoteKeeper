using Microsoft.AspNetCore.Mvc;
using NoteKeeper.Aplicacao.ModuloCategoria;

namespace NoteKeeper.WebApi.Controllers
{
    [ApiController]
    [Route("/api/categorias")]
    public class CategoriaController(CategoriaAppService categoriaAppService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<CadastrarCategoriaResult>> Cadastrar([FromBody] CadastrarCategoriaCommand command)
        {
            var resultado = await categoriaAppService.Cadastrar(command);

            if (resultado is null)
                return BadRequest("Não foi possível cadastrar a categoria. Tente novamente mais tarde!");

            return Ok(resultado); // HTTP Sucesso
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Editar(Guid id, [FromBody] EditarCategoriaPartialCommand partialCommand)
        {
            var command = new EditarCategoriaCommand(id, partialCommand.Titulo);

            var result = await categoriaAppService.Editar(command);

            if (result is null)
                return BadRequest("Falha ao editar. Tente novamente mais tarde!");

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Excluir(Guid id)
        {
            return Ok();
        }

        [HttpGet("{id:guid}")]
        public IActionResult SelecionarPorId(Guid id)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<SelecionarCategoriasResult>> SelecionarTodas()
        {
            var result = await categoriaAppService.SelecionarTodas();

            return Ok(result);
        }
    }
}
