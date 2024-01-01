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
			new (4, "one", 20, false, 1)
        };

		var filteredStudents = StudentHelper.GetFilteredStudents(students);

		Assert.Single(filteredStudents);
		Assert.Equal(3, filteredStudents[0].Id);
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

        var studentsWithTypes2 = StudentHelper.GetStudentsWithDefaultName2(students, types, 20);

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
		Assert.True(sortedStudentNames.SequenceEqual(["a", "b", "c", "y", "z"]));
	}

	[Fact]
	public void GetChangeTests()
	{
		var changeItems = SandBox.GetChange(100, 251);

		Assert.Equal(1, changeItems[0].count);
	}

    [Fact]
    public void PrintRandomItem()
    {
        SandBox.PrintChange();
    }

    [Fact]
	public void TestSorting()
	{
		SandBox.SortThem(["a", "c", "b", "d", "e", "m", "g"]);
	}

    [Fact]
    public void TestFindIndex()
    {
		List<string> items = ["a", "c", "b", "d", "e", "g", "m"];

        var index = SandBox.FindIndex(items, "h", items, items.Count / 2);

		items.Insert(index, "h");
    }

    [Fact]
    public void TestFindIndex2()
    {
		var sortedInts = new List<int>() { 1, 4, 5, 7, 9 };

		var itemToInsert = 6;
        var index = SandBox.FindIndex(sortedInts, itemToInsert, 0, sortedInts.Count);

        sortedInts.Insert(index, itemToInsert);
    }
}
