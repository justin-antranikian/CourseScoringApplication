using System;
using System.Linq;

namespace Orchestration.GetIrp
{
	public static class PercentileHelper
	{
		public static string GetPercentile(int rank, int totalRacers)
		{
			var percent = rank / (double)totalRacers;
			var percentDouble = Math.Round(percent * 100);
			var topRanks = new[] { 1, 2, 3 };

			if (topRanks.Contains(rank))
			{
				return GetTopRanksPercentile(rank);
			}

			if (rank == totalRacers)
			{
				return "last place";
			}

			var pecentile = $"{percentDouble}th percentile";
			return pecentile;
		}

		private static string GetTopRanksPercentile(int rank)
		{
			return rank switch
			{
				1 => "1rst place",
				2 => "2nd place",
				3 => "3rd place",
				_ => null
			};
		}
	}
}
