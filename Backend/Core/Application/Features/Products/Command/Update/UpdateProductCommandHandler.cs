using Application.Responses;
using Application.Responses.Products;
using AutoMapper;
using Domain.Contracts;
using MediatR;

namespace Application.Features.Products.Command.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, TResponse<UpdateProductResponse>>
{
    readonly IProductRepository _productRepository;
    readonly IMapper _mapper;
    public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<TResponse<UpdateProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(request);
        
        if (validationResult.Errors.Count > 0)
        {
            return new TResponse<UpdateProductResponse>
            {
                Success = false,
                Message = validationResult.Errors.ToString()
            };
        }

        var entity = await _productRepository.GetByIdAsync(request.Id);
        _mapper.Map(request, entity);
        var result = await _productRepository.UpdateAsync(entity);
        var updated = _mapper.Map<UpdateProductResponse>(result);

        return new TResponse<UpdateProductResponse>
        {
            Data = updated,
            Message = "Product updated successfully",
            Success = true
        };
        
    }
}
