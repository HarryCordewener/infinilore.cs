// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Data.Models.Content.Account;

namespace InfiniLore.Server.Contracts.Types.Unions;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public readonly partial struct UserUnion() : IUnion<InfiniLoreUser, Guid, string> {
    public string AsUserId {
        get {
            if (IsInfiniLoreUser) return AsInfiniLoreUser.Id;
            if (IsGuid) return AsGuid.ToString();
            if (IsString) return AsString;

            throw new ArgumentException("Union does not contain a value");
        }
    }
}
