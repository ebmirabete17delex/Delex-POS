using Delex_POS.Application.Common.Interfaces.Repositories.PersonalInfo;
namespace Delex_POS.Application.Accounts.Commands.DeletePersonalInfo;

public class DeletePersonalInfoCommandValidator : AbstractValidator<DeletePersonalInfoCommand>
{
    private readonly IPersonalInfoQueryRepository _query;
    public DeletePersonalInfoCommandValidator(IPersonalInfoQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(Exists);
    }
    private async Task<bool> Exists(int id, CancellationToken cancellationToken)
    {
        return await _query.ExistAsync(e => e.Id == id, cancellationToken);
    }
}
