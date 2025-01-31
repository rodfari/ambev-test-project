using Application.Dtos;
using Application.Responses;
using Application.Responses.Products;
using MediatR;

namespace Application.Features.Products.Command.Update;

public class UpdateProductCommand : IRequest<TResponse<UpdateProductResponse>>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }
    public RatingDto Rating { get; set; }
}
