namespace Api.Orchestration.Courses.GetAwards;

public record AwardsDto
{
    public required string BracketName { get; init; }
    public required AwardWinnerDto? FirstPlaceAthlete { get; init; }
    public required AwardWinnerDto? SecondPlaceAthlete { get; init; }
    public required AwardWinnerDto? ThirdPlaceAthlete { get; init; }
}

public record AwardWinnerDto
{
    public required int AthleteId { get; init; }
    public required int AthleteCourseId { get; init; }
    public required string FullName { get; init; }
    public required string FinishTime { get; init; }
    public required PaceWithTime PaceWithTime { get; init; }
}
