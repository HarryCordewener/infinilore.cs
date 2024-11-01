// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using OneOf;
using OneOf.Types;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLoreLib.Results;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[GenerateOneOf]
public partial class CommandOutput : OneOfBase<Success, Error<string>> {
    public bool IsSuccess => IsT0;
    public bool IsError => IsT1;
    
    public static implicit operator CommandOutput(string input) => new Error<string>(input);

}

[GenerateOneOf]
public partial class QueryOutput<T> : OneOfBase<Success<T>, None, Error<string>> {
    public bool IsSuccess => IsT0;
    public bool IsNone => IsT1;
    public bool IsError => IsT2;

    public Success<T> AsSuccess => AsT0;
    public None AsNone => AsT1;
    public Error<string> AsError => AsT2;
    
    public bool TryGetSuccessValue([NotNullWhen(true)] out T? value) {
        value = default;
        if (!IsSuccess) return false;
        value = AsSuccess.Value;
        return true;
    }
    
    public static implicit operator QueryOutput<T>(string input) => new Error<string>(input);
    public static implicit operator QueryOutput<T>(T item) => new Success<T>(item);
}

[GenerateOneOf]
public partial class QueryOutputMany<T> : OneOfBase<Success<T[]>, None, Error<string>> {
    public bool IsSuccess => IsT0;
    public bool IsNone => IsT1;
    public bool IsError => IsT2;

    public Success<T[]> AsSuccess => AsT0;
    public None AsNone => AsT1;
    public Error<string> AsError => AsT2;
    
    public bool TryGetSuccessValue([NotNullWhen(true)] out T[]? value) {
        value = default;
        if (!IsSuccess) return false;
        value = AsSuccess.Value;
        return true;
    }
    
    public static implicit operator QueryOutputMany<T>(string input) => new Error<string>(input);
    public static implicit operator QueryOutputMany<T>(T[] items) => new Success<T[]>(items);
}


