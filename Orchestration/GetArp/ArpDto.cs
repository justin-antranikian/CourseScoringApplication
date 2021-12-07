using Core.Enums;
using static Core.Enums.AthleteWellnessType;

namespace Orchestration.GetArp
{
	public static class ArpDtoMapper
	{
		public static ArpDto GetArpDto(Athlete athlete, List<ArpResultDto> results, List<Athlete> rivalsAndFollowings, List<ArpGoalDto> goals, ArpGoalDto allEventsGoal)
		{
			var rivals = GetAthletesByType(athlete, AthleteRelationshipType.Rival, rivalsAndFollowings);
			var followings = GetAthletesByType(athlete, AthleteRelationshipType.Following, rivalsAndFollowings);
			var combinedGoals = new List<ArpGoalDto>() { allEventsGoal }.Concat(goals).ToList();
			var wellnessEntries = athlete.AthleteWellnessEntries.ToList();

			var arpDto = new ArpDto
			(
				wellnessEntries,
				athlete.FullName,
				athlete.FirstName,
				DateTimeHelper.GetCurrentAge(athlete.DateOfBirth),
				athlete.Gender.ToAbbreviation(),
				athlete.GetTags(),
				new LocationInfoWithRank(athlete),
				results,
				rivals,
				followings,
				combinedGoals,
				allEventsGoal
			);

			return arpDto;
		}

		private static List<DisplayNameWithIdDto> GetAthletesByType(Athlete athlete, AthleteRelationshipType athleteRelationshipType, List<Athlete> rivalsAndFollowings)
		{
			var athletesByRelationshipType = athlete.AthleteRelationshipEntries.Where(oo => oo.AthleteRelationshipType == athleteRelationshipType);

			DisplayNameWithIdDto MapToDisplayNameWithIdDto(AthleteRelationshipEntry athleteRelationship)
			{
				var athleteToId = athleteRelationship.AthleteToId;
				var athleteFromMasterList = rivalsAndFollowings.Single(oo => oo.Id == athleteToId);
				return new DisplayNameWithIdDto(athleteFromMasterList.Id, athleteFromMasterList.FullName);
			}

			var athletes = athletesByRelationshipType.Select(MapToDisplayNameWithIdDto).ToList();
			return athletes;
		}
	}

	/// <summary>
	/// Arp stands for athlete results page. The idea is to display the athlete information along with all the results.
	/// </summary>
	public class ArpDto
	{
		private readonly List<AthleteWellnessEntry> _wellNessEntries;

		public string FullName { get; }

		public string FirstName { get; }

		public int Age { get; }

		public string GenderAbbreviated { get; }

		public List<string> Tags { get; }

		public LocationInfoWithRank LocationInfoWithRank { get; }

		public List<ArpResultDto> Results { get; }

		public List<DisplayNameWithIdDto> Rivals { get; }

		public List<DisplayNameWithIdDto> Followings { get; }

		public List<ArpGoalDto> Goals { get; }

		public ArpGoalDto AllEventsGoal { get; }

		public List<AthleteWellnessEntry> WellnessTrainingAndDiet { get => GetWellnessEntries(Training, Diet); }

		public List<AthleteWellnessEntry> WellnessGoals { get => GetWellnessEntries(Goal); }

		public List<AthleteWellnessEntry> WellnessGearList { get => GetWellnessEntries(Gear); }

		public List<AthleteWellnessEntry> WellnessMotivationalList { get => GetWellnessEntries(Motivational); }

		private List<AthleteWellnessEntry> GetWellnessEntries(params AthleteWellnessType[] wellnessTypes)
		{
			return _wellNessEntries.Where(oo => wellnessTypes.Contains(oo.AthleteWellnessType)).ToList();
		}

		public ArpDto
		(
			List<AthleteWellnessEntry> wellNessEntries,
			string fullName,
			string firstName,
			int age,
			string genderAbbreviated,
			List<string> tags,
			LocationInfoWithRank locationInfoWithRank,
			List<ArpResultDto> results,
			List<DisplayNameWithIdDto> rivals,
			List<DisplayNameWithIdDto> followings,
			List<ArpGoalDto> goals,
			ArpGoalDto allEventsGoal
		)
		{
			_wellNessEntries = wellNessEntries;
			FullName = fullName;
			FirstName = firstName;
			Age = age;
			GenderAbbreviated = genderAbbreviated;
			Tags = tags;
			LocationInfoWithRank = locationInfoWithRank;
			Results = results;
			Rivals = rivals;
			Followings = followings;
			Goals = goals;
			AllEventsGoal = allEventsGoal;
		}
	}
}
