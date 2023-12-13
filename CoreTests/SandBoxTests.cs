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
		int[] numbers = [1, 1, 3, 1, 5, 3, 2];
		int[] expectedNumbers = [1, 3, 5, 2];
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
			new (1, "c", 19, false, 1),
		};

		var sortedStudentNames = SandBox.GetSortedNames(students);
		Assert.True(sortedStudentNames.SequenceEqual(["a", "b", "c", "y", "z"]));
	}

	[Fact]
	public void IsPalidrome_ReturnsCorrectResults()
	{
		Assert.False(SandBox.IsPalidrome("magic"));
		Assert.True(SandBox.IsPalidrome("terret"));
		Assert.False(SandBox.IsPalidrome("terreu"));
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

		var topLevelBoss = GetTopLevelBossHelper.GetTopLevelBoss(employees);
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
    public void GetTopLevelBossWithBower_ReturnsCorrectResult()
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

        var topLevelBoss = GetTopLevelBossHelper.GetTopLevelBossWithBower(employees);
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
    public void ComputeBowers_ReturnsCorrectResult()
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

        var topLevelBoss = GetTopLevelBossHelper.GetTopLevelBossWithBower(employees);

		GetTopLevelBossHelper.ComputeBowers(topLevelBoss);

		Assert.Equal(1, topLevelBoss.leftBower);
		Assert.Equal(12, topLevelBoss.rightBower);

        Assert.Equal(2, topLevelBoss.EmployeeViewModels[0].leftBower);
        Assert.Equal(9, topLevelBoss.EmployeeViewModels[0].rightBower);

        Assert.Equal(3, topLevelBoss.EmployeeViewModels[0].EmployeeViewModels[0].leftBower);
        Assert.Equal(6, topLevelBoss.EmployeeViewModels[0].EmployeeViewModels[0].rightBower);

        Assert.Equal(10, topLevelBoss.EmployeeViewModels[1].leftBower);
        Assert.Equal(11, topLevelBoss.EmployeeViewModels[1].rightBower);
    }
}
