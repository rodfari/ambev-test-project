using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Sales.Commands.Delete;

public class DeleteSaleCommand : IRequest<Unit>
{
    public Guid SaleId { get; set; }
}
