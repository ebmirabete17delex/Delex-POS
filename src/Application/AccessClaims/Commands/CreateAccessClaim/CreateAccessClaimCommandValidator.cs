namespace Delex_POS.Application.AccessClaims.Commands.CreateAccessClaim;

public class CreateAccessClaimCommandValidator : AbstractValidator<CreateAccessClaimCommand>
{
    public CreateAccessClaimCommandValidator()
    {
    
        RuleFor(v => v.Name)
            .NotEmpty()
            .NotNull();
        
        RuleFor(v => v.Feature)
            .NotEmpty()
            .NotNull()
            .MaximumLength(200);

        RuleFor(v => v.BackendUrl)
            .NotEmpty()
            .NotNull();

        RuleFor(v => v.FrontendUrl)
            .NotEmpty()
            .NotNull();

    }
}

