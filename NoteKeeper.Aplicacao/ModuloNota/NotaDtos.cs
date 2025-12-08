using NoteKeeper.Aplicacao.ModuloCategoria;

namespace NoteKeeper.Aplicacao.ModuloNota;

public record CadastrarNotaCommand(string Titulo, string Conteudo, Guid CategoriaId);
public record CadastrarNotaResult(Guid Id);

public record EditarNotaPartialCommand(string Titulo, string Conteudo, Guid CategoriaId);
public record EditarNotaCommand(Guid Id, string Titulo, string Conteudo, Guid CategoriaId);
public record EditarNotaResult(string Titulo, string Conteudo, Guid CategoriaId);

public record ExcluirNotaCommand(Guid Id);
public record ExcluirNotaResult();

public record SelecionarNotaPorIdQuery(Guid Id);
public record SelecionarNotaPorIdResult(Guid Id, string Titulo, string Conteudo, SelecionarCategoriaDto categoria);

public record SelecionarNotasQuery();
public record SelecionarNotasResult(IReadOnlyList<SelecionarNotasDto> notas);
public record SelecionarNotasDto(Guid Id, string Titulo, string Conteudo, Guid CategoriaId);
