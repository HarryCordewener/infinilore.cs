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
public partial class JwtResult : OneOfBase<Success<JwtTokenData>, Error<string>> {
    public bool IsSuccess => IsT0;
    public bool IsError => IsT1;

    public Success<JwtTokenData> AsSuccess => AsT0;
    public Error<string> AsError => AsT1;
    public string ErrorString => TryPickT1(out Error<string> error, out _) ? error.Value : string.Empty;

    public static implicit operator JwtResult(string input) => new Error<string>(input);
    public static implicit operator JwtResult(JwtTokenData jwtToken) => new Success<JwtTokenData>(jwtToken);
}
