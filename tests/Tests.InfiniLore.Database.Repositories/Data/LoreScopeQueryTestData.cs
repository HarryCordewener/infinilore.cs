// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using InfiniLore.Database.Models.Content.Account;
using InfiniLore.Database.Models.Content.UserData;
using InfiniLore.Server.Contracts.Types.Unions;
using System.Linq.Expressions;

namespace Tests.InfiniLore.Database.Repositories.Data;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class LoreScopeQueryTestData
{
    public static IEnumerable<LoreScopeModel> GetSingleModels()
    {
        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        yield return new LoreScopeModel
        {
            Id = Guid.NewGuid(),
            Name = "Test Query Scope 1",
            Description = "Test Query Scope Description 1",
            Owner = user1
        };

        yield return new LoreScopeModel
        {
            Id = Guid.NewGuid(),
            Name = "Test Query Scope 2",
            Description = "Test Query Scope Description 2",
            Owner = user1
        };
    }

    public static (UserIdUnion userId, LoreScopeModel model) GetUserModels()
    {
        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        return new(user1, new LoreScopeModel
        {
            Id = Guid.NewGuid(),
            Name = "Test Query Scope for User 1",
            Description = "Test Query Scope Description for User 1",
            Owner = user1
        });
    }

    public static IEnumerable<LoreScopeModel> GetMultipleModels()
    {
        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        yield return new()
        {
            Id = Guid.NewGuid(),
            Name = "Test Query Scope 3",
            Description = "Test Query Scope Description 3",
            Owner = user1
        };

        yield return new()
        {
            Id = Guid.NewGuid(),
            Name = "Test Query Scope 4",
            Description = "Test Query Scope Description 4",
            Owner = user1
        };
    }

    public static (Expression<Func<LoreScopeModel, bool>> expression, LoreScopeModel model) GetCriteriaModels()
    {
        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        var model = new LoreScopeModel
        {
            Id = Guid.NewGuid(),
            Name = "Test Query Scope with Criteria",
            Description = "Test Query Scope Description with Criteria",
            Owner = user1
        };

        return new(
            scope => scope.Name == "Test Query Scope with Criteria",
            model
        );
    }
}