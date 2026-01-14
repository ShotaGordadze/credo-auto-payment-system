using Microsoft.EntityFrameworkCore;

namespace Credo.Infrastructure;

public class Database : DbContext
{
    public Database(DbContextOptions<Database> options) : base(options){}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}