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
public class LorescopeQueryRepositoryTest(DatabaseFixture fixture) : UserContentRepositoryTestBase<ILorescopeRepository, LorescopeModel>(fixture) {

    #region Commands
    [Theory]
    [MemberData(nameof(LorescopeCommandTestData.GetSingleModels), MemberType = typeof(LorescopeCommandTestData))]
    public override Task TestCanCreateSingleModel(LorescopeModel model) => CanCreateSingleModel(model);

    [Theory]
    [MemberData(nameof(LorescopeCommandTestData.GetMultipleModels), MemberType = typeof(LorescopeCommandTestData))]
    public override Task TestCanCreateMultipleModels(IEnumerable<LorescopeModel> models) => CanCreateMultipleModels(models);

    [Theory]
    [MemberData(nameof(LorescopeCommandTestData.GetUpdate), MemberType = typeof(LorescopeCommandTestData))]
    public override Task TestCanUpdateModel(LorescopeModel model, Func<LorescopeModel, ValueTask<LorescopeModel>> updateFunc, Func<LorescopeModel, bool> validateFunc) => CanUpdateModel(model, updateFunc, validateFunc);

    [Theory]
    [MemberData(nameof(LorescopeCommandTestData.GetDeletes), MemberType = typeof(LorescopeCommandTestData))]
    public override Task TestCanDeleteModel(LorescopeModel model) => CanDeleteModel(model);
    #endregion

    #region Queries
    [Theory]
    [MemberData(nameof(LorescopeQueryTestData.GetSingleModels), MemberType = typeof(LorescopeQueryTestData))]
    public override Task TestCanGetByIdAsync(LorescopeModel model) => CanGetByIdAsync(model);

    [Theory]
    [MemberData(nameof(LorescopeQueryTestData.GetUserModels), MemberType = typeof(LorescopeQueryTestData))]
    public override Task TestCanGetByUserAsync(UserIdUnion userUnion, LorescopeModel model) => CanGetByUserAsync(userUnion, model);

    [Theory]
    [MemberData(nameof(LorescopeQueryTestData.GetMultipleModels), MemberType = typeof(LorescopeQueryTestData))]
    public override Task TestCanGetAllAsync(IEnumerable<LorescopeModel> models) => CanGetAllAsync(models);

    [Theory]
    [MemberData(nameof(LorescopeQueryTestData.GetCriteriaModels), MemberType = typeof(LorescopeQueryTestData))]
    public override Task TestCanGetByCriteriaAsync(Expression<Func<LorescopeModel, bool>> predicate, LorescopeModel model) => CanGetByCriteriaAsync(predicate, model);
    #endregion
}
