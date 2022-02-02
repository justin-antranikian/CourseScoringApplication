namespace Orchestration.GetIrp;

public static class IrpResultByBracketDtoMapper
{
	public static IrpResultByBracketDto GetIrpResultByBracketDto(Bracket bracket, Result result, int totalRacers)
	{
		var rank = result.Rank;
		var (didBetterThenPercent, didWorseThenPercent) = GetBetterOrWorseThenPercent(rank, totalRacers);

		var irpResultByBracketDto = new IrpResultByBracketDto
		(
			bracket.Id,
			bracket.Name,
			rank,
			totalRacers,
			PercentileHelper.GetPercentile(rank, totalRacers),
			didBetterThenPercent,
			didWorseThenPercent
		);

		return irpResultByBracketDto;
	}

	private static (double, double) GetBetterOrWorseThenPercent(int rank, int totalRacers)
	{
		var percent = rank / (double)totalRacers;
		var percentDouble = Math.Round(percent * 100);

		if (rank == 1)
		{
			return (100, 0);
		}

		if (rank == totalRacers)
		{
			return (0, 100);
		}

		return (100 - percentDouble, percentDouble);
	}
}

public record IrpResultByBracketDto
(
	int Id,
	string Name,
	int Rank,
	int TotalRacers,
	string Percentile,
	double DidBetterThenPercent,
	double DidWorseThenPercent
);
