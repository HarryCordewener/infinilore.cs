// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models.Content.Account;
using InfiniLore.Database.Models.Content.UserData;

namespace Tests.InfiniLore.Database.Repositories.Data;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class LorescopeCommandTestData {
    public static TheoryData<LorescopeModel> GetSingleModels() {
        var data = new TheoryData<LorescopeModel>();

        // Ensure GetUser1 method exists in InfiniLoreUserTestData
        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        data.Add(new LorescopeModel {
            Id = Guid.NewGuid(),
            Name = "Test Scope 1",
            Description = "Test Scope Description 1",
            Owner = user1
        });

        data.Add(new LorescopeModel {
            Id = Guid.NewGuid(),
            Name = "Test Scope 2",
            Description = "Test Scope Description 2",
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
                Name = "Test Scope 3",
                Description = "Test Scope Description 3",
                Owner = user1
            },
            new() {
                Id = Guid.NewGuid(),
                Name = "Test Scope 4",
                Description = "Test Scope Description 4",
                Owner = user1
            }
        });

        data.Add(new List<LorescopeModel> {
            new() {
                Name = "Test Scope Without Server Side Id",
                Description = "Test Scope Description 3",
                Owner = user1
            },
            new() {
                Name = "Test Scope Without Server Side Id 2",
                Description = "Test Scope Description 4",
                Owner = user1
            }
        });

        return data;
    }

    public static TheoryData<LorescopeModel, Func<LorescopeModel, ValueTask<LorescopeModel>>, Func<LorescopeModel, bool>> GetUpdate() {
        var data = new TheoryData<LorescopeModel, Func<LorescopeModel, ValueTask<LorescopeModel>>, Func<LorescopeModel, bool>>();

        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        data.Add(
            new LorescopeModel {
                Id = Guid.NewGuid(),
                Name = "Test Scope 5",
                Description = "Test Scope Description 5",
                Owner = user1
            },
            p2: async model => {
                model.Description = "Updated Scope Description 5";
                return await Task.FromResult(model);
            },
            p3: model => model.Description == "Updated Scope Description 5"
        );

        return data;
    }

    public static TheoryData<LorescopeModel> GetDeletes() {
        var data = new TheoryData<LorescopeModel>();

        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        data.Add(new LorescopeModel {
            Id = Guid.NewGuid(),
            Name = "Test Scope 6",
            Description = "Will be deleted",
            Owner = user1
        });

        return data;
    }
}
