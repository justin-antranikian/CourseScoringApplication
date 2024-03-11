namespace DataModels;

public record Result : ResultBase
{
    public int DivisionRank { get; set; }
    public int GenderRank { get; set; }
    public bool IsHighestIntervalCompleted { get; set; }
    public int OverallRank { get; set; }

    public AthleteCourse AthleteCourse { get; set; }
    public Bracket Bracket { get; set; }
    public Course Course { get; set; }
    public Interval Interval { get; set; }
}
