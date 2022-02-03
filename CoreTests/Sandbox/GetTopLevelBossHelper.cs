using System.Collections.Generic;
using System.Linq;

namespace CoreTests.Sandbox;

internal class GetTopLevelBossHelper
{
	internal EmployeeViewModel GetTopLevelBoss(List<Employee> employees)
	{
		var theBoss = employees.Single(oo => oo.ParentId == null);
		return GetViewModel(employees, theBoss.Id);
	}

	internal EmployeeViewModel GetViewModel(List<Employee> employees, int id)
	{
		var employeesUnderBoss = employees.Where(oo => oo.ParentId == id);
		var employeesToAdd = employeesUnderBoss.Select(oo => GetViewModel(employees, oo.Id)).ToList();
		return new EmployeeViewModel(id, employeesToAdd);
	}
}

