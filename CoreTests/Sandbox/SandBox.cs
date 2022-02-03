using CoreTests.Sandbox;
using System.Collections.Generic;
using System.Linq;

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

		return noDups.ToArray();
	}

	internal static List<string> GetSortedNames(List<Student> students)
	{
		var sorted = new List<string>();
		foreach (var name in students.Select(oo => oo.Name))
		{
			var index = 0;
			foreach (var studentPoolName in sorted.Where(oo => oo != name))
			{
				var comesBefore = name.CompareTo(studentPoolName) == -1;

				if (comesBefore)
				{
					break;
				}

				index++;
			}

			sorted.Insert(index, name);
		}

		return sorted;
	}

	internal static bool IsPalidrome(string str)
	{
		var stringLength = str.Length;

		for (var i = 0; i < stringLength / 2; i++)
		{
			var fromStart = str[i];
			var fromEnd = str[stringLength - 1 - i];

			if (fromStart != fromEnd)
			{
				return false;
			}
		}

		return true;
	}
}
