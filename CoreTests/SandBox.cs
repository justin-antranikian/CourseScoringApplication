using System.Collections.Generic;
using System.Linq;

namespace CoreTests;

public record Employee(int Id, int? ParentId);

public record EmployeeViewModel(int Id, List<EmployeeViewModel> EmployeeViewModels);

public record Student(int Id, string Name, int Age, bool IsStudent, int StudentTypeId);

public record StudentType(int Id, string StudentTypeName);

public record StudentByTypeGrouping(string StudentTypeName, int Count, List<StudentByTypeViewModel> students);

public record StudentByTypeViewModel(string StudentName, bool IsStudent);

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
						.Select(oo => oo.OrderBy(oo => oo.Age).First());

		return query.ToList();
	}

	internal static List<Student> CreateTheUnitTestsFor(List<Student> unfilteredStudents)
	{
		var query = unfilteredStudents.Where(oo => (oo.IsStudent || oo.StudentTypeId == 1) && oo.Age >= 20);
		return query.ToList();
	}

	internal static List<(Student Student, string TypeName)> GetStudentsWithName(List<Student> allStudentsPool, List<StudentType> studentTypes)
	{
		var studentsWithName = allStudentsPool.Join(studentTypes,
			student => student.StudentTypeId,
			type => type.Id,
			(student, type) => (student, type.StudentTypeName)
		).ToList();

		return studentsWithName;
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

	internal static bool IsPalidrome(string str)
	{
		var stringLength = str.Length;

		if (stringLength % 2 == 0)
		{
			return false;
		}

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

	public static EmployeeViewModel GetTopLevelBoss(List<Employee> employees)
	{
		var theBoss = employees.Single(oo => oo.ParentId == null);
		return GetViewModel(employees, theBoss.Id);
	}

	public static EmployeeViewModel GetViewModel(List<Employee> employees, int id)
	{
		var employeesUnderBoss = employees.Where(oo => oo.ParentId == id);
		var employeesToAdd = employeesUnderBoss.Select(oo => GetViewModel(employees, oo.Id)).ToList();
		return new EmployeeViewModel(id, employeesToAdd);
	}

	public static List<string> GetSortedNames(List<Student> students)
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
}
