using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Application.AccessClaims.Queries.AccessClaimDTOs;

namespace Delex_POS.Application.AccessClaims.Queries.GetAccessClaims;

public record GetAccessClaimsQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<AccessClaimDto>>
{};

public class GetAccessClaimsQueryHandler : IRequestHandler<GetAccessClaimsQuery, PaginatedList<AccessClaimDto>>
{
    private readonly IMapper _mapper;
    private readonly IAccessClaimQueryRepository _accessClaimQueryRepository;

    public GetAccessClaimsQueryHandler(IMapper mapper, IAccessClaimQueryRepository accessClaimQueryRepository)
    {
        _mapper = mapper;
        _accessClaimQueryRepository = accessClaimQueryRepository;
    }

    public async Task<PaginatedList<AccessClaimDto>> Handle(GetAccessClaimsQuery request,
        CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;

        Expression<Func<Domain.Entities.RBAC.AccessClaim, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.Name.Contains(searchQuery)
                        || x.Feature.Contains(searchQuery)
                        || x.BackendUrl.Contains(searchQuery)
                        || x.FrontendUrl.Contains(searchQuery),
        };

        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "Created" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;

        var query = _accessClaimQueryRepository.GetAll().Where(filter);
        
        query = query.OrderBy(sortBy, sortDirection);

        var projectedQuery = query.ProjectTo<AccessClaimDto>(_mapper.ConfigurationProvider);

        return await
            projectedQuery.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
