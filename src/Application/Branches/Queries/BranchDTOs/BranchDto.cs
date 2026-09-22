using Delex_POS.Domain.Entities.RBAC;
namespace Delex_POS.Application.Branches.Queries.BranchDTOs;
public class BranchDto
{
    public int Id { get; init; }
    public string Code { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string ContactNumber { get; init; } = string.Empty;
    public DateTimeOffset AddedOn { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Branch, BranchDto>();
        }
    }
};