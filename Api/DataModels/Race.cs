namespace Api.DataModels;

public record Race
{
    public int Id { get; init; }
    public int RaceSeriesId { get; init; }

    public required DateTime KickOffDate { get; init; }
    public required string Name { get; init; }
    public required string TimeZoneId { get; init; }

    public List<Course> Courses { get; set; } = [];
    public RaceSeries? RaceSeries { get; init; }
}
