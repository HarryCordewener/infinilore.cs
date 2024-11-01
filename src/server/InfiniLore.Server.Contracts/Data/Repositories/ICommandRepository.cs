// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Base;
using InfiniLoreLib.Results;
using OneOf;
using OneOf.Types;

namespace InfiniLore.Server.Contracts.Data.Repositories;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ICommandRepository<T> where T : UserContent<T> {
    ValueTask<CommandOutput> TryAddAsync(T model, CancellationToken ct = default);
    ValueTask<CommandOutput> TryUpdateAsync(T model, Func<T, ValueTask<T>> update, CancellationToken ct = default);
    ValueTask<CommandOutput> TryAddOrUpdateAsync(T model, Func<T, ValueTask<T>> update, CancellationToken ct = default);
    ValueTask<CommandOutput> TryDeleteAsync(T model, CancellationToken ct = default); 
    ValueTask<CommandOutput> TryAddRange(IEnumerable<T> models, CancellationToken ct = default); 
    ValueTask<CommandOutput> TryDeleteRange(IEnumerable<T> models, CancellationToken ct = default); 
}

