using Delex_POS.Application.Common.Interfaces.Repositories.Nationality;

namespace Delex_POS.Application.Nationalities.Commands.CreateNationality;

public record CreateNationalityCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string? LocalName { get; set; }
}

public class CreateNationalityCommandHandler : IRequestHandler<CreateNationalityCommand, int>
{
    private readonly INationalityCommandRepository _repo;
    public CreateNationalityCommandHandler(INationalityCommandRepository repo) => _repo = repo;

    public async Task<int> Handle(CreateNationalityCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Nationality(request.Name, request.LocalName);
        return await _repo.AddAsync(entity, cancellationToken);
    }
}
