// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models;
using Microsoft.AspNetCore.Http;

namespace InfiniLore.Server.Contracts.Services.Authorization;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IUserContentAuthorizationService {
    Task<bool> ValidateAsync(HttpContext accessor, Guid contentId, AccessKind accessKind, CancellationToken ct = default);
    Task<bool> ValidateIsOwnerAsync(HttpContext accessor, UserIdUnion ownerId, CancellationToken ct = default);
}
