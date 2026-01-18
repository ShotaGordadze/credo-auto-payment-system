using Credo.Infrastructure.Entities.Abstraction;

namespace Credo.Infrastructure.Entities;

public class Provider : Entity
{
    public string ProviderName { get; set; }
    public string ProviderAccount { get; set; }
    public int ProviderCategoryId { get; set; }
    public ProviderCategory ProviderCategory { get; set; }
    public virtual ICollection<CustomerSubscribtion> Subscribers { get; set; } = [];
}