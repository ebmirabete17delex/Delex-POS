using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
namespace Delex_POS.Application.AccessClaims.Commands.DeleteAccessClaim;

public class DeleteAccessClaimCommandValidator : AbstractValidator<DeleteAccessClaimCommand>
{
    private readonly IAccessClaimQueryRepository _accessClaimQueryRepository;
    public DeleteAccessClaimCommandValidator(IAccessClaimQueryRepository accessClaimQueryRepository)
    {
        _accessClaimQueryRepository = accessClaimQueryRepository;
    
        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(AccessClaimExists);
    }

    private async Task<bool> AccessClaimExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _accessClaimQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
}


