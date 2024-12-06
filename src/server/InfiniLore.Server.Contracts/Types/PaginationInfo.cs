// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;

namespace InfiniLore.Server.Contracts.Types;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public readonly record struct PaginationInfo(int PageNumber, int PageSize) {
    public int SkipAmount => (PageNumber - 1) * PageSize;

    public bool IsValid(out Failure<string> error) {
        if (PageNumber < 1) {
            error = new Failure<string>("Page number must be greater than 0.");
            return false;
        }

        if (PageSize < 1) {
            error = new Failure<string>("Page size must be greater than 0.");
            return false;
        }

        error = new Failure<string>();
        return true;
    }

    public bool IsNotValid(out Failure<string> error) => !IsValid(out error);
}
