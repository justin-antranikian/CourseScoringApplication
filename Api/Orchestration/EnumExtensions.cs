namespace Api.Orchestration;

public static class EnumExtensions
{
    public static Enum GetRandomEnumValue(this Type t)
    {
        return Enum.GetValues(t).OfType<Enum>().ToArray().GetRandomEnumValue();
    }

    public static T GetRandomEnumValue<T>(this T[] possibleValues) where T : Enum
    {
        var random = new Random();
        var randomValue = possibleValues[random.Next(possibleValues.Length)];
        return randomValue;
    }

    public static T[] GetValues<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
    }
}
