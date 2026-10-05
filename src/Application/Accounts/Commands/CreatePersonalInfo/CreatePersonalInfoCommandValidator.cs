using Delex_POS.Application.Common.Interfaces.Repositories.Account;
using Delex_POS.Application.Common.Interfaces.Repositories.Nationality;
namespace Delex_POS.Application.Accounts.Commands.CreatePersonalInfo;

public class CreatePersonalInfoCommandValidator : AbstractValidator<CreatePersonalInfoCommand>
{
    private readonly IAccountQueryRepository _query;
    private readonly INationalityQueryRepository _nationalityQueryRepository;
    public CreatePersonalInfoCommandValidator(IAccountQueryRepository query, INationalityQueryRepository nationalityQueryRepository)
    {
        _query = query;
        _nationalityQueryRepository = nationalityQueryRepository;

        RuleFor(v => v.AcctId)            
            .NotEmpty()
            .NotNull()
            .MustAsync(Exists);

        RuleFor(v => v.BirthDate)
            .NotEmpty()
            .NotNull();

        RuleFor(v => v.Gender)
            .NotNull()
            .NotNull();

        RuleFor(v => v.CivilStatus)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.NationalityId)
            .NotEmpty()
            .NotEmpty()
            .MustAsync(NationalityIdExists);
            
    }

    private async Task<bool> Exists(int id, CancellationToken cancellationToken)
    {
        var exists = await _query.ExistAsync(e => e.Id == id, cancellationToken);
        return exists;
    }

    private async Task<bool> NationalityIdExists(int id, CancellationToken cancellationToken)
    {
        var exists = await _nationalityQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return exists;
    }

}
