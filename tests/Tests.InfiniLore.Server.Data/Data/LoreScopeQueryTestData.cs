// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Account;
using InfiniLore.Server.Data.Models.UserData;
using System.Linq.Expressions;

namespace Tests.InfiniLore.Server.Data.Data;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class LoreScopeQueryTestData {

    public static TheoryData<LoreScopeModel> GetSingleModels() {
        var data = new TheoryData<LoreScopeModel>();

        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        data.Add(new LoreScopeModel {
            Id = Guid.NewGuid(),
            Name = "Test Query Scope 1",
            Description = "Test Query Scope Description 1",
            OwnerId = user1.Id
        });

        data.Add(new LoreScopeModel {
            Id = Guid.NewGuid(),
            Name = "Test Query Scope 2",
            Description = "Test Query Scope Description 2",
            OwnerId = user1.Id
        });

        return data;
    }

    public static TheoryData<UserUnion, LoreScopeModel> GetUserModels() {
        var data = new TheoryData<UserUnion, LoreScopeModel>();

        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        data.Add(new UserUnion(user1), new LoreScopeModel {
            Id = Guid.NewGuid(),
            Name = "Test Query Scope for User 1",
            Description = "Test Query Scope Description for User 1",
            OwnerId = user1.Id
        });

        return data;
    }

    public static TheoryData<IEnumerable<LoreScopeModel>> GetMultipleModels() {
        var data = new TheoryData<IEnumerable<LoreScopeModel>>();

        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        data.Add(new List<LoreScopeModel> {
            new() {
                Id = Guid.NewGuid(),
                Name = "Test Query Scope 3",
                Description = "Test Query Scope Description 3",
                OwnerId = user1.Id
            },
            new() {
                Id = Guid.NewGuid(),
                Name = "Test Query Scope 4",
                Description = "Test Query Scope Description 4",
                OwnerId = user1.Id
            }
        });

        return data;
    }

    public static TheoryData<Expression<Func<LoreScopeModel, bool>>, LoreScopeModel> GetCriteriaModels() {
        var data = new TheoryData<Expression<Func<LoreScopeModel, bool>>, LoreScopeModel>();

        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        var model = new LoreScopeModel {
            Id = Guid.NewGuid(),
            Name = "Test Query Scope with Criteria",
            Description = "Test Query Scope Description with Criteria",
            OwnerId = user1.Id
        };

        data.Add(p1: scope => scope.Name == "Test Query Scope with Criteria", model);

        return data;
    }
}
