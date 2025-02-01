using Application.Dtos;
using Application.Features.Sales.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/sales")]
[ApiController]
public class SalesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SaleDto>> GetSaleById(Guid id)
    {
        var result = await _mediator.Send(new GetSaleByIdQuery(id));
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<SaleDto>>> GetSalesList()
    {
        var result = await _mediator.Send(new GetAllSaleQuery());
        return Ok(result);
    }
}
