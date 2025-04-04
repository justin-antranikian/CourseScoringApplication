﻿namespace Api.DataModels;

public enum BracketType
{
    Overall,
    Gender,
    PrimaryDivision,
    NonPrimaryDivision,
}

public class Bracket
{
    public int Id { get; set; }
    public int CourseId { get; set; }

    public required BracketType BracketType { get; set; }
    public required string Name { get; set; }

    public List<AthleteCourseBracket> AthleteCourseBrackets { get; set; } = [];
    public List<BracketMetadata> BracketMetadatas { get; set; } = [];
    public Course? Course { get; set; }
    public List<Result> Results { get; set; } = [];

    public static Bracket Create(int courseId, string name, BracketType bracketType)
    {
        return new Bracket
        {
            CourseId = courseId,
            BracketType = bracketType,
            Name = name
        };
    }
}
