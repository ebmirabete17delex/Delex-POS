using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.CancelReasons.Queries.CancelReasonDTOs;

public class CancelReasonListDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CancelReason, CancelReasonListDto>();
        }
    }
}
