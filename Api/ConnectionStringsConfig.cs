namespace Api;

public record ConnectionStringsConfig : IConfigSectionName
{
    public static string SectionName => "ConnectionStrings";

    public required string Database { get; init; }

    public IEnumerable<string> GetValidationMessages()
    {
        if (string.IsNullOrWhiteSpace(Database))
        {
            yield return $"{nameof(Database)} is required.";
        }
    }
}