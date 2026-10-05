using Delex_POS.Application.Common.Interfaces.Repositories.Generic;

namespace Delex_POS.Application.GenericItems.Commands.CreateGeneric;

public record CreateGenericCommand : IRequest<int>
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Memo { get; set; }
}

public class CreateGenericCommandHandler : IRequestHandler<CreateGenericCommand, int>
{
    private readonly IGenericCommandRepository _repo;
    public CreateGenericCommandHandler(IGenericCommandRepository repo) => _repo = repo;

    public async Task<int> Handle(CreateGenericCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Generic(request.Code, request.Name, request.Memo);
        return await _repo.AddAsync(entity, cancellationToken);
    }
}
