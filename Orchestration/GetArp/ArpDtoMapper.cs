using Core.Enums;

namespace Orchestration.GetArp;

public static class ArpDtoMapper
{
    public static ArpDto GetArpDto(Athlete athlete, List<ArpResultDto> results, List<ArpGoalDto> goals, ArpGoalDto allEventsGoal)
    {
        var combinedGoals = new List<ArpGoalDto>() { allEventsGoal }.Concat(goals).ToList();
        var wellnessEntries = athlete.AthleteWellnessEntries.ToList();

        var arpDto = new ArpDto
        {
            Age = DateTimeHelper.GetCurrentAge(athlete.DateOfBirth),
            AllEventsGoal = allEventsGoal,
            FirstName = athlete.FirstName,
            FullName = athlete.FullName,
            GenderAbbreviated = athlete.Gender.ToAbbreviation(),
            Goals = combinedGoals,
            LocationInfoWithRank = new LocationInfoWithRank(athlete),
            Results = results,
            Tags = athlete.GetTags(),
            WellnessGearList = GetWellnessEntries(wellnessEntries, AthleteWellnessType.Gear),
            WellnessGoals = GetWellnessEntries(wellnessEntries, AthleteWellnessType.Goal),
            WellnessMotivationalList = GetWellnessEntries(wellnessEntries, AthleteWellnessType.Motivational),
            WellnessTrainingAndDiet = GetWellnessEntries(wellnessEntries, AthleteWellnessType.Training, AthleteWellnessType.Diet),
        };

        return arpDto;
    }

    private static List<AthleteWellnessEntry> GetWellnessEntries(List<AthleteWellnessEntry> wellnessEntries, params AthleteWellnessType[] wellnessTypes)
    {
        return wellnessEntries.Where(oo => wellnessTypes.Contains(oo.AthleteWellnessType)).ToList();
    }
}
