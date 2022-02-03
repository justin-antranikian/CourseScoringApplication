using System.Collections.Generic;
using System.Linq;

namespace CoreTests.Sandbox;

internal static class PerformanceSorter
{
	public static IEnumerable<(int StudentId, int StudentWithSameAgeId)> GetLessEffecient(List<Student> students, List<Student> otherStudents)
	{
		foreach (var student in students)
		{
			var studentWithSameName = otherStudents.FirstOrDefault(oo => oo.Name == student.Name);
			yield return (student.Id, studentWithSameName?.Id ?? 0);
		}
	}

	public static IEnumerable<(int StudentId, int? StudentWithSameAgeId)> GetMoreEffecient(List<Student> students, List<Student> otherStudents)
	{
		var keyValuePairs = otherStudents
								.GroupBy(oo => oo.Name)
								.Select(oo => oo.First())
								.ToDictionary(oo => oo.Name, oo => oo.Id);

		foreach (var student in students)
		{
			var hasValue = keyValuePairs.TryGetValue(student.Name, out int value);
			int? studentWithSameAgeId = hasValue ? value : null;
			yield return (student.Id, studentWithSameAgeId);
		}
	}
}
