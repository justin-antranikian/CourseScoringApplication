using Api.DataModels;
using Api.Orchestration.GenerateData;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
const string policyName = "AllowAll";

builder.Services.AddCors(options =>
{
    options.AddPolicy(policyName, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var dbConnection = "server=JustinPC;database=ScoringDB;Trusted_Connection=true;TrustServerCertificate=True";
//builder.Services.AddDbContextPool<ScoringDbContext>(
//    options => options.UseSqlServer(dbConnection, oo => oo.UseNetTopologySuite())
//);

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

var wkt = "POLYGON((-124.2444 46.2587,-124.0384 46.2634,-124.0205 46.2891,-123.9368 46.2407,-123.8681 46.2388,-123.7390 46.2691,-123.6868 46.2520,-123.6456 46.2596,-123.5468 46.2577,-123.4863 46.2672,-123.4287 46.2369,-123.4287 46.1817,-123.3765 46.1513,-123.3051 46.1494,-123.1512 46.1874,-123.0222 46.1380,-122.9398 46.0980,-122.8848 46.0551,-122.8189 45.9588,-122.8162 45.9053,-122.7887 45.8824,-122.7969 45.8173,-122.7585 45.7637,-122.7750 45.6851,-122.7530 45.6505,-122.5937 45.6064,-122.4426 45.5679,-122.3712 45.5756,-122.3245 45.5506,-122.2064 45.5641,-122.0856 45.5987,-121.8933 45.6659,-121.8906 45.6812,-121.8164 45.7081,-121.7560 45.6966,-121.5335 45.7254,-121.3907 45.6928,-121.3646 45.7081,-121.2039 45.6582,-121.1957 45.6083,-121.1462 45.6102,-121.0803 45.6524,-121.0144 45.6582,-120.9814 45.6467,-120.9430 45.6563,-120.9100 45.6371,-120.8524 45.6755,-120.6052 45.7445,-120.5310 45.7158,-120.4733 45.6966,-120.2893 45.7234,-120.2124 45.7234,-120.1575 45.7714,-119.9680 45.8211,-119.7839 45.8498,-119.6658 45.8575,-119.6136 45.9149,-119.5724 45.9244,-119.5258 45.9110,-119.2676 45.9378,-119.1687 45.9168,-119.0149 45.9779,-118.9847 46.0027,-116.9165 45.9970,-116.8781 45.9569,-116.8588 45.8957,-116.8066 45.8747,-116.7902 45.8307,-116.7654 45.8192,-116.7105 45.8231,-116.6748 45.7828,-116.6144 45.7828,-116.5430 45.7522,-116.5402 45.6889,-116.4606 45.6121,-116.5567 45.5006,-116.5567 45.4640,-116.5869 45.4447,-116.6721 45.3213,-116.6940 45.2633,-116.7325 45.1414,-116.7764 45.1065,-116.7847 45.0696,-116.8451 45.0231,-116.8616 44.9765,-116.8286 44.9298,-116.9302 44.7955,-117.0346 44.7487,-117.1225 44.5787,-117.1445 44.5455,-117.2076 44.4847,-117.2269 44.4789,-117.2269 44.4063,-117.2488 44.3926,-117.1939 44.3455,-117.2269 44.2983,-117.1774 44.2570,-117.1445 44.2609,-117.1005 44.2806,-117.0511 44.2314,-117.0428 44.2491,-116.9797 44.2432,-116.9742 44.1940,-116.8945 44.1664,-116.9330 44.0935,-116.9714 44.0836,-116.9879 44.0521,-116.9302 44.0244,-116.9659 43.9533,-117.0209 43.8207,-117.0264 43.6639,-117.0264 42.0024,-121.2836 41.9983,-122.5003 42.0085,-123.0798 42.0064,-123.1540 42.0105,-123.2419 42.0044,-123.6209 42.0024,-124.3982 41.9952,-124.5493 42.1593,-124.5877 42.6521,-124.7305 42.8115,-124.4119 43.7552,-124.2554 46.0065,-124.2444 46.2587,-124.2444 46.2587))";

var reader = new WKTReader();
var polygon = reader.Read(wkt);

var course = await scoringDbContext.Courses.SingleAsync(oo => oo.Id == 1);
course.Location = polygon;

//await scoringDbContext.SaveChangesAsync();

//var oregonPoint = new Point(-124.4000, 42.0000);
//var oregonPointTwo = new Point(-124.4000, 43.0000);
//var californiaPoint = new Point(-124.4000, 41.0000);

//var queryOne = await scoringDbContext.Courses.Where(oo => oo.Location != null && oo.Location.Intersects(oregonPoint)).ToListAsync();
//var queryTwo = await scoringDbContext.Courses.Where(oo => oo.Location != null && oo.Location.Intersects(oregonPointTwo)).ToListAsync();
//var queryThree = await scoringDbContext.Courses.Where(oo => oo.Location != null && oo.Location.Intersects(californiaPoint)).ToListAsync();

app.Run();
return;