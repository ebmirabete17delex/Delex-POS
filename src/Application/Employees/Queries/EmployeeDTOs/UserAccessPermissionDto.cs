namespace Delex_POS.Application.Employees.Queries.EmployeeDTOs;

public class UserAccessPermissionDto
{
    public int AccessId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Feature { get; set; } = string.Empty;
    public List<UserPermissionDto> Permissions { get; set; } = [];
}
