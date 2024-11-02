// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using OneOf;
using OneOf.Types;

namespace InfiniLore.Server.Contracts.Types.Results;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[GenerateOneOf]
public partial class TrueFalseOrError : OneOfBase<True, False, Error<string>> {
    public bool IsTrue => IsT0;
    public bool IsFalse => IsT1;
    public bool IsError => IsT2;

    public True AsTrue => AsT0;
    public False AsFalse => AsT1;
    public Error<string> AsError => AsT2;
    public string ErrorString => TryPickT2(out Error<string> error, out _) ? error.Value : string.Empty;

    public static implicit operator TrueFalseOrError(string input) => new Error<string>(input);
    public static implicit operator TrueFalseOrError(bool input) => input ? new True() : new False();
}
