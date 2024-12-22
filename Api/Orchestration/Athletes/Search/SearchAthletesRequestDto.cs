namespace Api.Orchestration.Athletes.Search;

public class SearchAthletesRequestDto
{
    public string? Area { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? SearchTerm { get; set; }
}
