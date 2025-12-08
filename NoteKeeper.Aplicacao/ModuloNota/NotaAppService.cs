using Microsoft.Extensions.Logging;
using NoteKeeper.Aplicacao.ModuloCategoria;
using NoteKeeper.Dominio.ModuloNota;
using NoteKeeper.Infraestrutura.Orm.Compartilhado;
using NoteKeeper.Infraestrutura.Orm.ModuloNota;
using System.Collections.Immutable;

namespace NoteKeeper.Aplicacao.ModuloNota
{
    public class NotaAppService(AppDbContext dbContext, RepositorioNotaEmOrm repositorioNota, ILogger<NotaAppService> logger)
    {
        public async Task<CadastrarNotaResult?> Cadastrar(CadastrarNotaCommand command)
        {
            try
            {
                if (command is null)
                    return null;

                var notas = await repositorioNota.SelecionarTodosAsync();

                if (notas.Any(n => n.Titulo.Equals(command.Titulo, StringComparison.OrdinalIgnoreCase)))
                    return null;

                var novaNota = new Nota(command.Titulo, command.Conteudo, command.CategoriaId);

                if (novaNota is null)
                    return null;

                await repositorioNota.CadastrarAsync(novaNota);

                await dbContext.SaveChangesAsync();

                return new CadastrarNotaResult(novaNota.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ocorreu um erro durante o cadastro da nota {@Command}.", command);
                throw;
            }
        }

        public async Task<EditarNotaResult?> Editar(EditarNotaCommand command)
        {
            try
            {
                if (command is null)
                    return null;

                var notas = await repositorioNota.SelecionarTodosAsync();

                var tituloEmUso = notas.Any(n => !n.Id.Equals(command.Id) && n.Titulo.Equals(command.Titulo, StringComparison.OrdinalIgnoreCase));

                if (tituloEmUso)
                    return null;

                var dadosEditados = new Nota(command.Titulo, command.Conteudo, command.CategoriaId);

                var sucesso = await repositorioNota.EditarAsync(command.Id, dadosEditados);

                if (!sucesso)
                    return null;

                await dbContext.SaveChangesAsync();

                return new EditarNotaResult(command.Titulo, command.Conteudo, command.CategoriaId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ocorreu um erro durante a edição da nota {@Command}.", command);
                throw;
            }
        }

        public async Task<ExcluirNotaResult?> Excluir(ExcluirNotaCommand command)
        {
            try
            {
                if (command is null)
                    return null;

                var sucesso = await repositorioNota.ExcluirAsync(command.Id);

                if (!sucesso)
                    return null;

                await dbContext.SaveChangesAsync();

                return new ExcluirNotaResult();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ocorreu um erro durante a exclusão da nota {@Command}.", command);
                throw;
            }
        }

        public async Task<SelecionarNotaPorIdResult?> SelecionarPorId(SelecionarNotaPorIdQuery query)
        {

            if (query is null)
                return null;

            var nota = await repositorioNota.SelecionarPorIdAsync(query.Id);

            return new SelecionarNotaPorIdResult(nota!.Id, nota.Titulo, nota.Conteudo, new SelecionarCategoriaDto(nota.CategoriaId, nota.Categoria.Titulo));
        }

        public async Task<SelecionarNotasResult?> SelecionarTodas(SelecionarNotasQuery query)
        {
            if (query is null)
                return null;

            var notas = await repositorioNota.SelecionarTodosAsync();

            var dtos = notas.Select(n => new SelecionarNotasDto(n.Id, n.Titulo, n.Conteudo, n.CategoriaId)).ToImmutableList();

            return new SelecionarNotasResult(dtos);
        }
    }
}
