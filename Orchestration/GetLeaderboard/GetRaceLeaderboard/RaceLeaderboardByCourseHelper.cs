﻿using DataModels;
using System.Collections.Generic;
using System.Linq;

namespace Orchestration.GetLeaderboard.GetRaceLeaderboard
{
	internal class RaceLeaderboardByCourseHelper
	{
		private readonly List<Interval> _highestCompletedIntervalsForAllCourses;

		private readonly List<Result> _resultsForAllCourses;

		public RaceLeaderboardByCourseHelper(List<Interval> highestCompletedIntervalsForAllCourses, List<Result> resultsForAllCourses)
		{
			_highestCompletedIntervalsForAllCourses = highestCompletedIntervalsForAllCourses;
			_resultsForAllCourses = resultsForAllCourses;
		}

		/// <summary>
		/// Takes a list a list of courses and returns a list of TopThreeAthletesForCourseDtos.
		/// </summary>
		/// <param name="courses"></param>
		/// <returns></returns>
		public IEnumerable<RaceLeaderboardByCourseDto> GetResults(List<Course> courses)
		{
			foreach (var course in courses)
			{
				var interval = _highestCompletedIntervalsForAllCourses.Single(oo => oo.CourseId == course.Id);
				var leaderboardResultDtos = GetLeaderboardResultDtos(course, interval).ToList();
				yield return new RaceLeaderboardByCourseDto(course.Id, course.Name, course.SortOrder, interval.Name, interval.IntervalType, leaderboardResultDtos);
			}
		}

		private IEnumerable<LeaderboardResultDto> GetLeaderboardResultDtos(Course course, Interval intervalForCourse)
		{
			var resultsForCourse = _resultsForAllCourses.Where(oo => oo.IntervalId == intervalForCourse.Id);

			foreach (var result in resultsForCourse)
			{
				var distanceCompleted = intervalForCourse.DistanceFromStart;
				var timeOnCoursePace = course.GetPaceWithTime(result.TimeOnCourse, distanceCompleted);
				var athlete = result.AthleteCourse.Athlete;
				yield return LeaderboardResultDtoMapper.GetLeaderboardResultDto(result, athlete, timeOnCoursePace, course);
			}
		}
	}
}
