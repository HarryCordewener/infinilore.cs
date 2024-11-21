// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Data.Models.Content.UserData;
using MediatR;

namespace InfiniLore.Server.Services.CQRS.Handlers.Commands;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record CreateLoreScopeCommand(LoreScopeModel Model) : IRequest<Union<Success<Guid>, Error<string>>>;
