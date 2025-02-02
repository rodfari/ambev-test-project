using MongoDB.Driver;

namespace MongoData;

public class SalesServiceCollection
{
    private readonly IMongoCollection<SalesServiceCollection> _salesServiceCollection;

    public string Id { get; private set; }

    public SalesServiceCollection(IMongoDatabase database)
    {
        _salesServiceCollection = database.GetCollection<SalesServiceCollection>("SalesCollection");
    }

    public async Task<List<SalesServiceCollection>> GetSalesServicesAsync()
    {
        return await _salesServiceCollection.Find(s => true).ToListAsync();
    }

    public async Task<SalesServiceCollection> GetSalesServiceAsync(string id)
    {
        return await _salesServiceCollection.Find(s => s.Id == id).FirstOrDefaultAsync();
    }

    public async Task<SalesServiceCollection> CreateSalesServiceAsync(SalesServiceCollection salesService)
    {
        await _salesServiceCollection.InsertOneAsync(salesService);
        return salesService;
    }

    public async Task UpdateSalesServiceAsync(string id, SalesServiceCollection salesService)
    {
        await _salesServiceCollection.ReplaceOneAsync(s => s.Id == id, salesService);
    }

    public async Task DeleteSalesServiceAsync(string id)
    {
        await _salesServiceCollection.DeleteOneAsync(s => s.Id == id);
    }
}
