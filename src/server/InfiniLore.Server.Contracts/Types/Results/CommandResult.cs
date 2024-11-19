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
[UnionAliases("Success", "Error")]
public readonly partial struct CommandResult<T>() : IUnion<Success<EntityEntry<T>>, Error<string>> where T : class {
    public string ErrorString => TryGetAsError(out Error<string> error) ? error.Value : string.Empty;
    public bool TryGetSuccessValue([NotNullWhen(true)] out EntityEntry<T>? value) {
        value = default;
        if (!IsSuccess) return false;

        value = AsSuccess.Value;

        // Solves a warning during building
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        return value is not null;
    }

    public static implicit operator CommandResult<T>(string input) => new Error<string>(input);
    public static implicit operator CommandResult<T>(EntityEntry<T> value) => new Success<EntityEntry<T>>(value);
}
