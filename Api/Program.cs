using Api.DataModels;
using Api.Orchestration.GenerateData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var dbConnection = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContextPool<ScoringDbContext>(
    options => options.UseSqlServer(dbConnection, oo => oo.UseNetTopologySuite())
);

var app = builder.Build();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/health", Results.NoContent);

using var serviceScope = app.Services.CreateScope();
await using var dbContext = serviceScope.ServiceProvider.GetService<ScoringDbContext>()!;

var anyAthletes = await dbContext.Athletes.AnyAsync();

if (!anyAthletes)
{
    var orchestrator = new GenerateDataOrchestrator(dbContext);
    await orchestrator.Generate();
}

app.Run();
return;
