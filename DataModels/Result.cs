using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels;

public record Result : ResultBase
{
    public int OverallRank { get; init; }

    public int GenderRank { get; init; }

    public int DivisionRank { get; init; }

    public bool IsHighestIntervalCompleted { get; init; }

    public AthleteCourse AthleteCourse { get; set; }
    public Bracket Bracket { get; init; }
    public Course Course { get; set; }
    public Interval Interval { get; set; }
}
