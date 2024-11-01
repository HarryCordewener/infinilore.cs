// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories.Commands;
using InfiniLore.Server.Data.Models.UserData;

namespace InfiniLore.Server.Data.Repositories.Command.UserData;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[RegisterService<ILoreScopesCommands>(LifeTime.Scoped)]
public class LoreScopesRepository(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : CommandRepository<LoreScopeModel>(unitOfWork), ILoreScopesCommands;
