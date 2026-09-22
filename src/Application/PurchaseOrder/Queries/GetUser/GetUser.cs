using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Extensions;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Interfaces.Repositories.User;
using Delex_POS.Domain.Entities.RBAC;
using Delex_POS.Application.Users.Queries.UserDTOs;

namespace Delex_POS.Application.Users.Queries.GetUser;

public record GetUserQuery() : IRequest<UserDto>
{
    public int Id { get; init; }
};

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUserQueryRepository _userQueryRepository;

    public GetUserQueryHandler(IMapper mapper, IUserQueryRepository userQueryRepository)
    {
        _mapper = mapper;
        _userQueryRepository = userQueryRepository;
    }

    public async Task<UserDto> Handle(GetUserQuery request,CancellationToken cancellationToken)
    {
        var user = await _userQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<UserDto>(user);
    }
}
