using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Nationalities.Queries.NationalityDTOs;

public class NationalityDto
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string? LocalName { get; set; } = string.Empty;

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Nationality, NationalityDto>();
        }
    }
}
