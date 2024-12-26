using Api.DataModels;

namespace Api.Orchestration.GenerateData;

internal record LocationBasic
{
    public required string Name { get; init; }
    public List<LocationBasic> ChildLocations { get; init; } = [];
}

public static class LocationGenerator
{
    public static IEnumerable<Location> GenerateLocations()
    {
        foreach (var stateLocation in GetLocationBasics())
        {
            var state = new Location
            {
                LocationType = LocationType.State,
                Name = stateLocation.Name,
                Slug = stateLocation.Name.ToUrlFriendlyText()
            };

            foreach (var areaLocation in stateLocation.ChildLocations)
            {
                var area = new Location
                {
                    LocationType = LocationType.Area,
                    Name = areaLocation.Name,
                    Slug = $"{state.Slug}/{areaLocation.Name.ToUrlFriendlyText()}"
                };

                state.ChildLocations.Add(area);

                foreach (var cityLocation in areaLocation.ChildLocations)
                {
                    var city = new Location
                    {
                        LocationType = LocationType.City,
                        Name = cityLocation.Name,
                        Slug = $"{area.Slug}/{cityLocation.Name.ToUrlFriendlyText()}"
                    };

                    area.ChildLocations.Add(city);
                }
            }

            yield return state;
        }
    }

    private static IEnumerable<LocationBasic> GetLocationBasics()
    {
        yield return new LocationBasic
        {
            Name = "Alabama",
            ChildLocations =
            [
                new LocationBasic
                {
                    Name = "Greater Montgomery Area",
                    ChildLocations =
                    [
                        new LocationBasic { Name = "Montgomery" },
                        new LocationBasic { Name = "Montgomery Suburbs" },
                    ]
                }
            ]
        };

        yield return new LocationBasic
        {
            Name = "Arizona",
            ChildLocations =
            [
                new LocationBasic
                {
                    Name = "Greater Phoenix Area",
                    ChildLocations =
                    [
                        new LocationBasic { Name = "Phoenix" },
                        new LocationBasic { Name = "Tempe" },
                        new LocationBasic { Name = "Scottsdale" },
                    ]
                }
            ]
        };

        yield return new LocationBasic
        {
            Name = "California",
            ChildLocations =
            [
                new LocationBasic
                {
                    Name = "Greater San Diego Area",
                    ChildLocations =
                    [
                        new LocationBasic { Name = "San Diego" },
                        new LocationBasic { Name = "La Jolla" },
                        new LocationBasic { Name = "Oceanside" },
                        new LocationBasic { Name = "Chula Vista" },
                    ]
                }
            ]
        };

        yield return new LocationBasic
        {
            Name = "Colorado",
            ChildLocations =
            [
                new LocationBasic
                {
                    Name = "Greater Denver Area",
                    ChildLocations =
                    [
                        new LocationBasic { Name = "Denver" },
                        new LocationBasic { Name = "Boulder" },
                        new LocationBasic { Name = "Broomfield" },
                        new LocationBasic { Name = "Westminster" },
                        new LocationBasic { Name = "Morrison" },
                    ]
                },
                new LocationBasic
                {
                    Name = "Greater Colorado Springs Area",
                    ChildLocations =
                    [
                        new LocationBasic { Name = "Colorado Springs" },
                    ]
                }
            ]
        };

        yield return new LocationBasic
        {
            Name = "Connecticut",
            ChildLocations =
            [
                new LocationBasic
                {
                    Name = "Greater Hartford Area",
                    ChildLocations =
                    [
                        new LocationBasic { Name = "Hartford" },
                    ]
                }
            ]
        };

        yield return new LocationBasic
        {
            Name = "Delaware",
            ChildLocations =
            [
                new LocationBasic
                {
                    Name = "Greater Dover Area",
                    ChildLocations = 
                    [
                        new LocationBasic { Name = "Dover" },
                    ]
                }
            ]
        };

        yield return new LocationBasic
        {
            Name = "Florida",
            ChildLocations =
            [
                new LocationBasic
                {
                    Name = "Greater Miami Area",
                    ChildLocations =
                    [
                      new LocationBasic { Name = "Miami" },
                      new LocationBasic { Name = "Ft. Lauderdale" },
                    ]
                },
                new LocationBasic
                {
                    Name = "Florida North",
                    ChildLocations =
                    [
                      new LocationBasic { Name = "Destin" },
                      new LocationBasic { Name = "Jacksonville" },
                    ]
                },
                new LocationBasic
                {
                    Name = "Florida Central",
                    ChildLocations =
                    [
                      new LocationBasic { Name = "Orlando" },
                      new LocationBasic { Name = "Tampa Bay" },
                    ]
                }
            ]
        };

        yield return new LocationBasic
        {
            Name = "Georgia",
            ChildLocations =
            [
                new LocationBasic
                {
                    Name = "Greater Atlanta Area",
                    ChildLocations =
                    [
                      new LocationBasic { Name = "Atlanta" },
                      new LocationBasic { Name = "Alpharetta" },
                      new LocationBasic { Name = "Marietta" },
                    ]
                },
                new LocationBasic
                {
                    Name = "Georgia South",
                    ChildLocations =
                    [
                        new LocationBasic { Name = "Valdosta" },
                        new LocationBasic { Name = "Macon" },
                    ]
                },
                new LocationBasic
                {
                    Name = "Georgia West",
                    ChildLocations =
                    [
                        new LocationBasic { Name = "Athens" },
                    ]
                }
            ]
        };

        yield return new LocationBasic
        {
            Name = "Hawaii",
            ChildLocations =
            [
                new LocationBasic
                {
                    Name = "Greater Honolulu",
                    ChildLocations =
                    [
                        new LocationBasic { Name = "Honolulu" },
                    ]
                }
            ]
        };

        yield return new LocationBasic
        {
            Name = "Idaho",
            ChildLocations =
            [
                new LocationBasic
                {
                    Name = "Greater Boise Area",
                    ChildLocations =
                    [
                        new LocationBasic { Name = "Boise" },
                    ]
                }
            ]
        };

        yield return new LocationBasic
        {
            Name = "Illinois",
            ChildLocations =
            [
                new LocationBasic
                {
                    Name = "Greater Chicago Area",
                    ChildLocations =
                    [
                        new LocationBasic { Name = "Chicago" },
                        new LocationBasic { Name = "Naperville" },
                        new LocationBasic { Name = "Western Suburbs" },
                    ]
                }
            ]
        };
    }
}