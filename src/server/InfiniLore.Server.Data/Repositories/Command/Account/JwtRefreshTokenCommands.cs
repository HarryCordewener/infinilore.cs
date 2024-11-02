// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories.Commands;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Account;
using OneOf.Types;

namespace InfiniLore.Server.Data.Repositories.Command.Account;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[RegisterService<IJwtRefreshTokenCommands>(LifeTime.Scoped)]
public class JwtRefreshTokenCommands(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : IJwtRefreshTokenCommands {
    public async ValueTask<CommandOutput> TryAddAsync(JwtRefreshTokenModel model, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        if (await dbContext.JwtRefreshTokens.AnyAsync(m => m.Id == model.Id, cancellationToken: ct)) return "Model already exists";
        await dbContext.JwtRefreshTokens.AddAsync(model, ct);
        return new Success();
    }
    
    public async ValueTask<CommandOutput> TryAddRangeAsync(IEnumerable<JwtRefreshTokenModel> models, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        if (await dbContext.JwtRefreshTokens.AnyAsync(m => models.Any(m2 => m2.Id == m.Id), cancellationToken: ct)) return "One or more Models already exist";
        await dbContext.JwtRefreshTokens.AddRangeAsync(models, ct);
        return new Success();
    }

    public async ValueTask<CommandOutput> TryPermanentDeleteAsync(JwtRefreshTokenModel model, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        JwtRefreshTokenModel? existing = await dbContext.JwtRefreshTokens.FindAsync([model.Id], cancellationToken:ct);
        
        if (existing == null) return "Model does not exist";
        dbContext.JwtRefreshTokens.Remove(existing);
        return new Success();
    }
    
    public async ValueTask<CommandOutput> TryPermanentDeleteRangeAsync(IEnumerable<JwtRefreshTokenModel> models, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        int recordsAffected = await dbContext.JwtRefreshTokens.ExecuteDeleteAsync(cancellationToken: ct);

        if (recordsAffected <= 0) return "No models were deleted";
        return new Success();
    }
    
    public async ValueTask<CommandOutput> TryPermanentDeleteAllForUserAsync(UserUnion userUnion, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        
        int recordsAffected = await dbContext.JwtRefreshTokens
            .Where(m => m.OwnerId == userUnion.AsUserId)
            .ExecuteDeleteAsync(cancellationToken: ct);
        
        if (recordsAffected <= 0) return "No models were deleted";
        return new Success();
    }
}
