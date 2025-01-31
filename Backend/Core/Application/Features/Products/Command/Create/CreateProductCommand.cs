using Application.Dtos;
using Application.Responses;
using Application.Responses.Products;
using MediatR;

namespace Application.Features.Products.Command.Create;

public class CreateProductCommand: IRequest<TResponse<CreateProductResponse>>
{
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }
    public RatingDto Rating { get; set; }
    public string Description { get; set; }
}
