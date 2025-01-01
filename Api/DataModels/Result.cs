namespace Api.DataModels;

public record Result
{
    public int Id { get; set; }
    public int AthleteCourseId { get; set; }
    public int BracketId { get; set; }
    public int CourseId { get; set; }

    public required int DivisionRank { get; set; }
    public required int GenderRank { get; set; }
    public required int IntervalId { get; set; }
    public required bool IsHighestIntervalCompleted { get; set; }
    public required int OverallRank { get; set; }
    public required int Rank { get; set; }
    public required int TimeOnInterval { get; set; }
    public required int TimeOnCourse { get; set; }

    public AthleteCourse AthleteCourse { get; set; }
    public Bracket Bracket { get; set; }
    public Course Course { get; set; }
    public Interval Interval { get; set; }
}
