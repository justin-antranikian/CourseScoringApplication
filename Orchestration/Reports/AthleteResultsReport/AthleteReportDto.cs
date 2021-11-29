using Core;
using System;
using System.Collections.Generic;

namespace Orchestration.Reports.AthleteResultsReport
{
	public record AthleteReportDto(int AthleteId, string FirstName, string LastName, string FullName, List<AthleteReportCourseDto> AthleteReportCourseDtos);

	public record AthleteReportCourseDto(int CourseId, string CourseName, string RaceSeriesName, DateTime KickOffDate, double CourseDistance, RaceSeriesType RaceSeriesType);
}
