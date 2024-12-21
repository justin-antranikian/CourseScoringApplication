namespace Api.Orchestration;

public static class StringExtensions
{
    public static string ToUrlFriendlyText(this string text)
    {
        return text.ToLower().Replace(" ", "-");
    }
}
