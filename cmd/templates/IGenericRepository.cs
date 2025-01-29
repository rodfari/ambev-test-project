using System.Linq.Expressions;
using Core.Domain.Entities;

namespace Core.Domain.Contracts;

public interface IGenericRepository<T> where T : DefaultEntity
{
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task DeleteAsync(T Entity);
}