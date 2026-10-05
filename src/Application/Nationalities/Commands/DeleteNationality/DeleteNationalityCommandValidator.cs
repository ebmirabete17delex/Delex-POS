using Delex_POS.Application.Common.Interfaces.Repositories.Nationality;

namespace Delex_POS.Application.Nationalities.Commands.DeleteNationality;

public class DeleteNationalityCommandValidator : AbstractValidator<DeleteNationalityCommand>
{
    private readonly INationalityQueryRepository _query;
    public DeleteNationalityCommandValidator(INationalityQueryRepository query)
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
