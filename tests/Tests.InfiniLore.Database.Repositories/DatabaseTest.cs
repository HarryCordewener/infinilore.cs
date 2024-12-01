// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.MsSqlServer;
using InfiniLore.Server.Contracts.Database;
using Microsoft.Extensions.DependencyInjection;
using Tests.InfiniLore.Database.Repositories.Fixtures;

namespace Tests.InfiniLore.Database.Repositories;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class LoreScopeCommandRepositoryTest(DatabaseFixture fixture) : IClassFixture<DatabaseFixture> {
    private readonly IDbUnitOfWork<MsSqlDbContext> _unitOfWork = fixture.ServiceProvider.GetRequiredService<IDbUnitOfWork<MsSqlDbContext>>();

    [Fact]
    public async Task TestCanConnect() {
        // Arrange: get dbContext
        MsSqlDbContext dbContext = await _unitOfWork.GetDbContextAsync();

        // Act: check the connection
        bool canConnect = await dbContext.Database.CanConnectAsync();

        // Assert: verify connection success
        Assert.True(canConnect);
    }
}
