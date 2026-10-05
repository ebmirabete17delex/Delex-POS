using Microsoft.AspNetCore.Identity;
using Delex_POS.Domain.Entities;
namespace Delex_POS.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string UserCode { get; set; } = string.Empty;
    public int BranchId { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; } = string.Empty;

    public void Update(string userCode, string lastName, string firstName, string? middleName)
    {
        UserCode = userCode;
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
    }

    public void SetBranch(int branchId)
    {
        BranchId = branchId;
    }
}
