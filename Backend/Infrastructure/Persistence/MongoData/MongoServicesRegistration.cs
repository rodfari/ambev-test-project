
using Application.Repository;
using Microsoft.Extensions.DependencyInjection;
using MongoData.Repository;
using MongoDB.Driver;

namespace MongoData;
public static class MongoServicesRegistration
{
    private static MongoSettings _mongoSettings;
    private static void setSetting(MongoSettings mongoSettings){
        _mongoSettings = new MongoSettings{
            ConnectionString = mongoSettings.ConnectionString,
            DatabaseName = mongoSettings.DatabaseName
        };
    }
    public static IServiceCollection AddMongoServices(this IServiceCollection services, Action<MongoSettings> settings)
    {
        var set = new MongoSettings();
        settings(set);
        setSetting(set);

        services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            return new MongoClient(_mongoSettings.ConnectionString);
        });
        

        services.AddSingleton(serviceProvider =>
        {
            var client = serviceProvider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(_mongoSettings.DatabaseName);
        });
        services.AddScoped<ISalesReadRepository, SalesReadRepository>();
        return services;
    }
}
