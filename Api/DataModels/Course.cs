using Api.Orchestration;
using NetTopologySuite.Geometries;

namespace Api.DataModels;

public class Course
{
    public int Id { get; set; }
    public int RaceId { get; set; }

    public required CourseType CourseType { get; set; }
    public required double Distance { get; set; }
    public required string Name { get; set; }
    public required PaceType PaceType { get; set; }
    public required PreferredMetric PreferedMetric { get; set; }
    public required int SortOrder { get; set; }
    public required DateTime StartDate { get; set; }
    public Geometry? Location { get; set; }

    public List<AthleteCourse> AthleteCourses { get; init; } = [];
    public List<AthleteCourseBracket> AthleteCourseBrackets { get; init; } = [];
    public List<Bracket> Brackets { get; init; } = [];
    public List<BracketMetadata> BracketMetadatas { get; init; } = [];
    public List<Interval> Intervals { get; init; } = [];
    public Race Race { get; set; }
    public List<Result> Results { get; init; } = [];
    public List<TagRead> TagReads { get; init; } = [];

    /// <summary>
    /// Shows the finishTime from when the course starts.
    /// If the course starts at 7:30, and you had a time of 1000 this returns "7:46:40 AM"
    /// </summary>
    public string GetCrossingTime(int timeInSeconds)
    {
        return DateTimeHelper.GetCrossingTime(StartDate, timeInSeconds);
    }

    public PaceWithTime GetPaceWithTime(int timeInSeconds, double? intervalDistance = null)
    {
        var distance = intervalDistance ?? Distance;
        return PaceCalculator.GetPaceWithTime(PaceType, PreferedMetric, timeInSeconds, distance);
    }
}
