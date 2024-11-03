// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data;
using InfiniLore.Server.Data.Models.Base;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.InfiniLore.Server.Data.Repositories;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class CommandRepositoryTestBase<TQueryRepository,TCommandRepository, TModel>(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
    where TQueryRepository : IQueryRepository<TModel>
    where TCommandRepository : ICommandRepository<TModel>
    where TModel : UserContent<TModel> {
    
    [UsedImplicitly] protected readonly TCommandRepository Repository = fixture.ServiceProvider.GetRequiredService<TCommandRepository>();
    [UsedImplicitly] protected readonly TQueryRepository QueryRepository = fixture.ServiceProvider.GetRequiredService<TQueryRepository>();
    [UsedImplicitly] protected readonly IDbUnitOfWork<InfiniLoreDbContext>  UnitOfWork = fixture.ServiceProvider.GetRequiredService<IDbUnitOfWork<InfiniLoreDbContext>>();
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    
    [UsedImplicitly] public abstract Task TestCanCreateSingleModel(TModel model);
    [UsedImplicitly] public abstract Task TestCanCreateMultipleModels(IEnumerable<TModel> models);
    [UsedImplicitly] public abstract Task TestCanUpdateModel(TModel model, Func<TModel, ValueTask<TModel>> updateFunc, Func<TModel, bool> validateFunc);
    [UsedImplicitly] public abstract Task TestCanDeleteModel(TModel model);
    
    public async Task CanCreateSingleModel(TModel model) {
        // Act
        CommandOutput commandResult = await Repository.TryAddAsync(model);
        bool commitResult = await UnitOfWork.TryCommitAsync();
        
        // Assert
        Assert.True(commitResult);
        Assert.True(commandResult.IsSuccess);

        // Verify
        QueryOutput<TModel> addedModel = await QueryRepository.TryGetByIdAsync(model.Id);
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
            QueryOutput<TModel> addedModel = await QueryRepository.TryGetByIdAsync(model.Id);
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
        QueryOutput<TModel> updatedModel = await QueryRepository.TryGetByIdAsync(model.Id);
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
        QueryOutput<TModel> deletedModel = await QueryRepository.TryGetByIdAsync(model.Id);
        Assert.True(deletedModel.IsNone);
    }
}