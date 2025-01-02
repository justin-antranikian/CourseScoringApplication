namespace Api.Orchestration.Athletes.GetDetails;

/// <summary>
/// Abbreviation stands for athlete results page.
/// </summary>
public class ArpDto
{
    public required int Age { get; init; }
    public required string FirstName { get; init; }
    public required string FullName { get; init; }
    public required string GenderAbbreviated { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required List<ArpResultDto> Results { get; init; }
    public required List<string> WellnessGoals { get; init; }
    public required List<string> WellnessMotivationalList { get; init; }
    public required List<string> WellnessTrainingAndDiet { get; init; }
}
