// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Database.Models.Content.Account;

namespace InfiniLore.Server.Contracts.Types;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public readonly partial struct UserIdUnion() : IUnion<InfiniLoreUser, Guid, string> {
    public Guid ToGuid() => this switch {
        { IsInfiniLoreUser: true, AsInfiniLoreUser.Id: var guid } => guid,
        { IsGuid: true, AsGuid: var guid } => guid,
        { IsString : true, AsString: var stringValue } => Guid.Parse(stringValue),
        _ => throw new ArgumentException("Union does not contain a value")
    };
}
