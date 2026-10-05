using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.GenericItems.Queries.GenericDTOs;

public class GenericsListDto
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Generic, GenericsListDto>();
        }
    }

}
