using DataModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApi;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

        services.AddLogging(builder => builder
            .SetMinimumLevel(LogLevel.Debug)
            .AddConsole()
            .AddDebug()
        );

        var dbConnection = "server=localhost;database=ScoringDB2;Trusted_Connection=true;TrustServerCertificate=True";
        services.AddDbContextPool<ScoringDbContext>(options => options.UseSqlServer(dbConnection));

        services.AddSingleton<IMemoryCache, MemoryCache>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseCors("AllowAll");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
