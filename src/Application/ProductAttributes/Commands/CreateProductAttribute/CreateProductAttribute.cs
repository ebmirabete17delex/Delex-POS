using Delex_POS.Application.Common.Interfaces.Repositories.ProductAttribute;

namespace Delex_POS.Application.ProductAttributes.Commands.CreateProductAttribute;

public record CreateProductAttributeCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
}

public class CreateProductAttributeCommandHandler : IRequestHandler<CreateProductAttributeCommand, int>
{
    private readonly IProductAttributeCommandRepository _repo;
    public CreateProductAttributeCommandHandler(IProductAttributeCommandRepository repo) => _repo = repo;

    public async Task<int> Handle(CreateProductAttributeCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.ProductAttribute(request.Name);
        return await _repo.AddAsync(entity, cancellationToken);
    }
}
