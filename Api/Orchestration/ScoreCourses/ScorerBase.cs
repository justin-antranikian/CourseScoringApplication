using Api.DataModels;
using Core;

namespace Api.Orchestration.ScoreCourses;

public abstract class ScorerBase
{
    protected record ResultWithRankOnly : ResultBase
    {
        public BracketType BracketType { get; init; }
    }

    protected record TagReadWithIntervalOrder : TagRead
    {
        public int IntervalOrder { get; set; }

        public TagReadWithIntervalOrder(TagRead read, int intervalOrder) : base(read.CourseId, read.AthleteCourseId, read.IntervalId, read.TimeOnInterval, read.TimeOnCourse)
        {
            IntervalOrder = intervalOrder;
        }
    }

    protected readonly Course _course;
    protected readonly List<Bracket> _brackets;
    protected readonly List<TagReadWithIntervalOrder> _tagReads;
    protected readonly List<AthleteCourseBracket> _atheleteCourseBrackets;
    protected readonly List<Interval> _intervals;

    protected ScorerBase(Course course, List<Bracket> brackets, List<TagRead> tagReads, List<AthleteCourseBracket> atheleteCourseBrackets, List<Interval> intervals)
    {
        _course = course;
        _brackets = brackets;

        var reads = from read in tagReads
                    join iv in intervals on read.IntervalId equals iv.Id
                    select new TagReadWithIntervalOrder(read, iv.Order);

        _tagReads = reads.ToList();
        _atheleteCourseBrackets = atheleteCourseBrackets;
        _intervals = intervals;
    }

    protected IEnumerable<TagReadWithIntervalOrder> FilterReadsByBracket(Bracket bracket)
    {
        if (bracket.BracketType == BracketType.Overall)
        {
            return _tagReads;
        }

        var query = from courseBrackets in _atheleteCourseBrackets
                    join reads in _tagReads on courseBrackets.AthleteCourseId equals reads.AthleteCourseId
                    where courseBrackets.BracketId == bracket.Id
                    select reads;

        return query;
    }

    protected ResultWithRankOnly MapToRankOnlyResult(TagRead read, Bracket bracket, int count)
    {
        return new()
        {
            AthleteCourseId = read.AthleteCourseId,
            CourseId = _course.Id,
            BracketId = bracket.Id,
            TimeOnCourse = read.TimeOnCourse,
            TimeOnInterval = read.TimeOnInterval,
            IntervalId = read.IntervalId,
            Rank = count,
            BracketType = bracket.BracketType
        };
    }

    protected IEnumerable<Result> MapToResults(List<ResultWithRankOnly> results, int athleteCourseId, bool isHighestIntervalCompleted)
    {
        var overallBracket = results.Single(oo => oo.BracketType == BracketType.Overall);
        var genderBracket = results.Single(oo => oo.BracketType == BracketType.Gender);
        var primaryDivision = results.Single(oo => oo.BracketType == BracketType.PrimaryDivision);

        foreach (var result in results)
        {
            var customOrNonPrimaryDivision = result.BracketType == BracketType.NonPrimaryDivision;
            var divisionRank = customOrNonPrimaryDivision ? result.Rank : primaryDivision.Rank;

            yield return new Result
            {
                AthleteCourseId = athleteCourseId,
                CourseId = _course.Id,
                BracketId = result.BracketId,
                TimeOnCourse = result.TimeOnCourse,
                TimeOnInterval = result.TimeOnInterval,
                IntervalId = result.IntervalId,
                Rank = result.Rank,
                OverallRank = overallBracket.Rank,
                GenderRank = genderBracket.Rank,
                DivisionRank = divisionRank,
                IsHighestIntervalCompleted = isHighestIntervalCompleted
            };
        }
    }
}