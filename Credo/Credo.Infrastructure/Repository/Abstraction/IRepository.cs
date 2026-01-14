using System.Linq.Expressions;

namespace Credo.Infrastructure.Repository.Abstraction;

public interface IRepository<T> where T : class 
{
    Task<T?> Find (int id, bool onlyActive = true);
    IQueryable<T> Query(Expression<Func<T, bool>>? predicate = null, bool onlyActive = true);
    Task Store (T document);
}