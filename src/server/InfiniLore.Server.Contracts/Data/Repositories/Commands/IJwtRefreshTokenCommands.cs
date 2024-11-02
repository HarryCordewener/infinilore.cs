// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Account;

namespace InfiniLore.Server.Contracts.Data.Repositories.Commands;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IJwtRefreshTokenCommands :
    ICommandHasTryAddAsync<JwtRefreshTokenModel> ,
    ICommandHasTryPermanentDeleteAsync<JwtRefreshTokenModel>,
    ICommandHasTryPermanentDeleteAllForUserAsync<JwtRefreshTokenModel>
;
