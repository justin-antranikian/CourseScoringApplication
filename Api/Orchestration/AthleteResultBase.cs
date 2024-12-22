using Api.DataModels;

namespace Api.Orchestration;

public abstract class AthleteResultBase(
    int athleteId,
    string fullName,
    DateTime dateOfBirth,
    Gender gender,
    string bib,
    DateTime courseStartTime,
    PaceWithTime paceWithTimeCumulative)
{
    public int AthleteId { get; } = athleteId;
    public string FullName { get; } = fullName;
    public int RaceAge { get; } = DateTimeHelper.GetRaceAge(dateOfBirth, courseStartTime);
    public string GenderAbbreviated { get; } = gender.ToAbbreviation();
    public string Bib { get; } = bib;
    public PaceWithTime PaceWithTimeCumulative { get; } = paceWithTimeCumulative;
}
