using System.Collections.Generic;

namespace CoreTests.Sandbox;

public record EmployeeWithBower(int Id, List<EmployeeWithBower> EmployeeViewModels)
{
    public int leftBower;
    public int rightBower;
};

public record Employee(int Id, int? ParentId);

public record EmployeeViewModel(int Id, List<EmployeeViewModel> EmployeeViewModels);

public record Student(int Id, string Name, int Age, bool IsStudent, int StudentTypeId)
{
    public static Student CreateDefault() => new (0, "", 10, true, 1);
};

public record StudentType(int Id, string StudentTypeName);

public record StudentByTypeGrouping(string StudentTypeName, int Count, List<StudentByTypeViewModel> students);

public record StudentByTypeViewModel(string StudentName, bool IsStudent);