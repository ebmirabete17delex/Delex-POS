using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Roles.Queries.RoleDTOs;

public class RoleDto
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<IdentityRoleDto, RoleDto>();
        }
    }

}
