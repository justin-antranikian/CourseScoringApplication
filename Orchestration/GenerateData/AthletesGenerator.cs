using Bogus;
using Core.Enums;

namespace Orchestration.GenerateData;

internal class AthleteFaker
{
	public readonly Guid Identifier;
	public readonly string FirstName;
	public readonly string LastName;
	public readonly DateTime DateOfBirth;
	public readonly Gender Gender;
}

public static class AthletesGenerator
{
	public static List<Athlete> GetAthletes()
	{
		var athletesFaker = new Faker<AthleteFaker>()
			.RuleFor(oo => oo.Identifier, f => Guid.NewGuid())
			.RuleFor(oo => oo.FirstName, f => f.Person.FirstName)
			.RuleFor(oo => oo.LastName, f => f.Person.LastName)
			.RuleFor(oo => oo.DateOfBirth, f => f.Person.DateOfBirth)
			.RuleFor(oo => oo.Gender, f => typeof(Gender).GetRandomEnumValue()
		);

		var athletes = athletesFaker.Generate(500).Select(MapToAthlete).ToList();
		return athletes;
	}

	private static Athlete MapToAthlete(AthleteFaker athleteFaker)
	{
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

		var possibleGoals = raceSeriesTypes.Select(oo => new AthleteRaceSeriesGoal(oo, totalEventsPossibleValues.GetRandomValue()));
		var goalsTakeAmount = (new[] { 2, 3, 4, 5 }).GetRandomValue();
		var goals = possibleGoals.GetRandomValues(goalsTakeAmount);
		var location = LocationHelper.GetRandomLocation();

		return new Athlete
		{
			FirstName = athleteFaker.FirstName,
			LastName = athleteFaker.LastName,
			FullName = athleteFaker.FirstName + " " + athleteFaker.LastName,
			DateOfBirth = athleteFaker.DateOfBirth,
			City = location.City,
			Area = location.Area,
			State = location.State,
			Gender = athleteFaker.Gender,
			AthleteRaceSeriesGoals = goals.ToList(),
			AthleteWellnessEntries = GetWellnessEntries(),
		};
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
		return new AthleteWellnessEntry(wellNessType, description);
	}

	private static IEnumerable<AthleteWellnessEntry> GetWellnessEntries(IGrouping<AthleteWellnessType, AthleteWellnessEntry> grouping)
	{
		var amountToTake = (new[] { 1, 2, 3 }).GetRandomValue();
		return grouping.GetRandomValues(amountToTake);
	}
}

