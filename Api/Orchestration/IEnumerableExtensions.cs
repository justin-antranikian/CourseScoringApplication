namespace Api.Orchestration;

public static class IEnumerableExtensions
{
    public static T? GetRandomValue<T>(this IEnumerable<T> items)
    {
        return items.OrderBy(static _ => Guid.NewGuid()).FirstOrDefault();
    }

    /// <summary>
    /// Adds a single item to an IEnumerable. Calls Linq.Concat() under the hood.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <param name="itemToAdd"></param>
    /// <returns></returns>
    public static List<T> ConcatSingle<T>(this IEnumerable<T> items, T itemToAdd)
    {
        return items.Concat([itemToAdd]).ToList();
    }

    public static IEnumerable<T> GetRandomValues<T>(this IEnumerable<T> items, int maxValue)
    {
        var randomTake = Enumerable.Range(1, maxValue).GetRandomValue();
        return items.OrderBy(_ => Guid.NewGuid()).Take(randomTake);
    }
}
