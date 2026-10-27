using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Roles.Queries.RoleDTOs;

public class RoleAccessDto
{
    public int Id { get; init; } 
    public string RoleId { get; init; } = string.Empty;
    public int AccessId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Feature { get; init; } = string.Empty;
    public bool IsActive { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RoleAccess, RoleAccessDto>();
        }
    }
}
