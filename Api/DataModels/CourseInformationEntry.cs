namespace Api.DataModels;

public enum CourseInformationType
{
    Description,
    Promotional,
    HowToPrepare
}

public record CourseInformationEntry
{
    public int Id { get; init; }
    public int CourseId { get; set; }

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
