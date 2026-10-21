using AutoMapper;
using Delex_POS.Application.Common.Exceptions;
using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Psgc.Commands;
using Delex_POS.Domain.Constants;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Linq; // add this

namespace Delex_POS.Infrastructure.Psgc;

public sealed class PsgcService : IPsgcService
{
    private readonly IMapper _mapper;
    private readonly HttpClient _client;
    private readonly ILogger<PsgcService> _logger;
    private readonly IAuditLogService _auditLogService;
    public PsgcService(              // make constructor public
        IMapper mapper,
        HttpClient client,
        ILogger<PsgcService> logger,
        IAuditLogService auditLogService        )
    {
        _mapper = mapper;
        _client = client;
        _logger = logger;
        _auditLogService = auditLogService;
    }

    public async Task<IEnumerable<PsgcResponseResult>> GetRegionsAsync(CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"{PSGC.PsgcBaseUrl}/{PSGC.PsgcVersion_Q2_2024}/regions?token={PSGC.PsgcToken}");
        var contentString = await response.Content.ReadAsStringAsync(cancellationToken);
        List<PsgcResponseResult> results;
        if (response.IsSuccessStatusCode)
        {
            var responseObject = JsonSerializer.Deserialize<PsgcResponse>(contentString);
            // handle possible nulls and convert to List<T>
            results = responseObject?.Results?.ToList() ?? [];
        }
        else
        {
            _logger.LogError("{PsgcError}", contentString);
            throw new IntegrationException(nameof(PsgcService), contentString);
        }
        return results; // fixed variable name
    }
    public async Task<IEnumerable<PsgcResponseResult>> GetProvincesAsync(CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"{PSGC.PsgcBaseUrl}/{PSGC.PsgcVersion_Q2_2024}/provinces?token={PSGC.PsgcToken}");
        var contentString = await response.Content.ReadAsStringAsync(cancellationToken);
        List<PsgcResponseResult> results;
        if (response.IsSuccessStatusCode)
        {
            var responseObject = JsonSerializer.Deserialize<PsgcResponse>(contentString);
            // handle possible nulls and convert to List<T>
            results = responseObject?.Results?.ToList() ?? [];
        }
        else
        {
            _logger.LogError("{PsgcError}", contentString);
            throw new IntegrationException(nameof(PsgcService), contentString);
        }
        return results; // fixed variable name
    }
    public async Task<IEnumerable<PsgcResponseResult>> GetMunicipalitiesAsync(CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"{PSGC.PsgcBaseUrl}/{PSGC.PsgcVersion_Q2_2024}/municipalities?token={PSGC.PsgcToken}");
        var contentString = await response.Content.ReadAsStringAsync(cancellationToken);
        List<PsgcResponseResult> results;
        if (response.IsSuccessStatusCode)
        {
            var responseObject = JsonSerializer.Deserialize<PsgcResponse>(contentString);
            // handle possible nulls and convert to List<T>
            results = responseObject?.Results?.ToList() ?? [];
        }
        else
        {
            _logger.LogError("{PsgcError}", contentString);
            throw new IntegrationException(nameof(PsgcService), contentString);
        }
        return results; // fixed variable name
    }
    public async Task<IEnumerable<PsgcResponseResult>> GetBarangaysAsync(CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"{PSGC.PsgcBaseUrl}/{PSGC.PsgcVersion_Q2_2024}/barangays?token={PSGC.PsgcToken}");
        var contentString = await response.Content.ReadAsStringAsync(cancellationToken);
        List<PsgcResponseResult> results;
        if (response.IsSuccessStatusCode)
        {
            var responseObject = JsonSerializer.Deserialize<PsgcResponse>(contentString);
            // handle possible nulls and convert to List<T>
            results = responseObject?.Results?.ToList() ?? [];
        }
        else
        {
            _logger.LogError("{PsgcError}", contentString);
            throw new IntegrationException(nameof(PsgcService), contentString);
        }
        return results; // fixed variable name
    }
};
