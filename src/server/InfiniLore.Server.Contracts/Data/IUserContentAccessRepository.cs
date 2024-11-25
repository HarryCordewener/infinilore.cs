// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models;

namespace InfiniLore.Server.Contracts.Data;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IUserContentAccessRepository {
    public ValueTask<bool> UserHasKindAsync(Guid contentId, UserIdUnion accessorId, AccessKind accessKind, CancellationToken ct = default);
}
