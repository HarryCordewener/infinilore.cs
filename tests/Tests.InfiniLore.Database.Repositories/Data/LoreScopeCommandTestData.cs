// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using InfiniLore.Database.Models.Content.Account;
using InfiniLore.Database.Models.Content.UserData;

namespace Tests.InfiniLore.Database.Repositories.Data;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class LoreScopeCommandTestData
{
    public static IEnumerable<Func<LoreScopeModel>> GetSingleModels()
    {
        // Ensure GetUser1 method exists in InfiniLoreUserTestData
        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        yield return () => new LoreScopeModel
        {
            Id = Guid.NewGuid(),
            Name = "Test Scope 1",
            Description = "Test Scope Description 1",
            Owner = user1
        };

        yield return () => new LoreScopeModel
        {
            Id = Guid.NewGuid(),
            Name = "Test Scope 2",
            Description = "Test Scope Description 2",
            Owner = user1
        };
    }

    public static IEnumerable<Func<IEnumerable<LoreScopeModel>>> GetMultipleModels()
    {
        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        yield return () => new List<LoreScopeModel>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Test Scope 3",
                Description = "Test Scope Description 3",
                Owner = user1
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Test Scope 4",
                Description = "Test Scope Description 4",
                Owner = user1
            }
        };

        yield return () =>
        [
            new()
            {
                Name = "Test Scope Without Server Side Id",
                Description = "Test Scope Description 3",
                Owner = user1
            },
            new()
            {
                Name = "Test Scope Without Server Side Id 2",
                Description = "Test Scope Description 4",
                Owner = user1
            }
        ];
    }

    public static IEnumerable<(LoreScopeModel model, Func<LoreScopeModel, ValueTask<LoreScopeModel>> function,
        Func<LoreScopeModel, bool> predicate)> GetUpdate()
    {
        yield return new(
            new LoreScopeModel
            {
                Id = Guid.NewGuid(),
                Name = "Test Scope 5",
                Description = "Test Scope Description 5",
                Owner = InfiniLoreUserCommandTestData.GetUser1()
            },
            async model =>
            {
                model.Description = "Updated Scope Description 5";
                return await Task.FromResult(model);
            },
            model => model.Description == "Updated Scope Description 5"
        );
    }

    public static IEnumerable<Func<LoreScopeModel>> GetDeletes()
    {
        yield return () => new LoreScopeModel
        {
            Id = Guid.NewGuid(),
            Name = "Test Scope 6",
            Description = "Will be deleted",
            Owner = InfiniLoreUserCommandTestData.GetUser1()
        };
    }
}