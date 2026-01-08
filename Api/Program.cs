using Api;
using Api.DataModels;
using Api.Orchestration.GenerateData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var customConfigBuilder = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.placeholder.json", optional: false)
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables();

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();

var configuration = customConfigBuilder.Build();

var configHelper = new ConfigHelper(services, configuration);

configHelper.RegisterConfig<ConnectionStringsConfig>();

AddDbContext(services);

var app = builder.Build();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/health", Results.NoContent);

using var serviceScope = app.Services.CreateScope();
await using var dbContext = serviceScope.ServiceProvider.GetService<ScoringDbContext>()!;

await Migrate(dbContext);

var anyAthletes = await dbContext.Athletes.AnyAsync();

if (!anyAthletes)
{
    var orchestrator = new GenerateDataOrchestrator(dbContext);
    await orchestrator.Generate();
}

app.Run();
return;

static async Task Migrate(ScoringDbContext dbContext)
{
#if DEBUG
    return;
#endif

    await dbContext.Database.MigrateAsync();
}

static void AddDbContext(IServiceCollection services)
{
    services.AddDbContextPool<ScoringDbContext>((serviceProvider, optionsBuilder) => {
        var connectionStrings = serviceProvider.GetRequiredService<IOptions<ConnectionStringsConfig>>();
        optionsBuilder.UseSqlServer(connectionStrings.Value.Database, oo =>
        {
            oo.UseNetTopologySuite();
        });
    });
}