using Application.Responses;
using MediatR;

namespace Application.Features.Products.Command.Delete;
public class DeleteProductCommand : IRequest<TResponse<string>>
{
    public int Id { get; set; }
}