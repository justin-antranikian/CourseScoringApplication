namespace Orchestration.GetIrp;

/// <summary>
/// Irp (Individual result projection) is used to populate the irp page.
/// </summary>
public class IrpDto
{
    public required int AthleteId { get; init; }
    public required string Bib { get; init; }
    public required List<IrpResultByBracketDto> BracketResults { get; init; }
    public required string CourseGoalDescription { get; init; }
    public required string? FinishTime { get; init; }
    public required string FirstName { get; init; }
    public required string FullName { get; init; }
    public required string GenderAbbreviated { get; init; }
    public required List<IrpResultByIntervalDto> IntervalResults { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required PaceWithTime PaceWithTimeCumulative { get; init; }
    public required string PersonalGoalDescription { get; init; }
    public required int RaceAge { get; init; }
    public required List<string> Tags { get; init; }
    public required string TimeZoneAbbreviated { get; init; }
    public required List<string> TrainingList { get; init; }
}
