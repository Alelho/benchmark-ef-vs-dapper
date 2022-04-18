using Benchmark.Data.Constants;
using Benchmark.Data.Entities;
using Dapper;
using MySql.Data.MySqlClient;
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
			var query = @"
				SELECT *
				FROM Employees e
				WHERE e.Id = @id";

			using (var connection = new MySqlConnection(ConnectionStrings.Value))
			{
				var employee = connection.QuerySingle<Employee>(query, new { id });

				return employee;
			}
		}

		public Employee GetEmployeeAndCompany(long id)
		{
			var query = @"
				SELECT *
				FROM Employees e
					INNER JOIN Companies c on c.Id = e.CompanyId
				WHERE e.Id = @id";

			using (var connection = new MySqlConnection(ConnectionStrings.Value))
			{
				var employeeDict = new Dictionary<long, Employee>();

				var employee = connection.Query<Employee, Company, Employee>(query,
					(empl, company) =>
					{
						if (!employeeDict.TryGetValue(empl.Id, out var employeeEntry))
						{
							employeeEntry = empl;
							employeeEntry.Company = company;

							employeeDict.Add(empl.Id, employeeEntry);
						}

						return employeeEntry;
					},
					new { id })
					.SingleOrDefault();

				return employee;
			}
		}
	}
}
