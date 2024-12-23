namespace Api.Orchestration.Athletes.Search;

public record SearchAthletesRequestDto
{
    public string? Area { get; init; }
    public string? City { get; init; }
    public string? State { get; init; }
    public string? SearchTerm { get; init; }
}
