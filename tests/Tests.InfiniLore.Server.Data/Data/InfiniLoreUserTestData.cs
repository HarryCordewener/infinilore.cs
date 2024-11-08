// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Account;
using InfiniLore.Server.Data.Models.UserData;

namespace Tests.InfiniLore.Server.Data.Data;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class InfiniLoreUserCommandTestData {
    public static InfiniLoreUser GetUser1() {
        return new InfiniLoreUser {
            UserName = "User1",
            Email = "user1@example.com"
        };
    }
    
    public static TheoryData<InfiniLoreUser> GetInfiniLoreUsers() {
        var data = new TheoryData<InfiniLoreUser> {
            // User with no scopes, multiverses, universes, or refresh tokens
            GetUser1()
        };

        // User with some scopes
        var user2 = new InfiniLoreUser {
            UserName = "User2",
            Email = "user2@example.com",
        };
        user2.LoreScopes.Add(new LoreScopeModel { 
            Id = Guid.NewGuid(), 
            Name = "Scope1", 
            Description = "Description1", 
            Owner = user2
        });
        user2.LoreScopes.Add(new LoreScopeModel { 
            Id = Guid.NewGuid(), 
            Name = "Scope2", 
            Description = "Description2", 
            Owner = user2
        });
        data.Add(user2);

        // User with some multiverses and universes
        var user3 = new InfiniLoreUser { 
            UserName = "User3",
            Email = "user3@example.com",
        };
        var user3LoreScope = new LoreScopeModel { 
            Id = Guid.NewGuid(),
            Name = "Scope1",
            Description = "Description1",
            Owner = user3
        };
        var user3Multiverse = new MultiverseModel { 
            Id = Guid.NewGuid(), 
            Name = "Multiverse1", 
            Description = "Description1", 
            LoreScope = user3LoreScope,
            Owner = user3
        };
        var user3Universe = new UniverseModel { 
            Id = Guid.NewGuid(), 
            Name = "Universe1", 
            Description = "Description1", 
            Multiverse = user3Multiverse,
            Owner = user3
        };
                
        user3.LoreScopes.Add(user3LoreScope);
        user3.Multiverses.Add(user3Multiverse);
        user3.Universes.Add(user3Universe); 
        data.Add(user3);

        return data;
    }
}