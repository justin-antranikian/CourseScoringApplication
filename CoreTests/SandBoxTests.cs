using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoreTests
{
	public record Employee(int Id, int? ParentId);

	public record EmployeeViewModel(int Id, List<EmployeeViewModel> EmployeeViewModels);

	public record Student(int Id, string Name, int Age, bool IsStudent, int StudentTypeId);

	public record StudentType(int Id, string StudentTypeName);

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

		internal static List<Student> GetFilteredStudents(List<Student> unfilteredStudents)
		{
			var query = unfilteredStudents
							.Where(oo => oo.Age >= 20 && oo.IsStudent)
							.GroupBy(oo => oo.Name)
							.Select(oo => oo.OrderBy(oo => oo.Age).First());

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
	}

	public class SandBoxTests
	{
		[Fact]
		public void RemoveDuplicates_ReturnsCorrectResults()
		{
			var numbers = new int[] { 1, 1, 3, 1, 5, 3, 2 };
			var expectedNumbers = new int[] { 1, 3, 5, 2 };
			var filteredNumbers = SandBox.RemoveDuplicates(numbers);

			Assert.True(filteredNumbers.SequenceEqual(expectedNumbers));
		}

		[Fact]
		public void GetFilteredStudents_ReturnsCorrectResults()
		{
			var students = new List<Student>
			{
				new (1, "one", 19, false, 1),
				new (2, "one", 50, true, 1),
				new (3, "one", 21, true, 1),
				new (4, "one", 20, false, 1),
				new (5, "one", 25, true, 1),
			};

			var filteredStudents = SandBox.GetFilteredStudents(students);

			Assert.Single(filteredStudents);
			Assert.Equal(3, filteredStudents[0].Id);
		}

		[Fact]
		public void GetStudentsWithName_ReturnsCorrectResults()
		{
			var students = new List<Student>
			{
				new (1, "one", 19, false, 1),
				new (2, "two", 21, true, 2),
				new (3, "three", 25, true, 3),
			};

			var types = new List<StudentType>
			{
				new StudentType(1, "primary student"),
				new StudentType(3, "secondary student"),
			};

			var studentsWithTypes = SandBox.GetStudentsWithName(students, types);

			Assert.Collection(studentsWithTypes, student =>
			{
				Assert.Equal(1, student.Student.Id);
				Assert.Equal("primary student", student.TypeName);
			}, student =>
			{
				Assert.Equal(3, student.Student.Id);
				Assert.Equal("secondary student", student.TypeName);
			});
		}

		[Fact]
		public void GetStudentsWithDefaultName_ReturnsCorrectResults()
		{
			var students = new List<Student>
			{
				new (1, "one", 19, false, 1),
				new (2, "two", 21, true, 2),
				new (3, "three", 25, true, 3),
			};

			var types = new List<StudentType>
			{
				new StudentType(1, "primary student"),
				new StudentType(3, "secondary student"),
			};

			var studentsWithTypes = SandBox.GetStudentsWithDefaultName(students, types, 20);

			Assert.Collection(studentsWithTypes, (student) =>
			{
				Assert.Equal(2, student.StudentId);
				Assert.Equal("fake student", student.TypeName);
			}, (student) =>
			{
				Assert.Equal(3, student.StudentId);
				Assert.Equal("secondary student", student.TypeName);
			});
		}

		[Fact]
		public void IsPalidrome_ReturnsCorrectResults()
		{
			Assert.False(SandBox.IsPalidrome("magic"));
			Assert.False(SandBox.IsPalidrome("terret"));
			Assert.True(SandBox.IsPalidrome("racecar"));
		}

		[Fact]
		public void GetTopLevelBoss_ReturnsCorrectResult()
		{
			var employees = new List<Employee>
			{
				new Employee(1, null),
				new Employee(2, 1),
				new Employee(3, 1),
				new Employee(4, 2),
				new Employee(5, 2),
				new Employee(6, 4),
			};

			var topLevelBoss = SandBox.GetTopLevelBoss(employees);
			var level2Employees = topLevelBoss.EmployeeViewModels;

			Assert.Equal(2, level2Employees.Count);

			Assert.Equal(2, level2Employees[0].Id);
			Assert.Equal(3, level2Employees[1].Id);
			Assert.Empty(level2Employees[1].EmployeeViewModels);

			var level3Employees = level2Employees[0].EmployeeViewModels;
			Assert.Equal(4, level3Employees[0].Id);
			Assert.Equal(5, level3Employees[1].Id);
			Assert.Equal(6, level3Employees[0].EmployeeViewModels[0].Id);
			Assert.Empty(level3Employees[0].EmployeeViewModels[0].EmployeeViewModels);
		}
	}
}
