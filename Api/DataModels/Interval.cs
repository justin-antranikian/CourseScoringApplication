namespace Api.DataModels;

public record Interval
{
    public int Id { get; init; }
    public int CourseId { get; init; }

    public string? Description { get; init; }
    public double Distance { get; init; }
    public double DistanceFromStart { get; init; }
    public IntervalType IntervalType { get; init; }
    public bool IsFullCourse { get; init; }
    public string Name { get; init; }
    public int Order { get; init; }
    public PaceType PaceType { get; init; }

    public List<BracketMetadata> BracketMetadatas { get; init; } = [];
    public Course Course { get; set; }
    public List<Result> Results { get; set; } = [];
    public List<TagRead> TagReads { get; set; } = [];

    public Interval() {}

    public Interval
    (
        int courseId,
        string name,
        double distance,
        double distanceFromStart,
        int order,
        bool isFullCourse,
        PaceType paceType,
        IntervalType intervalType,
        string? description
    ) : this
    (
        name,
        distance,
        distanceFromStart,
        order,
        isFullCourse,
        paceType,
        intervalType,
        description
    ) => CourseId = courseId;

    public Interval(string name, double distance, double distanceFromStart, int order, bool isFullCourse, PaceType paceType, IntervalType intervalType, string? description)
    {
        Name = name;
        Distance = distance;
        DistanceFromStart = distanceFromStart;
        Order = order;
        IsFullCourse = isFullCourse;
        PaceType = paceType;
        IntervalType = intervalType;
        Description = description;
    }
}
