// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using InfiniLore.Database.MsSqlServer;
using InfiniLore.Database.Repositories;
using InfiniLore.Server.Contracts.Database;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using Testcontainers.MsSql;
using TUnit.Core.Interfaces;

namespace Tests.InfiniLore.Database.Repositories.Fixtures;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class DatabaseInfrastructure : IAsyncInitializer, IAsyncDisposable
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-CU10-ubuntu-22.04")
        .Build();
    public MsSqlDbContext DbContext { get; private set; } = null!;
    public IServiceProvider ServiceProvider { get; private set; } = null!;

    public DatabaseInfrastructure()
    {
        var services = new ServiceCollection();

        _msSqlContainer.StartAsync().Wait();

        services.AddSingleton(Logger.None);
        services.AddDbContextFactory<MsSqlDbContext>(
            options => options.UseSqlServer(_msSqlContainer.GetConnectionString())
        );

        services.RegisterServicesFromInfiniLoreDatabaseMsSqlServer();
        services.RegisterServicesFromInfiniLoreDatabaseRepositories();

        ServiceProvider = services.BuildServiceProvider().CreateScope().ServiceProvider;

        MsSqlDbContext db = ServiceProvider.GetRequiredService<IDbUnitOfWork<MsSqlDbContext>>()
            .GetDbContextAsync().GetAwaiter().GetResult();
        db.Database.EnsureCreatedAsync().Wait();
        db.SaveChangesAsync().Wait();
        DbContext = db;
    }
    
    public async Task InitializeAsync()
    {
        await Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        await _msSqlContainer.DisposeAsync();
        await DbContext.DisposeAsync();
    }
}