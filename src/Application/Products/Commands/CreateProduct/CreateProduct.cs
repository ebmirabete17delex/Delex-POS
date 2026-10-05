using Delex_POS.Application.Common.Interfaces.Repositories.Product;

namespace Delex_POS.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public int ProductClassId { get; set; }
    public int ProductTypeId { get; set; }
    public int UnitOfMeasureId { get; set; }
    public string? Memo { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductCommandRepository _productCommandRepository;
    public CreateProductCommandHandler(IProductCommandRepository productCommandRepository)
    {
        _productCommandRepository = productCommandRepository;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Product(
            name: request.Name,
            productClassId: request.ProductClassId,
            productTypeId: request.ProductTypeId,
            unitOfMeasureId: request.UnitOfMeasureId,
            memo: request.Memo ?? string.Empty
        );

        return await _productCommandRepository.AddAsync(entity, cancellationToken);
    }
}
