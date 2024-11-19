// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using Testcontainers.MsSql;

namespace Tests.InfiniLore.Server.Data;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class DatabaseFixture : IAsyncLifetime {
    private MsSqlContainer MsSqlContainer { get; set; } = null!;
    public InfiniLoreDbContext DbContext { get; set; } = null!;
    public IServiceProvider ServiceProvider { get; set; } = null!;

    public async Task InitializeAsync() {
        var services = new ServiceCollection();

        MsSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-CU10-ubuntu-22.04")
            .Build();

        await MsSqlContainer.StartAsync();

        services.AddSingleton(Logger.None);
        services.AddDbContextFactory<InfiniLoreDbContext>(
            options => options.UseSqlServer(MsSqlContainer.GetConnectionString())
        );

        services.RegisterServicesFromInfiniLoreServerData();

        ServiceProvider = services.BuildServiceProvider().CreateScope().ServiceProvider;

        InfiniLoreDbContext db = await ServiceProvider.GetRequiredService<IDbUnitOfWork<InfiniLoreDbContext>>().GetDbContextAsync();
        await db.Database.EnsureCreatedAsync();
        await db.SaveChangesAsync();
        DbContext = db;
    }

    public async Task DisposeAsync() {
        await MsSqlContainer.DisposeAsync();
        await DbContext.DisposeAsync();
    }
}
