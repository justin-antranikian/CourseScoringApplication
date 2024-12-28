namespace Api.Orchestration.Locations;

public record LocationDto
{
    public required int Id { get; init; }
    public bool IsSelected { get; init; }
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public List<LocationDto> ChildLocations { get; init; } = [];
    public bool IsExpanded { get; set; }
}