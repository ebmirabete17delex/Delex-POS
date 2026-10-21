using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Psgc.Commands;
using Delex_POS.Application.Psgc.Queries.GetBarangays;
using Delex_POS.Application.Psgc.Queries.GetMunicipalities;
using Delex_POS.Application.Psgc.Queries.GetProvinces;
using Delex_POS.Application.Psgc.Queries.GetRegions;

namespace Delex_POS.Application.Common.Interfaces;

public interface IPsgcService
{
    Task<IEnumerable<PsgcResponseResult>> GetRegionsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<PsgcResponseResult>> GetProvincesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<PsgcResponseResult>> GetMunicipalitiesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<PsgcResponseResult>> GetBarangaysAsync(CancellationToken cancellationToken);
    //Task<Result> UpdateRegionsAsync(CancellationToken cancellationToken);
    //Task<Result> UpdateProvincesAsync(CancellationToken cancellationToken);
    //Task<Result> UpdateMunicipalitiesAsync(CancellationToken cancellationToken);
    //Task<Result> UpdateBarangaysAsync(CancellationToken cancellationToken);
}
