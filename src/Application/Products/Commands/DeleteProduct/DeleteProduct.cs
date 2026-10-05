using Delex_POS.Application.Common.Interfaces.Repositories.Product;

namespace Delex_POS.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductCommandRepository _productCommandRepository;
    private readonly IProductQueryRepository _productQueryRepository;

    public DeleteProductCommandHandler(IProductCommandRepository productCommandRepository, IProductQueryRepository productQueryRepository)
    {
        _productCommandRepository = productCommandRepository;
        _productQueryRepository = productQueryRepository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _productQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _productCommandRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
