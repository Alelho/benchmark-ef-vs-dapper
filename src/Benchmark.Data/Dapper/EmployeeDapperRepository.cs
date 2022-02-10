using Benchmark.Data.Constants;
using Benchmark.Data.Entities;
using Dapper;
using MySqlConnector;
using System.Collections.Generic;
using System.Linq;

namespace Benchmark.Data.Dapper
{
	public class EmployeeDapperRepository
	{
		public EmployeeDapperRepository()
		{
		}

		public Employee GetEmployee(long id)
		{
			using var connection = new MySqlConnection(ConnectionStrings.Value);

			var query = @$"
				SELECT *
				FROM Employees e
				WHERE e.Id = @id";

			var employeeDict = new Dictionary<long, Employee>();

			var employee = connection.QuerySingle<Employee>(query, new { id });

			return employee;
		}

		public Employee GetEmployeeAndCompany(long id)
		{
			using var connection = new MySqlConnection(ConnectionStrings.Value);

			var query = @$"
				SELECT *
				FROM Employees e
					INNER JOIN Companies c on c.Id = e.CompanyId
				WHERE e.Id = @id";


			var employeeDict = new Dictionary<long, Employee>();

			var employee = connection.Query<Employee, Company, Employee>(query,
				(employee, company) =>
				{
					if (!employeeDict.TryGetValue(employee.Id, out var employeeEntry))
					{
						employeeEntry = employee;
						employeeEntry.Company = company;

						employeeDict.Add(employee.Id, employeeEntry);
					}

					return employeeEntry;
				},
				new { id })
				.SingleOrDefault();

			return employee;
		}
	}
}
