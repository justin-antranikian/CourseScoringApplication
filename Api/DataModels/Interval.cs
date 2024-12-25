namespace Api.DataModels;

public enum IntervalType
{
    Swim,
    Bike,
    Run,
    Transition,
    FullCourse,
    MountainBike,
    CrossCountrySki,
}


public class Interval
{
    public int Id { get; set; }
    public int CourseId { get; set; }

    public required double Distance { get; set; }
    public required double DistanceFromStart { get; set; }
    public required IntervalType IntervalType { get; set; }
    public required bool IsFullCourse { get; set; }
    public required string Name { get; set; }
    public required int Order { get; set; }
    public required PaceType PaceType { get; set; }

    public List<BracketMetadata> BracketMetadatas { get; set; } = [];
    public Course? Course { get; set; }
    public List<Result> Results { get; set; } = [];
    public List<TagRead> TagReads { get; set; } = [];
}
