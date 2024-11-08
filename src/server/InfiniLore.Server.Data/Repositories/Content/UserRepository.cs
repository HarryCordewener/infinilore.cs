// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Account;
using OneOf.Types;
using System.Linq.Expressions;

namespace InfiniLore.Server.Data.Repositories.Content;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[RegisterService<IUserRepository>(LifeTime.Singleton)]
public class UserRepository(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : IUserRepository  {
    public async ValueTask<QueryOutput<InfiniLoreUser>> TryGetByIdAsync(UserIdUnion userId, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        string id = userId.AsUserId;
        
        InfiniLoreUser? result = await dbContext.Users
            // .Include(u => u.Roles)
            // .Include(u => u.Permissions)
            .FirstOrDefaultAsync(predicate: u => u.Id == id, ct);

        if (result is null) return new None();
        return new Success<InfiniLoreUser>(result);
    }
    
    public async ValueTask<QueryOutput<InfiniLoreUser>> TryGetByUserNameAsync(string userName, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        
        InfiniLoreUser? result = await dbContext.Users
            // .Include(u => u.Roles)
            // .Include(u => u.Permissions)
            .FirstOrDefaultAsync(predicate: u => u.Id == userName, ct);
        
        if (result is null) return new None();
        return new Success<InfiniLoreUser>(result);
    }
    
    public async ValueTask<QueryOutputMany<InfiniLoreUser>> TryGetByQueryAsync(Expression<Func<InfiniLoreUser, bool>> predicate, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        
        InfiniLoreUser[] result = await dbContext.Users
            // .Include(u => u.Roles)
            // .Include(u => u.Permissions)
            .Where(predicate)
            .ToArrayAsync(ct);

        if (result.Length == 0) return new None();
        return new Success<InfiniLoreUser[]>(result);
    }
    
}
