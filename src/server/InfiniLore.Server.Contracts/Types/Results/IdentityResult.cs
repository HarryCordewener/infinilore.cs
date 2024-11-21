// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Data.Models.Content.Account;

namespace InfiniLore.Server.Contracts.Types.Results;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UnionAliases("Success", "Error")]
public readonly partial struct UserIdentityResult() : IUnion<Success<InfiniLoreUser>, Error<string>> {
    public string ErrorString => TryGetAsError(out Error<string> error) ? error.Value : string.Empty;

    public static implicit operator UserIdentityResult(string input) => new Error<string>(input);
    public static implicit operator UserIdentityResult(InfiniLoreUser user) => new Success<InfiniLoreUser>(user);
}
