namespace Delex_POS.Application.AccessClaims.Queries.AccessClaimDTOs;
public class AccessClaimDto
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Feature { get; set; } = string.Empty;
    public string BackendUrl { get; set; } = string.Empty;
    public string FrontendUrl { get; set; } = string.Empty;
    public DateTimeOffset AddedOn { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Entities.RBAC.AccessClaim, AccessClaimDto>();
        }
    }
};