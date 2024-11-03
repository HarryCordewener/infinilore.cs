// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using OneOf;

namespace InfiniLore.Server.Contracts.Types.Unions;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[GenerateOneOf]
public partial class UserIdUnion :  OneOfBase<Guid, string> {
    public bool IsGuid => IsT0;
    public bool IsString => IsT1;
    
    public Guid AsGuid => AsT0;
    public string AsString => AsT1;

    public string AsUserId => IsGuid ? AsGuid.ToString() : AsString;
}
