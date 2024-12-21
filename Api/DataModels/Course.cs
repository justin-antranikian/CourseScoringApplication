using Api.DataModels.Enums;
using NetTopologySuite.Geometries;

namespace Api.DataModels;

public record Course
{
    public int Id { get; init; }
    public int RaceId { get; init; }

    public CourseType CourseType { get; init; }
    public double Distance { get; set; }
    public string Name { get; init; }
    public PaceType PaceType { get; init; }
    public PreferredMetric PreferedMetric { get; init; }
    public int SortOrder { get; init; }
    public DateTime StartDate { get; init; }
    public Geometry? Location { get; set; }

    public List<AthleteCourse> AthleteCourses { get; set; } = [];
    public List<AthleteCourseBracket> AthleteCourseBrackets { get; init; } = [];
    public List<Bracket> Brackets { get; init; }
    public List<BracketMetadata> BracketMetadatas { get; init; } = [];
    public List<CourseInformationEntry> CourseInformationEntries { get; init; } = [];
    public List<Interval> Intervals { get; init; }
    public Race Race { get; init; }
    public List<Result> Results { get; set; } = [];
    public List<TagRead> TagReads { get; set; } = [];
}
