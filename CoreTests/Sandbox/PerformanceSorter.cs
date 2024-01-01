using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace CoreTests.Sandbox;

internal static class PerformanceSorter
{
	public static IEnumerable<(int TeacherId, int? StudentWithSameName)> GetLessEffecient(List<Student> teachers, List<Student> students)
	{
		foreach (var student in teachers)
		{
			var studentWithSameName = students.FirstOrDefault(oo => oo.Name == student.Name);
			yield return (student.Id, studentWithSameName?.Id);
		}
	}

	public static async Task TestTask()
	{
		var tasks = new List<Task>();
        using var slim = new SemaphoreSlim(4);

		foreach (var index in Enumerable.Range(1, 10))
		{
			var innerTask = Task.Run(async () => 
			{
				await slim.WaitAsync();

                await Task.Delay(5000);

                slim.Release();
			});

			tasks.Add(innerTask);
		}

		await Task.WhenAll(tasks);
    }

    public static T? BinarySearch<T>(List<T> items, T itemToFind) where T : INumber<T>
	{
		var middleIndex = items.Count / 2;
		var currentItem = items is [var singleItem] ? singleItem : items[middleIndex - 1];

		if (currentItem == itemToFind)
		{
			return currentItem;
		}

		if (items is [var _])
		{
			return default;
		}

        var reducedItems = itemToFind > currentItem ? items[middleIndex..] : items[..middleIndex];
        return BinarySearch(reducedItems, itemToFind);
	}

    public static List<string> GetSortedNames(List<string> allNames)
	{
		var sortedList = new List<string>();

		foreach (var name in allNames)
		{
			var index = 0;

			foreach (var existingName in sortedList)
			{
				if (name.CompareTo(existingName) > 0)
				{
					index++;
					continue;
				}
			}

			sortedList.Insert(index, name);
		}

		return sortedList;
	}

	public static int GetFactorial(int number)
	{
		if (number is 1)
		{
			return 1;
		}

		var factorial = GetFactorial(number - 1);
        return number * factorial;
	}

	public static void PrintFrom(int endingIndex)
	{
		Console.WriteLine("Printing end:" + endingIndex);

		if (endingIndex == 1)
		{
			return;
		}

		PrintFrom(endingIndex - 1);
	}

    public static IEnumerable<(int TeacherId, int? StudentWithSameAgeId)> GetMoreEffecient2(List<Student> teachers, List<Student> students)
	{
		var query = from teacher in teachers
					join student in students on teacher.Name equals student.Name
					into temp
					from t in temp.DefaultIfEmpty()
					select (teacher.Id, t?.Id);

		return query.ToList();
	}

	public static IEnumerable<(int TeacherId, int? StudentWithSameAgeId)> GetMoreEffecient(List<Student> teachers, List<Student> students)
	{
		var studentLookups = students.ToDictionary(oo => oo.Name, oo => oo.Id);

		foreach (var teacher in teachers)
		{
			int? studentWithSameAgeId = studentLookups.TryGetValue(teacher.Name, out int value) ? value : null;
			yield return (teacher.Id, studentWithSameAgeId);
		}
	}
}
