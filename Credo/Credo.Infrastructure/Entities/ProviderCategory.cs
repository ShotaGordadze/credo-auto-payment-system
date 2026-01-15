using Credo.Infrastructure.Entities.Abstraction;

namespace Credo.Infrastructure.Entities;

public class ProviderCategory : Entity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Provider> Providers { get; set; } = [];
}