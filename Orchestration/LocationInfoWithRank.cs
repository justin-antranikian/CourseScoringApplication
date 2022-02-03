namespace Orchestration;

public class LocationInfoWithRank : LocationInfoWithUrl
{
	public int OverallRank { get; }

	public int StateRank { get; }

	public int AreaRank { get; }

	public int CityRank { get; }

	public LocationInfoWithRank(RaceSeries raceSeries) : base(raceSeries)
	{
		OverallRank = raceSeries.OverallRank;
		StateRank = raceSeries.StateRank;
		AreaRank = raceSeries.AreaRank;
		CityRank = raceSeries.CityRank;
	}

	public LocationInfoWithRank(Athlete athlete) : base(athlete)
	{
		OverallRank = athlete.OverallRank;
		StateRank = athlete.StateRank;
		AreaRank = athlete.AreaRank;
		CityRank = athlete.CityRank;
	}
}
