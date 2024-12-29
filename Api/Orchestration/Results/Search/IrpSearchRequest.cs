namespace Api.Orchestration.Results.Search;

public record IrpSearchRequest
{
    public int? CourseId { get; init; }
    public required int RaceId { get; init; }
    public required string SearchTerm { get; init; }
}