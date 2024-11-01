// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories.Commands;
using InfiniLore.Server.Data.Models.Account;
using InfiniLore.Server.Data.Models.UserData;
using InfiniLoreLib.Results;

namespace InfiniLore.Server.Data.Repositories.Command.Account;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[RegisterService<IInfiniLoreUserCommands>(LifeTime.Scoped)]
public class InfiniLoreUserRepository(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : IInfiniLoreUserCommands {
    #region GetLoreScopesAsync
    public Task<ResultMany<LoreScopeModel>> GetLoreScopesAsync(InfiniLoreUser user, CancellationToken ct = default)
        => GetLoreScopesAsync(user.Id, ct);
    public Task<ResultMany<LoreScopeModel>> GetLoreScopesAsync(Guid userId, CancellationToken ct = default)
        => GetLoreScopesAsync(userId.ToString(), ct);

    public async Task<ResultMany<LoreScopeModel>> GetLoreScopesAsync(string userId, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync();
        IQueryable<InfiniLoreUser> query = dbContext.Users
            .Include(u => u.LoreScopes)
            .Where(u => u.Id == userId);

        InfiniLoreUser? user = await query.FirstOrDefaultAsync(cancellationToken: ct);
        return user == null
            ? ResultMany<LoreScopeModel>.Failure("User not found")
            : ResultMany<LoreScopeModel>.Success(user.LoreScopes);
    }
    #endregion

    #region GetMultiversesAsync
    public Task<ResultMany<MultiverseModel>> GetMultiversesAsync(InfiniLoreUser user, CancellationToken ct = default)
        => GetMultiversesAsync(user.Id, ct);
    public Task<ResultMany<MultiverseModel>> GetMultiversesAsync(Guid userId, CancellationToken ct = default)
        => GetMultiversesAsync(userId.ToString(), ct);

    public async Task<ResultMany<MultiverseModel>> GetMultiversesAsync(string userId, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync();
        IQueryable<InfiniLoreUser> query = dbContext.Users
            .Include(u => u.Multiverses)
            .Where(u => u.Id == userId);

        InfiniLoreUser? user = await query.FirstOrDefaultAsync(cancellationToken: ct);
        return user == null
            ? ResultMany<MultiverseModel>.Failure("User not found")
            : ResultMany<MultiverseModel>.Success(user.Multiverses);
    }
    #endregion

    #region GetUniversesAsync
    public Task<ResultMany<UniverseModel>> GetUniversesAsync(InfiniLoreUser user, CancellationToken ct = default)
        => GetUniversesAsync(user.Id, ct);
    public Task<ResultMany<UniverseModel>> GetUniversesAsync(Guid userId, CancellationToken ct = default)
        => GetUniversesAsync(userId.ToString(), ct);

    public async Task<ResultMany<UniverseModel>> GetUniversesAsync(string userId, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync();
        IQueryable<InfiniLoreUser> query = dbContext.Users
            .Include(u => u.Universes)
            .Where(u => u.Id == userId);

        InfiniLoreUser? user = await query.FirstOrDefaultAsync(cancellationToken: ct);
        return user == null
            ? ResultMany<UniverseModel>.Failure("User not found")
            : ResultMany<UniverseModel>.Success(user.Universes);
    }
    #endregion
}
