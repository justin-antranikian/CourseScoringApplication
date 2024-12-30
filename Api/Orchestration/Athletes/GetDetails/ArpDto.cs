using Api.DataModels;

namespace Api.Orchestration.Athletes.GetDetails;

public static class ArpDtoMapper
{
    public static ArpDto GetArpDto(Athlete athlete, List<ArpResultDto> results)
    {
        var wellnessEntries = athlete.AthleteWellnessEntries.ToList();

        var arpDto = new ArpDto
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

        return arpDto;
    }

    private static List<AthleteWellnessEntryDto> GetWellnessEntries(List<AthleteWellnessEntry> wellnessEntries, params AthleteWellnessType[] wellnessTypes)
    {
        var filteredList = wellnessEntries.Where(oo => wellnessTypes.Contains(oo.AthleteWellnessType));
        return filteredList.Select(oo => new AthleteWellnessEntryDto { AthleteWellnessType = oo.AthleteWellnessType, Description = oo.Description }).ToList();
    }
}


/// <summary>
/// Arp stands for athlete results page. The idea is to display the athlete information along with all the results.
/// </summary>
public class ArpDto
{
    public required int Age { get; init; }
    public required string FirstName { get; init; }
    public required string FullName { get; init; }
    public required string GenderAbbreviated { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required List<ArpResultDto> Results { get; init; }
    public required List<AthleteWellnessEntryDto> WellnessTrainingAndDiet { get; init; }
    public required List<AthleteWellnessEntryDto> WellnessGoals { get; init; }
    public required List<AthleteWellnessEntryDto> WellnessMotivationalList { get; init; }
}
