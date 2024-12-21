namespace Api.Orchestration.GetArp;

/// <summary>
/// Arp stands for athlete results page. The idea is to display the athlete information along with all the results.
/// </summary>
public class ArpDto
{
    public required int Age { get; init; }
    public required ArpGoalDto AllEventsGoal { get; init; }
    public required string FirstName { get; init; }
    public required string FullName { get; init; }
    public required string GenderAbbreviated { get; init; }
    public required List<ArpGoalDto> Goals { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required List<ArpResultDto> Results { get; init; }
    public required List<string> Tags { get; init; }
    public required List<AthleteWellnessEntryDto> WellnessTrainingAndDiet { get; init; }
    public required List<AthleteWellnessEntryDto> WellnessGoals { get; init; }
    public required List<AthleteWellnessEntryDto> WellnessGearList { get; init; }
    public required List<AthleteWellnessEntryDto> WellnessMotivationalList { get; init; }
}
