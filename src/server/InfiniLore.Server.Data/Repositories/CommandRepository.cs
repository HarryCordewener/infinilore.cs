// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Data.Models.Base;
using InfiniLoreLib.Results;
using OneOf.Types;

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
    public async ValueTask<CommandOutput> TryAddAsync(T model, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync();
        if (dbSet.Any(m => m.Id == model.Id)) return "Model already exists";

        await dbSet.AddAsync(model, ct);
        return new Success();
    }
    
    public async ValueTask<CommandOutput> TryUpdateAsync(T model,  Func<T, ValueTask<T>> update, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T? existing = await dbSet.FindAsync([model.Id], cancellationToken:ct);
        if (existing == null) return "Model does not exist";

        dbSet.Update(await update(existing));
        
        return new Success();
    }
    
    public async ValueTask<CommandOutput> TryAddOrUpdateAsync(T model, Func<T, ValueTask<T>> update, CancellationToken ct = default) {
        if (model.Id == Guid.Empty) return await TryAddAsync(model, ct);
        
        DbSet<T> dbSet = await GetDbSetAsync();
        T? existing = await dbSet.FindAsync([model.Id], ct);
        if (existing is null) {
            await dbSet.AddAsync(model, ct);
            return new Success();
        }
        
        dbSet.Update(await update(existing));
        return new Success();
    }
    
    public async ValueTask<CommandOutput> TryDeleteAsync(T model, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T? existing = await dbSet.FindAsync([model.Id], cancellationToken:ct);
        if (existing == null) return "Model does not exist";
        
        existing.SoftDelete();
        return new Success();
    }
    
    public async ValueTask<CommandOutput> TryAddRange(IEnumerable<T> models, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync();
        if (dbSet.Any(m => models.Any(m2 => m2.Id == m.Id))) return "One or more Models already exist";
        await dbSet.AddRangeAsync(models, ct);
        return new Success();
    }

    public async ValueTask<CommandOutput> TryDeleteRange(IEnumerable<T> models, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync();
        IEnumerable<Guid> ids = models.Select(model => model.Id);

        await dbSet
            .Where(model => ids.Contains(model.Id))
            .ForEachAsync(model => model.SoftDelete(), ct);

        return new Success();
    }
}
