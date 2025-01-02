namespace Api.Orchestration;

public record LocationInfoWithRank
{
    public required string Area { get; init; }
    public required int AreaRank { get; init; }
    public required string AreaUrl { get; init; }
    public required string City { get; init; }
    public required int CityRank { get; init; }
    public required string CityUrl { get; init; }
    public required int OverallRank { get; init; }
    public required string State { get; init; }
    public required int StateRank { get; init; }
    public required string StateUrl { get; init; }
}
