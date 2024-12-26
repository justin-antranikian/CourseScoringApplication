namespace Api.Orchestration.Races.Search;

public record SearchEventsRequestDto
{
    public string? SearchTerm { get; init; }
    public string? State { get; init; }
    public string? Area { get; init; }
    public string? City { get; init; }
}
