// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OneOf;
using OneOf.Types;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.Server.Contracts.Types.Results;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[GenerateOneOf]
public partial class CommandResult<T> : OneOfBase<Success<EntityEntry<T>>, Error<string>> where T : class {
    public bool IsSuccess => IsT0;
    public bool IsError => IsT1;

    public Success<EntityEntry<T>> AsSuccess => AsT0;
    public Error<string> AsError => AsT1;
    public string ErrorString => TryPickT1(out Error<string> error, out _) ? error.Value : string.Empty;
    
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
