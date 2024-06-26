﻿namespace DataModels;

public abstract record ResultBase
{
    public int Id { get; init; }

    public int AthleteCourseId { get; set; }
    public int BracketId { get; init; }
    public int CourseId { get; set; }
    public int IntervalId { get; set; }

    public int Rank { get; init; }

    /// <summary>
    /// (In seconds)
    /// </summary>
    public int TimeOnInterval { get; init; }

    /// <summary>
    /// (In seconds)
    /// </summary>
    public int TimeOnCourse { get; init; }
}
