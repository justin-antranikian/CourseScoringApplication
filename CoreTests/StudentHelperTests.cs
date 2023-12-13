using CoreTests.Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoreTests;

public class StudentHelperTests
{
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
            new (5, "two", 25, true, 1),
        };

		var filteredStudents = StudentHelper.GetFilteredStudents(students);

		Assert.Single(filteredStudents);
		Assert.Equal(3, filteredStudents[0].Id);
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

		var fileteredStudents = StudentHelper.CreateTheUnitTestsFor(students);
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

		var studentsWithTypes = StudentHelper.GetStudentsWithDefaultName(students, types, 20);

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
}
