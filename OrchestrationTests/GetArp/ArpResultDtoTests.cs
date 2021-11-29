using Core;
using DataModels;
using Orchestration;
using Orchestration.GetArp;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OrchestrationTests.GetArp
{
	public class ArpResultDtoTests
	{
		[Fact]
		public void MapsAllFields()
		{
			var result = new ResultWithBracketType(1, 1, BracketType.Gender, 1, 1, 1000, 4, 3, 2);
			var course = GetCourse();
			var getTotalHelper = GetTotalHelper();
			var arpDto = ArpResultDtoMapper.GetArpResultDto(result, course, null, getTotalHelper);

			Assert.Equal(1, arpDto.AthleteCourseId);
			Assert.Equal(1, arpDto.RaceId);
			Assert.Equal("RA", arpDto.RaceName);
			Assert.Equal(RaceSeriesType.RoadBiking, arpDto.RaceSeriesType);
			Assert.Equal(1, arpDto.CourseId);
			Assert.Equal("CA", arpDto.CourseName);
			Assert.Equal("SA", arpDto.State);
			Assert.Equal("CA", arpDto.City);

			Assert.Equal(4, arpDto.OverallRank);
			Assert.Equal(3, arpDto.GenderRank);
			Assert.Equal(2, arpDto.PrimaryDivisionRank);
			Assert.Equal(40, arpDto.OverallCount);
			Assert.Equal(30, arpDto.GenderCount);
			Assert.Equal(20, arpDto.PrimaryDivisionCount);

			Assert.Null(arpDto.PaceWithTimeCumulative);
		}

		#region test preperation methods

		private static MetadataGetTotalHelper GetTotalHelper()
		{
			var brackets = new List<Bracket>
			{
				new() { Id = 1, BracketType = BracketType.Overall },
				new() { Id = 2, BracketType = BracketType.Gender },
				new() { Id = 3, BracketType = BracketType.PrimaryDivision },
			};

			var bracketMetadataEntries = new List<BracketMetadata>
			{
				new() { BracketId = 1, TotalRacers = 40 },
				new() { BracketId = 2, TotalRacers = 30 },
				new() { BracketId = 3, TotalRacers = 20 },
			};

			return new MetadataGetTotalHelper(bracketMetadataEntries, brackets);
		}

		private static Course GetCourse()
		{
			return new Course
			{
				Id = 1,
				Name = "CA",
				RaceId = 1,
				Race = new()
				{
					Id = 1,
					Name = "RA",
					RaceSeries = new()
					{
						Id = 1,
						RaceSeriesType = RaceSeriesType.RoadBiking,
						State = "SA",
						City = "CA",
					}
				}
			};
		}

		#endregion
	}
}
