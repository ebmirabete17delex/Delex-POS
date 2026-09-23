namespace Delex_POS.Application.Roles.Queries.RoleDTOs;

public class RoleAccessDto
{
    public int Id { get; init; } 
    public string RoleId { get; init; } = string.Empty;
    public int AccessId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Feature { get; init; } = string.Empty;
    public string BackendUrl { get; init; } = string.Empty;
    public string FrontendUrl { get; init; } = string.Empty;
}
