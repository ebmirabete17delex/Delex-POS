using Delex_POS.Application.Common.Interfaces.Repositories.UserPermission;

namespace Delex_POS.Application.Employees.Commands.CreateUserPermission;

public record CreateUserPermissionCommand : IRequest<int>
{ 
    public int UserId { get; set; }
    public int PermissionId { get; set; }
}

public class CreateUserPermissionCommandHandler : IRequestHandler<CreateUserPermissionCommand, int>
{
    private readonly IUserPermissionCommandRepository _userPermissionCommandRepository;
    private readonly IUserPermissionQueryRepository _userPermissionQueryRepository;
    public CreateUserPermissionCommandHandler(
        IUserPermissionCommandRepository userPermissionCommandRepository,
        IUserPermissionQueryRepository userPermissionQueryRepository)
    {
        _userPermissionQueryRepository = userPermissionQueryRepository;
        _userPermissionCommandRepository = userPermissionCommandRepository;
    }
    public async Task<int> Handle(CreateUserPermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _userPermissionQueryRepository.GetAll().SingleOrDefaultAsync(e => e.UserId == request.UserId && e.PermissionId == request.PermissionId);
        if (entity is not null)
            return entity.Id;
        else
            return await _userPermissionCommandRepository.AddAsync(new Domain.Entities.UserPermission(request.UserId, request.PermissionId), cancellationToken);
    }
}
