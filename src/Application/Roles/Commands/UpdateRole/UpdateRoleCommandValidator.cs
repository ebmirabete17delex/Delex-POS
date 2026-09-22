namespace Delex_POS.Application.Roles.Commands.UpdateRole;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(v => v.Description)
            .NotEmpty()
            .MaximumLength(200);
    }
}