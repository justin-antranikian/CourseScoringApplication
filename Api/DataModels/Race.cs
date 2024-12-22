namespace Api.DataModels;

public record Race
{
    public int Id { get; set; }
    public int RaceSeriesId { get; set; }

    public required DateTime KickOffDate { get; set; }
    public required string Name { get; set; }
    public required string TimeZoneId { get; set; }

    public List<Course> Courses { get; set; } = [];
    public RaceSeries? RaceSeries { get; set; }
}
