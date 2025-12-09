using Microsoft.AspNetCore.Identity;
using NoteKeeper.Dominio.ModuloAutenticacao;
using NoteKeeper.Infraestrutura.Orm.Compartilhado;

namespace NoteKeeper.Aplicacao.ModuloAutenticacao
{
    public class AutenticacaoAppService(AppDbContext dbContext, UserManager<Usuario> userManager, AccessTokenProvider accessToken)
    {
        public async Task<AccessToken?> Registrar(RegistrarUsuarioCommand command)
        {
            if (!command.Senha.Equals(command.ConfirmarSenha))
                return null;

            Usuario usuario = new Usuario { FullName = command.NomeCompleto, UserName = command.Email, Email = command.Email };

            IdentityResult result = await userManager.CreateAsync(usuario, command.Senha);

            if (!result.Succeeded)
                return null;

            return accessToken.GerarAccessToken(usuario);
        }

        public async Task<AccessToken?> Autenticar(AutenticarUsuarioCommand command)
        {
            Usuario? usuario = await userManager.FindByEmailAsync(command.Email);

            if (usuario is null)
                return null;
            
            bool senhaValida = await userManager.CheckPasswordAsync(usuario, command.Senha);
            
            if (!senhaValida)
                return null;
            
            return accessToken.GerarAccessToken(usuario);
        }
    }
}
