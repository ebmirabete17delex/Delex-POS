using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Branches.Queries.BranchDTOs;

public class BranchListDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Branch, BranchListDto>();
        }
    }
}
