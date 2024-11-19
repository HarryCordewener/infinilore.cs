// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;

using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.Server.Contracts.Types.Results;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UnionAliases("Success", null, "Error")]
public readonly partial struct QueryResult<T>() : IUnion<Success<T>, None, Error<string>> {
    public string ErrorString => TryGetAsError(out Error<string> error) ? error.Value : string.Empty;

    public bool TryGetSuccessValue([NotNullWhen(true)] out T? value) {
        value = default;
        if (!IsSuccess) return false;

        value = AsSuccess.Value;
        return value is not null;
    }

    public static implicit operator QueryResult<T>(string input) => new Error<string>(input);
    public static implicit operator QueryResult<T>(T item) => new Success<T>(item);
}
