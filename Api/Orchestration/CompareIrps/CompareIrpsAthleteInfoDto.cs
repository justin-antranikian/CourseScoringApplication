using Api.DataModels;

namespace Api.Orchestration.CompareIrps;

public record FinishInfo(string FinishTime, PaceWithTime PaceWithTimeCumulative);

public record CompareIrpsAthleteInfoDto
{
    public int AthleteId { get; }

    public string FullName { get; }

    public int RaceAge { get; }

    public string GenderAbbreviated { get; }

    public string Bib { get; }

    public CompareIrpsRank CompareIrpsRank { get; }

    public string City { get; }

    public string State { get; }

    public int AthleteCourseId { get; }

    public FinishInfo? FinishInfo { get; }

    public List<CompareIrpsIntervalDto> CompareIrpsIntervalDtos { get; }

    public CompareIrpsAthleteInfoDto
    (
        int athleteId,
        string fullName,
        DateTime dateOfBirth,
        Gender gender,
        string bib,
        DateTime courseStartTime,
        CompareIrpsRank compareIrpsRank,
        string city,
        string state,
        int athleteCourseId,
        FinishInfo finishInfo,
        List<CompareIrpsIntervalDto> compareIrpsIntervalDtos
    )
    {
        AthleteId = athleteId;
        FullName = fullName;
        Bib = bib;
        GenderAbbreviated = gender.ToAbbreviation();
        RaceAge = DateTimeHelper.GetRaceAge(dateOfBirth, courseStartTime);
        CompareIrpsRank = compareIrpsRank;
        City = city;
        State = state;
        AthleteCourseId = athleteCourseId;
        FinishInfo = finishInfo;
        CompareIrpsIntervalDtos = compareIrpsIntervalDtos;
    }
}

