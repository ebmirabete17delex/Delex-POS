using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Roles.Queries.RoleDTOs;

public class RoleAccessDto
{
    public int Id { get; init; } 
    public string RoleId { get; init; } = string.Empty;
    public int AccessId { get; init; }
    public string Name { get; init; } = string.Empty;
    public bool HasRead { get; init; }
    public bool HasWrite { get; init; }
    public bool HasDelete { get; init; }
    public bool HasUpdate { get; init; }
    public bool IsActive { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RoleAccess, RoleAccessDto>();
        }
    }
}
