using Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{

	[Table("CourseInformationEntries")]
	public record CourseInformationEntry
	{
		[Key]
		public int Id { get; init; }

		public int CourseId { get; init; }

		public CourseInformationType CourseInformationType { get; init; }

		public string Description { get; init; }

		public CourseInformationEntry() { }

		public CourseInformationEntry(CourseInformationType courseInformationType, string description)
		{
			CourseInformationType = courseInformationType;
			Description = description;
		}
	}
}
