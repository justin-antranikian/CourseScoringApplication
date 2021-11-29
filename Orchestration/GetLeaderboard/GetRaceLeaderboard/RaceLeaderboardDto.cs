using Core;
using DataModels;
using System.Collections.Generic;

namespace Orchestration.GetLeaderboard.GetRaceLeaderboard
{
	public static class RaceLeaderboardDtoMapper
	{
		public static RaceLeaderboardDto GetRaceLeaderboardDto(Race race, List<RaceLeaderboardByCourseDto> leaderboards)
		{
			return new
			(
				race.Name,
				race.RaceSeries.Description,
				race.KickOffDate.ToShortDateString(),
				race.RaceSeries.RaceSeriesType,
				new LocationInfoWithRank(race.RaceSeries),
				leaderboards
			);
		}
	}

	public record RaceLeaderboardDto
	(
		string RaceName,
		string RaceSeriesDescription,
		string RaceKickOffDate,
		RaceSeriesType RaceSeriesType,
		LocationInfoWithRank LocationInfoWithRank,
		List<RaceLeaderboardByCourseDto> Leaderboards
	);
}
