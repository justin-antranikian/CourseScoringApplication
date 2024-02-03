namespace Orchestration.ScoreCourses;

public class HighestIntervalCompletedScorer : ScorerBase
{
    public HighestIntervalCompletedScorer(Course course, List<Bracket> brackets, List<TagRead> tagReads, List<AthleteCourseBracket> atheleteCourseBrackets, List<Interval> intervals) : base(course, brackets, tagReads, atheleteCourseBrackets, intervals)
    {
    }

    public List<Result> GetResults()
    {
        var rankOnlyResults = GetResultsRankOnly();
        var grouping = rankOnlyResults.GroupBy(oo => oo.AthleteCourseId);
        var results = new List<Result>();

        foreach (var resultsByAthlete in grouping)
        {
            var irpsForAthlete = MapToResults(resultsByAthlete.ToList(), resultsByAthlete.Key, true);
            results.AddRange(irpsForAthlete);
        }

        return results;
    }

    private IEnumerable<ResultWithRankOnly> GetResultsRankOnly()
    {
        foreach (var bracket in _brackets)
        {
            var count = 1;
            var reads = GetSortedReads(bracket);

            foreach (var read in reads)
            {
                yield return MapToRankOnlyResult(read, bracket, count);
                count++;
            }
        }
    }

    private IEnumerable<TagRead> GetSortedReads(Bracket bracket)
    {
        static IEnumerable<TagReadWithIntervalOrder> SortByBestInterval(IEnumerable<TagReadWithIntervalOrder> reads)
        {
            return reads.OrderByDescending(oo => oo.IntervalOrder).ThenBy(oo => oo.TimeOnCourse);
        }

        static TagReadWithIntervalOrder GetHighestInterval(IEnumerable<TagReadWithIntervalOrder> reads)
        {
            return SortByBestInterval(reads).First();
        }

        var filteredReads = FilterReadsByBracket(bracket);
        var readsGrouping = filteredReads.GroupBy(oo => oo.AthleteCourseId);
        var highestAthleteReads = readsGrouping.Select(GetHighestInterval);
        return SortByBestInterval(highestAthleteReads);
    }
}
