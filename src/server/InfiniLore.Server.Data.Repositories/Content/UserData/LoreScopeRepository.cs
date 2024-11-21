// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using FastEndpoints;
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Data.Models.Content.UserData;
using InfiniLore.Server.Data.SqlServer;

namespace InfiniLore.Server.Data.Repositories.Content.UserData;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[RegisterService<ILoreScopeRepository>(LifeTime.Scoped)]
public class LoreScopeRepository(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : UserContentRepository<LoreScopeModel>(unitOfWork), ILoreScopeRepository;
