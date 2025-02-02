
using Application.Model;
using Application.Repository;
using MongoDB.Driver;

namespace MongoData.Repository;
public class SalesReadRepository : ISalesReadRepository
{
    private readonly IMongoCollection<SaleReadModel> _salesServiceCollection;

    public SalesReadRepository(IMongoDatabase database)
    {
        _salesServiceCollection = database.GetCollection<SaleReadModel>("SalesCollection");
        
    }
    public async Task DeleteAsync(string id)
    {
        await _salesServiceCollection.DeleteOneAsync(s => s.Id == id);
    }

    public async Task<List<SaleReadModel>> GetAllAsync(int pageNumber, int pageSize)
    {
       return await _salesServiceCollection.Find(s => true).ToListAsync();
    }

    public async Task<SaleReadModel> GetByIdAsync(string id)
    {
        return await _salesServiceCollection.Find(s => s.Id == id).FirstOrDefaultAsync();
    }

    public async Task InsertAsync(SaleReadModel sale)
    {
       await _salesServiceCollection.InsertOneAsync(sale);
    }

    public async Task UpdateAsync(SaleReadModel sale)
    {
        await _salesServiceCollection.ReplaceOneAsync(s => s.Id == sale.Id, sale);
    }
}