using Api.DataModels;

namespace Api.Orchestration.GenerateData;

public class BracketIntervalScorer(
    Course course,
    List<Bracket> brackets,
    List<TagRead> tagReads,
    List<AthleteCourseBracket> atheleteCourseBrackets,
    List<Interval> intervals)
    : ScorerBase(course, brackets, tagReads, atheleteCourseBrackets, intervals)
{
    public (List<Result> results, List<BracketMetadata> bracketMetaData) GetResults()
    {
        var (results, bracketMetaData) = GetResultsRankOnly();
        var irps = GetResultsForIntervalBrackets(results);
        return (irps, bracketMetaData);
    }

    private (List<ResultWithRankOnly> results, List<BracketMetadata> bracketMetaData) GetResultsRankOnly()
    {
        var results = new List<ResultWithRankOnly>();
        var metadataEntries = new List<BracketMetadata>();

        foreach (var (interval, bracket) in GetIntervalBrackets())
        {
            var count = 1;
            var reads = GetSortedReads(bracket, interval.Id).ToList();

            foreach (var read in reads)
            {
                var result = MapToRankOnlyResult(read, bracket, count);
                results.Add(result);
                count++;
            }

            metadataEntries.Add(new BracketMetadata(_course.Id, bracket.Id, reads.Count, interval.Id));
        }

        return (results, metadataEntries);
    }

    private List<(Interval interval, Bracket bracket)> GetIntervalBrackets()
    {
        var query = from iv in _intervals
                    from br in _brackets
                    orderby iv.Order
                    select new { interval = iv, bracket = br };

        return query.Select(oo => (oo.interval, oo.bracket)).ToList();
    }

    private IEnumerable<TagRead> GetSortedReads(Bracket bracket, int intervalId)
    {
        var filteredReads = FilterReadsByBracket(bracket);
        var readsPerInterval = filteredReads.Where(oo => oo.IntervalId == intervalId);
        return readsPerInterval.OrderBy(oo => oo.TimeOnCourse);
    }

    private List<Result> GetResultsForIntervalBrackets(List<ResultWithRankOnly> rankOnlyResults)
    {
        var grouping = rankOnlyResults.GroupBy(static oo => new { oo.AthleteCourseId, oo.IntervalId });
        var results = new List<Result>();

        foreach (var resultsByAthlete in grouping)
        {
            var irpsForAthlete = MapToResults(resultsByAthlete.ToList(), resultsByAthlete.Key.AthleteCourseId, false);
            results.AddRange(irpsForAthlete);
        }

        return results;
    }
}
