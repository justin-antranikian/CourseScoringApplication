namespace Api.DataModels;

/// <summary>
/// TimeOnInterval and TimeOnCourse are in seconds.
/// </summary>
public abstract record ResultBase
{
    public int Id { get; set; }
    public int AthleteCourseId { get; set; }
    public int BracketId { get; set; }
    public int CourseId { get; set; }

    public required int IntervalId { get; set; }
    public required int Rank { get; set; }
    public required int TimeOnInterval { get; set; }
    public required int TimeOnCourse { get; set; }
}
