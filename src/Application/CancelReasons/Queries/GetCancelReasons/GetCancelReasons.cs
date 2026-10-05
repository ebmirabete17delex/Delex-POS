using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces.Repositories.CancelReason;
using Delex_POS.Application.CancelReasons.Queries.CancelReasonDTOs;
using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.CancelReasons.Queries.GetCancelReasons;

public record GetCancelReasonsQuery(
    int PageNumber,
    int PageSize,
    string? SearchQuery,
    string? SortBy,
    TableSort? Sort) : IRequest<PaginatedList<CancelReasonDto>>
{};

public class GetCancelReasonsQueryHandler : IRequestHandler<GetCancelReasonsQuery, PaginatedList<CancelReasonDto>>
{
    private readonly IMapper _mapper;
    private readonly ICancelReasonQueryRepository _cancelReasonQueryRepository;

    public GetCancelReasonsQueryHandler(IMapper mapper, ICancelReasonQueryRepository cancelReasonQueryRepository)
    {
        _mapper = mapper;
        _cancelReasonQueryRepository = cancelReasonQueryRepository;
    }

    public async Task<PaginatedList<CancelReasonDto>> Handle(GetCancelReasonsQuery request,
        CancellationToken cancellationToken)
    {
        string? searchQuery = request.SearchQuery;

        Expression<Func<CancelReason, bool>> filter = (searchQuery) switch
        {
            (null) => x => true,
            (_) => x => x.Name.Contains(searchQuery) || (x.Memo ?? string.Empty).Contains(searchQuery),
        };

        var sortBy = string.IsNullOrEmpty(request.SortBy) ? "Created" : request.SortBy;
        var sortDirection = request.Sort is null ? TableSort.DESC : (TableSort)request.Sort;

        var query = _cancelReasonQueryRepository.GetAll().Where(filter);

        query = query.OrderBy(sortBy, sortDirection);

        var projectedQuery = query.ProjectTo<CancelReasonDto>(_mapper.ConfigurationProvider);

        return await projectedQuery.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
