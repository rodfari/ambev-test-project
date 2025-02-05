using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace pgSQL;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
    : base(options) { }
   
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
    // public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    // {
    //     foreach (var entry in base.ChangeTracker.Entries<DefaultEntity>()
    //             .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
    //     {
    //         entry.Entity.UpdatedAt = DateTime.Now;

    //         if (entry.State == EntityState.Added)
    //         {
    //             entry.Entity.CreatedAt = DateTime.Now;
    //             entry.Entity.IsDeleted = false;
    //         }
    //     }

    //     return base.SaveChangesAsync(cancellationToken);
    // }
}
