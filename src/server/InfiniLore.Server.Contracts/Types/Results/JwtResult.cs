// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;

namespace InfiniLore.Server.Contracts.Types.Results;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UnionAliases(aliasT1: "Error")]
public readonly partial struct JwtResult() : IUnion<JwtTokenData, Error<string>> {
    public static implicit operator JwtResult(string input) => new Error<string>(input);
}
