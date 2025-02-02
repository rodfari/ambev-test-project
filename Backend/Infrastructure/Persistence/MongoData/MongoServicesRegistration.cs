
using Application.Repository;
using Microsoft.Extensions.DependencyInjection;
using MongoData.Repository;
using MongoDB.Driver;

namespace MongoData;
public static class MongoServicesRegistration
{
    public static IServiceCollection AddMongoServices(this IServiceCollection services, MongoSettings mongoSettings)
    {
        services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            return new MongoClient(mongoSettings.ConnectionString);
        });
        

        services.AddSingleton(serviceProvider =>
        {
            var client = serviceProvider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(mongoSettings.DatabaseName);
        });
        services.AddScoped<ISalesReadRepository, SalesReadRepository>();
        return services;
    }
}
