// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Content.Account;

namespace InfiniLore.Server.Contracts.Data.Repositories;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IJwtRefreshTokenRepository :
    IQueryHasTryGetByIdAsync<JwtRefreshTokenModel>,
    ICommandHasTryAddAsync<JwtRefreshTokenModel>,
    ICommandHasTryPermanentDeleteAsync<JwtRefreshTokenModel>,
    ICommandHasTryPermanentDeleteAllForUserAsync<JwtRefreshTokenModel>;
