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

using var serviceScope = app.Services.CreateScope();
await using var scoringDbContext = serviceScope.ServiceProvider.GetService<ScoringDbContext>()!;

var anyAthletes = await scoringDbContext.Athletes.AnyAsync();

if (!anyAthletes)
{
    var orchestrator = new GenerateDataOrchestrator(scoringDbContext);
    await orchestrator.Generate();
}

app.Run();
return;