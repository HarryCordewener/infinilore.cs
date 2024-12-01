// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models.Content.UserData;
using InfiniLore.Server.Contracts.Database.Repositories.Content.Data.User;
using InfiniLore.Server.Contracts.Types.Unions;
using System.Linq.Expressions;
using Tests.InfiniLore.Database.Repositories.Data;
using Tests.InfiniLore.Database.Repositories.Fixtures;

namespace Tests.InfiniLore.Database.Repositories;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[Collection(CollectionOrderer.QueryTestCollection)]
[TestClassPriority(0)]
public class LoreScopeQueryRepositoryTest(DatabaseFixture fixture) : UserContentRepositoryTestBase<ILoreScopeRepository, LoreScopeModel>(fixture) {

    #region Commands
    [Theory]
    [MemberData(nameof(LoreScopeCommandTestData.GetSingleModels), MemberType = typeof(LoreScopeCommandTestData))]
    public override Task TestCanCreateSingleModel(LoreScopeModel model) => CanCreateSingleModel(model);

    [Theory]
    [MemberData(nameof(LoreScopeCommandTestData.GetMultipleModels), MemberType = typeof(LoreScopeCommandTestData))]
    public override Task TestCanCreateMultipleModels(IEnumerable<LoreScopeModel> models) => CanCreateMultipleModels(models);

    [Theory]
    [MemberData(nameof(LoreScopeCommandTestData.GetUpdate), MemberType = typeof(LoreScopeCommandTestData))]
    public override Task TestCanUpdateModel(LoreScopeModel model, Func<LoreScopeModel, ValueTask<LoreScopeModel>> updateFunc, Func<LoreScopeModel, bool> validateFunc) => CanUpdateModel(model, updateFunc, validateFunc);

    [Theory]
    [MemberData(nameof(LoreScopeCommandTestData.GetDeletes), MemberType = typeof(LoreScopeCommandTestData))]
    public override Task TestCanDeleteModel(LoreScopeModel model) => CanDeleteModel(model);
    #endregion

    #region Queries
    [Theory]
    [MemberData(nameof(LoreScopeQueryTestData.GetSingleModels), MemberType = typeof(LoreScopeQueryTestData))]
    public override Task TestCanGetByIdAsync(LoreScopeModel model) => CanGetByIdAsync(model);

    [Theory]
    [MemberData(nameof(LoreScopeQueryTestData.GetUserModels), MemberType = typeof(LoreScopeQueryTestData))]
    public override Task TestCanGetByUserAsync(UserIdUnion userUnion, LoreScopeModel model) => CanGetByUserAsync(userUnion, model);

    [Theory]
    [MemberData(nameof(LoreScopeQueryTestData.GetMultipleModels), MemberType = typeof(LoreScopeQueryTestData))]
    public override Task TestCanGetAllAsync(IEnumerable<LoreScopeModel> models) => CanGetAllAsync(models);

    [Theory]
    [MemberData(nameof(LoreScopeQueryTestData.GetCriteriaModels), MemberType = typeof(LoreScopeQueryTestData))]
    public override Task TestCanGetByCriteriaAsync(Expression<Func<LoreScopeModel, bool>> predicate, LoreScopeModel model) => CanGetByCriteriaAsync(predicate, model);
    #endregion
}
