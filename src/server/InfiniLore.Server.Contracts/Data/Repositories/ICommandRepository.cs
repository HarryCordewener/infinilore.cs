// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Base;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models.Account;

namespace InfiniLore.Server.Contracts.Data.Repositories;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ICommandRepository<T> :
    ICommandHasTryAddAsync<T>,
    ICommandHasTryUpdateAsync<T>,
    ICommandHasTryAddOrUpdateAsync<T>,
    ICommandHasTryDeleteAsync<T>

    where T : UserContent<T>;

public interface ICommandHasTryAddAsync<in T> where T : BaseContent<T> {
    ValueTask<CommandOutput> TryAddAsync(T model, CancellationToken ct = default);
    ValueTask<CommandOutput> TryAddRangeAsync(IEnumerable<T> models, CancellationToken ct = default);
}

public interface ICommandHasTryUpdateAsync<T> where T : BaseContent<T> {
    ValueTask<CommandOutput> TryUpdateAsync(T model, Func<T, ValueTask<T>> update, CancellationToken ct = default);
}

public interface ICommandHasTryAddOrUpdateAsync<T> where T : BaseContent<T> {
    ValueTask<CommandOutput> TryAddOrUpdateAsync(T model, Func<T, ValueTask<T>> update, CancellationToken ct = default);
    ValueTask<CommandOutput> TryAddOrUpdateRangeAsync(IEnumerable<T> models, Func<T, ValueTask<T>> update, CancellationToken ct = default);
}

public interface ICommandHasTryDeleteAsync<in T> where T : BaseContent<T> {
    ValueTask<CommandOutput> TryDeleteAsync(T model, CancellationToken ct = default);
    ValueTask<CommandOutput> TryDeleteRangeAsync(IEnumerable<T> models, CancellationToken ct = default);
}

// Not "normal" interfaces
public interface ICommandHasTryPermanentDeleteAsync<in T> where T : BaseContent<T> {
    ValueTask<CommandOutput> TryPermanentDeleteAsync(T model, CancellationToken ct = default);
    ValueTask<CommandOutput> TryPermanentDeleteRangeAsync(IEnumerable<T> models, CancellationToken ct = default);
}

public interface ICommandHasTryPermanentDeleteAllForUserAsync<in T> where T : UserContent<T> {
    ValueTask<CommandOutput> TryPermanentDeleteAllForUserAsync(InfiniLoreUser user, CancellationToken ct = default);
    ValueTask<CommandOutput> TryPermanentDeleteAllForUserAsync(Guid userId, CancellationToken ct = default);
    ValueTask<CommandOutput> TryPermanentDeleteAllForUserAsync(string userId, CancellationToken ct = default);
}
