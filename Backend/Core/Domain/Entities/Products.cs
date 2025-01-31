using Core.Domain.Entities;
using Domain.Contracts;

namespace Domain.Entities;

public class Products: DefaultEntity
{
    public string Title { get; set; }
    public Decimal Price { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }

    public void ValidateRules(){
        //TODO: Implement validation rules
    }

    public async Task<Products> SaveAsync(IProductRepository repository){
        var result = await repository.AddAsync(this);
        return result;
    }
}
