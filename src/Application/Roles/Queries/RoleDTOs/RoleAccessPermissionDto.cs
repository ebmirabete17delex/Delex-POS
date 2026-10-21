namespace Delex_POS.Application.Roles.Queries.RoleDTOs;

public class RoleAccessPermissionDto
{
    public int AccessId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Feature { get; set; } = string.Empty;
    public List<RolePermissionDto> Permissions { get; set; } = []; 
}
