using CoreTests.Sandbox;
using System.Collections.Generic;
using System.Linq;

namespace CoreTests;

public class StudentHelper
{
	internal static List<StudentByTypeGrouping> MapToStudentByTypeGrouping(List<Student> unfilteredStudents, List<StudentType> studentTypes)
	{
		var query = unfilteredStudents.GroupBy(oo => oo.StudentTypeId).Select(grouping =>
		{
			var type = studentTypes.Single(oo => oo.Id == grouping.Key);
			var studentViewModels = grouping.Select(oo => new StudentByTypeViewModel(oo.Name, oo.IsStudent)).ToList();
			return new StudentByTypeGrouping(type.StudentTypeName, grouping.Count(), studentViewModels);
		});

		return query.ToList();
	}

	internal static List<Student> GetFilteredStudents(List<Student> unfilteredStudents)
	{
		var query = unfilteredStudents
						.Where(oo => oo.Age >= 20 && oo.IsStudent)
						.GroupBy(oo => oo.Name)
						.Select(oo => oo.MinBy(oo => oo.Age));

		return query.ToList();
	}

	internal static List<(int StudentId, string TypeName)> GetStudentsWithDefaultName2(List<Student> allStudentsPool, List<StudentType> studentTypes, int ageThreshold)
	{
		var studentTypeLookups = studentTypes.ToDictionary(oo => oo.Id, oo => oo.StudentTypeName);

		var results = new List<(int StudentId, string TypeName)>();
		foreach (var student in allStudentsPool)
		{
			var studentTypeLookup = studentTypeLookups.TryGetValue(student.StudentTypeId, out string value) ? value : "Unknown";
			results.Add((student.Id, studentTypeLookup));
		}

		return results;
    }

    internal static List<(int StudentId, string TypeName)> GetStudentsWithDefaultName(List<Student> allStudentsPool, List<StudentType> studentTypes, int ageThreshold)
	{
		var studentTypeKeys = studentTypes.ToDictionary(oo => oo.Id, oo => oo.StudentTypeName);

		(int StudentId, string TypeName) MapToTypeName(Student student)
		{
			var typeName = studentTypeKeys.ContainsKey(student.StudentTypeId) ? studentTypeKeys[student.StudentTypeId] : "fake student";
			return (student.StudentTypeId, typeName);
		}

		return allStudentsPool.Where(oo => oo.Age > ageThreshold).Select(MapToTypeName).ToList();
	}
}
