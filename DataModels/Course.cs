using Core;
using System;
using System.Collections.Generic;

namespace DataModels;

public record Course
{
    public int Id { get; init; }
    public int RaceId { get; init; }

    public string Name { get; init; }
    public int SortOrder { get; init; }
    public PaceType PaceType { get; init; }
    public PreferedMetric PreferedMetric { get; init; }
    public double Distance { get; set; }
    public DateTime StartDate { get; init; }
    public CourseType CourseType { get; init; }

    public Race Race { get; init; }
    public List<Interval> Intervals { get; init; }
    public List<Bracket> Brackets { get; init; }
    public List<CourseInformationEntry> CourseInformationEntries { get; init; } = [];
    public List<AthleteCourse> AthleteCourses { get; set; } = [];
}
