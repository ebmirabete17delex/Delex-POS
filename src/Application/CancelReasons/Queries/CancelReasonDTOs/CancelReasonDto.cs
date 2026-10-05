using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.CancelReasons.Queries.CancelReasonDTOs;
public class CancelReasonDto
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsAllowUserToEnterMemo { get; set; }
    public string? Memo { get; set; }
    public DateTimeOffset Created { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CancelReason, CancelReasonDto>();
        }
    }
};
