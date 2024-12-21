using Core;

namespace Api.DataModels;

public record Bracket
{
    public int Id { get; set; }
    public int CourseId { get; set; }

    public BracketType BracketType { get; set; }
    public string Name { get; set; }

    public List<AthleteCourseBracket> AthleteCourseBrackets { get; set; } = [];
    public List<BracketMetadata> BracketMetadatas { get; set; } = [];
    public Course Course { get; set; }
    public List<Result> Results { get; set; } = [];

    public Bracket(string name, BracketType bracketType) => (Name, BracketType) = (name, bracketType);

    public Bracket(int courseId, string name, BracketType bracketType) : this(name, bracketType) => CourseId = courseId;

    public Bracket() { }
}
