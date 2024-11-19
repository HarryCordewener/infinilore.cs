// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;


namespace InfiniLore.Server.Contracts.Types.Results;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UnionAliases(aliasT2:"Error")]
public readonly partial struct TrueFalseOrError() : IUnion<True, False, Error<string>> {
    public string ErrorString => TryGetAsError(out Error<string> error) ? error.Value : string.Empty;

    public static implicit operator TrueFalseOrError(string input) => new Error<string>(input);
    public static implicit operator TrueFalseOrError(bool input) => input ? new True() : new False();
}
