// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Content.Account;

namespace InfiniLore.Server.Contracts.Data.Repositories;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IJwtRefreshTokenRepository :
    IHasTryGetByIdAsync<JwtRefreshTokenModel>,
    IHasTryAddAsync<JwtRefreshTokenModel>,
    IHasTryRemoveAsync<JwtRefreshTokenModel>,
    IHasTryPermanentRemoveAllForUserAsync;
