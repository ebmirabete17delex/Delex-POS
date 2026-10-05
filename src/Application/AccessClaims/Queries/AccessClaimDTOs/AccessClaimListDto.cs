using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.AccessClaims.Queries.AccessClaimDTOs;

public class AccessClaimListDto
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AccessClaim, AccessClaimListDto>();
        }
    }
}
