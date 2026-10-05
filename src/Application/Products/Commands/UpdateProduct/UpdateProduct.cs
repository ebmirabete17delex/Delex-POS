using Delex_POS.Application.Common.Interfaces.Repositories.Product;

namespace Delex_POS.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest
{
    public int Id { get; set; }
    public int ProductClassId { get; set; } 
    public int ProductTypeId { get; set; } 
    public int UnitOfMeasureId { get; set; } 
    public string? Memo { get; set; } = string.Empty;
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductCommandRepository _productCommandRepository;
    private readonly IProductQueryRepository _productQueryRepository;

    public UpdateProductCommandHandler(IProductCommandRepository productCommandRepository, IProductQueryRepository productQueryRepository)
    {
        _productCommandRepository = productCommandRepository;
        _productQueryRepository = productQueryRepository;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _productQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        entity.Update(
            productClassId: request.ProductClassId,
            productTypeId: request.ProductTypeId,
            unitOfMeasureId: request.UnitOfMeasureId,
            memo: request.Memo);
        await _productCommandRepository.UpdateAsync(entity, cancellationToken);
    }
}
