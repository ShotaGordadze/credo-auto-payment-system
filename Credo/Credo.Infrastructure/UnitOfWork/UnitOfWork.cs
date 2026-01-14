using Credo.Infrastructure.Entities.Abstraction;
using Credo.Infrastructure.UnitOfWork.Abstraction;

namespace Credo.Infrastructure.UnitOfWork;

public class UnitOfWork(Database dbContext) : IUnitOfWork
{
    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        _ =  dbContext.ChangeTracker.Entries<Entity>()
            .Select(x => x)
            .ToList();
        
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}