using Microsoft.AspNetCore.Identity;
using Delex_POS.Domain.Entities.RBAC;
namespace Delex_POS.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string UserCode { get; private set; } = string.Empty;
    public int BranchId { get; private set; }
    public string LastName { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string? MiddleName { get; private set; } = string.Empty;

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
