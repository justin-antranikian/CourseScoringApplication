using Core;
using DataModels;
using Orchestration.GetIrp;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OrchestrationTests.GetIrp
{
	public class IrpDtoTests
	{
		[Fact]
		public void MapsAllFields()
		{
			var irpDto = GetIrpDto();

			Assert.Equal(1, irpDto.AthleteId);
			Assert.Equal("FA LA", irpDto.FullName);
			Assert.Equal(10, irpDto.RaceAge);
			Assert.Equal("M", irpDto.GenderAbbreviated);
			Assert.Equal("BA", irpDto.Bib);

			Assert.Equal("01:01", irpDto.PaceWithTimeCumulative.TimeFormatted);
			Assert.False(irpDto.PaceWithTimeCumulative.HasPace);
			Assert.Null(irpDto.PaceWithTimeCumulative.PaceValue);
			Assert.Null(irpDto.PaceWithTimeCumulative.PaceLabel);

			Assert.Equal("FA", irpDto.FirstName);
			Assert.Equal("RA", irpDto.RaceName);
			Assert.Equal(1, irpDto.CourseId);
			Assert.Equal("CA", irpDto.CourseName);
			Assert.Equal(1000, irpDto.CourseDistance);
			Assert.Equal(RaceSeriesType.Triathalon, irpDto.RaceSeriesType);
			Assert.Equal("PST", irpDto.TimeZoneAbbreviated);
			Assert.Null(irpDto.FinishTime);
			Assert.Equal("1/1/2010", irpDto.CourseDate);
			Assert.Equal("12:00:00 AM", irpDto.CourseTime);
			Assert.Empty(irpDto.Tags);
			Assert.Equal("CA", irpDto.RaceSeriesCity);
			Assert.Equal("SA", irpDto.RaceSeriesState);
			Assert.Equal("DA", irpDto.RaceSeriesDescription);
			Assert.Empty(irpDto.TrainingList);
			Assert.Null(irpDto.CourseGoalDescription);
			Assert.Null(irpDto.PersonalGoalDescription);

			Assert.Equal("CA", irpDto.LocationInfoWithRank.City);
			Assert.Equal("AA", irpDto.LocationInfoWithRank.Area);
			Assert.Equal("SA", irpDto.LocationInfoWithRank.State);
			Assert.Equal("ca", irpDto.LocationInfoWithRank.CityUrl);
			Assert.Equal("aa", irpDto.LocationInfoWithRank.AreaUrl);
			Assert.Equal("sa", irpDto.LocationInfoWithRank.StateUrl);
			Assert.Equal(1, irpDto.LocationInfoWithRank.CityRank);
			Assert.Equal(2, irpDto.LocationInfoWithRank.AreaRank);
			Assert.Equal(3, irpDto.LocationInfoWithRank.StateRank);
			Assert.Equal(4, irpDto.LocationInfoWithRank.OverallRank);

			Assert.Empty(irpDto.BracketResults);

			Assert.Collection(irpDto.IntervalResults, result =>
			{
				Assert.Equal("full course", result.IntervalName);
				Assert.True(result.IsFullCourse);
			});
		}

		#region test preperation methods

		private static IrpDto GetIrpDto()
		{
			var athlete = new Athlete
			{
				Id = 1,
				FirstName = "FA",
				LastName = "LA",
				FullName = "FA LA",
				Gender = Gender.Male,
				City = "CA",
				State = "SA",
				Area = "AA",
				OverallRank = 4,
				StateRank = 3,
				AreaRank = 2,
				CityRank = 1,
				DateOfBirth = new DateTime(2000, 1, 1)
			};

			var athleteCourse = new AthleteCourse
			{
				AthleteId = 1,
				Bib = "BA",
				Athlete = athlete
			};

			var course = new Course
			{
				Id = 1,
				Name = "CA",
				StartDate = new DateTime(2010, 1, 1),
				Distance = 1000,
				RaceId = 1,
				Race = new()
				{
					Name = "RA",
					RaceSeries = new()
					{
						RaceSeriesType = RaceSeriesType.Triathalon,
						State = "SA",
						City = "CA",
						Description = "DA"
					},
					TimeZoneId = "Pacific Standard Time"
				}
			};

			var paceWithTime = new PaceWithTime("01:01", false);

			var intervalResults = new IrpResultByIntervalDto[]
			{
				new
				(
					"full course",
					IntervalType.Bike,
					false,
					null,
					null,
					null,
					null,
					null,
					0,
					0,
					0,
					BetweenIntervalTimeIndicator.NotStarted,
					BetweenIntervalTimeIndicator.NotStarted,
					BetweenIntervalTimeIndicator.NotStarted,
					null,
					true,
					"",
					"",
					0.0,
					0.0
				)
			};

			return IrpDtoMapper.GetIrpDto(athleteCourse, course, paceWithTime, new List<IrpResultByBracketDto>(), intervalResults.ToList());
		}

		#endregion
	}
}
