// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.DependencyInjection;
using AterraEngine.Unions;
using FastEndpoints;
using InfiniLore.Server.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.Server.Services;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableService<IMediatorOutputService>(ServiceLifetime.Scoped)]
public class MediatorOutputService : IMediatorOutputService {

    public Results<Ok<TResponse>, BadRequest<ProblemDetails>> ToHttpResults<TResponse, TModel>(
        SuccessOrFailure<TModel> successOrFailure,
        Func<TModel, TResponse> mapper
    ) {
        if (successOrFailure.TryGetAsFailureValue(out string? failure)) {
            return TypedResults.BadRequest(new ProblemDetails {
                Detail = failure
            });
        }

        return TypedResults.Ok(mapper(successOrFailure.AsSuccess.Value));
    }

    public Results<Ok<TResponse>, BadRequest<ProblemDetails>> ToBadRequest<TResponse>(string? failure = null) =>
        TypedResults.BadRequest(new ProblemDetails {
            Detail = failure
        });
}
