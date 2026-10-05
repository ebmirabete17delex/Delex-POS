using Delex_POS.Application.Common.Interfaces.Repositories.Account;
using Delex_POS.Domain.Entities;
using Delex_POS.Domain.Enums;

namespace Delex_POS.Application.Accounts.Commands.CreateSupplierAccount;

public record CreateSupplierAccountCommand : IRequest<int>
{
    public int AccountId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AltKey { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int BranchId { get; set; }
    public bool IsRestrictToAssignedBranchOnly { get; set; }
    public bool IsAllowToLogon { get; set; }
    public string? Password { get; set; }
    public string AccessLevel { get; set; } = string.Empty;
}

public class CreateSupplierAccountCommandHandler : IRequestHandler<CreateSupplierAccountCommand, int>
{
    private readonly IAccountCommandRepository _accountCommandRepository;
    public CreateSupplierAccountCommandHandler(IAccountCommandRepository accountCommandRepository)
    {
        _accountCommandRepository = accountCommandRepository;
    }
    public async Task<int> Handle(CreateSupplierAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = new Account(
            accountId: request.AccountId,
            name: request.Name,
            altKey: request.AltKey,
            branchId: request.BranchId,
            isRestrictToAssignedBranchOnly: request.IsRestrictToAssignedBranchOnly,
            accountType: AccountType.Supplier, // Set the account type to Supplier
            isAllowToLogon: request.IsAllowToLogon,
            password: request.Password,
            accessLevel: request.AccessLevel
        );
        return await _accountCommandRepository.AddAsync(entity, cancellationToken);
    }
}
