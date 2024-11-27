// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using FastEndpoints;
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Services.Auth.Authorization;
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
public class UserContentAuthorizationService(IUserRepository userRepository, IUserContentAccessRepository userContentAccessRepository, IHttpContextAccessor contextAccessor) : IUserContentAuthorizationService {

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ValueTask<bool> ValidateAsync<T>(T model, AccessKind accessKind, CancellationToken ct = default) where T : UserContent 
        => ValidateAsync(model.Id, accessKind, ct);
    
    public async ValueTask<bool> ValidateAsync(Guid contentId, AccessKind accessKind, CancellationToken ct = default) {
        if (!(await GetUserFromClaimsPrincipalAsync(ct)).TryGetAsT0(out InfiniLoreUser accessorUser)) return false;
        
        return await userContentAccessRepository.UserHasKindAsync(contentId, accessorUser.Id, accessKind, ct);
    }
    
    public async ValueTask<bool> ValidateIsOwnerAsync(UserIdUnion ownerId, CancellationToken ct = default) {
        if (!(await GetUserFromClaimsPrincipalAsync(ct)).TryGetAsT0(out InfiniLoreUser accessorUser)) return false;
        
        return accessorUser.Id == ownerId.AsUserId;
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Helper Methods
    // -----------------------------------------------------------------------------------------------------------------
    private async ValueTask<Union<InfiniLoreUser, None, Failure<string>>> GetUserFromClaimsPrincipalAsync(CancellationToken ct) {
        if (contextAccessor.HttpContext is not {} accessor) return new Failure<string>("No HttpContext found in IHttpContextAccessor");
        
        QueryResult<InfiniLoreUser> accessorResult = await userRepository.TryGetByClaimsPrincipalAsync(accessor.User, ct);
        if (accessorResult.IsNone) return accessorResult.AsNone;
        if (accessorResult.IsFailure) return accessorResult.AsFailure;
        
        InfiniLoreUser accessorUser = accessorResult.AsSuccess.Value;
        return accessorUser;
    }
}
