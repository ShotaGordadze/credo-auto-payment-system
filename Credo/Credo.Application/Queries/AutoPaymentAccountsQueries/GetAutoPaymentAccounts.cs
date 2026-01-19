using System.Reflection;
using Credo.Infrastructure.Entities;
using Credo.Infrastructure.Repository.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Credo.Application.Queries.AutoPaymentAccountsQueries;

public record GetAutoPaymentAccounts() : IRequest<IEnumerable<AutoPaymentAccount>>;

public class GetAutoPaymentAccountsHandler : IRequestHandler<GetAutoPaymentAccounts, IEnumerable<AutoPaymentAccount>>
{
    private readonly IRepository<AutoPaymentAccount> _repository;

    public GetAutoPaymentAccountsHandler(IRepository<AutoPaymentAccount> repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<AutoPaymentAccount>> Handle(GetAutoPaymentAccounts request, CancellationToken cancellationToken)
    {
        var apcs =  await _repository.Query().ToListAsync(cancellationToken);

        return apcs;
    }
}