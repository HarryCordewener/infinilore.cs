// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models.Base;

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
        if (await dbSet.AnyAsync(m => m.Id == model.Id, cancellationToken: ct)) return "Model already exists";

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

    public async ValueTask<CommandOutput> TryAddOrUpdateRangeAsync(IEnumerable<T> models, Func<T, ValueTask<T>> update, CancellationToken ct = default) {
        var modelsToAdd = new List<T>();
        var modelsToUpdate = new List<T>();
        
        IEnumerable<T> userContents = models as T[] ?? models.ToArray();
        List<Guid> modelIds = userContents.Select(m => m.Id).Where(id => id != Guid.Empty).ToList();

        DbSet<T> dbSet = await GetDbSetAsync();
        List<T> existingModels = await dbSet.Where(m => modelIds.Contains(m.Id)).ToListAsync(ct);

        Dictionary<Guid, T> existingModelDict = existingModels.ToDictionary(keySelector: m => m.Id, elementSelector: m => m);

        foreach (T model in userContents) {
            if (model.Id == Guid.Empty) {
                modelsToAdd.Add(model);
                continue;
            }

            if (existingModelDict.TryGetValue(model.Id, out T? existingModel)) {
                modelsToUpdate.Add(await update(existingModel));
                continue;
            }

            modelsToAdd.Add(model);
        }

        if (modelsToAdd.Count != 0) await dbSet.AddRangeAsync(modelsToAdd, ct);
        if (modelsToUpdate.Count != 0) dbSet.UpdateRange(modelsToUpdate);

        return new Success();
    }

    public async ValueTask<CommandOutput> TryDeleteAsync(T model, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T? existing = await dbSet.FindAsync([model.Id], cancellationToken:ct);
        if (existing == null) return "Model does not exist";
        
        existing.SoftDelete();
        return new Success();
    }
    
    public async ValueTask<CommandOutput> TryAddRangeAsync(IEnumerable<T> models, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync();
        if (await dbSet.AnyAsync(m => models.Any(m2 => m2.Id == m.Id), cancellationToken: ct)) return "One or more Models already exist";
        await dbSet.AddRangeAsync(models, ct);
        return new Success();
    }

    public async ValueTask<CommandOutput> TryDeleteRangeAsync(IEnumerable<T> models, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync();
        IEnumerable<Guid> ids = models.Select(model => model.Id);

        await dbSet
            .Where(model => ids.Contains(model.Id))
            .ForEachAsync(model => model.SoftDelete(), ct);

        return new Success();
    }
}
