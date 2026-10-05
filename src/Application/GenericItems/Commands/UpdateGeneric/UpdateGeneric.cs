using Delex_POS.Application.Common.Interfaces.Repositories.Generic;

namespace Delex_POS.Application.GenericItems.Commands.UpdateGeneric;

public record UpdateGenericCommand : IRequest
{
    public int Id { get;  set; }
    public string Code { get;  set; } = string.Empty;
    public string Name { get;  set; } = string.Empty;
    public string? Memo { get;  set; }
}

public class UpdateGenericCommandHandler : IRequestHandler<UpdateGenericCommand>
{
    private readonly IGenericCommandRepository _repo;
    private readonly IGenericQueryRepository _query;

    public UpdateGenericCommandHandler(IGenericCommandRepository repo, IGenericQueryRepository query)
    {
        _repo = repo;
        _query = query;
    }

    public async Task Handle(UpdateGenericCommand request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        entity.Update(name: request.Name, memo: request.Memo);

        await _repo.UpdateAsync(entity, cancellationToken);
    }
}
