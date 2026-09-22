namespace Delex_POS.Application.AccessClaims.Commands.CreateAccessClaim;

public class CreateAccessClaimCommandValidator : AbstractValidator<CreateAccessClaimCommand>
{
    public CreateAccessClaimCommandValidator()
    {
    
        RuleFor(v => v.Name)
            .NotEmpty();
        
        RuleFor(v => v.Feature)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.BackendUrl)
            .NotEmpty();

        RuleFor(v => v.FrontendUrl)
            .NotEmpty();

    }
}

