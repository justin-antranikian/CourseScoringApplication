namespace Api.DataModels;

public enum CourseInformationType
{
    Description,
    Promotional,
    HowToPrepare
}

public class CourseInformationEntry
{
    public int Id { get; set; }
    public int CourseId { get; set; }

    public required CourseInformationType CourseInformationType { get; set; }
    public required string Description { get; set; }

    public Course? Course { get; set; }
}
