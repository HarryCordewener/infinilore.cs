// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;

namespace InfiniLore.Server.Contracts.Types.Unions;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public readonly partial struct UserIdUnion() : IUnion<Guid, string> {
     public string AsUserId {
          get {
               if (IsGuid) return AsGuid.ToString();
               if (IsString) return AsString;
               throw new ArgumentException("Union does not contain a value");
          }
     }
}
