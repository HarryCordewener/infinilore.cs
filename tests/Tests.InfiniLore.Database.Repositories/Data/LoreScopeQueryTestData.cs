// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models.Content.Account;
using InfiniLore.Database.Models.Content.UserData;
using System.Linq.Expressions;
using UserIdUnion=InfiniLore.Server.Contracts.Types.UserIdUnion;

namespace Tests.InfiniLore.Database.Repositories.Data;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class LorescopeQueryTestData {

    public static TheoryData<LorescopeModel> GetSingleModels() {
        var data = new TheoryData<LorescopeModel>();

        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        data.Add(new LorescopeModel {
            Id = Guid.NewGuid(),
            Name = "Test Query Scope 1",
            Description = "Test Query Scope Description 1",
            Owner = user1
        });

        data.Add(new LorescopeModel {
            Id = Guid.NewGuid(),
            Name = "Test Query Scope 2",
            Description = "Test Query Scope Description 2",
            Owner = user1
        });

        return data;
    }

    public static TheoryData<UserIdUnion, LorescopeModel> GetUserModels() {
        var data = new TheoryData<UserIdUnion, LorescopeModel>();

        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        data.Add(user1, new LorescopeModel {
            Id = Guid.NewGuid(),
            Name = "Test Query Scope for User 1",
            Description = "Test Query Scope Description for User 1",
            Owner = user1
        });

        return data;
    }

    public static TheoryData<IEnumerable<LorescopeModel>> GetMultipleModels() {
        var data = new TheoryData<IEnumerable<LorescopeModel>>();

        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        data.Add(new List<LorescopeModel> {
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

    public static TheoryData<Expression<Func<LorescopeModel, bool>>, LorescopeModel> GetCriteriaModels() {
        var data = new TheoryData<Expression<Func<LorescopeModel, bool>>, LorescopeModel>();

        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        var model = new LorescopeModel {
            Id = Guid.NewGuid(),
            Name = "Test Query Scope with Criteria",
            Description = "Test Query Scope Description with Criteria",
            Owner = user1
        };

        data.Add(p1: scope => scope.Name == "Test Query Scope with Criteria", model);

        return data;
    }
}
