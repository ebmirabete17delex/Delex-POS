using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Psgc.Queries.GetProvinces;

public class ProvinceDto
{
    public string Code { get; init; } = string.Empty;
    public string AreaName { get; init; } = string.Empty;
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PsgcProvince, ProvinceDto>();
        }
    }
}
