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
	public void GetLessEffecient_ReturnsCorrectResults()
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

		var sortedStudentNames = PerformanceSorter.GetMoreEffecient(teachers, extraStudents).ToArray();

		Assert.Collection(sortedStudentNames[0..2], result =>
		{
			Assert.Equal(1, result.TeacherId);
			Assert.Equal(100000000, result.StudentWithSameAgeId);
		}, result =>
		{
			Assert.Equal(2, result.TeacherId);
			Assert.Equal(200000000, result.StudentWithSameAgeId);
		});
	}
}
