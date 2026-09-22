using Delex_POS.Application.Common.Interfaces.Repositories.User ;

namespace Delex_POS.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(int Id) : IRequest;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IUserQueryRepository _userQueryRepository;

    public DeleteUserCommandHandler(IUserCommandRepository userCommandRepository, IUserQueryRepository userQueryRepository)
    {
        _userCommandRepository = userCommandRepository;
        _userQueryRepository = userQueryRepository;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _userQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _userCommandRepository.DeleteAsync(request.Id, cancellationToken);
    }
}