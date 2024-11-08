// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Data.Models.UserData;

namespace InfiniLore.Server.Data.Repositories.Content;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[RegisterService<ILoreScopeRepository>(LifeTime.Scoped)]
public class LoreScopeRepository(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : UserContentRepository<LoreScopeModel>(unitOfWork), ILoreScopeRepository;
