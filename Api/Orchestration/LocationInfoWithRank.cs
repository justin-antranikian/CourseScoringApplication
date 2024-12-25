namespace Api.Orchestration;

public class LocationInfoWithRank : LocationInfoWithUrl
{
    public required int AreaRank { get; init; }
    public required int CityRank { get; init; }
    public required int OverallRank { get; init; }
    public required int StateRank { get; init; }
}
