using Orchestration.GetBreadcrumb;
using System.Threading.Tasks;
using Xunit;

namespace OrchestrationTests.GetBreadcrumb;

public class GetEventsBreadcrumbOrchestratorTests
{
    [Fact]
    public async Task SearchOnState()
    {
        var orchestrator = new GetEventsBreadcrumbOrchestrator(null);

        var request = new BreadcrumbRequestDto
        {
            BreadcrumbNavigationLevel = BreadcrumbNavigationLevel.State,
            SearchTerm = "colorado"
        };

        var result = await orchestrator.GetResult(request);

        Assert.Equal("Colorado", result.LocationInfoWithUrl.State);
        Assert.Equal("colorado", result.LocationInfoWithUrl.StateUrl);
    }

    [Fact]
    public async Task SearchOnArea()
    {
        var orchestrator = new GetEventsBreadcrumbOrchestrator(null);

        var request = new BreadcrumbRequestDto
        {
            BreadcrumbNavigationLevel = BreadcrumbNavigationLevel.Area,
            SearchTerm = "greater-denver-area"
        };

        var result = await orchestrator.GetResult(request);

        Assert.Equal("Colorado", result.LocationInfoWithUrl.State);
        Assert.Equal("colorado", result.LocationInfoWithUrl.StateUrl);
        Assert.Equal("Greater Denver Area", result.LocationInfoWithUrl.Area);
        Assert.Equal("greater-denver-area", result.LocationInfoWithUrl.AreaUrl);
    }

    [Fact]
    public async Task SearchOnCity()
    {
        var orchestrator = new GetEventsBreadcrumbOrchestrator(null);

        var request = new BreadcrumbRequestDto
        {
            BreadcrumbNavigationLevel = BreadcrumbNavigationLevel.City,
            SearchTerm = "denver"
        };

        var result = await orchestrator.GetResult(request);

        Assert.Equal("Colorado", result.LocationInfoWithUrl.State);
        Assert.Equal("colorado", result.LocationInfoWithUrl.StateUrl);
        Assert.Equal("Greater Denver Area", result.LocationInfoWithUrl.Area);
        Assert.Equal("greater-denver-area", result.LocationInfoWithUrl.AreaUrl);
        Assert.Equal("Denver", result.LocationInfoWithUrl.City);
        Assert.Equal("denver", result.LocationInfoWithUrl.CityUrl);
    }
}
