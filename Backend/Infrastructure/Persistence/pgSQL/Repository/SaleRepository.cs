using Domain.Contracts;
using Domain.Entities;

namespace pgSQL.Repository;
public class SaleRepository : GenericRepository<Sale>, ISaleRepository
{
    public SaleRepository(DataContext context) : base(context)
    {
    }
}