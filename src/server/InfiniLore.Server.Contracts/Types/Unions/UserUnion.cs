// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Account;
using OneOf;

namespace InfiniLore.Server.Contracts.Types.Unions;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[GenerateOneOf]
public partial class UserUnion : OneOfBase<InfiniLoreUser, Guid, string> {
    public bool IsInfiniLoreUser => IsT0;
    public bool IsGuid => IsT1;
    public bool IsString => IsT2;

    public InfiniLoreUser AsInfiniLoreUser => AsT0;
    public Guid AsGuid => AsT1;
    public string AsString => AsT2;

    public string AsUserId {
        get {
            if (IsInfiniLoreUser) return AsInfiniLoreUser.Id;
            if (IsGuid) return AsGuid.ToString();

            return AsString;
        }
    }
}
