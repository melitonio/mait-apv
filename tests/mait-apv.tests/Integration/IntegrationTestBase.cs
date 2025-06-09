using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Storage;
using MAIT.DataAccess;
using Persistence;
using MAIT.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Integration;

public abstract class IntegrationTestBase(CustomWebApplicationFactory<Program> factory) : IAsyncLifetime
{
    protected readonly CustomWebApplicationFactory<Program> Factory = factory;
    protected IServiceScope Scope = default!;
    protected ServiceDbContext Db = default!;
    private IDbContextTransaction _transaction = default!;

    protected virtual bool UseTransaction => true;

    public async Task InitializeAsync()
    {
        Scope = Factory.Services.CreateScope();
        var srv = (DataService<ServiceDbContext>)Scope.ServiceProvider.GetRequiredService<IDataService>();
        Db = srv.DbFactory.CreateDbContext();
        await Db.Database.EnsureDeletedAsync();
        await Db.Database.MigrateAsync();
        if (UseTransaction)
            _transaction = await Db.Database.BeginTransactionAsync();

        await CleanDatabaseAsync();
        await SeedAsync();
    }

    public async Task DisposeAsync()
    {
        if (UseTransaction)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
        Scope.Dispose();
    }

    /// <summary> Limpia la base de datos entre pruebas </summary>
    protected virtual async Task CleanDatabaseAsync()
    {
        //  await Db.Database.ExecuteSqlRawAsync($"DELETE FROM \"{nameof(Contacto)}\";");

        await Task.CompletedTask;
    }

    /// <summary> Inserta datos semilla antes de cada prueba </summary>
    protected virtual Task SeedAsync() => Task.CompletedTask;
}
