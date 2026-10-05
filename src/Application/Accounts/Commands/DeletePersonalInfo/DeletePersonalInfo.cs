using Delex_POS.Application.Common.Interfaces.Repositories.PersonalInfo;

namespace Delex_POS.Application.Accounts.Commands.DeletePersonalInfo;

public record DeletePersonalInfoCommand(int Id) : IRequest;

public class DeletePersonalInfoCommandHandler : IRequestHandler<DeletePersonalInfoCommand>
{
    private readonly IPersonalInfoCommandRepository _repo;
    private readonly IPersonalInfoQueryRepository _query;
    public DeletePersonalInfoCommandHandler(IPersonalInfoCommandRepository repo, IPersonalInfoQueryRepository query)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(DeletePersonalInfoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _repo.DeleteAsync(request.Id, cancellationToken);
    }
}
