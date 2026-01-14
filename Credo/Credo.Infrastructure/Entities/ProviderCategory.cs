namespace Credo.Infrastructure.Entities;

public class ProviderCategory
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Provider> Providers { get; set; } = [];
}