// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using MediatR;

namespace InfiniLore.Server.Services.CQRS;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> {

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
        // Limit the Response to be an extesnions specific OneOff
        // Depending on the request type, we could also check if it needs to be authorized
        // For now, we'll just pass to make this work.
        return next();
    }
}
