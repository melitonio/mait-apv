using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MAIT.DataAccess;
using Persistence;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MAIT.Interfaces;

namespace Integration;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.Test.json", optional: true); // config especial para test
        });

        builder.ConfigureServices(static services =>
        {
            // Elimina la configuración del contexto existente
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(IDataService));
            if (descriptor != null)
                services.Remove(descriptor);

            // Agrega la configuración específica para tests
            var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            services.Configure<DbOptions>(config.GetSection("DbOptions"));

            services.AddSingleton<IDataService>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<DbOptions>>();
                var logger = sp.GetRequiredService<ILogger<DataService<ServiceDbContext>>>();
                return new DataService<ServiceDbContext>(options, logger);
            });

            // Crear base de datos automáticamente si no existe
            using var scope = services.BuildServiceProvider().CreateScope();
            var ctx = (DataService<ServiceDbContext>)scope.ServiceProvider.GetRequiredService<IDataService>();
            ctx.DbFactory.CreateDbContext().Database.EnsureCreated(); // o .Migrate()
        });

        return base.CreateHost(builder);
    }


}
