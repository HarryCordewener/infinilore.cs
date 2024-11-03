// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data.Repositories.Commands;
using InfiniLore.Server.Contracts.Data.Repositories.Queries;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.UserData;
using System.Linq.Expressions;
using Tests.InfiniLore.Server.Data.Data.Queries;

namespace Tests.InfiniLore.Server.Data.Repositories.Queries;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[Collection(CollectionOrderer.QueryTestCollection)]
[TestClassPriority(0)]
public class LoreScopeQueryRepositoryTest(DatabaseFixture fixture) : QueryRepositoryTestBase<ILoreScopeQueries, ILoreScopesCommands, LoreScopeModel>(fixture) {
    [Theory]
    [MemberData(nameof(LoreScopeQueryTestData.GetSingleModels), MemberType = typeof(LoreScopeQueryTestData))]
    public override Task TestCanGetByIdAsync(LoreScopeModel model) => CanGetByIdAsync(model);
    
    [Theory]
    [MemberData(nameof(LoreScopeQueryTestData.GetUserModels), MemberType = typeof(LoreScopeQueryTestData))]
    public override Task TestCanGetByUserAsync(UserUnion userUnion, LoreScopeModel model) => CanGetByUserAsync(userUnion, model);
    
    [Theory]
    [MemberData(nameof(LoreScopeQueryTestData.GetMultipleModels), MemberType = typeof(LoreScopeQueryTestData))]
    public override Task TestCanGetAllAsync(IEnumerable<LoreScopeModel> models) => CanGetAllAsync(models);
    
    [Theory]
    [MemberData(nameof(LoreScopeQueryTestData.GetCriteriaModels), MemberType = typeof(LoreScopeQueryTestData))]
    public override Task TestCanGetByCriteriaAsync(Expression<Func<LoreScopeModel, bool>> predicate, LoreScopeModel model) => CanGetByCriteriaAsync(predicate, model);
}
