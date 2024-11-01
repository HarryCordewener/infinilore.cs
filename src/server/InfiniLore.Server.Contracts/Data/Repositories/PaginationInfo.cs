// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.Server.Contracts.Data.Repositories;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public readonly record struct PaginationInfo(int PageNumber, int PageSize) {
    public int SkipAmount => (PageNumber - 1) * PageSize;
    
    public bool IsValid() => PageNumber > 0 && PageSize > 0;
    public bool IsNotValid() => !IsValid();
}
