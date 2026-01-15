using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Credo.Infrastructure;

public class InfrastructureHandler
{
    public static async  Task InitDbContext(IServiceProvider serviceProvider)
    {
        var db = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<CredoDbContext>();
        
        if ((await db.Database.GetPendingMigrationsAsync()).Any())
        {
            await db.Database.MigrateAsync();
        }
    }
}