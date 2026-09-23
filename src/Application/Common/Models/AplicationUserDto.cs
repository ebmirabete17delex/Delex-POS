namespace Delex_POS.Application.Common.Models;

public class ApplicationUserDto
{
    public string Id { get; init; } = string.Empty;
    public string UserCode { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string NormalizedName { get; init; } = string.Empty;
    public string LastName { get;  init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string? MiddleName { get; init; }
    public int BranchId { get; init; }
    public string Email { get; init; } = string.Empty;

    public ApplicationUserDto(string userCode, 
                            int branchId, 
                            string lastName, 
                            string firstName, 
                            string? middleName, 
                            string email)
    {
        UserCode = userCode;
        BranchId = branchId;
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        Email = email;
    }
}
