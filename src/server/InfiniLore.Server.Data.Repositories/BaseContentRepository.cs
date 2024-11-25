// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using CodeOfChaos.Extensions;
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models;
using InfiniLore.Server.Data.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace InfiniLore.Server.Data.Repositories;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class BaseContentRepository<T>(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : InfiniLoreDbContextRepository<T>(unitOfWork), IBaseContentRepository<T> where T : BaseContent {
    #region Commands
    public async virtual ValueTask<CommandOutput> TryAddAsync(T model, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync();
        if (await dbSet.AnyAsync(predicate: m => m.Id == model.Id, ct)) return "Model already exists";

        await dbSet.AddAsync(model, ct);
        return new Success();
    }

    public async virtual ValueTask<CommandResult<T>> TryAddWithResultAsync(T model, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync();
        if (await dbSet.AnyAsync(predicate: m => m.Id == model.Id, ct)) return "Model already exists";

        EntityEntry<T> result = await dbSet.AddAsync(model, ct);
        return result;
    }

    public async virtual ValueTask<CommandOutput> TryUpdateAsync(T model, Func<T, ValueTask<T>> update, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T? existing = await dbSet.FindAsync([model.Id], ct);
        if (existing == null) return "Model does not exist";

        EntityEntry<T> result = dbSet.Update(await update(existing));
        result.Entity.UpdateLastModifiedDate();
        
        return new Success();
    }

    public async virtual ValueTask<CommandResult<T>> TryUpdateWithResultAsync(T model, Func<T, ValueTask<T>> update, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T? existing = await dbSet.FindAsync([model.Id], ct);
        if (existing == null) return "Model does not exist";

        EntityEntry<T> result = dbSet.Update(await update(existing));
        result.Entity.UpdateLastModifiedDate();
        
        return result;
    }

    public async virtual ValueTask<CommandOutput> TryAddOrUpdateAsync(T model, Func<T, ValueTask<T>> update, CancellationToken ct = default) {
        if (model.Id == Guid.Empty) return await TryAddAsync(model, ct);

        DbSet<T> dbSet = await GetDbSetAsync();
        T? existing = await dbSet.FindAsync([model.Id], ct);
        if (existing is null) {
            await dbSet.AddAsync(model, ct);
            return new Success();
        }

        EntityEntry<T> result = dbSet.Update(await update(existing));
        result.Entity.UpdateLastModifiedDate();
        
        return new Success();
    }

    public async virtual ValueTask<CommandOutput> TryAddOrUpdateRangeAsync(IEnumerable<T> models, Func<T, ValueTask<T>> update, CancellationToken ct = default) {
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
                T updatedModel = await update(existingModel);
                existingModel.UpdateLastModifiedDate();
                modelsToUpdate.Add(updatedModel);
                continue;
            }

            modelsToAdd.Add(model);
        }

        if (modelsToAdd.Count != 0) await dbSet.AddRangeAsync(modelsToAdd, ct);
        if (modelsToUpdate.Count != 0) dbSet.UpdateRange(modelsToUpdate);

        return new Success();
    }

    public async virtual ValueTask<CommandOutput> TryDeleteAsync(T model, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T? existing = await dbSet.FindAsync([model.Id], ct);
        if (existing == null) return "Model does not exist";

        existing.SoftDelete();
        return new Success();
    }

    public async virtual ValueTask<CommandOutput> TryAddRangeAsync(IEnumerable<T> models, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync();
        IEnumerable<T> userContents = models as T[] ?? models.ToArray();
        List<Guid> modelIds = userContents.Select(m => m.Id).ToList();

        // Get all models in the db that match any Ids of the passed-in models
        List<T> existingModels = await dbSet.Where(m => modelIds.Contains(m.Id)).ToListAsync(ct);

        if (existingModels.Count > 0)
            return "One or more Models already exist";

        await dbSet.AddRangeAsync(userContents, ct);
        return new Success();
    }

    public async virtual ValueTask<CommandOutput> TryDeleteRangeAsync(IEnumerable<T> models, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync();
        IEnumerable<Guid> ids = models.Select(model => model.Id);

        await dbSet
            .Where(model => ids.Contains(model.Id))
            .ForEachAsync(action: model => model.SoftDelete(), ct);

        return new Success();
    }
    #endregion

    #region Queries
    public async virtual ValueTask<QueryResult<T>> TryGetByIdAsync(Guid id, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T? result = await dbSet
            .FirstOrDefaultAsync(predicate: ls => ls.Id == id, ct);

        if (result is null) return new None();

        return new Success<T>(result);
    }

    public async virtual ValueTask<QueryResultMany<T>> TryGetAllAsync(CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
    }

    public async virtual ValueTask<QueryResultMany<T>> TryGetAllASync(PaginationInfo pageInfo, CancellationToken ct) {
        if (pageInfo.IsNotValid(out Failure<string> pageInfoFailure)) return pageInfoFailure;

        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Skip(pageInfo.SkipAmount)
            .Take(pageInfo.PageSize)
            .ToArrayAsync(ct);

        return result.Length > 0
            ? result
            : new None();
    }

    public async virtual ValueTask<QueryResultMany<T>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Where(predicate)
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
    }

    public async virtual ValueTask<QueryResultMany<T>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Where(predicate)
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
    }

    public async virtual ValueTask<QueryResultMany<T>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, PaginationInfo pageInfo, CancellationToken ct, Expression<Func<T, object>>? orderBy = null) {
        if (pageInfo.IsNotValid(out Failure<string> pageInfoFailure)) return pageInfoFailure;

        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Where(predicate)
            .ConditionalOrderByNotNull(orderBy)// Checks for not null and then orders by it, else skips the order by
            .Skip(pageInfo.SkipAmount)
            .Take(pageInfo.PageSize)
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
    }

    public async virtual ValueTask<QueryResultMany<T>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, PaginationInfo pageInfo, CancellationToken ct, Expression<Func<T, object>>? orderBy = null) {
        if (pageInfo.IsNotValid(out Failure<string> pageInfoFailure)) return pageInfoFailure;

        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Where(predicate)
            .ConditionalOrderByNotNull(orderBy)// Checks for not null and then orders by it, else skips the order by
            .Skip(pageInfo.SkipAmount)
            .Take(pageInfo.PageSize)
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
    }
    #endregion
}
