// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Database.Models.Content.Account;

namespace InfiniLore.Server.Contracts.Types.Unions;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public readonly partial struct UserIdUnion() : IUnion<InfiniLoreUser, Guid, string> {
    public Guid ToGuid() {
        switch (this) {
            case { IsInfiniLoreUser: true, AsInfiniLoreUser.Id: var guid }: return guid;
            case { IsGuid: true, AsGuid: var guid }: return guid;
            case { IsString : true, AsString: var stringValue }: return Guid.Parse(stringValue);
            default: throw new ArgumentException("Union does not contain a value");
        }
    }
}
