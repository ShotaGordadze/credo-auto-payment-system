using System.ComponentModel.DataAnnotations;
using Credo.Infrastructure.Entities.Abstraction;

namespace Credo.Infrastructure.Entities;

public class CustomerSubscribtion : Entity
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public int Debt { get; set; }
    public string SubscriberNumber { get; set; } = null!;
    public int ProviderId { get; set; }
    public Provider Provider { get; set; }
    public ICollection<Account> Accounts { get; set; } = [];
}