namespace Delex_POS.Application.Employees.Queries.EmployeeDTOs;

public class UserPermissionDto
{
    public int PermisionId { get; set; }
    public string Feature { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int UserPermissionId { get; set; }
}
