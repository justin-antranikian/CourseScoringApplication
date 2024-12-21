﻿using Api.DataModels;
using Api.Orchestration;
using Api.Orchestration.GetDashboardInfo;
using Api.Orchestration.GetDashboardInfo.DashboardInfoCreators;

namespace ApiTests.Orchestration.GetDashboardInfo.DashboardInfoCreators;

public class DashboardInfoByStateResultCreatorTests
{
    [Fact]
    public void MapsAllFields()
    {
        var locations = GetRaceSeriesEntries().Select(oo => new Location(oo.State, oo.Area, oo.City)).ToList();
        var firstLocation = locations.First(oo => oo.City == "Denver");
        var dashboardInfoCreator = new DashboardInfoByStateResultCreator(firstLocation);

        var result = dashboardInfoCreator.GetResult(locations);

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

    private static void AssertNavItem(NavigationItem navItem, string displayName, int count, bool isHighlighted, bool isOpen)
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

    #endregion
}
