// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data.Repositories.Commands;

namespace InfiniLore.Server.Data.Repositories.Command.Account;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[RegisterService<IInfiniLoreUserCommands>(LifeTime.Scoped)]
public class InfiniLoreUserRepository : IInfiniLoreUserCommands;
