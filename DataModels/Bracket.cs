using Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels;

[Table("Brackets")]
public record Bracket
{
	[Key]
	public int Id { get; init; }

	public int CourseId { get; init; }

	public string Name { get; init; }

	public BracketType BracketType { get; init; }

	public Bracket(string name, BracketType bracketType) => (Name, BracketType) = (name, bracketType);

	public Bracket(int courseId, string name, BracketType bracketType) : this(name, bracketType) => CourseId = courseId;

	public Bracket() { }
}
