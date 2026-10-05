using Delex_POS.Application.Common.Interfaces.Repositories.Generic;

namespace Delex_POS.Application.GenericItems.Commands.DeleteGeneric;

public record DeleteGenericCommand(int Id) : IRequest;

public class DeleteGenericCommandHandler : IRequestHandler<DeleteGenericCommand>
{
    private readonly IGenericCommandRepository _repo;
    private readonly IGenericQueryRepository _query;

    public DeleteGenericCommandHandler(IGenericCommandRepository repo, IGenericQueryRepository query)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(DeleteGenericCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _repo.DeleteAsync(request.Id, cancellationToken);
    }
}
