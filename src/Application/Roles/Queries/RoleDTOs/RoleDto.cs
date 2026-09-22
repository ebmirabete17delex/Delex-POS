using Delex_POS.Domain.Entities.RBAC;
namespace Delex_POS.Application.Roles.Queries.RoleDTOs;
public class RoleDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTimeOffset AddedOn { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Role, RoleDto>();
        }
    }
};