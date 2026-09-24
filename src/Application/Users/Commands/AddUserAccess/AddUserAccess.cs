using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;
namespace Delex_POS.Application.Users.Commands.AddUserAccess;

public record AddUserAccessCommand : IRequest<int>
{
    public string UserId { get; set; } = string.Empty;
    public int AccessId { get; set; }
}

public class AddUserAccessCommandHandler : IRequestHandler<AddUserAccessCommand, int>
{
    private readonly IUserAccessCommandRepository _userAccessCommandRepository;
    public AddUserAccessCommandHandler(IUserAccessCommandRepository userAccessCommandRepository)
    {
        _userAccessCommandRepository = userAccessCommandRepository;
    }

    public async Task<int> Handle(AddUserAccessCommand request, CancellationToken cancellationToken)
    {
        return await _userAccessCommandRepository
            .AddAsync(new Domain.Entities.RBAC.UserAccess(request.UserId, request.AccessId), cancellationToken);
    }
}