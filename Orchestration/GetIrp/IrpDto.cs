namespace Orchestration.GetIrp;

public static class IrpDtoMapper
{
	public static IrpDto GetIrpDto(AthleteCourse athleteCourse, Course course, PaceWithTime paceWithTime, List<IrpResultByBracketDto> bracketResults, List<IrpResultByIntervalDto> intervalResults)
	{
		var athlete = athleteCourse.Athlete;
		var race = course.Race;
		var raceSeries = race.RaceSeries;

		var (courseDate, courseTime) = DateTimeHelper.GetFormattedFields(course.StartDate);
		var trainingList = athleteCourse.GetTrainingList();
		var finishTime = intervalResults.Single(oo => oo.IsFullCourse).CrossingTime;

		var irpDto = new IrpDto
		(
			athlete.Id,
			athlete.FullName,
			athlete.DateOfBirth,
			athlete.Gender,
			athleteCourse.Bib,
			course.StartDate,
			paceWithTime,
			athlete.FirstName,
			race.Name,
			course.Id,
			course.Name,
			course.Distance,
			raceSeries.RaceSeriesType,
			race.TimeZoneId.ToAbbreviation(),
			finishTime,
			courseDate,
			courseTime,
			athlete.GetTags(),
			raceSeries.City,
			raceSeries.State,
			raceSeries.Description,
			trainingList,
			athleteCourse.CourseGoalDescription,
			athleteCourse.PersonalGoalDescription,
			new LocationInfoWithRank(athlete),
			bracketResults,
			intervalResults
		);

		return irpDto;
	}
}

/// <summary>
/// Irp (Individual result projection) is used to populate the irp page.
/// </summary>
public class IrpDto : AthleteResultBase
{
	public string FirstName { get; }

	public string RaceName { get; }

	public int CourseId { get; }

	public string CourseName { get; }

	public double CourseDistance { get; }

	public RaceSeriesType RaceSeriesType { get; }

	public string TimeZoneAbbreviated { get; }

	public string? FinishTime { get; }

	public string CourseDate { get; }

	public string CourseTime { get; }

	public List<string> Tags { get; }

	public string RaceSeriesCity { get; }

	public string RaceSeriesState { get; }

	public string RaceSeriesDescription { get; }

	public List<string> TrainingList { get; }

	public string CourseGoalDescription { get; }

	public string PersonalGoalDescription { get; }

	public LocationInfoWithRank LocationInfoWithRank { get; }
		
	public List<IrpResultByBracketDto> BracketResults { get; }

	public List<IrpResultByIntervalDto> IntervalResults { get; }

	public IrpDto
	(
		int athleteId,
		string fullName,
		DateTime dateOfBirth,
		Gender gender,
		string bib,
		DateTime courseStartTime,
		PaceWithTime paceWithTimeCumulative,
		string firstName,
		string raceName,
		int courseId,
		string courseName,
		double courseDistance,
		RaceSeriesType raceSeriesType,
		string timeZoneAbbreviated,
		string finishTime,
		string courseDate,
		string courseTime,
		List<string> tags,
		string raceSeriesCity,
		string raceSeriesState,
		string raceSeriesDescription,
		List<string> trainingList,
		string courseGoalDescription,
		string personalGoalDescription,
		LocationInfoWithRank locationInfoWithRank,
		List<IrpResultByBracketDto> bracketResults,
		List<IrpResultByIntervalDto> intervalResults
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
		RaceName = raceName;
		CourseId = courseId;
		CourseName = courseName;
		CourseDistance = courseDistance;
		RaceSeriesType = raceSeriesType;
		TimeZoneAbbreviated = timeZoneAbbreviated;
		FinishTime = finishTime;
		CourseDate = courseDate;
		CourseTime = courseTime;
		Tags = tags;
		RaceSeriesCity = raceSeriesCity;
		RaceSeriesState = raceSeriesState;
		RaceSeriesDescription = raceSeriesDescription;
		TrainingList = trainingList;
		CourseGoalDescription = courseGoalDescription;
		PersonalGoalDescription = personalGoalDescription;
		LocationInfoWithRank = locationInfoWithRank;
		BracketResults = bracketResults;
		IntervalResults = intervalResults;
	}
}
