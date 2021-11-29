using Core;
using DataModels;
using System;

namespace Orchestration.GetCompetetorsForIrp
{
	public static class IrpCompetetorDtoMapper
	{
		public static IrpCompetetorDto GetIrpCompetetorDto(Result result, Course course, Interval highestIntervalCompleted)
		{
			var athleteCourse = result.AthleteCourse;
			var athlete = athleteCourse.Athlete;
			var paceWithTimeCumulative = course.GetPaceWithTime(result.TimeOnCourse, highestIntervalCompleted.DistanceFromStart);

			return new
			(
				athlete.Id,
				athlete.FullName,
				athlete.DateOfBirth,
				athlete.Gender,
				athleteCourse.Bib,
				course.StartDate,
				paceWithTimeCumulative,
				athleteCourse.Id,
				athlete.FirstName
			);
		}
	}

	public class IrpCompetetorDto : AthleteResultBase
	{
		public int AthleteCourseId { get; }

		public string FirstName { get; }

		public IrpCompetetorDto
		(
			int athleteId,
			string fullName,
			DateTime dateOfBirth,
			Gender gender,
			string bib,
			DateTime courseStartTime,
			PaceWithTime paceWithTimeCumulative,
			int athleteCourseId,
			string firstName
		) : base
		(
			athleteId,
			fullName,
			dateOfBirth,
			gender,
			bib,
			courseStartTime,
			paceWithTimeCumulative
		)
		{
			FirstName = firstName;
			AthleteCourseId = athleteCourseId;
		}
	}
}
