using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;
namespace Delex_POS.Application.Users.Commands.RemoveUserAccess;

public record RemoveUserAccessCommand : IRequest
{
    public string UserId { get; set; } = string.Empty;
    public int AccessId { get; set; }
}

public class RemoveUserAccessCommandHandler : IRequestHandler<RemoveUserAccessCommand>
{
    private readonly IUserAccessCommandRepository _userAccessCommandRepository;
    private readonly IUserAccessQueryRepository _userAccessQueryRepository;

    public RemoveUserAccessCommandHandler(
        IUserAccessCommandRepository userAccessCommandRepository,
        IUserAccessQueryRepository userAccessQueryRepository)
    {
        _userAccessQueryRepository = userAccessQueryRepository;
        _userAccessCommandRepository = userAccessCommandRepository;
    }

    public async Task Handle(RemoveUserAccessCommand request, CancellationToken cancellationToken)
    {
        var entity = await _userAccessQueryRepository
            .GetByUserIdAndAccessIdAsync(request.UserId, request.AccessId);
        
        Guard.Against.NotFound(request.AccessId, entity);

        await _userAccessCommandRepository
            .DeleteAsync(entity.Id, cancellationToken);
    }
}