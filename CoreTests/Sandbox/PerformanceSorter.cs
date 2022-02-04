using System.Collections.Generic;
using System.Linq;

namespace CoreTests.Sandbox;

internal static class PerformanceSorter
{
	public static IEnumerable<(int TeacherId, int StudentWithSameAgeId)> GetLessEffecient(List<Student> teachers, List<Student> students)
	{
		foreach (var student in teachers)
		{
			var studentWithSameName = students.FirstOrDefault(oo => oo.Name == student.Name);
			yield return (student.Id, studentWithSameName?.Id ?? 0);
		}
	}

	public static IEnumerable<(int TeacherId, int? StudentWithSameAgeId)> GetMoreEffecient(List<Student> teachers, List<Student> students)
	{
		var keyValuePairs = students
								.GroupBy(oo => oo.Name)
								.Select(oo => oo.First())
								.ToDictionary(oo => oo.Name, oo => oo.Id);

		foreach (var student in teachers)
		{
			var hasValue = keyValuePairs.TryGetValue(student.Name, out int value);
			int? studentWithSameAgeId = hasValue ? value : null;
			yield return (student.Id, studentWithSameAgeId);
		}
	}
}
