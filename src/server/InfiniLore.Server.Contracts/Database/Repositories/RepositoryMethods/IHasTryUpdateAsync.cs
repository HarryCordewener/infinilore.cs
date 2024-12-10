// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models;

namespace InfiniLore.Server.Contracts.Database.Repositories.RepositoryMethods;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
///     Defines methods for attempting to update records of type <typeparamref name="T" /> asynchronously in the database.
/// </summary>
/// <typeparam name="T">
///     The type of the content to be updated, which must inherit from <see cref="BaseContent" />.
/// </typeparam>
public interface IHasTryUpdateAsync<T> where T : BaseContent {
    /// Asynchronously attempts to update the specified model in the repository.
    /// <param name="model">The model to be updated, which must be of a type derived from BaseContent.</param>
    /// <param name="ct">
    ///     A cancellation token that can be used to cancel the asynchronous operation, if needed. Defaults to
    ///     CancellationToken.None.
    /// </param>
    /// <return>
    ///     A ValueTask representing the asynchronous operation, which contains a RepoResult indicating the success or
    ///     failure of the update operation.
    /// </return>
    ValueTask<RepoResult> TryUpdateAsync(T model, CancellationToken ct = default);

    /// <summary>
    ///     Attempts to update the specified model asynchronously and returns the result,
    ///     indicating success or failure, along with the updated model.
    /// </summary>
    /// <typeparam name="T">The type of the model to be updated, inheriting from BaseContent.</typeparam>
    /// <param name="model">The model instance that needs to be updated.</param>
    /// <param name="ct">An optional CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A ValueTask containing a RepoResult, which holds the update result status and the updated model if successful.</returns>
    ValueTask<RepoResult<T>> TryUpdateWithResultAsync(T model, CancellationToken ct = default);

    /// <summary>
    ///     Attempts to update the specified model asynchronously in the repository.
    /// </summary>
    /// <param name="model">The model of type <typeparamref name="T" /> to be updated.</param>
    /// <param name="ct">An optional cancellation token to cancel the operation.</param>
    /// <returns>
    ///     A <see cref="ValueTask{RepoResult}" /> representing the asynchronous update operation,
    ///     containing the result of the operation as a <see cref="RepoResult" />.
    /// </returns>
    ValueTask<RepoResult> TryUpdateAsync(IEnumerable<T> models, CancellationToken ct = default);
}
