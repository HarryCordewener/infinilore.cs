// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using AterraEngine.Unions;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.Server.Contracts.Types.Results;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UnionAliases("Success", null, "Failure")]
public readonly partial struct QueryResultMany<T>() : IUnion<Success<T[]>, None, Failure<string>> {
    public string ErrorString => TryGetAsFailure(out Failure<string> error) ? error.Value : string.Empty;
    public bool TryGetSuccessValue([NotNullWhen(true)] out T[]? value) {
        value = default;
        if (!IsSuccess) return false;

        value = AsSuccess.Value;
        return true;
    }

    public static implicit operator QueryResultMany<T>(string input) => new Failure<string>(input);
    public static implicit operator QueryResultMany<T>(T[] items) => new Success<T[]>(items);
}
