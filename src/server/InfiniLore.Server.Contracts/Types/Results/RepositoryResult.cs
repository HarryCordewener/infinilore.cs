// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.Server.Contracts.Types.Results;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UnionAliases("Success", "Failure")]
public readonly partial struct RepoResult() : IUnion<Success, Failure<string>> {
    public string FailureString => TryGetAsFailure(out Failure<string> failure) ? failure.Value : string.Empty;

    public static implicit operator RepoResult(string input) => new Failure<string>(input);
    public static implicit operator RepoResult(bool value) => value ? new Success() : new Failure<string>();
}

[UnionAliases("Success", "Failure")]
public readonly partial struct RepoResult<T>() : IUnion<Success<T>, Failure<string>> where T : class {
    public string FailureString => TryGetAsFailure(out Failure<string> failure) ? failure.Value : string.Empty;
    public bool TryGetSuccessValue([NotNullWhen(true)] out T? value) {
        if (IsSuccess) {
            value = AsSuccess.Value;
            return true;
        }

        value = default;
        return false;
    }

    public static implicit operator RepoResult<T>(string input) => new Failure<string>(input);
    public static implicit operator RepoResult<T>(T value) => new Success<T>(value);
    public static implicit operator RepoResult<T>(EntityEntry<T> value) => new Success<T>(value.Entity);

    public SuccessOrFailure<T> ToSuccessOrFailure() {
        if (IsSuccess) return AsSuccess;
        return AsFailure;
    }
}
