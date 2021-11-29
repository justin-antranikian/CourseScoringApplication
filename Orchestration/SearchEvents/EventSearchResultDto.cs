using Core;
using DataModels;
using System.Collections.Generic;
using System.Linq;

namespace Orchestration.GetRaceSeriesSearch
{
	public static class EventSearchResultDtoMapper
	{
		public static List<EventSearchResultDto> GetEventSearchResultDto(List<RaceSeries> raceSeriesEntries)
		{
			return raceSeriesEntries.Select(GetEventSearchResultDto).ToList();
		}

		public static EventSearchResultDto GetEventSearchResultDto(RaceSeries raceSeries)
		{
			var upcomingRace = raceSeries.Races.OrderByDescending(oo => oo.KickOffDate).First();
			var courses = upcomingRace.Courses.Select(oo => new DisplayNameWithIdDto(oo.Id, oo.Name)).ToList();
			var (dateFormatted, timeFormatted) = DateTimeHelper.GetFormattedFields(upcomingRace.KickOffDate);

			var eventSearchResultDto = new EventSearchResultDto
			(
				raceSeries.Id,
				raceSeries.Name,
				raceSeries.RaceSeriesType,
				raceSeries.RaceSeriesType.ToFriendlyText(),
				upcomingRace.Id,
				dateFormatted,
				timeFormatted,
				raceSeries.Description,
				courses,
				new LocationInfoWithRank(raceSeries),
				raceSeries.Rating
			);

			return eventSearchResultDto;
		}
	}

	public class EventSearchResultDto
	{
		public int Id { get; }

		public string Name { get; }

		public RaceSeriesType RaceSeriesType { get; }

		public string RaceSeriesTypeName { get; }

		public int UpcomingRaceId { get; }

		public string KickOffDate { get; }

		public string KickOffTime { get; }

		public string Description { get; }

		public List<DisplayNameWithIdDto> Courses { get; }

		public LocationInfoWithRank LocationInfoWithRank { get; }

		public int Rating { get; }

		public EventSearchResultDto
		(
			int id,
			string name,
			RaceSeriesType raceSeriesType,
			string raceSeriesTypeName,
			int upcomingRaceId,
			string kickOffDate,
			string kickOffTime,
			string description,
			List<DisplayNameWithIdDto> courses,
			LocationInfoWithRank locationInfoWithRank,
			int rating
		)
		{
			Id = id;
			Name = name;
			RaceSeriesType = raceSeriesType;
			RaceSeriesTypeName = raceSeriesTypeName;
			UpcomingRaceId = upcomingRaceId;
			KickOffDate = kickOffDate;
			KickOffTime = kickOffTime;
			Description = description;
			Courses = courses;
			LocationInfoWithRank = locationInfoWithRank;
			Rating = rating;
		}
	}
}
