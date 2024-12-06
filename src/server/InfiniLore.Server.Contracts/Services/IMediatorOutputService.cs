// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InfiniLore.Server.Contracts.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMediatorOutputService {
    public Results<Ok<TResponse>, BadRequest<ProblemDetails>> ToHttpResults<TResponse, TModel>(
        SuccessOrFailure<TModel> successOrFailure,
        Func<TModel, TResponse> mapper
    );
}
