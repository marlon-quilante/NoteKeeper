namespace NoteKeeper.Aplicacao.ModuloCategoria;

// DTO = Data Transfer Object

// Cadastro
public record CadastrarCategoriaCommand(string Titulo); // Objetos Imutáveis
public record CadastrarCategoriaResult(Guid Id);

// Edição
public record EditarCategoriaPartialCommand(string Titulo);
public record EditarCategoriaCommand(Guid Id, string Titulo);
public record EditarCategoriaResult(string Titulo);

// Exclusão
public record ExcluirCategoriaCommand(Guid Id);
public record ExcluirCategoriaResult();

// Seleção de Todos
public record SelecionarCategoriasResult(IReadOnlyList<SelecionarCategoriaDto> Registros);
public record SelecionarCategoriaDto(Guid Id, string Titulo);

// Seleção por Id
public record SelecionarCategoriaPorIdQuery(Guid Id);
public record SelecionarCategoriaPorIdResult(Guid Id, string Titulo);