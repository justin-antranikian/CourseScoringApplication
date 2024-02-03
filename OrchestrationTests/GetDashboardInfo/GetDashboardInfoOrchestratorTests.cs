using DataModels;
using Orchestration.GetDashboardInfo;
using Xunit;

namespace OrchestrationTests.GetDashboardInfo;

public class GetDashboardInfoOrchestratorTests
{
    [Fact]
    public void NoFiltersSet()
    {
        var result = GetResponse(DashboardInfoLocationType.All, null);

        Assert.Collection(result.States, result =>
        {
            Assert.Equal("California", result.DisplayName);
            Assert.Equal(2, result.Count);
        }, result =>
        {
            Assert.Equal("Colorado", result.DisplayName);
            Assert.Equal(5, result.Count);
        });
    }

    [Fact]
    public void FilterOnState()
    {
        var result = GetResponse(DashboardInfoLocationType.State, "colorado");

        Assert.Equal("Colorado", result.Title);
        Assert.Equal("colorado", result.TitleUrl);
        Assert.NotEmpty(result.Description);

        AssertNavItem(result.StateNavigationItem, "Colorado", 5, true, true);

        Assert.Collection(result.States, result =>
        {
            Assert.Equal("California", result.DisplayName);
            Assert.Equal(2, result.Count);
        }, result =>
        {
            Assert.Equal("Colorado", result.DisplayName);
            Assert.Equal(5, result.Count);
        });

        Assert.Collection(result.Areas, result =>
        {
            AssertNavItem(result, "Greater Colorado Springs Area", 2, false, false);
            Assert.Collection
            (
                result.Items,
                item => AssertNavItem(item, "Colorado Springs", 2, false, false)
            );
        }, result =>
        {
            AssertNavItem(result, "Greater Denver Area", 3, false, false);
            Assert.Collection
            (
                result.Items,
                item => AssertNavItem(item, "Boulder", 1, false, false),
                item => AssertNavItem(item, "Denver", 2, false, false)
            );
        });
    }

    [Fact]
    public void FilterOnArea()
    {
        var result = GetResponse(DashboardInfoLocationType.Area, "greater-denver-area");

        Assert.Equal("Greater Denver Area", result.Title);
        Assert.Equal("greater-denver-area", result.TitleUrl);
        Assert.NotEmpty(result.Description);

        AssertNavItem(result.StateNavigationItem, "Colorado", 5, false, true);

        Assert.Collection(result.Areas, result =>
        {
            AssertNavItem(result, "Greater Colorado Springs Area", 2, false, false);
            Assert.Collection
            (
                result.Items,
                item => AssertNavItem(item, "Colorado Springs", 2, false, false)
            );
        }, result =>
        {
            AssertNavItem(result, "Greater Denver Area", 3, true, true);
            Assert.Collection
            (
                result.Items,
                item => AssertNavItem(item, "Boulder", 1, false, false),
                item => AssertNavItem(item, "Denver", 2, false, false)
            );
        });
    }


    [Fact]
    public void FilterOnCity()
    {
        var result = GetResponse(DashboardInfoLocationType.City, "denver");

        Assert.Equal("Denver", result.Title);
        Assert.Equal("denver", result.TitleUrl);
        Assert.NotEmpty(result.Description);

        AssertNavItem(result.StateNavigationItem, "Colorado", 5, false, true);

        Assert.Collection(result.Areas, result =>
        {
            AssertNavItem(result, "Greater Colorado Springs Area", 2, false, false);
            Assert.Collection
            (
                result.Items,
                item => AssertNavItem(item, "Colorado Springs", 2, false, false)
            );
        }, result =>
        {
            AssertNavItem(result, "Greater Denver Area", 3, false, true);
            Assert.Collection
            (
                result.Items,
                item => AssertNavItem(item, "Boulder", 1, false, false),
                item => AssertNavItem(item, "Denver", 2, true, false)
            );
        });
    }

    private void AssertNavItem(NavigationItem navItem, string displayName, int count, bool isHighlighted, bool isOpen)
    {
        var displayNameUrl = displayName.ToLower().Replace(" ", "-");
        Assert.Equal(displayName, navItem.DisplayName);
        Assert.Equal(displayNameUrl, navItem.DisplayNameUrl);
        Assert.Equal(count, navItem.Count);
        Assert.Equal(isHighlighted, navItem.IsHighlighted);
        Assert.Equal(isOpen, navItem.IsOpen);
    }

    #region test preperation methods

    private static RaceSeries[] GetRaceSeriesEntries()
    {
        var denver = new RaceSeries { State = "Colorado", Area = "Greater Denver Area", City = "Denver" };
        var coloradoSprings = denver with { Area = "Greater Colorado Springs Area", City = "Colorado Springs" };
        var laJolla = new RaceSeries { State = "California", Area = "Greater San Diego Area", City = "La Jolla" };

        return new RaceSeries[]
        {
            denver,
            denver with {},
            denver with { City = "Boulder" },
            coloradoSprings,
            coloradoSprings with {},
            laJolla,
            laJolla with {},
        };
    }

    private static DashboardInfoResponseDto GetResponse(DashboardInfoLocationType type, string? searchOn)
    {
        var scoringDbContext = ScoringDbContextCreator.GetEmptyDbContext();
        var raceSeriesEntries = GetRaceSeriesEntries();

        scoringDbContext.RaceSeries.AddRange(raceSeriesEntries);
        scoringDbContext.SaveChanges();

        var orchestrator = new GetDashboardInfoOrchestrator(scoringDbContext);
        var requestDto = new DashboardInfoRequestDto
        {
            DashboardInfoType = DashboardInfoType.Events,
            DashboardInfoLocationType = type,
            LocationTermUrlFriendly = searchOn
        };

        return orchestrator.GetResult(requestDto);
    }

    #endregion
}
