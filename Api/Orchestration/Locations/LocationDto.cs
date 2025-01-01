namespace Api.Orchestration.Locations;

public record LocationDto
{
    public required int Id { get; init; }

    public required string LocationType { get; init; }
    public required string Name { get; init; }
    public required string Slug { get; init; }
}