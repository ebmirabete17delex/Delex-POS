using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;

namespace Delex_POS.Application.AccessClaims.Commands.CreatePermission;

public class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
{
    private readonly IAccessClaimQueryRepository _accessClaimQueryRepository;
    public CreatePermissionCommandValidator(
        IAccessClaimQueryRepository accessClaimQueryRepository)
    {
        _accessClaimQueryRepository = accessClaimQueryRepository;

        RuleFor(v => v.AccessId)
            .NotEmpty()
            .NotNull()
            .MustAsync(AccessClaimExists);

        RuleFor(v => v.Name)
            .NotEmpty()
            .NotNull();
    }
    private async Task<bool> AccessClaimExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _accessClaimQueryRepository.ExistAsync(e => e.Id.Equals(id), cancellationToken);
        return entity;
    }

}
