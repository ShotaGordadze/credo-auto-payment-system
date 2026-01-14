namespace Credo.Infrastructure.UnitOfWork.Abstraction;

public interface IUnitOfWork
{
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
}