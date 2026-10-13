using Delex_POS.Application.Common.Interfaces.Repositories.Region;

namespace Delex_POS.Application.Regions.Commands.CreateRegion;

public class CreateRegionCommandValidator : AbstractValidator<CreateRegionCommand>
{
    private readonly IRegionQueryRepository _query;
    public CreateRegionCommandValidator(IRegionQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.Name)
            .NotNull()
            .NotEmpty()
            .MustAsync(NotExists);
    }
    private async Task<bool> NotExists(string name, CancellationToken cancellationToken)
    {
        var exists = await _query.ExistAsync(e => e.Name == name, cancellationToken);
        return !exists;
    }

}
