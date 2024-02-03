using System.Threading.Tasks;

namespace Orchestration.GetBreadcrumb.EventsBreadcrumbCreators;

internal abstract class EventsBreadcrumbCreatorBase
{
    public abstract Task<EventsBreadcrumbResultDto> GetBreadcrumbResult(BreadcrumbRequestDto breadcrumbRequestDto, ScoringDbContext scoringDbContext);

    protected DisplayNameWithIdDto GetRaceSeriesDisplayName(RaceSeries raceSeries)
    {
        return new DisplayNameWithIdDto(raceSeries.Id, raceSeries.Name);
    }

    protected DisplayNameWithIdDto GetRaceDisplayName(Race race)
    {
        return new DisplayNameWithIdDto(race.Id, race.KickOffDate.ToShortDateString());
    }

    protected DisplayNameWithIdDto GetCourseDisplayName(Course course)
    {
        return new DisplayNameWithIdDto(course.Id, course.Name);
    }
}
