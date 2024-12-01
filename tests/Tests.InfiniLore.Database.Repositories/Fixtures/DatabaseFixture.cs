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

namespace Tests.InfiniLore.Database.Repositories.Fixtures;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class DatabaseFixture : IAsyncLifetime {
    private MsSqlContainer MsSqlContainer { get; set; } = null!;
    public MsSqlDbContext DbContext { get; set; } = null!;
    public IServiceProvider ServiceProvider { get; set; } = null!;

    public async Task InitializeAsync() {
        var services = new ServiceCollection();

        MsSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-CU10-ubuntu-22.04")
            .Build();

        await MsSqlContainer.StartAsync();

        services.AddSingleton(Logger.None);
        services.AddDbContextFactory<MsSqlDbContext>(
            options => options.UseSqlServer(MsSqlContainer.GetConnectionString())
        );

        services.RegisterServicesFromInfiniLoreDatabaseMsSqlServer();
        services.RegisterServicesFromInfiniLoreDatabaseRepositories();

        ServiceProvider = services.BuildServiceProvider().CreateScope().ServiceProvider;

        MsSqlDbContext db = await ServiceProvider.GetRequiredService<IDbUnitOfWork<MsSqlDbContext>>().GetDbContextAsync();
        await db.Database.EnsureCreatedAsync();
        await db.SaveChangesAsync();
        DbContext = db;
    }

    public async Task DisposeAsync() {
        await MsSqlContainer.DisposeAsync();
        await DbContext.DisposeAsync();
    }
}
