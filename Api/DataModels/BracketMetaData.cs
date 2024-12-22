namespace Api.DataModels;

public class BracketMetadata
{
    public int Id { get; set; }
    public int BracketId { get; set; }
    public int CourseId { get; set; }
    public int? IntervalId { get; set; }

    public required int TotalRacers { get; set; }

    public Bracket? Bracket { get; set; }
    public Course? Course { get; set; }
    public Interval? Interval { get; set; }

    public static BracketMetadata Create(int bracketId, int courseId, int? intervalId, int totalRacers)
    {
        return new BracketMetadata
        {
            BracketId = bracketId,
            CourseId = courseId,
            IntervalId = intervalId,
            TotalRacers = totalRacers
        };
    }
}
