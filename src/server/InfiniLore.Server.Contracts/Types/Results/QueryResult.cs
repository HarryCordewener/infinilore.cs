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
public readonly partial struct QueryResult<T>() : IUnion<Success<T>, None, Failure<string>> {
    public string FailureString => TryGetAsFailure(out Failure<string> failure) ? failure.Value : string.Empty;

    public bool TryGetSuccessValue([NotNullWhen(true)] out T? value) {
        value = default;
        if (!IsSuccess) return false;

        value = AsSuccess.Value;
        return value is not null;
    }

    public static implicit operator QueryResult<T>(string input) => new Failure<string>(input);
    public static implicit operator QueryResult<T>(T item) => new Success<T>(item);
}
