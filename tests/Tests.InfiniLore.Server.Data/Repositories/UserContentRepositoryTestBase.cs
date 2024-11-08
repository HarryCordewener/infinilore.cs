// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
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
public abstract class UserContentRepositoryTestBase<TRepository, TModel>(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
    where TRepository : IUserContentRepository<TModel>
    where TModel : UserContent<TModel> {
    
    [UsedImplicitly] protected readonly TRepository Repository = fixture.ServiceProvider.GetRequiredService<TRepository>();
    [UsedImplicitly] protected readonly IDbUnitOfWork<InfiniLoreDbContext>  UnitOfWork = fixture.ServiceProvider.GetRequiredService<IDbUnitOfWork<InfiniLoreDbContext>>();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    [UsedImplicitly] public abstract Task TestCanCreateSingleModel(TModel model);
    [UsedImplicitly] public abstract Task TestCanCreateMultipleModels(IEnumerable<TModel> models);
    [UsedImplicitly] public abstract Task TestCanUpdateModel(TModel model, Func<TModel, ValueTask<TModel>> updateFunc, Func<TModel, bool> validateFunc);
    [UsedImplicitly] public abstract Task TestCanDeleteModel(TModel model);
    
    [UsedImplicitly] public abstract Task TestCanGetByIdAsync(TModel model);
    [UsedImplicitly] public abstract Task TestCanGetByUserAsync(UserUnion userUnion, TModel model);
    [UsedImplicitly] public abstract Task TestCanGetAllAsync(IEnumerable<TModel> models);
    [UsedImplicitly] public abstract Task TestCanGetByCriteriaAsync(Expression<Func<TModel, bool>> predicate, TModel model);

    #region Commands
    public async Task CanCreateSingleModel(TModel model) {
        // Act
        CommandOutput commandResult = await Repository.TryAddAsync(model);
        bool commitResult = await UnitOfWork.TryCommitAsync();
        
        // Assert
        Assert.True(commitResult);
        Assert.True(commandResult.IsSuccess);

        // Verify
        QueryOutput<TModel> addedModel = await Repository.TryGetByIdAsync(model.Id);
        Assert.True(addedModel.IsSuccess);
        Assert.True(addedModel.TryGetSuccessValue(out TModel? addedModelValue));
        Assert.Equal(model.Id, addedModelValue.Id);
    }
    
    public async Task CanCreateMultipleModels(IEnumerable<TModel> models) {
        // Act
        IEnumerable<TModel> userContents = models as TModel[] ?? models.ToArray();
        CommandOutput commandResult = await Repository.TryAddRangeAsync(userContents);
        bool commitResult = await UnitOfWork.TryCommitAsync();
        
        // Assert
        Assert.True(commitResult);
        Assert.True(commandResult.IsSuccess);
        
        // Verify
        foreach (TModel model in userContents) {
            QueryOutput<TModel> addedModel = await Repository.TryGetByIdAsync(model.Id);
            Assert.True(addedModel.IsSuccess);
            Assert.True(addedModel.TryGetSuccessValue(out TModel? addedModelValue));
            Assert.Equal(model.Id, addedModelValue.Id);
        }
    }
    public async Task CanUpdateModel(TModel model, Func<TModel, ValueTask<TModel>> updateFunc, Func<TModel, bool> validateFunc) {
        // Arrange
        await Repository.TryAddAsync(model);
        bool commitResult = await UnitOfWork.TryCommitAsync();

        // Act
        CommandOutput commandResult = await Repository.TryUpdateAsync(model, updateFunc);
        bool commitResult2 = await UnitOfWork.TryCommitAsync();
        
        // Assert
        Assert.True(commitResult);
        Assert.True(commitResult2);
        Assert.True(commandResult.IsSuccess);
        
        // Verify
        QueryOutput<TModel> updatedModel = await Repository.TryGetByIdAsync(model.Id);
        Assert.True(updatedModel.IsSuccess);
        Assert.True(updatedModel.TryGetSuccessValue(out TModel? value));
        Assert.True(validateFunc(value));
    }
    
    public async Task CanDeleteModel(TModel model) {
        // Arrange
        await Repository.TryAddAsync(model);
        bool commitResult = await UnitOfWork.TryCommitAsync();

        // Act
        CommandOutput commandResult = await Repository.TryDeleteAsync(model);
        bool commitResult2 = await UnitOfWork.TryCommitAsync();
        
        // Assert
        Assert.True(commitResult);
        Assert.True(commitResult2);
        Assert.True(commandResult.IsSuccess);

        // Verify
        QueryOutput<TModel> deletedModel = await Repository.TryGetByIdAsync(model.Id);
        Assert.True(deletedModel.IsNone);
    }
    #endregion
    
    #region Queries
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
        var repository = fixture.ServiceProvider.GetRequiredService<TRepository>();
        await repository.TryAddAsync(model);
        await UnitOfWork.TryCommitAsync();
    }
    #endregion
}