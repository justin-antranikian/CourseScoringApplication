using Orchestration.GetBreadcrumb;
using Xunit;

namespace OrchestrationTests.GetBreadcrumb;

public class GetAthleteBreadcrumbOrchestratorTests
{
    [Fact]
    public void SearchOnState()
    {
        var orchestrator = new GetAthleteBreadcrumbOrchestrator();

        var request = new BreadcrumbRequestDto
        {
            BreadcrumbNavigationLevel = BreadcrumbNavigationLevel.State,
            SearchTerm = "colorado"
        };

        var result = orchestrator.GetResult(request);

        Assert.Equal("Colorado", result.LocationInfoWithUrl.State);
        Assert.Equal("colorado", result.LocationInfoWithUrl.StateUrl);
    }

    [Fact]
    public void SearchOnArea()
    {
        var orchestrator = new GetAthleteBreadcrumbOrchestrator();

        var request = new BreadcrumbRequestDto
        {
            BreadcrumbNavigationLevel = BreadcrumbNavigationLevel.Area,
            SearchTerm = "greater-denver-area"
        };

        var result = orchestrator.GetResult(request);

        Assert.Equal("Colorado", result.LocationInfoWithUrl.State);
        Assert.Equal("colorado", result.LocationInfoWithUrl.StateUrl);
        Assert.Equal("Greater Denver Area", result.LocationInfoWithUrl.Area);
        Assert.Equal("greater-denver-area", result.LocationInfoWithUrl.AreaUrl);
    }

    [Fact]
    public void SearchOnCity()
    {
        var orchestrator = new GetAthleteBreadcrumbOrchestrator();

        var request = new BreadcrumbRequestDto
        {
            BreadcrumbNavigationLevel = BreadcrumbNavigationLevel.City,
            SearchTerm = "denver"
        };

        var result = orchestrator.GetResult(request);

        Assert.Equal("Colorado", result.LocationInfoWithUrl.State);
        Assert.Equal("colorado", result.LocationInfoWithUrl.StateUrl);
        Assert.Equal("Greater Denver Area", result.LocationInfoWithUrl.Area);
        Assert.Equal("greater-denver-area", result.LocationInfoWithUrl.AreaUrl);
        Assert.Equal("Denver", result.LocationInfoWithUrl.City);
        Assert.Equal("denver", result.LocationInfoWithUrl.CityUrl);
    }
}
