using Delex_POS.Domain.Entities;
namespace Delex_POS.Application.AccessClaims.Queries.AccessClaimDTOs;

public class AccessPermissionDto
{
    public int Id { get; set; }
    public string Feature { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AccessClaim, AccessPermissionDto>()
                .ForMember(dest => dest.Feature, opt => opt.MapFrom(src => src.Feature));
            CreateMap<Permission, AccessPermissionDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
