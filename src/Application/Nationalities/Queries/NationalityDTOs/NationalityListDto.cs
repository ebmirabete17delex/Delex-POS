using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Nationalities.Queries.NationalityDTOs;

public class NationalityListDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Nationality, NationalityListDto>();
        }
    }
}
