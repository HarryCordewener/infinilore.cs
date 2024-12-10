// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models.Content.Account;
using InfiniLore.Database.MsSqlServer;
using InfiniLore.Database.Repositories;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
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
    private MsSqlContainer MsSqlContainer { get; set; } = default!;
    public ServiceProvider Provider { get; private set; } = default!;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task InitializeAsync() {
        var services = new ServiceCollection();

        // Use a TestContainer for the database.
        MsSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-CU10-ubuntu-22.04") // Use the same image as the real thing.
            .Build();
        await MsSqlContainer.StartAsync();

        // Repositories use a lot of services, so we'll register them all here.
        services.AddSingleton(Logger.None);
        services.AddDbContextFactory<MsSqlDbContext>(
            options => options.UseSqlServer(MsSqlContainer.GetConnectionString())
        );

        services.AddIdentityCore<InfiniLoreUser>(options => {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<MsSqlDbContext>()
            .AddSignInManager();

        // Register the services which access and manipulate the database.
        services.RegisterServicesFromInfiniLoreDatabaseMsSqlServer();
        services.RegisterServicesFromInfiniLoreDatabaseRepositories();

        Provider = services.BuildServiceProvider();
        
        // Ensure the database structure is created, else our tests will fail anyway.
        //  Uses a fresh to limit the impact of the tests.
        using IServiceScope currentScope = Provider.CreateScope();
        var db = currentScope.ServiceProvider.GetRequiredService<MsSqlDbContext>();
        await db.Database.EnsureCreatedAsync();
        await db.SaveChangesAsync();
    }

    public async Task DisposeAsync() {
        // Don't forget to dispose!
        // Else we will run into a couple of issues like the container not being disposed.
        await MsSqlContainer.DisposeAsync();
        await Provider.DisposeAsync();
    }
}
