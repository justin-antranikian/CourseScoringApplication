﻿using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.GetBreadcrumb.EventsBreadcrumbCreators;

internal class CourseBreadcrumbCreator : EventsBreadcrumbCreatorBase
{
    public sealed override async Task<EventsBreadcrumbResultDto> GetBreadcrumbResult(BreadcrumbRequestDto breadcrumbRequestDto, ScoringDbContext scoringDbContext)
    {
        var courseId = int.Parse(breadcrumbRequestDto.SearchTerm);
        var course = await scoringDbContext.Courses.Include(oo => oo.Race).ThenInclude(oo => oo.RaceSeries).SingleAsync(oo => oo.Id == courseId);

        var raceSeries = course.Race.RaceSeries;
        var courseDisplay = GetCourseDisplayName(course);
        var raceDisplay = GetRaceDisplayName(course.Race);
        var raceSeriesDisplay = GetRaceSeriesDisplayName(raceSeries);
        var locationInfoWithUrl = new LocationInfoWithUrl(raceSeries);
        return new EventsBreadcrumbResultDto(locationInfoWithUrl, raceSeriesDisplay, raceDisplay, courseDisplay);
    }
}
