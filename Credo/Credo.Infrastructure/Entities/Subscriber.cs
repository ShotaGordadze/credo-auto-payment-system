using Credo.Infrastructure.Entities.Abstraction;

namespace Credo.Infrastructure.Entities;

public class Subscriber : Entity
{
    public string SubscriberNumber { get; set; } = null!;
    
    public int ProviderId { get; set; }
    public Provider Provider { get; set; }
}