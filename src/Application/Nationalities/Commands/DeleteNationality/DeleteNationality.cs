using Delex_POS.Application.Common.Interfaces.Repositories.Nationality;

namespace Delex_POS.Application.Nationalities.Commands.DeleteNationality;

public record DeleteNationalityCommand(int Id) : IRequest;

public class DeleteNationalityCommandHandler : IRequestHandler<DeleteNationalityCommand>
{
    private readonly INationalityCommandRepository _repo;
    private readonly INationalityQueryRepository _query;
    public DeleteNationalityCommandHandler(INationalityCommandRepository repo, INationalityQueryRepository query)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(DeleteNationalityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _repo.DeleteAsync(request.Id, cancellationToken);
    }
}
