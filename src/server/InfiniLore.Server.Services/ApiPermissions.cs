// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.DependencyInjection;
using InfiniLore.Permissions;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.Server.Services;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableService<ApiPermissions>(ServiceLifetime.Singleton)]
[PermissionsStore(GeneratorFlags.ParsePrefix)]
public partial class ApiPermissions {
    // -----------------------------------------------------------------------------------------------------------------
    // SectionNames
    // -----------------------------------------------------------------------------------------------------------------
    private const string Data = nameof(Data);
    private const string User = nameof(User);
    private const string System = nameof(System);
    private const string Discovery = nameof(Discovery);
    
    private const string Account = nameof(Account);

    // -----------------------------------------------------------------------------------------------------------------
    // Permissions
    // -----------------------------------------------------------------------------------------------------------------
    [Prefix(Data, User)] public partial string LorescopeRead { get; } // can read data from user's lorescopes
    [Prefix(Data, User)] public partial string LorescopeWrite { get; } // can write data to user's lorescopes
    [Prefix(Data, User)] public partial string LorescopeDelete { get; } // can delete  user's lorescopes
    [Prefix(Data, User)] public partial string LorescopeManage { get; } // can manage  user's lorescopes
    [Prefix(Data, Discovery)] public partial string LorescopeDiscover { get; } // Can look up lorescopes
    [Prefix(Data, System)] public partial string LorescopesInfo { get; } // can read system info about lorescopes
}
