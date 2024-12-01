// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models;

namespace InfiniLore.Server.Contracts.Services.Auth.Authorization;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IUserContentAuthorizationService {
    ValueTask<bool> ValidateAsync<T>(T model, AccessKind accessKind, CancellationToken ct = default) where T : UserContent;
    ValueTask<bool> ValidateAsync(Guid contentId, AccessKind accessKind, CancellationToken ct = default);
    ValueTask<bool> ValidateIsOwnerAsync(Guid ownerId, CancellationToken ct = default);
}