// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using OneOf;
using OneOf.Types;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.Server.Contracts.Types.Results;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[GenerateOneOf]
public partial class QueryResult<T> : OneOfBase<Success<T>, None, Error<string>> {
    public bool IsSuccess => IsT0;
    public bool IsNone => IsT1;
    public bool IsError => IsT2;

    public Success<T> AsSuccess => AsT0;
    public None AsNone => AsT1;
    public Error<string> AsError => AsT2;
    public string ErrorString => TryPickT2(out Error<string> error, out _) ? error.Value : string.Empty;

    public bool TryGetSuccessValue([NotNullWhen(true)] out T? value) {
        value = default;
        if (!IsSuccess) return false;

        value = AsSuccess.Value;

        // Solves a warning during building
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        return value is not null;
    }

    public static implicit operator QueryResult<T>(string input) => new Error<string>(input);
    public static implicit operator QueryResult<T>(T item) => new Success<T>(item);
}
