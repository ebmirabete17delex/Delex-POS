using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;
using Delex_POS.Application.Employees.Queries.EmployeeDTOs;

namespace Delex_POS.Application.Employees.Queries.GetUserFeatures;

public record GetUserFeaturesQuery : IRequest<List<UserFeatureDto>>
{
    public string UserCode { get; set; } = string.Empty;
}
public class GetUserFeaturesQueryHandler : IRequestHandler<GetUserFeaturesQuery, List<UserFeatureDto>>
{
    private readonly IUserAccessQueryRepository _userAccessQueryRepository;
    public GetUserFeaturesQueryHandler(
        IUserAccessQueryRepository userAccessQueryRepository)
    {
        _userAccessQueryRepository = userAccessQueryRepository;
    }

    public async Task<List<UserFeatureDto>> Handle(GetUserFeaturesQuery request, CancellationToken cancellationToken)
    {
        var features = _userAccessQueryRepository.GetFeaturesByUserCode(request.UserCode);
        Guard.Against.NotFound(request.UserCode, features);
        return await features.ToListAsync(cancellationToken);
    }
}
