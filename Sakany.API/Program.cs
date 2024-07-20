using Sakany.API.Extensions.Middlewares;
using Sakany.API.Extensions.ServiceCollections;
using Sakany.Application.Extensions;
using Sakany.Infrastructure.Extensions;
using Sakany.Persistence.DataSeeding;
using Sakany.Persistence.Extensions;
using Sakany.Presentation.Extensions.Middlewares;
using Sakany.Presentation.Extensions.ServiceCollections;

namespace Sakany.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Create Web Application

            var builder = WebApplication.CreateBuilder(args);

            #endregion Create Web Application

            #region Clean Architecture Layers Configuration

            builder.Services.AddAPIServiceCollections(builder.Configuration)
                            .AddPresentationLayer(builder.Configuration)
                            .AddPersistenceLayer(builder.Configuration)
                            .AddInfrastructureLayer(builder.Configuration)
                            .AddApplicationLayer();

            #endregion Clean Architecture Layers Configuration

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Build Web Application

            var app = builder.Build();

            #endregion Build Web Application

            #region Use Middlewares

            app.UsePresentationMiddlewares();
            app.UseAPIMiddlewares();

            #endregion Use Middlewares

            app.MapControllers();

            #region Data Seeding

            await DataSeeding.Initialize(app.Services.CreateAsyncScope().ServiceProvider);

            #endregion Data Seeding

            #region Run Web Application

            app.Run();

            #endregion Run Web Application
        }
    }
}