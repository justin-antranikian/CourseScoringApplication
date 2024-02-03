namespace Orchestration;

public abstract class AthleteResultBase
{
    public int AthleteId { get; }

    public string FullName { get; }

    public int RaceAge { get; }

    public string GenderAbbreviated { get; }

    public string Bib { get; }

    public PaceWithTime PaceWithTimeCumulative { get; }

    protected AthleteResultBase
    (
        int athleteId,
        string fullName,
        DateTime dateOfBirth,
        Gender gender,
        string bib,
        DateTime courseStartTime,
        PaceWithTime paceWithTimeCumulative
    )
    {
        AthleteId = athleteId;
        FullName = fullName;
        Bib = bib;
        PaceWithTimeCumulative = paceWithTimeCumulative;
        GenderAbbreviated = gender.ToAbbreviation();
        RaceAge = DateTimeHelper.GetRaceAge(dateOfBirth, courseStartTime);
    }
}
