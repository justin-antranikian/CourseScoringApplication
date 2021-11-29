using Core;
using DataModels;
using System.Collections.Generic;
using System.Linq;

namespace Orchestration.SearchIrps
{
	public static class IrpSearchResultDtoMapper
	{
		public static List<IrpSearchResultDto> GetIrpSearchResultDto(List<AthleteCourse> athleteCourses)
		{
			return athleteCourses.Select(GetIrpSearchResultDto).ToList();
		}

		public static IrpSearchResultDto GetIrpSearchResultDto(AthleteCourse athleteCourse)
		{
			var athlete = athleteCourse.Athlete;
			var course = athleteCourse.Course;

			return new
			(
				athleteCourse.Id,
				athlete.FullName,
				DateTimeHelper.GetRaceAge(athlete.DateOfBirth, course.StartDate),
				athlete.Gender.ToAbbreviation(),
				athleteCourse.Bib,
				athlete.State,
				athlete.City,
				course.Name
			);
		}
	}

	public record IrpSearchResultDto
	(
		int AthleteCourseId,
		string FullName,
		int RaceAge,
		string GenderAbbreviated,
		string Bib,
		string State,
		string City,
		string CourseName
	);
}
