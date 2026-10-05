using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.CustomerTypes.Queries.CustomerTypeDTOs;
public class CustomerTypeDto
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset Created { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CustomerType, CustomerTypeDto>();
        }
    }
};
