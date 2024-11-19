// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;

namespace InfiniLore.Server.Contracts.Data;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public readonly record struct PaginationInfo(int PageNumber, int PageSize) {
    public int SkipAmount => (PageNumber - 1) * PageSize;

    public bool IsValid(out Error<string> error) {
        if (PageNumber < 1) {
            error = new Error<string>("Page number must be greater than 0.");
            return false;
        }

        if (PageSize < 1) {
            error = new Error<string>("Page size must be greater than 0.");
            return false;
        }

        error = new Error<string>();
        return true;
    }
    public bool IsNotValid(out Error<string> error) => !IsValid(out error);
}
