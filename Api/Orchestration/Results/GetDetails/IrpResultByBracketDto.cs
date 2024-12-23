namespace Api.Orchestration.Results.GetDetails;

public record IrpResultByBracketDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required int Rank { get; init; }
    public required int TotalRacers { get; init; }
}
