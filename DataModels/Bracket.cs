using Core;
using System.Collections.Generic;

namespace DataModels;

public record Bracket
{
    public int Id { get; init; }
    public int CourseId { get; init; }

    public BracketType BracketType { get; init; }
    public string Name { get; init; }

    public List<AthleteCourseBracket> AthleteCourseBrackets { get; set; } = [];
    public List<BracketMetadata> BracketMetadatas { get; init; } = [];
    public Course Course { get; set; }
    public List<Result> Results { get; set; } = [];

    public Bracket(string name, BracketType bracketType) => (Name, BracketType) = (name, bracketType);

    public Bracket(int courseId, string name, BracketType bracketType) : this(name, bracketType) => CourseId = courseId;

    public Bracket() { }
}
