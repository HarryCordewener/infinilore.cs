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
public readonly partial struct CommandResult<T>() : IUnion<Success<EntityEntry<T>>, Failure<string>> where T : class {
    public string FailureString => TryGetAsFailure(out Failure<string> failure) ? failure.Value : string.Empty;
    public bool TryGetSuccessValue([NotNullWhen(true)] out EntityEntry<T>? value) {
        value = default;
        if (!IsSuccess) return false;

        value = AsSuccess.Value;

        // Solves a warning during building
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        return value is not null;
    }

    public static implicit operator CommandResult<T>(string input) => new Failure<string>(input);
    public static implicit operator CommandResult<T>(EntityEntry<T> value) => new Success<EntityEntry<T>>(value);
}
