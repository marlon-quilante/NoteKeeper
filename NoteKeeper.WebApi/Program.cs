
using NoteKeeper.Aplicacao;
using NoteKeeper.Infraestrutura.Orm;
using NoteKeeper.WebApi.Config;

namespace NoteKeeper.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCamadaInfraestruturaOrm(builder.Configuration);
            builder.Services.AddCamadaInfraestruturaOrmIdentity(builder.Configuration);
            builder.Services.AddJwtAuthentication(builder.Configuration);
            builder.Services.AddCamadaAplicacao();

            builder.Services.AddSwaggerConfig();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.AplicarMigracoesEmOrm();

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
