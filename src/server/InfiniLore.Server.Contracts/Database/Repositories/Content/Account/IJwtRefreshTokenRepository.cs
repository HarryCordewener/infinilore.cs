// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models.Content.Account;
using InfiniLore.Server.Contracts.Database.Repositories.RepositoryMethods;

namespace InfiniLore.Server.Contracts.Database.Repositories.Content.Account;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IJwtRefreshTokenRepository :
    IHasTryGetByIdAsync<JwtRefreshTokenModel>,
    IHasTryAddAsync<JwtRefreshTokenModel>,
    IHasTryRemoveAsync<JwtRefreshTokenModel>,
    IHasTryPermanentRemoveAllForUserAsync;
