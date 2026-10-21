using Delex_POS.Application.Psgc.Queries.GetRegions;
using Delex_POS.Application.Psgc.Queries.GetProvinces;
using Delex_POS.Application.Psgc.Queries.GetMunicipalities;
using Delex_POS.Application.Psgc.Queries.GetBarangays;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Delex_POS.Web.Endpoints;

public class Psgc : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();

        groupBuilder.MapGet(GetRegions,"regions");
        groupBuilder.MapGet(GetProvinces, "provinces/{regionCode}");
        groupBuilder.MapGet(GetMunicipalities, "municipalities/{provinceCode}");
        groupBuilder.MapGet(GetBarangays, "barangays/{municipalityCode}");
    }

    [EndpointSummary("Get all regions")]
    [EndpointDescription("Retrieves all regions.")]
    public static async Task<Ok<List<RegionDto>>> GetRegions(ISender sender, [AsParameters] GetRegionsQuery query)
    {
        var vm = await sender.Send(query);

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get provinces")]
    [EndpointDescription("Retrieves all provinces based on provided region code.")]
    public static async Task<Ok<List<ProvinceDto>>> GetProvinces(ISender sender, string regionCode)
    {
        var vm = await sender.Send(new GetProvincesQuery() { RegionCode = regionCode });

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get municipalities")]
    [EndpointDescription("Retrieves all provinces based on provided region code.")]
    public static async Task<Ok<List<MunicipalityDto>>> GetMunicipalities(ISender sender, string provinceCode)
    {
        var vm = await sender.Send(new GetMunicipalitiesQuery() { ProvinceCode = provinceCode });

        return TypedResults.Ok(vm);
    }

    [EndpointSummary("Get barangays")]
    [EndpointDescription("Retrieves all provinces based on provided region code.")]
    public static async Task<Ok<List<BarangayDto>>> GetBarangays(ISender sender, string municipalityCode)
    {
        var vm = await sender.Send(new GetBarangaysQuery() { MunicipalityCode = municipalityCode });

        return TypedResults.Ok(vm);
    }


}
