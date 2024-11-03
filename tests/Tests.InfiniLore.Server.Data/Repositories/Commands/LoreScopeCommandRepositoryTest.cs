// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data.Repositories.Commands;
using InfiniLore.Server.Contracts.Data.Repositories.Queries;
using InfiniLore.Server.Data.Models.UserData;
using Tests.InfiniLore.Server.Data.Data.Commands;

namespace Tests.InfiniLore.Server.Data.Repositories.Commands;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[Collection(CollectionOrderer.CommandTestCollection)]
[TestClassPriority(0)]
public class LoreScopeCommandRepositoryTest(DatabaseFixture fixture) : CommandRepositoryTestBase<ILoreScopeQueries, ILoreScopesCommands, LoreScopeModel>(fixture) {

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
}