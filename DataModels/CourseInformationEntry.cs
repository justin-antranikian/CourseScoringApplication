using Core.Enums;

namespace DataModels;

public record CourseInformationEntry
{
    public int Id { get; init; }
    public int CourseId { get; init; }

    public CourseInformationType CourseInformationType { get; set; }
    public string Description { get; set; }

    public CourseInformationEntry() { }

    public CourseInformationEntry(CourseInformationType courseInformationType, string description)
    {
        CourseInformationType = courseInformationType;
        Description = description;
    }
    public Course Course { get; set; }
}
