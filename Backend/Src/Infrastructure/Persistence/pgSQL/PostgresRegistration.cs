using Domain.Contracts;
using pgSQL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace pgSQL;
public static class PostgresRegistration
{
    public static IServiceCollection AddPostgresDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ISaleRepository, SaleRepository>();
        return services;
    }
}