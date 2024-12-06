// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models;
using InfiniLore.Database.MsSqlServer;
using InfiniLore.Server.Contracts.Database;
using InfiniLore.Server.Contracts.Database.Repositories;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using Tests.InfiniLore.Database.Repositories.Fixtures;

namespace Tests.InfiniLore.Database.Repositories;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class UserContentRepositoryTestBase<TRepository, TModel>(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
    where TRepository : IUserContentRepository<TModel>
    where TModel : UserContent {

    private readonly TRepository _repository = fixture.ServiceProvider.GetRequiredService<TRepository>();
    private readonly IDbUnitOfWork<MsSqlDbContext> _unitOfWork = fixture.ServiceProvider.GetRequiredService<IDbUnitOfWork<MsSqlDbContext>>();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public abstract Task TestCanCreateSingleModel(TModel model);
    public abstract Task TestCanCreateMultipleModels(IEnumerable<TModel> models);
    public abstract Task TestCanUpdateModel(TModel model, Func<TModel, ValueTask<TModel>> updateFunc, Func<TModel, bool> validateFunc);
    public abstract Task TestCanDeleteModel(TModel model);

    public abstract Task TestCanGetByIdAsync(TModel model);
    public abstract Task TestCanGetByUserAsync(UserIdUnion userUnion, TModel model);
    public abstract Task TestCanGetAllAsync(IEnumerable<TModel> models);
    public abstract Task TestCanGetByCriteriaAsync(Expression<Func<TModel, bool>> predicate, TModel model);

    #region Commands
    protected async Task CanCreateSingleModel(TModel model) {
        // Act
        RepoResult commandResult = await _repository.TryAddAsync(model);
        bool commitResult = await _unitOfWork.TryCommitAsync();

        // Assert
        Assert.True(commitResult);
        Assert.True(commandResult.IsSuccess);

        // Verify
        RepoResult<TModel> addedModel = await _repository.TryGetByIdAsync(model.Id);
        Assert.True(addedModel.IsSuccess);
        Assert.True(addedModel.TryGetSuccessValue(out TModel? addedModelValue));
        Assert.Equal(model.Id, addedModelValue.Id);
    }

    protected async Task CanCreateMultipleModels(IEnumerable<TModel> models) {
        // Act
        IEnumerable<TModel> userContents = models as TModel[] ?? models.ToArray();
        RepoResult commandResult = await _repository.TryAddRangeAsync(userContents);
        bool commitResult = await _unitOfWork.TryCommitAsync();

        // Assert
        Assert.True(commitResult);
        Assert.True(commandResult.IsSuccess);

        // Verify
        foreach (TModel model in userContents) {
            RepoResult<TModel> addedModel = await _repository.TryGetByIdAsync(model.Id);
            Assert.True(addedModel.IsSuccess);
            Assert.True(addedModel.TryGetSuccessValue(out TModel? addedModelValue));
            Assert.Equal(model.Id, addedModelValue.Id);
        }
    }
    protected async Task CanUpdateModel(TModel model, Func<TModel, ValueTask<TModel>> updateFunc, Func<TModel, bool> validateFunc) {
        // Arrange
        await _repository.TryAddAsync(model);
        bool commitResult = await _unitOfWork.TryCommitAsync();
        model = await updateFunc(model);

        // Act
        RepoResult commandResult = await _repository.TryUpdateAsync(model);
        bool commitResult2 = await _unitOfWork.TryCommitAsync();

        // Assert
        Assert.True(commitResult);
        Assert.True(commitResult2);
        Assert.True(commandResult.IsSuccess);

        // Verify
        RepoResult<TModel> updatedModel = await _repository.TryGetByIdAsync(model.Id);
        Assert.True(updatedModel.IsSuccess);
        Assert.True(updatedModel.TryGetSuccessValue(out TModel? value));
        Assert.True(validateFunc(value));
    }

    protected async Task CanDeleteModel(TModel model) {
        // Arrange
        await _repository.TryAddAsync(model);
        bool commitResult = await _unitOfWork.TryCommitAsync();

        // Act
        RepoResult commandResult = await _repository.TryDeleteAsync(model);
        bool commitResult2 = await _unitOfWork.TryCommitAsync();

        // Assert
        Assert.True(commitResult);
        Assert.True(commitResult2);
        Assert.True(commandResult.IsSuccess);

        // Verify
        RepoResult<TModel> deletedModel = await _repository.TryGetByIdAsync(model.Id);
        Assert.True(deletedModel.IsFailure);// should be deleted
        Assert.Equal("Content not found.", deletedModel.AsFailure.Value);
    }
    #endregion

    #region Queries
    protected async Task CanGetByIdAsync(TModel model) {
        // Arrange
        await AddModelToDatabaseAsync(model);

        // Act
        RepoResult<TModel> result = await _repository.TryGetByIdAsync(model.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.TryGetSuccessValue(out TModel? value));
        Assert.Equal(model.Id, value.Id);
    }

    protected async Task CanGetByUserAsync(UserIdUnion userUnion, TModel model) {
        // Arrange
        await AddModelToDatabaseAsync(model);

        // Act
        RepoResult<TModel[]> result = await _repository.TryGetByUserAsync(userUnion);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.TryGetSuccessValue(out TModel[]? values));
        Assert.Contains(values, filter: m => m.Id == model.Id);
    }

    protected async Task CanGetAllAsync(IEnumerable<TModel> models) {
        // Arrange
        RepoResult<TModel[]> originalAmountResult = await _repository.TryGetAllAsync();
        Assert.True(originalAmountResult.TryGetSuccessValue(out TModel[]? originalModels));
        int originalAmount = originalModels.Length;// We need to do this because we are using the same database for all tests

        IEnumerable<TModel> userContents = models as TModel[] ?? models.ToArray();
        foreach (TModel model in userContents) {
            await AddModelToDatabaseAsync(model);
        }

        // Act
        RepoResult<TModel[]> result = await _repository.TryGetAllAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.TryGetSuccessValue(out TModel[]? values));
        Assert.Equal(userContents.Count() + originalAmount, values.Length);
    }

    protected async Task CanGetByCriteriaAsync(Expression<Func<TModel, bool>> predicate, TModel model) {
        // Arrange
        await AddModelToDatabaseAsync(model);

        // Act
        RepoResult<TModel[]> result = await _repository.TryGetByCriteriaAsync(predicate);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.TryGetSuccessValue(out TModel[]? values));
        Assert.Contains(values, filter: m => m.Id == model.Id);
    }

    private async Task AddModelToDatabaseAsync(TModel model) {
        var repository = fixture.ServiceProvider.GetRequiredService<TRepository>();
        await repository.TryAddAsync(model);
        await _unitOfWork.TryCommitAsync();
    }
    #endregion
}
