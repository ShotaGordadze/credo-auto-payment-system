using Credo.Infrastructure.Entities;
using Credo.Infrastructure.Repository.Abstraction;
using Credo.Infrastructure.UnitOfWork.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Credo.Application.Queries.ProviderQueries;

public record GetProvidersQuery(int ProviderCategoryId) : IRequest<ICollection<Provider>>;

public class GetProvidersQueryHandler : IRequestHandler<GetProvidersQuery, ICollection<Provider>>
{
    private readonly IRepository<Provider> _repository;
    private readonly IRepository<ProviderCategory> _providerCategoryRepository;

    public GetProvidersQueryHandler(IRepository<Provider> repository, IRepository<ProviderCategory> providerCategoryRepository)
    {
        _repository = repository;
        _providerCategoryRepository = providerCategoryRepository;
    }
    
    public async Task<ICollection<Provider>> Handle(GetProvidersQuery request, CancellationToken cancellationToken)
    {
        var providers = await _repository.Query()
            .AsNoTracking()
            .Where(p => p.ProviderCategoryId == request.ProviderCategoryId)
            .ToListAsync(cancellationToken);

        if (providers.Count != 0) return providers;
        // If you REALLY need to distinguish: "category doesn't exist" vs "exists but empty"
        var categoryExists = await _providerCategoryRepository.Query()
            .AsNoTracking()
            .AnyAsync(x => x.Id == request.ProviderCategoryId, cancellationToken);

        if (!categoryExists)
            throw new KeyNotFoundException($"Provider category not found: {request.ProviderCategoryId}");

        return providers;
    }
}