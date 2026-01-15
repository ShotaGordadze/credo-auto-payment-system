using System.Linq.Expressions;
using Credo.Infrastructure.Entities.Abstraction;
using Credo.Infrastructure.Enums;
using Credo.Infrastructure.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Credo.Infrastructure.Repository;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    private readonly CredoDbContext _dbContext;
    protected readonly IQueryable<TEntity> BaseQuery;

    public Repository(CredoDbContext dbContext)
    {
        _dbContext = dbContext;
        BaseQuery = _dbContext.Set<TEntity>().AsQueryable();
    }

    public virtual async Task<TEntity?> Find(int id, bool onlyActive = true)
    {
        return onlyActive
            ? await BaseQuery.Where(x => x.IsActive()).SingleOrDefaultAsync(x => x.Id == id)
            : await BaseQuery.SingleOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null, bool onlyActive = true)
    {
        IQueryable<TEntity> query = BaseQuery;

        // if (onlyActive)
        //     query = query.Where(x => x.EntityStatus == EntityStatus.Active);

        return predicate == null ? query : query.Where(predicate);
    }

    public async Task Store(TEntity document)
    {
        await _dbContext.Set<TEntity>().AddAsync(document);
    }
}