namespace DataModels;

public record Result : ResultBase
{
    public int DivisionRank { get; init; }
    public int GenderRank { get; init; }
    public bool IsHighestIntervalCompleted { get; init; }
    public int OverallRank { get; init; }

    public AthleteCourse AthleteCourse { get; set; }
    public Bracket Bracket { get; init; }
    public Course Course { get; set; }
    public Interval Interval { get; set; }
}
