namespace Delex_POS.Application.Employees.Queries.GetUserAccess;

public class GetUserAccessQueryValidator : AbstractValidator<GetUserAccessQuery>
{
    public GetUserAccessQueryValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull();
    }
}
