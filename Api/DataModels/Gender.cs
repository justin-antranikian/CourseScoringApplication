namespace Api.DataModels;

public enum Gender
{
    Male,
    Femail
}

public static class GenderExtensions
{
    public static string ToAbbreviation(this Gender gender)
    {
        return gender switch
        {
            Gender.Femail => "F",
            Gender.Male => "M",
            _ => throw new NotImplementedException()
        };
    }
}
