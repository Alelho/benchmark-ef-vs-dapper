using Benchmark.Data.Entities;
using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Benchmark.Benchmarks
{
	public class BenchmarkDapper : BenchmarkBase
	{
		[GlobalSetup]
		public void Setup()
		{
			BaseSetup();
		}

		[Benchmark]
		public Employee DapperGetEmployeeById()
		{
			var employee = new Employee();

			for (var i = 0; i < Count; i++)
			{
				employee = _employeeDapperRepository.GetEmployee(EmployeeId);
			}

			return employee;
		}

		[Benchmark]
		public Employee DapperGetEmployeeAndCompanyById()
		{
			var employee = new Employee();

			for (var i = 0; i < Count; i++)
			{
				employee = _employeeDapperRepository.GetEmployeeAndCompany(EmployeeId);
			}

			return employee;
		}

		[Benchmark]
		public ICollection<Company> DapperGetAllCompanies()
		{
			var companies = Enumerable.Empty<Company>();

			for (var i = 0; i < Count; i++)
			{
				companies = _companyDapperRepository.GetAllCompanies();
			}

			return companies.ToList();
		}
	}
}
