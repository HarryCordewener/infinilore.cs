// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models;
using UserIdUnion=InfiniLore.Server.Contracts.Types.UserIdUnion;

namespace InfiniLore.Server.Contracts.Database.Repositories.Content.Account;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IUserContentAccessRepository {
    public ValueTask<bool> UserHasKindAsync(Guid contentId, UserIdUnion accessorId, AccessKind accessKind, CancellationToken ct = default);
}
