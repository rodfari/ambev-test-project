using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace pgSQL.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly DataContext _context;
    public GenericRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        _context.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var refe = await _context.FindAsync<T>(id);
        _context.Remove(refe);
        await _context.SaveChangesAsync();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}