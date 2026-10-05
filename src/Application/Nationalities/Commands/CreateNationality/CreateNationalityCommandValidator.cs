using System;
using System.Collections.Generic;
using System.Text;
using Delex_POS.Application.Common.Interfaces.Repositories.Nationality;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Delex_POS.Application.Nationalities.Commands.CreateNationality;

public class CreateNationalityCommandValidator : AbstractValidator<CreateNationalityCommand>
{
    private readonly INationalityQueryRepository _query;
    public CreateNationalityCommandValidator(INationalityQueryRepository query)
    {
        _query = query;
        RuleFor(v => v.Name)
            .NotEmpty()
            .NotNull();
            

    }

    private async Task<bool> NameNotExists(string name, CancellationToken cancellationToken)
    {
        var exists = await _query.ExistAsync(e => e.Name == name, cancellationToken);
        return !exists;
    }
}
