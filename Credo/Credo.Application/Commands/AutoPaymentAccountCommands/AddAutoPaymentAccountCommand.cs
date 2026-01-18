using Credo.Infrastructure.Entities;
using Credo.Infrastructure.Repository.Abstraction;
using Credo.Infrastructure.UnitOfWork.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Credo.Application.Commands.AutoPaymentAccountCommands;

public record AddAutoPaymentAccountCommand(
    int CustomerSubscriptionId,
    int AccountId,
    DateTime StartDate,
    DateTime EndDate,
    decimal Amount,
    int FrequencyInDays) : IRequest<AutoPaymentAccount>;

public class AddAutoPaymentAccountCommandHandler : IRequestHandler<AddAutoPaymentAccountCommand, AutoPaymentAccount>
{
    private readonly IRepository<Account> _accountRepository;
    private readonly IRepository<CustomerSubscribtion> _customerSubscribtionRepository;
    private readonly IRepository<AutoPaymentAccount> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public AddAutoPaymentAccountCommandHandler(IRepository<Account> AccountRepository,
        IRepository<CustomerSubscribtion> customerSubscribtionRepository,
        IRepository<AutoPaymentAccount> repository, IUnitOfWork unitOfWork)
    {
        _accountRepository = AccountRepository;
        _customerSubscribtionRepository = customerSubscribtionRepository;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AutoPaymentAccount> Handle(AddAutoPaymentAccountCommand request,
        CancellationToken cancellationToken)
    {
        var customerSubscription = await _customerSubscribtionRepository.Find(request.CustomerSubscriptionId);
        if (customerSubscription == null)
            throw new ArgumentNullException("Customer not found");

        var account = await _accountRepository.Query()
            .Where(a => a.CustomerSubscriptionId == customerSubscription.Id)
            .Where(a => a.Id == request.AccountId)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (account == null)
        {
            throw new ArgumentNullException("Account not found");
        }

        var apc = new AutoPaymentAccount()
        {
            TargetAccountNumber = account.AccountNumber,
            AccountId = account.Id,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Amount = request.Amount,
            FrequencyInDays = request.FrequencyInDays
        };

        await _repository.Store(apc);
        await _unitOfWork.SaveAsync(cancellationToken);

        return apc;
    }
}