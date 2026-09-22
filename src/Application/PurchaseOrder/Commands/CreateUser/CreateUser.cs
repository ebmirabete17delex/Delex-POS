using Delex_POS.Domain.Entities.RBAC;
using Delex_POS.Application.Common.Interfaces.Repositories.User;

namespace Delex_POS.Application.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<int>
{
    public int BranchId { get; set; }
    public string UserCode { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUserCommandRepository _userCommandRepository;

    public CreateUserCommandHandler(IUserCommandRepository userCommandRepository)
    {
        _userCommandRepository = userCommandRepository;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new User(
            branchId: request.BranchId,
            lastName: request.LastName,
            firstName: request.FirstName,
            middleName: request.MiddleName,
            email: request.Email,
            userCode: request.UserCode
        );

        return await _userCommandRepository.AddAsync(entity,cancellationToken);

    }
}
