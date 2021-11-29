using DataModels;
using Orchestration.GetArp;
using Xunit;

namespace OrchestrationTests.GetArp
{
	public class CourseGoalArpDtoTests
	{
		[Fact]
		public void MapsAllFields()
		{
			var courses = GetCourse();
			var result = CourseGoalArpDtoMapper.GetCourseGoalArpDto(courses);

			Assert.Equal(1, result.CourseId);
			Assert.Equal("CA", result.CourseName);
			Assert.Equal(1, result.RaceId);
			Assert.Equal("RA", result.RaceName);
			Assert.Equal("SA", result.RaceSeriesState);
			Assert.Equal("CA", result.RaceSeriesCity);
			Assert.Equal("DA", result.RaceSeriesDescription);
		}

		#region test preperation methods

		private static Course GetCourse()
		{
			return new()
			{
				Id = 1,
				Name = "CA",
				Race = new()
				{
					Id = 1,
					Name = "RA",
					RaceSeries = new()
					{
						State = "SA",
						City = "CA",
						Description = "DA"
					}
				}
			};
		}

		#endregion
	}
}
