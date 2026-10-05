using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.GenericItems.Queries.GenericDTOs;
public class GenericDto
{
    public int Id { get; init; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Memo { get; set; }
    public DateTimeOffset Created { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Generic, GenericDto>();
        }
    }
};
