using Application.Responses;
using Domain.Contracts;
using MediatR;

namespace Application.Features.Products.Command.Delete;
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, TResponse<string>>
{
    readonly IProductRepository _productRepository;
    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<TResponse<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteAsync(request.Id);
        return new TResponse<string>
        {
            Success = true,
            Message = "Product Deleted Successfully",
            Data = string.Empty
        };
    }
}