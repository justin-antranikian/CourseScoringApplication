using CoreTests.Sandbox;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoreTests;

public class PerformanceSorterTests
{
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

		var generalTeachersBody = Enumerable.Range(1, 50000).Select(oo =>
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

		var sortedStudentNames = PerformanceSorter.GetMoreEffecient(teachers, generalStudentBody).ToArray();

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
