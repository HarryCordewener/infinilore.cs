// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;

namespace InfiniLore.Server.Contracts.Types.Results;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UnionAliases("Success", "Failure")]
public readonly partial struct CommandOutput() : IUnion<Success, Failure<string>> {
    public string FailureString => TryGetAsFailure(out Failure<string> error) ? error.Value : string.Empty;
    public static implicit operator CommandOutput(string input) => new Failure<string>(input);
}
