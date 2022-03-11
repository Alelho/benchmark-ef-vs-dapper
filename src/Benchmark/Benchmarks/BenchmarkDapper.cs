using Benchmark.Data.Entities;
using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Benchmark.Benchmarks
{
	[Description("Dapper")]
	public class BenchmarkDapper : BenchmarkBase
	{
		[GlobalSetup]
		public void Setup()
		{
			BaseSetup();
		}

		[Benchmark(Description = "GetEmployeeById")]
		public Employee DapperGetEmployeeById()
		{
			var employee = new Employee();

			for (var i = 0; i < Count; i++)
			{
				employee = _employeeDapperRepository.GetEmployee(EmployeeId);
			}

			return employee;
		}

		[Benchmark(Description = "GetEmployeeAndCompanyById")]
		public Employee DapperGetEmployeeAndCompanyById()
		{
			var employee = new Employee();

			for (var i = 0; i < Count; i++)
			{
				employee = _employeeDapperRepository.GetEmployeeAndCompany(EmployeeId);
			}

			return employee;
		}

		[Benchmark(Description = "Get1000Companies")]
		public ICollection<Company> DapperGet1000Companies()
		{
			var companies = _companyDapperRepository.Get1000Companies();

			return companies.ToList();
		}
	}
}
