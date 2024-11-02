// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories.Queries;
using InfiniLore.Server.Data.Models.UserData;

namespace InfiniLore.Server.Data.Repositories.Query.UserData;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[RegisterService<ILoreScopeQueries>(LifeTime.Scoped)]
public class LoreScopeQueries(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : QueryRepository<LoreScopeModel>(unitOfWork), ILoreScopeQueries;
