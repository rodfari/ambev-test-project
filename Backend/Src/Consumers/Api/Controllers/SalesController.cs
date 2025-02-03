using Application.Dtos;
using Application.Features.Sales.Commands.Create;
using Application.Features.Sales.Commands.Delete;
using Application.Features.Sales.Commands.Update;
using Application.Features.Sales.Queries;
using Application.Model;
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
    public async Task<IActionResult> GetSaleById(string id)
    {
        var result = await _mediator.Send(new GetSaleByIdQuery(id));

        if (!result.Success)
            return BadRequest(new { Errors = result.Errors });
            
        return Ok(result.Data);
    }

    [HttpGet]
    public async Task<ActionResult<List<SaleReadModel>>> GetSalesList()
    {
        var result = await _mediator.Send(new GetAllSaleQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand createSaleCommand)
    {
        var result = await _mediator.Send(createSaleCommand);

        if(!result.Success)
            return BadRequest(result.Errors);

        return CreatedAtAction(nameof(GetSaleById), new { id = result.Data }, result.Data);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSale([FromBody] UpdateSaleCommand updateSaleCommand)
    {
        var result = await _mediator.Send(updateSaleCommand);

        if(!result.Success)
            return BadRequest(result.Errors);

        return Ok(result.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSale(Guid id)
    {
        var result = await _mediator.Send(new DeleteSaleCommand(id));

        if(!result.Success)
            return BadRequest(result.Errors);

        return Ok(result.Message);
    }
}
