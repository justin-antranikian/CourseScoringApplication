namespace Api.DataModels;

/// <summary>
/// TimeOnInterval and TimeOnCourse are in seconds.
/// </summary>
public abstract record ResultBase
{
    public int Id { get; init; }
    public required int AthleteCourseId { get; set; }
    public required int BracketId { get; set; }
    public required int CourseId { get; set; }
    public required int IntervalId { get; set; }
    public required int Rank { get; init; }
    public required int TimeOnInterval { get; init; }
    public required int TimeOnCourse { get; init; }
}
