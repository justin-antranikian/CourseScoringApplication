using Core;
using DataModels;
using System.Collections.Generic;

namespace Orchestration.GetLeaderboard.GetCourseLeaderboard
{
	public static class CourseLeaderboardDtoMapper
	{
		public static CourseLeaderboardDto GetCourseLeaderboardDto(Course course, Race race, CourseMetadata courseMetadata, List<CourseLeaderboardByIntervalDto> leaderboards)
		{
			var (dateFormatted, timeFormatted) = DateTimeHelper.GetFormattedFields(course.StartDate);
			var raceSeries = race.RaceSeries;

			var courseLeaderboardDto = new CourseLeaderboardDto
			(
				course.RaceId,
				race.Name,
				race.RaceSeriesId,
				raceSeries.Description,
				raceSeries.RaceSeriesType,
				new LocationInfoWithRank(raceSeries),
				course.Name,
				dateFormatted,
				timeFormatted,
				course.Distance,
				courseMetadata,
				leaderboards
			);

			return courseLeaderboardDto;
		}
	}

	/// <summary>
	/// For a course leaderboard we split the results up by interval.
	/// </summary>
	public class CourseLeaderboardDto
	{
		public int RaceId { get; }

		public string RaceName { get; }

		public int RaceSeriesId { get; }

		public string RaceSeriesDescription { get; }

		public RaceSeriesType RaceSeriesType { get; }

		public LocationInfoWithRank LocationInfoWithRank { get; }

		public string CourseName { get; }

		public string CourseDate { get; }

		public string CourseTime { get; }

		public double CourseDistance { get; }

		public CourseMetadata CourseMetadata { get; }

		public List<CourseLeaderboardByIntervalDto> Leaderboards { get; }

		public CourseLeaderboardDto
		(
			int raceId,
			string raceName,
			int raceSeriesId,
			string raceSeriesDescription,
			RaceSeriesType raceSeriesType,
			LocationInfoWithRank locationInfoWithRank,
			string courseName,
			string courseDate,
			string courseTime,
			double courseDistance,
			CourseMetadata courseMetadata,
			List<CourseLeaderboardByIntervalDto> leaderboards
		)
		{
			RaceId = raceId;
			RaceName = raceName;
			RaceSeriesId = raceSeriesId;
			RaceSeriesDescription = raceSeriesDescription;
			RaceSeriesType = raceSeriesType;
			LocationInfoWithRank = locationInfoWithRank;
			CourseName = courseName;
			CourseDate = courseDate;
			CourseTime = courseTime;
			CourseDistance = courseDistance;
			CourseMetadata = courseMetadata;
			Leaderboards = leaderboards;
		}
	}
}
