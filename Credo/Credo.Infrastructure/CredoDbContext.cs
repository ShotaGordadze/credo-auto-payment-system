using Credo.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Credo.Infrastructure;

public class CredoDbContext : DbContext
{
    public CredoDbContext(DbContextOptions<CredoDbContext> options) : base(options){}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        new ModelConfiguration.ModelConfiguration(modelBuilder).Configure();
    }
    
    public DbSet<Provider> Providers { get; set; }
    public DbSet<ProviderCategory> ProviderCategories { get; set; }
    public DbSet<Subscriber> Subscribers { get; set; }
}