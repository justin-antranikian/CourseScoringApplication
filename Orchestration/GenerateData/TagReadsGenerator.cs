namespace Orchestration.GenerateData;

public static class TagReadsGenerator
{
	public static List<TagRead> GetTagReads(List<Interval> allIntervals, List<AthleteCourse> athleteCourses)
	{
		return GetTagReadsAsEnumerable(allIntervals, athleteCourses).ToList();
	}

	private static IEnumerable<TagRead> GetTagReadsAsEnumerable(List<Interval> allIntervals, List<AthleteCourse> athleteCourses)
	{
		var reads = new List<TagRead>();
		var random = new Random();
		var intervalsDictionary = allIntervals.GroupBy(oo => oo.CourseId).ToDictionary(key => key.Key, value => value.ToList());

		foreach (var grouping in athleteCourses.GroupBy(oo => oo.CourseId))
		{
			var intervalsForCourse = intervalsDictionary[grouping.Key];
			var fullCourseInterval = intervalsForCourse.Single(oo => oo.IsFullCourse);
			var nonCourseIntervals = intervalsForCourse.Except(new[] { fullCourseInterval }).OrderBy(oo => oo.Order).ToList();
			var lastNonCourseInterval = nonCourseIntervals.Last();

			foreach (var athleteCourse in grouping)
			{
				var cumulativeTime = 0;
				var randomIntervals = random.Next(1, nonCourseIntervals.Count + 1);
				var filteredRandomIntervals = nonCourseIntervals.Take(randomIntervals);

				foreach (var interval in filteredRandomIntervals)
				{
					var intervalTime = random.Next(2000, 4001);
					cumulativeTime += intervalTime;
					yield return new TagRead(grouping.Key, athleteCourse.Id, interval.Id, intervalTime, cumulativeTime);
				}

				if (!filteredRandomIntervals.Contains(lastNonCourseInterval))
				{
					continue;
				}

				var fullCourseRead = new TagRead(grouping.Key, athleteCourse.Id, fullCourseInterval.Id, cumulativeTime, cumulativeTime);
				yield return fullCourseRead;
			}
		}
	}
}
