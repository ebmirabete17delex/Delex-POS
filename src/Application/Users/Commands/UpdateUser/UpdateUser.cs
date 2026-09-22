using Delex_POS.Application.Common.Interfaces.Repositories.User;

namespace Delex_POS.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand : IRequest
{
    public int Id { get; set; }

    public string LastName { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string? MiddleName { get; set; }

    public int BranchId { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IUserQueryRepository _userQueryRepository;

    public UpdateUserCommandHandler(IUserCommandRepository userCommandRepository, IUserQueryRepository userQueryRepository)
    {
        _userCommandRepository = userCommandRepository;
        _userQueryRepository = userQueryRepository;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _userQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        entity.Update(
            request.LastName, 
            request.FirstName, 
            request.MiddleName
            );
        entity.ChangeBranch(request.BranchId);
 
        await _userCommandRepository.UpdateAsync(entity, cancellationToken);
    }
}
