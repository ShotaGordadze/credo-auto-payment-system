using Credo.Infrastructure.Entities;
using Credo.Infrastructure.Repository.Abstraction;
using Credo.Infrastructure.UnitOfWork.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Credo.Application.Queries.ProviderQueries;

public record GetProvidersQuery(int providerCategoryId) : IRequest<ICollection<Provider>>;

public class GetProvidersQueryHandler : IRequestHandler<GetProvidersQuery, ICollection<Provider>>
{
    private readonly IRepository<Provider> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public GetProvidersQueryHandler(IRepository<Provider> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ICollection<Provider>> Handle(GetProvidersQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Query()
            .Where(p => p.ProviderCategoryId == request.providerCategoryId)
            .ToListAsync(cancellationToken);

        return result;
    }
}