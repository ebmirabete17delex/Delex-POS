using Delex_POS.Application.Common.Interfaces.Repositories.Account;
using Delex_POS.Domain.Enums;

namespace Delex_POS.Application.Accounts.Commands.UpdateAccount;

public record UpdateAccountCommand : IRequest
{
    public int Id { get; set; }
    public string? AltKey { get; set; }
    public bool IsActive { get; set; }
    public int BranchId { get; set; }
    public bool IsRestrictToAssignedBranchOnly { get; set; }
    public AccountType AccountType { get; set; }
    public bool IsAllowToLogon { get; set; }
    public string? Password { get; set; }
    public string AccessLevel { get; set; } = string.Empty;
}

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand>
{
    private readonly IAccountCommandRepository _accountCommandRepository;
    private readonly IAccountQueryRepository _accountQueryRepository;

    public UpdateAccountCommandHandler(IAccountCommandRepository accountCommandRepository, IAccountQueryRepository accountQueryRepository)
    {
        _accountCommandRepository = accountCommandRepository;
        _accountQueryRepository = accountQueryRepository;
    }

    public async Task Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await _accountQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        entity.Update(
            request.AltKey,
            request.IsActive,
            request.BranchId,
            request.IsRestrictToAssignedBranchOnly,
            request.AccountType,
            request.IsAllowToLogon,
            request.Password,
            request.AccessLevel
        );

        await _accountCommandRepository.UpdateAsync(entity, cancellationToken);
    }
}
