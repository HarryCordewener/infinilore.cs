// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories.Commands;
using InfiniLore.Server.Contracts.Data.Repositories.Queries;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data;
using InfiniLore.Server.Data.Models.Account;
using InfiniLore.Server.Data.Models.UserData;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.InfiniLore.Server.Api;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class LoreScopeCommandRepositoryTest(DatabaseFixture fixture) : IClassFixture<DatabaseFixture> {
    private readonly ILoreScopesCommands _commands = fixture.ServiceProvider.GetRequiredService<ILoreScopesCommands>();
    private readonly ILoreScopeQueries _queries = fixture.ServiceProvider.GetRequiredService<ILoreScopeQueries>();
    private readonly IDbUnitOfWork<InfiniLoreDbContext> _unitOfWork = fixture.ServiceProvider.GetRequiredService<IDbUnitOfWork<InfiniLoreDbContext>>();

    [Fact]
    public async Task TestCanConnect() => Assert.True(await (await _unitOfWork.GetDbContextAsync()).Database.CanConnectAsync());

    [Fact]
    public async Task TestCanCreateScope() {
        var user = new InfiniLoreUser();

        var scopeId = Guid.NewGuid();
        var scope = new LoreScopeModel {
            Id = scopeId,
            Name = "Test Scope",
            Description = "Test Scope Description",
            Owner = user
        };

        CommandOutput result = await _commands.TryAddAsync(scope);
        Assert.True(result.IsSuccess);

        await _unitOfWork.CommitAsync();

        QueryOutput<LoreScopeModel> resultQuery = await _queries.TryGetByIdAsync(scopeId);
        Assert.True(resultQuery.TryGetSuccessValue(out LoreScopeModel? scopeQuery));
        Assert.Equal(scope.Id, scopeQuery.Id);
        Assert.Equal(scope.Name, scopeQuery.Name);
        Assert.Equal(scope.Description, scopeQuery.Description);
        Assert.Equal(scope.Owner, scopeQuery.Owner);
    }
}
