using Application.Model;

namespace Application.Repository;

public interface ISalesReadRepository
{
    Task<SaleReadModel?> GetByIdAsync(string id);
    Task<List<SaleReadModel>> GetAllAsync(int pageNumber, int pageSize);
    Task InsertAsync(SaleReadModel sale);
    Task UpdateAsync(SaleReadModel sale);
    Task DeleteAsync(string id);
}