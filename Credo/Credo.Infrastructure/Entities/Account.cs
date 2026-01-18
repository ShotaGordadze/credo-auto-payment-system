using Credo.Infrastructure.Entities.Abstraction;
using Credo.Infrastructure.Enums;

namespace Credo.Infrastructure.Entities;

public class Account : Entity
{
    public string AccountNumber { get; set; }
    public Currency Currency { get; set; }
    public decimal Balance { get; set; }
    public AccountType AccountType { get; set; }
    public int CustomerSubscriptionId { get; set; }
    public CustomerSubscribtion CustomerSubscribtion { get; set; }

    public ICollection<AutoPaymentAccount> AutoPaymentAccounts { get; set; } = [];
}