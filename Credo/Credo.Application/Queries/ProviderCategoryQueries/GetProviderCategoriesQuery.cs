using Credo.Infrastructure.Entities;
using Credo.Infrastructure.Repository.Abstraction;
using Credo.Infrastructure.UnitOfWork.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Credo.Application.Queries.ProviderCategoryQueries;

public record GetProviderCategoriesQuery() : IRequest<ICollection<ProviderCategory>>;

public class GetProviderCategoriesQueryHandler : IRequestHandler<GetProviderCategoriesQuery, ICollection<ProviderCategory>>
{
    private readonly IRepository<ProviderCategory> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public GetProviderCategoriesQueryHandler(IRepository<ProviderCategory> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ICollection<ProviderCategory>> Handle(GetProviderCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.Query().AsNoTracking().ToListAsync(cancellationToken);
    }
}