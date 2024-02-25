namespace Orchestration.GetCompetetorsForIrp;

public class IrpCompetetorDto : AthleteResultBase
{
    public int AthleteCourseId { get; }

    public string FirstName { get; }

    public IrpCompetetorDto
    (
        int athleteId,
        string fullName,
        DateTime dateOfBirth,
        Gender gender,
        string bib,
        DateTime courseStartTime,
        PaceWithTime paceWithTimeCumulative,
        int athleteCourseId,
        string firstName
    ) : base
    (
        athleteId,
        fullName,
        dateOfBirth,
        gender,
        bib,
        courseStartTime,
        paceWithTimeCumulative
    )
    {
        FirstName = firstName;
        AthleteCourseId = athleteCourseId;
    }
}
