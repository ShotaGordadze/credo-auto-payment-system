using System.Linq.Expressions;
using Credo.Infrastructure.Entities;
using Credo.Infrastructure.Entities.Abstraction;
using Credo.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace Credo.Infrastructure.ModelConfiguration;

public sealed class ModelConfiguration
{
    private readonly ModelBuilder _modelBuilder;

    public ModelConfiguration(ModelBuilder _modelBuilder)
    {
        this._modelBuilder = _modelBuilder;
    }
    
    public void Configure()
    {
        ConfigureProvider();
        ConfigureProviderCategory();
        ConfigureSubscriber();
        
        ApplySoftDeleteFilter(); 
    }

    private void ConfigureProvider()
    {
        _modelBuilder.Entity<Provider>(entity =>
        {
            entity.ToTable("Providers");

            entity.Property(x => x.providerName)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasMany(x => x.Subscribers)
                .WithOne(x => x.Provider)
                .HasForeignKey(x => x.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigureProviderCategory()
    {
        _modelBuilder.Entity<ProviderCategory>(entity =>
        {
            entity.ToTable("ProviderCategories");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasMany(x => x.Providers)
                .WithOne(x => x.ProviderCategory)
                .HasForeignKey(x => x.ProviderCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigureSubscriber()
    {
        _modelBuilder.Entity<Subscriber>(entity =>
        {
            entity.ToTable("Subscribers");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.SubscriberNumber)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasOne(x => x.Provider)
                .WithMany(x => x.Subscribers)
                .HasForeignKey(x => x.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ApplySoftDeleteFilter()
    {
        foreach (var entityType in _modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(Entity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");

                var property = Expression.Property(
                    parameter,
                    nameof(Entity.EntityStatus));

                var condition = Expression.Equal(
                    property,
                    Expression.Constant(EntityStatus.Active));

                var lambda = Expression.Lambda(condition, parameter);

                _modelBuilder.Entity(entityType.ClrType)
                    .HasQueryFilter(lambda);
            }
        }
    }
}