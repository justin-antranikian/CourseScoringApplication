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
	public void GetSortedNames_ReturnsCorrectResults()
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
}
