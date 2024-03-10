using DataModels;
using Microsoft.EntityFrameworkCore;
using Orchestration.GenerateData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var dbConnection = "server=localhost;database=ScoringDB;Trusted_Connection=true;TrustServerCertificate=True";
builder.Services.AddDbContextPool<ScoringDbContext>(options => options.UseSqlServer(dbConnection));

var app = builder.Build();
app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();

using var serviceScope = app.Services.CreateScope();
using var scoringDbContext = serviceScope.ServiceProvider.GetService<ScoringDbContext>()!;

var anyAthletes = await scoringDbContext.Athletes.AnyAsync();

if (!anyAthletes)
{
    var orchestrator = new GenerateDataOrchestrator(scoringDbContext);
    await orchestrator.Generate();
}

app.Run();
