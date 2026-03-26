namespace BackendAPI.BE.DAL.Interfaces;
using System.Linq.Expressions;
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    
    Task DeleteAsync(object id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(object id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAsync(
    Expression<Func<T, bool>> predicate,
    CancellationToken cancellationToken = default);
}