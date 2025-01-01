namespace Api.Orchestration.Locations.GetDirectory;

public record DirectoryDto
{
    public required int Id { get; init; }

    public required bool IsSelected { get; init; }
    public required bool IsExpanded { get; set; }
    public required string LocationType { get; init; }
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public required List<DirectoryDto> ChildLocations { get; init; }
}