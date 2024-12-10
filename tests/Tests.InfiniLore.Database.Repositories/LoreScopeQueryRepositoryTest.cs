// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using InfiniLore.Database.Models.Content.UserData;
using InfiniLore.Server.Contracts.Database.Repositories.Content.Data.User;
using System.Linq.Expressions;
using Tests.InfiniLore.Database.Repositories.Data;
using Tests.InfiniLore.Database.Repositories.Fixtures;
using UserIdUnion=InfiniLore.Server.Contracts.Types.UserIdUnion;

namespace Tests.InfiniLore.Database.Repositories;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[ClassDataSource<DatabaseInfrastructure>(Shared = SharedType.PerAssembly)]
[NotInParallel]
public class LoreScopeQueryRepositoryTest(DatabaseInfrastructure infrastructure)
    : UserContentRepositoryTestBase<ILoreScopeRepository, LoreScopeModel>(infrastructure)
{
    #region Commands

    [Test]
    [MethodDataSource(typeof(LoreScopeCommandTestData), nameof(LoreScopeCommandTestData.GetSingleModels))]
    public override Task TestCanCreateSingleModel(LoreScopeModel model)
        => CanCreateSingleModel(model);

    [Test]
    [MethodDataSource(typeof(LoreScopeCommandTestData), nameof(LoreScopeCommandTestData.GetMultipleModels))]
    public override Task TestCanCreateMultipleModels(IEnumerable<LoreScopeModel> models)
        => CanCreateMultipleModels(models);

    [Test]
    [MethodDataSource(typeof(LoreScopeCommandTestData), nameof(LoreScopeCommandTestData.GetUpdate))]
    public override Task TestCanUpdateModel(
        (LoreScopeModel model, Func<LoreScopeModel, ValueTask<LoreScopeModel>> updateFunc, Func<LoreScopeModel, bool>
            validateFunc) tuple)
        => CanUpdateModel(tuple);

    [Test]
    [MethodDataSource(typeof(LoreScopeCommandTestData), nameof(LoreScopeCommandTestData.GetDeletes))]
    public override Task TestCanDeleteModel(LoreScopeModel model)
        => CanDeleteModel(model);

    #endregion

    #region Queries

    [Test]
    [MethodDataSource(typeof(LoreScopeCommandTestData), nameof(LoreScopeQueryTestData.GetSingleModels))]
    public override Task TestCanGetByIdAsync(LoreScopeModel model)
        => CanGetByIdAsync(model);

    [Test]
    [MethodDataSource(typeof(LoreScopeQueryTestData), nameof(LoreScopeQueryTestData.GetUserModels))]
    public override Task TestCanGetByUserAsync((UserIdUnion userUnion, LoreScopeModel model) tuple)
        => CanGetByUserAsync(tuple);

    [Test]
    [MethodDataSource(typeof(LoreScopeCommandTestData), nameof(LoreScopeQueryTestData.GetMultipleModels))]
    public override Task TestCanGetAllAsync(IEnumerable<LoreScopeModel> models)
        => CanGetAllAsync(models);

    [Test]
    [MethodDataSource(typeof(LoreScopeQueryTestData), nameof(LoreScopeQueryTestData.GetCriteriaModels))]
    public override Task TestCanGetByCriteriaAsync(
        (Expression<Func<LoreScopeModel, bool>> predicate, LoreScopeModel model) tuple)
        => CanGetByCriteriaAsync(tuple);

    #endregion
}