namespace Api.Orchestration;

public static class DateTimeHelper
{
    public static int GetCurrentAge(DateTime athleteDateOfBirth)
    {
        return DateTime.UtcNow.Year - athleteDateOfBirth.Year;
    }

    /// <summary>
    /// Gets the race age relative to the athlete's DOB and the course startTime.
    /// </summary>
    /// <param name="athleteDateOfBirth">athlete (or any other entity) DOB. Must be passed as first param.</param>
    /// <param name="courseStartTime">course start (or any other dateTime). Must be passed as second param.</param>
    /// <returns>int</returns>
    public static int GetRaceAge(DateTime athleteDateOfBirth, DateTime courseStartTime)
    {
        var differenceInYears = courseStartTime.Year - athleteDateOfBirth.Year;
        var beforeBirthdayAdjuster = CourseStartsBeforeBirthday(courseStartTime, athleteDateOfBirth) ? -1 : 0;
        return differenceInYears + beforeBirthdayAdjuster;
    }

    /// <summary>
    /// If the course starts on Feb-5, and your birthday is Feb-6, this would return true.
    /// If the course starts on Feb-5, and your birthday is Feb-5, this would return false.
    /// </summary>
    /// <returns></returns>
    private static bool CourseStartsBeforeBirthday(DateTime courseStartTime, DateTime dateOfBirth)
    {
        if (courseStartTime.Month < dateOfBirth.Month)
        {
            return true;
        }

        if (courseStartTime.Month > dateOfBirth.Month)
        {
            return false;
        }

        return courseStartTime.Day < dateOfBirth.Day;
    }

    /// <summary>
    /// Returns the date and time as seperate fields in a Tuple so the formatting can be consistent accross the Platform.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static (string dateFormatted, string timeFormatted) GetFormattedFields(DateTime dateTime)
    {
        return (dateTime.ToShortDateString(), dateTime.ToString("hh:mm:ss tt"));
    }

    public static string GetCrossingTime(DateTime courseStartTime, int timeInSeconds)
    {
        return courseStartTime.AddSeconds(timeInSeconds).ToString("h:mm:ss tt");
    }
}
