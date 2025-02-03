using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace pgSQL.Repository;
public class SaleRepository : GenericRepository<Sale>, ISaleRepository
{
    public SaleRepository(DataContext context) : base(context)
    {

    }
    public override async Task<Sale> GetByIdAsync(Guid id)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}