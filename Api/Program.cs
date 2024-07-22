using DataModels;
using Microsoft.EntityFrameworkCore;
using Orchestration.GenerateData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
const string policyName = "AllowAll";

builder.Services.AddCors(options =>
{
    options.AddPolicy(policyName, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var dbConnection = "server=localhost\\MSSQLSERVER01;database=ScoringDB;Trusted_Connection=true;TrustServerCertificate=True";
builder.Services.AddDbContextPool<ScoringDbContext>(
    options => options.UseSqlServer(dbConnection, oo => oo.UseNetTopologySuite())
);

var app = builder.Build();
app.UseAuthorization();
app.UseCors(policyName);
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