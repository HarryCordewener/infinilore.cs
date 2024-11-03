// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Account;
using InfiniLore.Server.Data.Models.UserData;

namespace Tests.InfiniLore.Server.Data.Data.Commands;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class LoreScopeCommandTestData {
    public static TheoryData<LoreScopeModel> GetSingleModels() {
        var data = new TheoryData<LoreScopeModel>();

        // Ensure GetUser1 method exists in InfiniLoreUserTestData
        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        data.Add(new LoreScopeModel {
            Id = Guid.NewGuid(),
            Name = "Test Scope 1",
            Description = "Test Scope Description 1",
            Owner = user1
        });

        data.Add(new LoreScopeModel {
            Id = Guid.NewGuid(),
            Name = "Test Scope 2",
            Description = "Test Scope Description 2",
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
        
        data.Add(new List<LoreScopeModel> {
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

    public static TheoryData<LoreScopeModel, Func<LoreScopeModel, ValueTask<LoreScopeModel>>, Func<LoreScopeModel, bool>> GetUpdate() {
        var data = new TheoryData<LoreScopeModel, Func<LoreScopeModel, ValueTask<LoreScopeModel>>, Func<LoreScopeModel, bool>>();
        
        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        data.Add(
            new LoreScopeModel {
                Id = Guid.NewGuid(),
                Name = "Test Scope 5",
                Description = "Test Scope Description 5",
                Owner = user1
            },
            async model =>
            {
                model.Description = "Updated Scope Description 5";
                return await Task.FromResult(model);
            },
            model => model.Description == "Updated Scope Description 5"
        );

        return data;
    }

    public static TheoryData<LoreScopeModel> GetDeletes() {
        var data = new TheoryData<LoreScopeModel>();

        InfiniLoreUser user1 = InfiniLoreUserCommandTestData.GetUser1();

        data.Add(new LoreScopeModel {
            Id = Guid.NewGuid(),
            Name = "Test Scope 6",
            Description = "Will be deleted",
            Owner = user1
        });

        return data;
    }
}