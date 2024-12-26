using Api.DataModels;
using Bogus;

namespace Api.Orchestration.GenerateData;

file class AthleteFaker
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required DateTime DateOfBirth { get; init; }
    public required Gender Gender { get; init; }
}

public static class AthletesGenerator
{
    public static IEnumerable<Athlete> GetAthletes(List<Location> locations)
    {
        var fakerRuleSet = new Faker<AthleteFaker>()
            .RuleFor(oo => oo.FirstName, f => f.Person.FirstName)
            .RuleFor(oo => oo.LastName, f => f.Person.LastName)
            .RuleFor(oo => oo.DateOfBirth, f => f.Person.DateOfBirth)
            .RuleFor(oo => oo.Gender, _ => typeof(Gender).GetRandomEnumValue()
        );

        var cityLocations = locations.Where(oo => oo.LocationType == LocationType.City).ToList();

        foreach (var faker in fakerRuleSet.Generate(500))
        {
            var cityLocation = cityLocations.GetRandomValue();
            var areaLocation = cityLocation.ParentLocation!;
            var stateLocationId = areaLocation.ParentLocationId!.Value;

            var totalEventsPossibleValues = new[] { 1, 2, 4, 7, 10, 15 };

            var raceSeriesTypes = new[]
            {
                RaceSeriesType.Running,
                RaceSeriesType.Triathalon,
                RaceSeriesType.RoadBiking,
                RaceSeriesType.MountainBiking,
                RaceSeriesType.CrossCountrySkiing,
                RaceSeriesType.Swim,
            };

            var possibleGoals = raceSeriesTypes.Select(oo => AthleteRaceSeriesGoal.Create(oo, totalEventsPossibleValues.GetRandomValue()));
            var goalsTakeAmount = new[] { 2, 3, 4, 5 }.GetRandomValue();
            var goals = possibleGoals.GetRandomValues(goalsTakeAmount);

            yield return new Athlete
            {
                AreaLocationId = areaLocation.Id,
                CityLocationId = cityLocation.Id,
                StateLocationId = stateLocationId,
                AreaRank = 0,
                CityRank = 0,
                DateOfBirth = faker.DateOfBirth,
                FirstName = faker.FirstName,
                FullName = faker.FirstName + " " + faker.LastName,
                Gender = faker.Gender,
                LastName = faker.LastName,
                OverallRank = 0,
                StateRank = 0,
                AthleteRaceSeriesGoals = goals.ToList(),
                AthleteWellnessEntries = GetWellnessEntries(),
            };
        }
    }

    private static List<AthleteWellnessEntry> GetWellnessEntries()
    {
        var typeAndDescriptions = new[]
        {
            (AthleteWellnessType.Goal, "I want to complete a sprint triathalon."),
            (AthleteWellnessType.Goal, "I want to complete a half ironman."),
            (AthleteWellnessType.Goal, "I want to finish 5 triathalons."),
            (AthleteWellnessType.Goal, "I want to finish a 2 mile swim."),
            (AthleteWellnessType.Goal, "I want to compete in a downhill mountain bike event."),
            (AthleteWellnessType.Goal, "I want to run over 50 miles."),
            (AthleteWellnessType.Training, "Running 4 days a week"),
            (AthleteWellnessType.Training, "Swimming 3 days a week"),
            (AthleteWellnessType.Training, "Spin class 3 days a week"),
            (AthleteWellnessType.Training, "Yoga 5 days a week"),
            (AthleteWellnessType.Training, "Rock climbing once a week"),
            (AthleteWellnessType.Gear, "Clipless pedels"),
            (AthleteWellnessType.Gear, "Wetsuit"),
            (AthleteWellnessType.Gear, "New Balance shoes"),
            (AthleteWellnessType.Gear, "Non-cotten clothes"),
            (AthleteWellnessType.Gear, "Full suspension bike"),
            (AthleteWellnessType.Diet, "Low carbs"),
            (AthleteWellnessType.Diet, "Oatmeal and toast for breakfast"),
            (AthleteWellnessType.Diet, "Subway for lunch"),
            (AthleteWellnessType.Diet, "Grilled chicken and rice for dinner"),
            (AthleteWellnessType.Diet, "Protein shakes and No Cow bars"),
            (AthleteWellnessType.Motivational, "Raising money for children's illnesses."),
            (AthleteWellnessType.Motivational, "You should stick to your goals."),
            (AthleteWellnessType.Motivational, "Have fun doing something challenging."),
            (AthleteWellnessType.Motivational, "Crush those New Year's resolutions."),
        };

        return typeAndDescriptions
                .Select(MapToWellnessEntry)
                .GroupBy(oo => oo.AthleteWellnessType)
                .SelectMany(GetWellnessEntries)
                .ToList();
    }

    private static AthleteWellnessEntry MapToWellnessEntry((AthleteWellnessType, string) wellNessTypeDescriptionTuple)
    {
        var (wellNessType, description) = wellNessTypeDescriptionTuple;
        return new AthleteWellnessEntry
        {
            AthleteWellnessType = wellNessType,
            Description = description
        };
    }

    private static IEnumerable<AthleteWellnessEntry> GetWellnessEntries(IGrouping<AthleteWellnessType, AthleteWellnessEntry> grouping)
    {
        var amountToTake = new[] { 1, 2, 3 }.GetRandomValue();
        return grouping.GetRandomValues(amountToTake);
    }
}
