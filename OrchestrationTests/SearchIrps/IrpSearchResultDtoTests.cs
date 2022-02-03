using Core;
using DataModels;
using Orchestration.SearchIrps;
using System;
using Xunit;

namespace OrchestrationTests.SearchIrps;

public class IrpSearchResultDtoTests
{
	[Fact]
	public void MapsAllFields()
	{
		var athleteCourse = GetAthleteCourse();
		var searchResultDto = IrpSearchResultDtoMapper.GetIrpSearchResultDto(athleteCourse);

		Assert.Equal(1, searchResultDto.AthleteCourseId);
		Assert.Equal("FA LA", searchResultDto.FullName);
		Assert.Equal(10, searchResultDto.RaceAge);
		Assert.Equal("M", searchResultDto.GenderAbbreviated);
		Assert.Equal("BA", searchResultDto.Bib);
		Assert.Equal("SA", searchResultDto.State);
		Assert.Equal("CA", searchResultDto.City);
		Assert.Equal("CA", searchResultDto.CourseName);
	}

	#region test preperation methods

	private static AthleteCourse GetAthleteCourse()
	{
		return new()
		{
			Id = 1,
			Bib = "BA",
			Athlete = new()
			{
				FullName = "FA LA",
				DateOfBirth = new DateTime(2000, 1, 1),
				Gender = Gender.Male,
				State = "SA",
				City = "CA",
			},
			Course = new()
			{
				StartDate = new DateTime(2010, 1, 1),
				Name = "CA",
			}
		};
	}

	#endregion
}
