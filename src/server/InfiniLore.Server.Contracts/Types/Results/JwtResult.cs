// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;

namespace InfiniLore.Server.Contracts.Types.Results;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UnionAliases("Success", "Error")]
public readonly partial struct JwtResult() : IUnion<Success<JwtTokenData>, Error<string>> {
    public string ErrorString => TryGetAsError(out Error<string> error) ? error.Value : string.Empty;

    public static implicit operator JwtResult(string input) => new Error<string>(input);
    public static implicit operator JwtResult(JwtTokenData jwtToken) => new Success<JwtTokenData>(jwtToken);
}
