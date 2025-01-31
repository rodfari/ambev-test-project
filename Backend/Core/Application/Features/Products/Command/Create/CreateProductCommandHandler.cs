using Application.Responses;
using Application.Responses.Products;
using AutoMapper;
using Domain.Contracts;
using MediatR;
using Entities = Domain.Entities;

namespace Application.Features.Products.Command.Create;

public class CreateProductCommandHandler: 
    IRequestHandler<CreateProductCommand, TResponse<CreateProductResponse>>
{
    readonly IProductRepository _productRepository;
    readonly IMapper _mapper;
    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<TResponse<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Entities.Products product = new();
        
        _mapper.Map(request, product);
        var newProduct = await product.SaveAsync(_productRepository);
        var result = _mapper.Map<CreateProductResponse>(newProduct);
        return new TResponse<CreateProductResponse>
        {
            Success = true,
            Data = result
        };
    }
}
