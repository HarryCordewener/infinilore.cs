// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Data.Models.UserData;
using MediatR;


namespace InfiniLore.Server.Services.CQRS.Requests;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record CreateLoreScopeCommand(LoreScopeModel Model) : IRequest<Union<Success<Guid>, Error<string>>>;
