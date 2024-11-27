// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;

namespace InfiniLore.Server.Contracts.Types.Results;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UnionAliases(aliasT2: "Failure")]
public readonly partial struct BoolOrFailure() : IUnion<bool, Failure<string>> {
    public static implicit operator BoolOrFailure(string input) => new Failure<string>(input);
}
