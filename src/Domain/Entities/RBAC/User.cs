namespace Delex_POS.Domain.Entities.RBAC;
public class User : BaseAuditableEntity
{
    private readonly List<UserRole> _userRoles = [];
    public IReadOnlyCollection<UserRole> UserRoles =>
        _userRoles.AsReadOnly();
    private readonly List<UserAccess> _userAccesses = [];
    public IReadOnlyCollection<UserAccess> UserAccesses =>
        _userAccesses.AsReadOnly();

    public string UserCode { get; init; }
    public string Email { get; init; }
    public int BranchId { get; private set; }
    public string LastName { get; private set; }
    public string FirstName { get; private set; }
    public string? MiddleName { get; private set; }

    public User(int branchId, string lastName, string firstName, string? middleName, string email, string userCode)
    {
        BranchId = branchId;
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName ?? string.Empty;
        Email = email;
        UserCode = userCode;
    }

    public void Update(string lastName, string firstName, string? middleName)
    {
        LastName = lastName;
        MiddleName = middleName ?? string.Empty;
        FirstName = firstName;
    }
    public void ChangeBranch(int branchId)
    {
        BranchId = branchId;
    }

    public void AddAccess(AccessClaim access)
    {
        _userAccesses.Add(new UserAccess(this.Id, access.Id));
    }

    public void AddRole(Role role)
    {
        _userRoles.Add(new UserRole(this.Id, role.Id));
    }
}