// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models.Content.UserData;
using InfiniLore.Server.Contracts.Services.CQRS;

// ReSharper disable once CheckNamespace
namespace InfiniLore.Server.Services.CQRS.Requests.Commands;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// -------------------------------------------------------------------------/--------------------------------------------
public record CreateLorescopeCommand(
    LorescopeModel Lorescope
) : ICqrsRequest<LorescopeModel>;
