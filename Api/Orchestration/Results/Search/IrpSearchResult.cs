namespace Api.Orchestration.Results.Search;

public record IrpSearchResult
{
    public required int Id { get; init; }
    public required int AthleteId { get; init; }
    public required int CourseId { get; init; }
    public required string Bib { get; init; }
    public required string CourseName { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}