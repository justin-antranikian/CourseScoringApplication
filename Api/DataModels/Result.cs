namespace Api.DataModels;

public record Result : ResultBase
{
    public required int DivisionRank { get; set; }
    public required int GenderRank { get; set; }
    public required bool IsHighestIntervalCompleted { get; set; }
    public required int OverallRank { get; set; }

    public AthleteCourse? AthleteCourse { get; set; }
    public Bracket? Bracket { get; set; }
    public Course? Course { get; set; }
    public Interval? Interval { get; set; }
}
