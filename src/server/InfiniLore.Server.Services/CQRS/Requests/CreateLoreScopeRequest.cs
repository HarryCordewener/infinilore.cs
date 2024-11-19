// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.UserData;
using MediatR;
using OneOf;
using OneOf.Types;

namespace InfiniLore.Server.Services.CQRS.Requests;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record CreateLoreScopeCommand(LoreScopeModel Model) : IRequest<OneOf<Success<Guid>, Error<string>>>;
