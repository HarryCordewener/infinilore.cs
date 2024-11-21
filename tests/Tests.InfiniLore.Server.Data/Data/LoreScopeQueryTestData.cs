// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Content.Account;
using InfiniLore.Server.Data.Models.Content.UserData;
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
            Owner = user1
        });

        data.Add(new LoreScopeModel {
            Id = Guid.NewGuid(),
            Name = "Test Query Scope 2",
            Description = "Test Query Scope Description 2",
            Owner = user1
        });

        return data;
    }

    public static TheoryData<UserUnion, LoreScopeModel> GetUserModels() {
        var data = new TheoryData<UserUnion, LoreScopeModel>();

        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        data.Add(user1, new LoreScopeModel {
            Id = Guid.NewGuid(),
            Name = "Test Query Scope for User 1",
            Description = "Test Query Scope Description for User 1",
            Owner = user1
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
                Owner = user1
            },
            new() {
                Id = Guid.NewGuid(),
                Name = "Test Query Scope 4",
                Description = "Test Query Scope Description 4",
                Owner = user1
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
            Owner = user1
        };

        data.Add(p1: scope => scope.Name == "Test Query Scope with Criteria", model);

        return data;
    }
}
