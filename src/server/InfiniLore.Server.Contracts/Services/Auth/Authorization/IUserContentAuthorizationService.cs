// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models;

namespace InfiniLore.Server.Contracts.Services.Auth.Authorization;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IUserContentAuthorizationService {
    ValueTask<bool> InDevelopmentAsync();

    ValueTask<bool> ValidateAsync<T>(T model, AccessKind accessKind, CancellationToken ct = default) where T : UserContent;
    ValueTask<bool> ValidateAsync(Guid contentId, AccessKind accessKind, CancellationToken ct = default);
    ValueTask<bool> ValidateIsOwnerAsync(Guid ownerId, CancellationToken ct = default);

    ValueTask<bool> HasAccessRead(Guid contentId, CancellationToken ct = default);
    ValueTask<bool> HasAccessWrite(Guid contentId, CancellationToken ct = default);
    ValueTask<bool> HasAccessDelete(Guid contentId, CancellationToken ct = default);
}
