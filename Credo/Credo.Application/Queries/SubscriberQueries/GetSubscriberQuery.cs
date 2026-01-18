using Credo.Infrastructure.Entities;
using Credo.Infrastructure.Repository.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Credo.Application.Queries.SubscriberQueries;

public record GetSubscriberQuery(int ProviderId, string SubscriberNumber) : IRequest<CustomerSubscribtion>;

public class GetSubscriberQueryHandler : IRequestHandler<GetSubscriberQuery, CustomerSubscribtion>
{
    private readonly IRepository<CustomerSubscribtion> _repository;

    public GetSubscriberQueryHandler(IRepository<CustomerSubscribtion> repository)
    {
        _repository = repository;
    }

    public async Task<CustomerSubscribtion> Handle(GetSubscriberQuery request, CancellationToken cancellationToken)
    {
        var subscriber = await _repository.Query()
            .Where(s => s.SubscriberNumber == request.SubscriberNumber)
            .Where(s => s.ProviderId == request.ProviderId)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (subscriber == null)
            throw new NullReferenceException("Subscriber not found");

        return subscriber;
    }
}