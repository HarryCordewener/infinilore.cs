// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.InfiniLore.Server.Data;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class LoreScopeCommandRepositoryTest(DatabaseFixture fixture) : IClassFixture<DatabaseFixture> {
    private readonly IDbUnitOfWork<InfiniLoreDbContext> _unitOfWork = fixture.ServiceProvider.GetRequiredService<IDbUnitOfWork<InfiniLoreDbContext>>();

    [Fact]
    public async Task TestCanConnect() {
        // Arrange: get dbContext
        InfiniLoreDbContext dbContext = await _unitOfWork.GetDbContextAsync();

        // Act: check the connection
        bool canConnect = await dbContext.Database.CanConnectAsync();

        // Assert: verify connection success
        Assert.True(canConnect);
    }
}
