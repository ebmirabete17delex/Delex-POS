namespace Delex_POS.Application.Roles.Queries.RoleDTOs;

public class RolePermissionDto
{
    public int PermissionId { get; set; }
    public string Feature { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int? RolePermissionId { get; set; }
}
