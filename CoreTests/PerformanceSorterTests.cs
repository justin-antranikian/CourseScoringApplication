using CoreTests.Sandbox;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoreTests;

public class PerformanceSorterTests
{
	public static List<Student> GetStudents(int amount)
    {
		return Enumerable.Range(1, amount).Select(oo =>
		{
			return new Student(oo, oo + "Name", oo, true, 1);
		}).ToList();
	}

	[Fact]
    public async void BinarySearchTest()
    {
		var sortedInts = new List<int>()
		{
			1, 2, 3, 4, 5, 6, 7, 8
		};

		//Assert.Equal(1, PerformanceSorter.BinarySearch(sortedInts, 1));
		//Assert.Equal(2, PerformanceSorter.BinarySearch(sortedInts, 2));
		//Assert.Equal(3, PerformanceSorter.BinarySearch(sortedInts, 3));
		//Assert.Equal(4, PerformanceSorter.BinarySearch(sortedInts, 4));
		//Assert.Equal(5, PerformanceSorter.BinarySearch(sortedInts, 5));
		//Assert.Equal(6, PerformanceSorter.BinarySearch(sortedInts, 6));
		//Assert.Equal(7, PerformanceSorter.BinarySearch(sortedInts, 7));
		Assert.Equal(8, PerformanceSorter.BinarySearch(sortedInts, 8));
		//Assert.Null(PerformanceSorter.BinarySearch(sortedInts, 9));
	}

    [Fact]
	public async void TaskTest()
	{
		await PerformanceSorter.TestTask();
    }

    [Fact]
    public void GetSortedNamesTests()
    {
        var factorial = PerformanceSorter.GetSortedNames(["a", "c", "b", "e", "d"]);
    }

    [Fact]
	public void GetFactorialTests()
	{
		var factorial = PerformanceSorter.GetFactorial(5);
		Assert.Equal(120, factorial);
	}

    [Fact]
    public void PrintFromTests()
    {
        PerformanceSorter.PrintFrom(10);
    }

    [Fact]
	public void GetMoreEffecient_ReturnsCorrectResults()
	{
		var extraStudents = GetStudents(200000);

		var students = new List<Student>
		{
			new (500000000, "Nancy", 50, false, 1),
			new (200000000, "Bill", 20, false, 1),
			new (100000000, "John", 10, false, 1),
			new (400000000, "Debby", 40, false, 1),
			new (300000000, "Ron", 30, false, 1),
		};

		extraStudents.AddRange(students);

		var extraTeachers = GetStudents(50000);

		var teachers = new List<Student>
		{
			new (1, "John", 10, false, 1),
			new (2, "Bill", 20, false, 1),
			new (3, "Ron", 30, false, 1),
			new (4, "Debby", 40, false, 1),
			new (5, "Nancy", 50, false, 1),
		};

		teachers.AddRange(extraTeachers);

        var sortedStudentNames1 = PerformanceSorter.GetMoreEffecient(teachers, extraStudents).ToArray();

        var sortedStudentNames = PerformanceSorter.GetMoreEffecient2(teachers, extraStudents).ToArray();

		var x = "";

		//Assert.Collection(sortedStudentNames[0..2], result =>
		//{
		//	Assert.Equal(1, result.TeacherId);
		//	Assert.Equal(100000000, result.StudentWithSameAgeId);
		//}, result =>
		//{
		//	Assert.Equal(2, result.TeacherId);
		//	Assert.Equal(200000000, result.StudentWithSameAgeId);
		//});
	}
}
