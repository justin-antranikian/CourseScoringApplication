using CoreTests.Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;

namespace CoreTests;

public class SandBox
{
	internal static int[] RemoveDuplicates(int[] numbers)
	{
		var noDups = new List<int>();

		foreach (var number in numbers)
		{
			var isFound = false;
			foreach (var fromDupsArray in noDups)
			{
				if (number == fromDupsArray)
				{
					isFound = true;
					break;
				}
			}

			if (!isFound)
			{
				noDups.Add(number);
			}
		}

		return [.. noDups];
	}

	public static void PrintChange()
	{
		var random = new Random();

		var itemPrice = random.Next(1, 10000);
		var customerPrice = random.Next(itemPrice, itemPrice + 2500);

		var prices = GetChange(itemPrice, customerPrice);

		foreach (var (name, count) in prices)
		{
			Console.WriteLine($"{name} {count}");
		}
	}

	public class ChangeItem(string name, int denomination)
    {
		public string Name = name;
		public int Denomination = denomination;

		public int Count { get; private set; } = 0;
	}

	public static List<(string denomination, int count)> GetChange(int itemPrice, int customerPrice)
	{
		var change = customerPrice - itemPrice;

		var changeItems = new List<ChangeItem>()
		{
			new("dollars", 100),
			new("half dollars", 50),
			new("quaters", 25),
			new("dimes", 10),
			new("nickels", 5),
			new("pennies", 1),
		};

		var results = new List<(string denomination, int count)>();

		foreach (var changeItem in changeItems.OrderByDescending(oo => oo.Denomination))
		{
			var count = change / changeItem.Denomination;
            change -= changeItem.Denomination * count;
            results.Add((changeItem.Name, count));
        }

        return results;
	}

	public static List<string> SortThem(List<string> items)
	{
		var sortedItems = new List<string>();

		foreach (var number in items)
		{
			var index = FindIndex(sortedItems, number, sortedItems, sortedItems.Count / 2);
			sortedItems.Insert(index, number);
        }

		return sortedItems;
	}

	public static int FindIndex(List<int> allItems, int itemToAdd, int startingIndex, int endingIndex)
	{
		if (allItems is [])
		{
			return 0;
		}

        var middleIndex = (startingIndex + endingIndex) / 2;
        var middleIndexItem = allItems[middleIndex];

		if (middleIndexItem == itemToAdd)
		{
			return middleIndex;
		}

        var slicedItems = allItems[startingIndex..endingIndex];
        var itemToAddGreaterThanMiddleIndexItem = itemToAdd > middleIndexItem;

        if (slicedItems is [var _])
		{
			return itemToAddGreaterThanMiddleIndexItem ? endingIndex : startingIndex;
		}

		(int, int) GetSNextStartAndEndIndex()
		{
			if (itemToAddGreaterThanMiddleIndexItem)
			{
				return (middleIndex, endingIndex);
			}

			return (startingIndex, middleIndex);
		}

		var (nextStartingIndex, nextEndingCount) = GetSNextStartAndEndIndex();
		return FindIndex(allItems, itemToAdd, nextStartingIndex, nextEndingCount);
	}

	public static int FindIndex(List<string> allItems, string itemToAdd, List<string> reducedItems, int indexToCheck)
	{
		if (reducedItems is [])
		{
			return 0;
		}

		var isAfter = allItems[indexToCheck].CompareTo(itemToAdd) < 0;

		if (reducedItems is [var _])
		{
			return isAfter ? indexToCheck + 1 : indexToCheck;
		}

		var more = allItems.IndexOf(reducedItems[0]);
		var reducedList = isAfter ? allItems[indexToCheck..] : allItems[more..indexToCheck];
		var reducedListNewIndex = reducedList.Count / 2;
		return FindIndex(allItems, itemToAdd, reducedList, reducedListNewIndex + indexToCheck);
    }

	internal static List<string> GetSortedNames(List<Student> students)
	{
		var filteredNames = new List<string>();

        foreach (var name in students.Select(oo => oo.Name))
		{
			var index = 0;

            if (filteredNames.Contains(name))
            {
                break;
            }

            foreach (var existingName in filteredNames)
			{
				var comesBefore = name.CompareTo(existingName) == -1;

				if (comesBefore)
				{
					break;
				}

				index++;
			}

            filteredNames.Insert(index, name);
        }

		return filteredNames;
	}

	internal static bool IsPalidrome(string str)
	{
		var stringLength = str.Length;

		for (var i = 0; i < stringLength / 2; i++)
		{
			var fromStart = str[i];
			var fromEnd = str[^(i + 1)];

			if (fromStart != fromEnd)
			{
				return false;
			}
		}

		return true;
	}
}
