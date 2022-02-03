using CoreTests.Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoreTests;

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
			new (2, "one", 21, true, 2),
			new (3, "two", 25, true, 3),
		};

		var types = new List<StudentType>
		{
			new (1, "primary student"),
			new (3, "secondary student"),
		};

		var studentsWithTypes = SandBox.GetStudentsWithTypeName(students, types);

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
	public void GetStudentsWithName_CreatingTheUnitTests()
	{
		var students = new List<Student>
		{
			new (1, "one", 20, true, 2),
			new (2, "one", 20, false, 1),
			new (3, "one", 19, true, 1),
		};

		var fileteredStudents = SandBox.CreateTheUnitTestsFor(students);
		var studentIds = fileteredStudents.Select(oo => oo.Id).ToArray();

		Assert.True(studentIds.SequenceEqual(new int[] { 1, 2 }));
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
			new (1, "primary student"),
			new (3, "secondary student"),
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
		Assert.True(SandBox.IsPalidrome("terret"));
		Assert.True(SandBox.IsPalidrome("racecar"));
	}

	[Fact]
	public void GetTopLevelBoss_ReturnsCorrectResult()
	{
		var employees = new List<Employee>
		{
			new (1, null),
			new (2, 1),
			new (3, 1),
			new (4, 2),
			new (5, 2),
			new (6, 4),
		};

		var topLevelBoss = new GetTopLevelBossHelper().GetTopLevelBoss(employees);
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

	[Fact]
	public void SortThem_ReturnsCorrectResults()
	{
		var students = new List<Student>
		{
			new (1, "z", 19, false, 1),
			new (1, "b", 19, false, 1),
			new (1, "a", 19, false, 1),
			new (1, "y", 19, false, 1),
			new (1, "c", 19, false, 1),
		};


		var sortedStudentNames = SandBox.GetSortedNames(students);
		Assert.True(sortedStudentNames.SequenceEqual(new string[] { "a", "b", "c", "y", "z" }));
	}

	[Fact]
	public void GetLessEffecient_ReturnsCorrectResults()
	{
		var generalStudentBody = Enumerable.Range(1, 200000).Select(oo =>
        {
			return new Student(oo, oo + "Name", oo, true, 1);
        }).ToList();

		var generalStudentBodyNameMatches = new List<Student>
		{
			new (500000000, "Nancy", 50, false, 1),
			new (200000000, "Bill", 20, false, 1),
			new (100000000, "John", 10, false, 1),
			new (400000000, "Debby", 40, false, 1),
			new (300000000, "Ron", 30, false, 1),
		};

		generalStudentBody.AddRange(generalStudentBodyNameMatches);

		var generalTeachersBody = Enumerable.Range(1, 20000).Select(oo =>
		{
			return new Student(oo, oo + "Name", oo, true, 1);
		}).ToList();

		var teachers = new List<Student>
		{
			new (1, "John", 10, false, 1),
			new (2, "Bill", 20, false, 1),
			new (3, "Ron", 30, false, 1),
			new (4, "Debby", 40, false, 1),
			new (5, "Nancy", 50, false, 1),
		};

		teachers.AddRange(generalTeachersBody);

		var sortedStudentNames = SandBox.GetLessEffecient(teachers, generalStudentBody).ToArray();

		Assert.Collection(sortedStudentNames[0..2], result =>
		{
			Assert.Equal(1, result.StudentId);
			Assert.Equal(100000000, result.StudentWithSameAgeId);
		}, result =>
		{
			Assert.Equal(2, result.StudentId);
			Assert.Equal(200000000, result.StudentWithSameAgeId);
		});
	}
}
