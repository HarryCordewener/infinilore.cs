// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data;
using InfiniLore.Server.Data.Models.Base;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Tests.InfiniLore.Server.Data.Repositories;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class QueryRepositoryTestBase<TQueryRepository,TCommandRepository, TModel>(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
    where TQueryRepository : IQueryRepository<TModel>
    where TCommandRepository : ICommandRepository<TModel>
    where TModel : UserContent<TModel> {
    
    [UsedImplicitly] protected readonly TQueryRepository Repository = fixture.ServiceProvider.GetRequiredService<TQueryRepository>();
    [UsedImplicitly] protected readonly IDbUnitOfWork<InfiniLoreDbContext>  UnitOfWork = fixture.ServiceProvider.GetRequiredService<IDbUnitOfWork<InfiniLoreDbContext>>();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    
    [UsedImplicitly] public abstract Task TestCanGetByIdAsync(TModel model);
    [UsedImplicitly] public abstract Task TestCanGetByUserAsync(UserUnion userUnion, TModel model);
    [UsedImplicitly] public abstract Task TestCanGetAllAsync(IEnumerable<TModel> models);
    [UsedImplicitly] public abstract Task TestCanGetByCriteriaAsync(Expression<Func<TModel, bool>> predicate, TModel model);

    public async Task CanGetByIdAsync(TModel model) {
        // Arrange
        await AddModelToDatabaseAsync(model);

        // Act
        QueryOutput<TModel> result = await Repository.TryGetByIdAsync(model.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.TryGetSuccessValue(out TModel? value));
        Assert.Equal(model.Id, value.Id);
    }

    public async Task CanGetByUserAsync(UserUnion userUnion, TModel model) {
        // Arrange
        await AddModelToDatabaseAsync(model);

        // Act
        QueryOutputMany<TModel> result = await Repository.TryGetByUserAsync(userUnion);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.TryGetSuccessValue(out TModel[]? values));
        Assert.Contains(values, m => m.Id == model.Id);
    }

    public async Task CanGetAllAsync(IEnumerable<TModel> models) {
        // Arrange
        QueryOutputMany<TModel> originalAmountResult = await Repository.TryGetAllAsync();
        Assert.True(originalAmountResult.TryGetSuccessValue(out TModel[]? originalModels));
        int originalAmount = originalModels.Length; // We need to do this because we are using the same database for all tests
        
        IEnumerable<TModel> userContents = models as TModel[] ?? models.ToArray();
        foreach (TModel model in userContents) {
            await AddModelToDatabaseAsync(model);
        }

        // Act
        QueryOutputMany<TModel> result = await Repository.TryGetAllAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.TryGetSuccessValue(out TModel[]? values));
        Assert.Equal(userContents.Count() + originalAmount, values.Length);
    }

    public async Task CanGetByCriteriaAsync(Expression<Func<TModel, bool>> predicate, TModel model) {
        // Arrange
        await AddModelToDatabaseAsync(model);

        // Act
        QueryOutputMany<TModel> result = await Repository.TryGetByCriteriaAsync(predicate);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.TryGetSuccessValue(out TModel[]? values));
        Assert.Contains(values, m => m.Id == model.Id);
    }

    private async Task AddModelToDatabaseAsync(TModel model) {
        var repository = fixture.ServiceProvider.GetRequiredService<TCommandRepository>();
        await repository.TryAddAsync(model);
        await UnitOfWork.TryCommitAsync();
    }
}