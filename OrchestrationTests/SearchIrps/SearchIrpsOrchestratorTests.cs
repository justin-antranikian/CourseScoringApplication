using DataModels;
using Orchestration.SearchIrps;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OrchestrationTests.SearchIrps;

public class SearchIrpsOrchestratorTests
{
	private SearchIrpsRequestDto _baseRequest = new() { CourseId = 1, SearchOn = SearchOnField.Bib, SearchTerm = "A" };

	[Fact]
	public async Task FilterOnBib()
	{
		var orchestrator = GetOrchestrator();
		var searchResults = await orchestrator.GetSearchResults(_baseRequest);

		Assert.Equal(new[] { 1, 2, 4, 3 }, GetIds(searchResults));
	}

	[Fact]
	public async Task FilterOnFirstName()
	{
		var orchestrator = GetOrchestrator();
		var request = _baseRequest with { SearchOn = SearchOnField.FirstName, SearchTerm = "FFF" };
		var searchResults = await orchestrator.GetSearchResults(request);

		Assert.Equal(new[] { 2 }, GetIds(searchResults));
	}

	[Fact]
	public async Task FilterOnLastName()
	{
		var orchestrator = GetOrchestrator();
		var request = _baseRequest with { SearchOn = SearchOnField.LastName, SearchTerm = "CC" };
		var searchResults = await orchestrator.GetSearchResults(request);

		Assert.Equal(new[] { 4 }, GetIds(searchResults));
	}

	[Fact]
	public async Task FilterOnFullName()
	{
		var orchestrator = GetOrchestrator();
		var request = _baseRequest with { SearchOn = SearchOnField.FullName, SearchTerm = "FFF B" };
		var searchResults = await orchestrator.GetSearchResults(request);

		Assert.Equal(new[] { 2 }, GetIds(searchResults));
	}

	[Fact]
	public async Task FilterOnRaceId()
	{
		var orchestrator = GetOrchestrator();
		var request = _baseRequest with { RaceId = 1 };
		var searchResults = await orchestrator.GetSearchResults(request);

		Assert.Equal(new[] { 1, 2, 4, 3, 5 }, GetIds(searchResults));
	}

	#region test preperation methods

	private static int[] GetIds(List<IrpSearchResultDto> searchResults)
	{
		return searchResults.Select(oo => oo.AthleteCourseId).ToArray();
	}

	private static SearchIrpsOrchestrator GetOrchestrator()
	{
		var dbContext = ScoringDbContextCreator.GetEmptyDbContext();

		var firstCourse = new Course { Id = 1, RaceId = 1 };
		var otherCourse = new Course { Id = 2, RaceId = 1 };
		var otherRaceCourse = new Course { Id = 3, RaceId = 2 };

		var athleteCourses = new[]
		{
			new AthleteCourse
			{
				Id = 1,
				Bib = "A",
				Athlete = new()
				{
					FirstName = "FF",
					LastName = "AAA",
					FullName = "FF AAA"
				},
				Course = firstCourse
			},
			new AthleteCourse
			{
				Id = 2,
				Bib = "AA",
				Athlete = new()
				{
					FirstName = "FFF",
					LastName = "BBB",
					FullName = "FFF BBB"
				},
				Course = firstCourse
			},
			new AthleteCourse
			{
				Id = 3,
				Bib = "AAA",
				Athlete = new()
				{
					FirstName = "QQQ",
					LastName = "DDD",
					FullName = "QQQ DDD"
				},
				Course = firstCourse
			},
			new AthleteCourse
			{
				Id = 4,
				Bib = "AAAA",
				Athlete = new()
				{
					FirstName = "QQQ",
					LastName = "CCC",
					FullName = "QQQ CCC"
				},
				Course = firstCourse
			},
			new AthleteCourse
			{
				Id = 5,
				Bib = "AAAA",
				Athlete = new()
				{
					FirstName = "QQQ",
					LastName = "EEE",
					FullName = "QQQ CCC"
				},
				Course = otherCourse
			},
			new AthleteCourse
			{
				Id = 6,
				Bib = "AAAA",
				Athlete = new()
				{
					FirstName = "QQQ",
					LastName = "CCC",
					FullName = "QQQ CCC"
				},
				Course = otherRaceCourse
			}
		};

		dbContext.AthleteCourses.AddRange(athleteCourses);
		dbContext.SaveChanges();
		return new SearchIrpsOrchestrator(dbContext);
	}

	#endregion
}
