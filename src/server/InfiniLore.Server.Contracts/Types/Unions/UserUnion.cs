// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Data.Models.Content.Account;

namespace InfiniLore.Server.Contracts.Types.Unions;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public readonly partial struct UserIdUnion() : IUnion<InfiniLoreUser, Guid, string> {
    public Guid ToGuid() {
        if (IsInfiniLoreUser) return AsInfiniLoreUser.Id;
        if (IsGuid) return AsGuid;
        if (IsString) return Guid.Parse(AsString);

        throw new ArgumentException("Union does not contain a value");
    }
}
