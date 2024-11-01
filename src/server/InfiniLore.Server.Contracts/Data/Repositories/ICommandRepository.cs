// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Base;
using InfiniLoreLib.Results;

namespace InfiniLore.Server.Contracts.Data.Repositories;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ICommandRepository<T> where T : UserContent<T> {
    Task<BoolResult> TryAddAsync(T model, CancellationToken ct);
    Task<BoolResult> TryUpdateAsync(T model, CancellationToken ct);
    Task<BoolResult> TryAddOrUpdateAsync(T model, CancellationToken ct);
    Task<BoolResult> TryDeleteAsync(T model, CancellationToken ct); 
    Task<BoolResult> TryAddRange(IEnumerable<T> models, CancellationToken ct); 
    Task<BoolResult> TryDeleteRange(IEnumerable<T> models, CancellationToken ct); 
}