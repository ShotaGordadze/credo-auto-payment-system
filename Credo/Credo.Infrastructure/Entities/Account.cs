using Credo.Infrastructure.Entities.Abstraction;
using Credo.Infrastructure.Enums;

namespace Credo.Infrastructure.Entities;

public class Account : Entity
{
    public string AccountNumber { get; set; }
    public Currency Currency { get; set; }
    public decimal Balance { get; set; }
    public AccountType AccountType { get; set; }
    
    public int SubscriberId { get; set; }
    public Subscriber Subscriber { get; set; }
    
    public ICollection<AutoPaymentAccount> AutoPaymentAccounts { get; set; } = [];
}