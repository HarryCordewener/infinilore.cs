// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Account;
using OneOf;
using OneOf.Types;

namespace InfiniLore.Server.Contracts.Types.Results;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[GenerateOneOf]
public partial class UserIdentityResult : OneOfBase<Success<InfiniLoreUser>, Error<string>> {
    public bool IsSuccess => IsT0;
    public bool IsError => IsT1;

    public Success<InfiniLoreUser> AsSuccess => AsT0;
    public Error<string> AsError => AsT1;
    public string ErrorString => TryPickT1(out Error<string> error, out _) ? error.Value : string.Empty;

    public static implicit operator UserIdentityResult(string input) => new Error<string>(input);
    public static implicit operator UserIdentityResult(InfiniLoreUser user) => new Success<InfiniLoreUser>(user);
}
