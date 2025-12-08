using Microsoft.AspNetCore.Mvc;
using NoteKeeper.Aplicacao.ModuloNota;

namespace NoteKeeper.WebApi.Controllers
{
    [ApiController]
    [Route("api/notas")]
    public class NotaController(NotaAppService notaAppService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<CadastrarNotaResult>> Cadastrar([FromBody] CadastrarNotaCommand command)
        {
            var result = await notaAppService.Cadastrar(command);

            if (result is null)
                return BadRequest("Não foi possível cadastrar! Verifique se o título já existe ou se a categoria é válida.");

            return CreatedAtAction(nameof(SelecionarPorId), new { id = result.Id }, result);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<EditarNotaResult>> Editar(Guid id, [FromBody] EditarNotaPartialCommand partialCommand)
        {
            var command = new EditarNotaCommand(id, partialCommand.Titulo, partialCommand.Conteudo, partialCommand.CategoriaId);

            var result = await notaAppService.Editar(command);

            if (result is null)
                return BadRequest("Falha ao editar! Tente novamente mais tarde.");

            return Ok(result);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<ExcluirNotaResult>> Excluir(Guid id)
        {
            var command = new ExcluirNotaCommand(id);

            var result = await notaAppService.Excluir(command);

            if (result is null)
                return BadRequest("Falha ao excluir! Tente novamente mais tarde.");

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<SelecionarNotasResult>> SelecionarTodas()
        {
            var query = new SelecionarNotasQuery();

            var result = await notaAppService.SelecionarTodas(query);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SelecionarNotaPorIdResult>> SelecionarPorId(Guid id)
        {
            var query = new SelecionarNotaPorIdQuery(id);

            var result = await notaAppService.SelecionarPorId(query);

            if (result is null)
                return NotFound("Nota não encontrada! Tente novamente mais tarde.");

            return Ok(result);
        }
    }
}
