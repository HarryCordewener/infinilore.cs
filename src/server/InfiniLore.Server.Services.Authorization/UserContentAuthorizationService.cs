// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using FastEndpoints;
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Services.Authorization;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models;
using InfiniLore.Server.Data.Models.Content.Account;
using Microsoft.AspNetCore.Http;

namespace InfiniLore.Server.Services.Authorization;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[RegisterService<IUserContentAuthorizationService>(LifeTime.Scoped)]
public class UserContentAuthorizationService(IUserRepository userRepository, IUserContentAccessRepository userContentAccessRepository) : IUserContentAuthorizationService {

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Task<bool> ValidateAsync<T>(HttpContext accessor, T model, AccessKind accessKind, CancellationToken ct = default) where T : UserContent 
        => ValidateAsync(accessor, model.Id, accessKind, ct);
    
    public async Task<bool> ValidateAsync(HttpContext accessor, Guid contentId, AccessKind accessKind, CancellationToken ct = default) {
        if (!(await GetUserFromClaimsPrincipalAsync(accessor, ct)).TryGetAsT0(out InfiniLoreUser accessorUser)) return false;
        
        return await userContentAccessRepository.UserHasKindAsync(contentId, accessorUser.Id, accessKind, ct);
    }
    
    public async Task<bool> ValidateIsOwnerAsync(HttpContext accessor, UserIdUnion ownerId, CancellationToken ct = default) {
        if (!(await GetUserFromClaimsPrincipalAsync(accessor, ct)).TryGetAsT0(out InfiniLoreUser accessorUser)) return false;
        
        return accessorUser.Id == ownerId.AsUserId;
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Helper Methods
    // -----------------------------------------------------------------------------------------------------------------
    private async Task<Union<InfiniLoreUser, None, Failure<string>>> GetUserFromClaimsPrincipalAsync(HttpContext accessor, CancellationToken ct) {
        QueryResult<InfiniLoreUser> accessorResult = await userRepository.TryGetByClaimsPrincipalAsync(accessor.User, ct);
        if (accessorResult.IsNone) return accessorResult.AsNone;
        if (accessorResult.IsFailure) return accessorResult.AsFailure;
        
        InfiniLoreUser accessorUser = accessorResult.AsSuccess.Value;
        return accessorUser;
    }
}
