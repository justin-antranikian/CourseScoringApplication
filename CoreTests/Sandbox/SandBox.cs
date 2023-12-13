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

		return [.. noDups];
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
		var endingIndex = stringLength - 1;

		for (var i = 0; i < stringLength / 2; i++)
		{
			var fromStart = str[i];
			var fromEnd = str[endingIndex - i];

			if (fromStart != fromEnd)
			{
				return false;
			}
		}

		return true;
	}
}
