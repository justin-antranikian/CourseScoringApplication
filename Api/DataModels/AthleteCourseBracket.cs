﻿namespace Api.DataModels;

public class AthleteCourseBracket
{
    public int Id { get; set; }

    public int AthleteCourseId { get; set; }
    public int BracketId { get; set; }
    public int CourseId { get; set; }

    public AthleteCourse? AthleteCourse { get; set; }
    public Bracket? Bracket { get; set; }
    public Course? Course { get; set; }

    public static AthleteCourseBracket Create(int athleteCourseId, int bracketId, int courseId)
    {
        return new AthleteCourseBracket
        {
            AthleteCourseId = athleteCourseId,
            BracketId = bracketId,
            CourseId = courseId
        };
    }
}
