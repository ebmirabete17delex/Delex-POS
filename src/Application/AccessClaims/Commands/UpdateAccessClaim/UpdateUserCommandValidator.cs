using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;

namespace Delex_POS.Application.AccessClaims.Commands.UpdateAccessClaim;

public class UpdateAccessClaimCommandValidator : AbstractValidator<UpdateAccessClaimCommand>
{
    private readonly IAccessClaimQueryRepository _accessClaimQueryRepository;
    public UpdateAccessClaimCommandValidator(IAccessClaimQueryRepository accessClaimQueryRepository)
    {
        _accessClaimQueryRepository = accessClaimQueryRepository;

        RuleFor(v => v.Feature)
            .NotEmpty()
            .MaximumLength(200);
        
        RuleFor(v => v.BackendUrl)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.FrontendUrl)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Id)
            .NotEmpty()
            .MustAsync(AccessClaimExists);
    }

    private async Task<bool> AccessClaimExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _accessClaimQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
}