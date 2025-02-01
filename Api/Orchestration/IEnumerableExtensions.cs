namespace Api.Orchestration;

public static class IEnumerableExtensions
{
    public static T? GetRandomValue<T>(this IEnumerable<T> items)
    {
        return items.OrderBy(static _ => Guid.NewGuid()).FirstOrDefault();
    }

    public static IEnumerable<T> GetRandomValues<T>(this IEnumerable<T> items, int maxValue)
    {
        var randomTake = Enumerable.Range(1, maxValue).GetRandomValue();
        return items.OrderBy(_ => Guid.NewGuid()).Take(randomTake);
    }
}
