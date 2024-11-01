// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Data.Models.Base;
using InfiniLoreLib.Results;

namespace InfiniLore.Server.Data.Repositories;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class CommandRepository<T>(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : Repository<T>(unitOfWork), ICommandRepository<T> 
    where T : UserContent<T>
{

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task<BoolResult> TryAddAsync(T model, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        if (dbSet.Any(m => m.Id == model.Id)) return BoolResult.Failure("Model already exists");

        await dbSet.AddAsync(model, ct);
        return BoolResult.Success();
    }
    
    public async Task<BoolResult> TryUpdateAsync(T model, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T? existing = await dbSet.FindAsync([model.Id], cancellationToken:ct);
        if (existing == null) return BoolResult.Failure("Model does not exist");
        
        existing.Update(model);
        return BoolResult.Success();
    }
    
    public async Task<BoolResult> TryAddOrUpdateAsync(T model, CancellationToken ct) {
        BoolResult result =await TryUpdateAsync(model, ct);
        if (result.IsFailure) return await TryAddAsync(model, ct);
        return result;
    }
    
    public async Task<BoolResult> TryDeleteAsync(T model, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T? existing = await dbSet.FindAsync([model.Id], cancellationToken:ct);
        if (existing == null) return BoolResult.Failure("Model does not exist");
        
        existing.SoftDelete();
        return BoolResult.Success();
    }
    
    public async Task<BoolResult> TryAddRange(IEnumerable<T> models, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        if (dbSet.Any(m => models.Any(m2 => m2.Id == m.Id))) return BoolResult.Failure("One or more Models already exist");
        await dbSet.AddRangeAsync(models, ct);
        return BoolResult.Success();
    }

    public async Task<BoolResult> TryDeleteRange(IEnumerable<T> models, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        IEnumerable<Guid> ids = models.Select(model => model.Id);

        await dbSet
            .Where(model => ids.Contains(model.Id))
            .ForEachAsync(model => model.SoftDelete(), ct);

        return BoolResult.Success();
    }
}
