using System.ComponentModel.DataAnnotations;
using Credo.Infrastructure.Enums;

namespace Credo.Infrastructure.Entities.Abstraction;

public class Entity
{
    [Key]
    public int Id { get; protected set; }
    public Guid UId { get; protected init; } = Guid.NewGuid();
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? LastChangeDate { get; set; }
    public EntityStatus EntityStatus { get; set; } = EntityStatus.Active;

    public virtual void ResetStatus()
    {
        EntityStatus = EntityStatus.Active;
    }

    public virtual void Delete()
    {
        EntityStatus = EntityStatus.Deleted;
    }

    public bool IsActive()
    {
        return EntityStatus == EntityStatus.Active;
    }
}