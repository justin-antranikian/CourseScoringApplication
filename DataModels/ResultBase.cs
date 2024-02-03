using System.ComponentModel.DataAnnotations;

namespace DataModels;

public abstract record ResultBase
{
    [Key]
    public int Id { get; init; }

    public int AthleteCourseId { get; set; }

    public int CourseId { get; set; }

    public int IntervalId { get; set; }

    public int BracketId { get; init; }

    /// <summary>
    /// (In seconds)
    /// </summary>
    public int TimeOnInterval { get; init; }

    /// <summary>
    /// (In seconds)
    /// </summary>
    public int TimeOnCourse { get; init; }

    public int Rank { get; init; }

    public AthleteCourse AthleteCourse { get; set; }

    public Bracket Bracket { get; init; }
}
