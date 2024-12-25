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

    public List<AthleteCourse> AthleteCourses { get; set; } = [];
    public List<AthleteCourseBracket> AthleteCourseBrackets { get; init; } = [];
    public List<Bracket> Brackets { get; init; } = [];
    public List<BracketMetadata> BracketMetadatas { get; init; } = [];
    public List<CourseInformationEntry> CourseInformationEntries { get; init; } = [];
    public List<Interval> Intervals { get; init; } = [];
    public Race? Race { get; init; }
    public List<Result> Results { get; set; } = [];
    public List<TagRead> TagReads { get; set; } = [];
}
