using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Models;

namespace Delex_POS.Application.Users.Queries.GetUser;

public record GetUserQuery() : IRequest<ApplicationUserDto>
{
    public string Id { get; init; } = string.Empty;
};

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ApplicationUserDto>
{
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetUserQueryHandler(IMapper mapper, IIdentityService identityService)
    {
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<ApplicationUserDto> Handle(GetUserQuery request,CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserById(request.Id.Trim());
        return _mapper.Map<ApplicationUserDto>(user);
    }
}
