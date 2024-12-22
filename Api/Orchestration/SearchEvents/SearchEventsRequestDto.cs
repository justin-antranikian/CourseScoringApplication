using Api.DataModels;

namespace Api.Orchestration.SearchEvents;

public class SearchEventsRequestDto
{
    public string? SearchTerm { get; set; }

    public List<RaceSeriesType> RaceSeriesTypes { get; set; }

    public string? State { get; set; }

    public string? Area { get; set; }

    public string? City { get; set; }

    public SearchEventsRequestDto()
    {
        RaceSeriesTypes = new List<RaceSeriesType>();
    }

    public SearchEventsRequestDto(params RaceSeriesType[] raceSeriesTypes)
    {
        RaceSeriesTypes = raceSeriesTypes.ToList();
    }
}
