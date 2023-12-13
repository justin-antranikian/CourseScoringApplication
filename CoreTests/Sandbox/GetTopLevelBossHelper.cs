using System.Collections.Generic;
using System.Linq;

namespace CoreTests.Sandbox;

internal class GetTopLevelBossHelper
{
	internal static int BowerCount = 0;

	internal static EmployeeViewModel GetTopLevelBoss(List<Employee> employees)
	{
		var theBoss = employees.Single(oo => oo.ParentId == null);
		return GetViewModel(employees, theBoss.Id);
	}

	internal static EmployeeViewModel GetViewModel(List<Employee> employees, int id)
	{
		var employeesUnderBoss = employees.Where(oo => oo.ParentId == id);
		var employeesToAdd = employeesUnderBoss.Select(oo => GetViewModel(employees, oo.Id)).ToList();
		return new EmployeeViewModel(id, employeesToAdd);
	}

	internal static EmployeeWithBower GetTopLevelBossWithBower(List<Employee> employees)
	{
		var theBoss = employees.Single(oo => oo.ParentId == null);
		return GetWithBowerViewModel(employees, theBoss.Id);
	}

	internal static void ComputeBowers(EmployeeWithBower employeeWithBower, int bowerCount = 0)
	{
		bowerCount++;

		employeeWithBower.leftBower = bowerCount;

		var children = new List<EmployeeWithBower>();

		int GetNextBower(List<EmployeeWithBower> employees)
		{
			return employees.Any() ? children.Max(oo => oo.rightBower) : bowerCount;
		}

		foreach (var employeeViewModel in employeeWithBower.EmployeeViewModels)
		{
			var nextBower = GetNextBower(children);
			ComputeBowers(employeeViewModel, nextBower);
			children.Add(employeeViewModel);
		}

		var nextRightBower = GetNextBower(employeeWithBower.EmployeeViewModels);
		employeeWithBower.rightBower = nextRightBower + 1;
	}

	internal static EmployeeWithBower GetWithBowerViewModel(List<Employee> employees, int id)
	{
		var employeesUnderBoss = employees.Where(oo => oo.ParentId == id);
		var employeesWithBower = new List<EmployeeWithBower>();

		foreach (var employee in employeesUnderBoss)
		{
			var employeeWithBower = GetWithBowerViewModel(employees, employee.Id);
			employeesWithBower.Add(employeeWithBower);
		}

		var empoloyeeWithBower = new EmployeeWithBower(id, employeesWithBower);
		return empoloyeeWithBower;
	}
}

