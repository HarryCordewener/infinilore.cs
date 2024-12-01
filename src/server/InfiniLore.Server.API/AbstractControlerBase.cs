// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.MsSqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.Server.API;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class InfiniLoreControllerBase(IServiceProvider provider) : Controller {
    private readonly IDbContextFactory<MsSqlDbContext> _factory = provider.GetRequiredService<IDbContextFactory<MsSqlDbContext>>();

    protected Task<MsSqlDbContext> GetDbContext() => _factory.CreateDbContextAsync();
}
