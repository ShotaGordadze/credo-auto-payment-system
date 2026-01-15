using Credo.Application;
using Credo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    await InfrastructureHandler.InitDbContext(app.Services);
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoPayment API V1");
    c.RoutePrefix = string.Empty;
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CredoDbContext>();

    await db.Database.MigrateAsync();

    // Seed Database
    await DbSeeder.SeedAsync(db);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();