namespace Api.Orchestration.Results.GetDetails;

public static class PercentileHelper
{
    public static string GetPercentile(int rank, int totalRacers)
    {
        var percent = rank / (double)totalRacers;
        var percentDouble = Math.Round(percent * 100);
        int[] topRanks = [1, 2, 3];

        if (topRanks.Contains(rank))
        {
            return GetTopRanksPercentile(rank);
        }

        if (rank == totalRacers)
        {
            return "last place";
        }

        return $"{percentDouble}th percentile";
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
