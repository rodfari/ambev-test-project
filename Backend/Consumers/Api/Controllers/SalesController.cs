using Application.Dtos;
using Application.Features.Sales.Commands.Create;
using Application.Features.Sales.Commands.Delete;
using Application.Features.Sales.Commands.Update;
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

    [HttpPost]
    public async Task<ActionResult<SaleDto>> CreateSale([FromBody] CreateSaleCommand createSaleCommand)
    {
        var result = await _mediator.Send(createSaleCommand);

        if(!result.Success)
            return BadRequest(result.Errors);

        return Created("api/GetById", new { id = result.Data });
    }

    [HttpPut]
    public async Task<ActionResult<SaleDto>> UpdateSale([FromBody] UpdateSaleCommand updateSaleCommand)
    {
        var result = await _mediator.Send(updateSaleCommand);

        if(!result.Success)
            return BadRequest(result.Errors);

        return Ok(result.Message);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<SaleDto>> DeleteSale(Guid id)
    {
        var result = await _mediator.Send(new DeleteSaleCommand(id));

        if(!result.Success)
            return BadRequest(result.Errors);

        return Ok(result.Message);
    }
}
