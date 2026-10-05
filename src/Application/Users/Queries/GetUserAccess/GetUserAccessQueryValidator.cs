
using Delex_POS.Application.Common.Interfaces.Repositories.Branch;

namespace Delex_POS.Application.Users.Queries.GetUserAccess;

public class GetUserAccessQueryValidator : AbstractValidator<GetUserAccessQuery>
{
    public GetUserAccessQueryValidator()
    {
        RuleFor(v => v.UserId)
            .NotEmpty()
            .NotNull();
    }
}
