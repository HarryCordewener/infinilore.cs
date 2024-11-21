// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Content.UserData;
using MediatR;

namespace InfiniLore.Server.Services.CQRS.Requests;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class GetLoreScopesQuery(
    Dictionary<UserIdUnion, Guid[]> LoreScopesByUser
) : IRequest<SuccessOrFailure<LoreScopeModel, string>> {
    public bool IsEmpty => LoreScopesByUser.Count == 0;
    
    public static GetLoreScopesQuery FromOneLoreScope(UserIdUnion userId, Guid loreScope) => new(
        new Dictionary<UserIdUnion, Guid[]> {
            [userId] = [loreScope]
        }
    );
}