using Benchmark.Data.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Benchmark.Data.DapperOpenedConnection
{
	public class EmployeeDapperOpenedConnRepository
	{
		private readonly IDbConnection _connection;

		public EmployeeDapperOpenedConnRepository(IDbConnection connection)
		{
			_connection = connection;
		}

		public Employee GetEmployee(long id)
		{
			var query = @"
				SELECT *
				FROM Employees e
				WHERE e.Id = @id";

			var employee = _connection.QuerySingle<Employee>(query, new { id });

			return employee;
		}

		public Employee GetEmployeeAndCompany(long id)
		{
			var query = @"
				SELECT *
				FROM Employees e
					INNER JOIN Companies c on c.Id = e.CompanyId
				WHERE e.Id = @id";


			var employeeDict = new Dictionary<long, Employee>();

			var employee = _connection.Query<Employee, Company, Employee>(query,
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
