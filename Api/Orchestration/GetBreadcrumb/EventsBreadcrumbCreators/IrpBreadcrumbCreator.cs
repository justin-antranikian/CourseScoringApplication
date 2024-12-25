using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.GetBreadcrumb.EventsBreadcrumbCreators;

internal class IrpBreadcrumbCreator : EventsBreadcrumbCreatorBase
{
    public override async Task<EventsBreadcrumbResultDto> GetBreadcrumbResult(BreadcrumbRequestDto breadcrumbRequestDto, ScoringDbContext scoringDbContext)
    {
        var irpId = int.Parse(breadcrumbRequestDto.SearchTerm);
        var athleteCourse = await scoringDbContext.AthleteCourses.SingleAsync(oo => oo.Id == irpId);
        var course = await scoringDbContext.Courses.Include(oo => oo.Race).ThenInclude(oo => oo.RaceSeries).SingleAsync(oo => oo.Id == athleteCourse.CourseId);

        var raceSeries = course.Race.RaceSeries;
        var irpDisplay = new DisplayNameWithIdDto(irpId, athleteCourse.Bib);
        var courseDisplay = GetCourseDisplayName(course);
        var raceDisplay = GetRaceDisplayName(course.Race);
        var raceSeriesDisplay = GetRaceSeriesDisplayName(raceSeries);
        //var locationInfoWithUrl = new LocationInfoWithUrl(raceSeries);
        return new EventsBreadcrumbResultDto(null, raceSeriesDisplay, raceDisplay, courseDisplay, irpDisplay);
    }
}

