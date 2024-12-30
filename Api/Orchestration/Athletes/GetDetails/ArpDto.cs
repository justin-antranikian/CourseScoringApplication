using Api.DataModels;

namespace Api.Orchestration.Athletes.GetDetails;

public static class ArpDtoMapper
{
    public static ArpDto GetArpDto(Athlete athlete, List<ArpResultDto> results)
    {
        var wellnessEntries = athlete.AthleteWellnessEntries;

        return new ArpDto
        {
            Age = DateTimeHelper.GetCurrentAge(athlete.DateOfBirth),
            FirstName = athlete.FirstName,
            FullName = athlete.FullName,
            GenderAbbreviated = athlete.Gender.ToAbbreviation(),
            LocationInfoWithRank = athlete.ToLocationInfoWithRank(),
            Results = results,
            WellnessGoals = GetWellnessEntries(wellnessEntries, AthleteWellnessType.Goal),
            WellnessMotivationalList = GetWellnessEntries(wellnessEntries, AthleteWellnessType.Motivational),
            WellnessTrainingAndDiet = GetWellnessEntries(wellnessEntries, AthleteWellnessType.Training, AthleteWellnessType.Diet),
        };
    }

    private static List<string> GetWellnessEntries(List<AthleteWellnessEntry> wellnessEntries, params AthleteWellnessType[] wellnessTypes)
    {
        return wellnessEntries.Where(oo => wellnessTypes.Contains(oo.AthleteWellnessType)).Select(oo => oo.Description).ToList();
    }
}


/// <summary>
/// Results for the athlete. Athlete Results Page.
/// </summary>
public class ArpDto
{
    public required int Age { get; init; }
    public required string FirstName { get; init; }
    public required string FullName { get; init; }
    public required string GenderAbbreviated { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required List<ArpResultDto> Results { get; init; }
    public required List<string> WellnessTrainingAndDiet { get; init; }
    public required List<string> WellnessGoals { get; init; }
    public required List<string> WellnessMotivationalList { get; init; }
}
