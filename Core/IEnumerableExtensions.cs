using System;
using System.Collections.Generic;
using System.Linq;

namespace Core;

public static class IEnumerableExtensions
{
	public static T GetRandomValue<T>(this IEnumerable<T> items)
	{
		return items.OrderBy(oo => Guid.NewGuid()).FirstOrDefault();
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
		return items.Concat(new[] { itemToAdd }).ToList();
	}

	public static IEnumerable<T> GetRandomValues<T>(this IEnumerable<T> items)
	{
		return items.GetRandomValues(items.Count());
	}

	public static IEnumerable<T> GetRandomValues<T>(this IEnumerable<T> items, int maxValue)
	{
		var randomTake = Enumerable.Range(1, maxValue).GetRandomValue();
		return items.OrderBy(oo => Guid.NewGuid()).Take(randomTake);
	}
}
