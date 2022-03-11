using Benchmark.Data.Entities;
using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Benchmark.Benchmarks
{
	[Description(".NET 5")]
	public class BenchmarkNET5 : BenchmarkBase
	{
		[GlobalSetup]
		public void Setup()
		{
			BaseSetup();
		}

		[Benchmark(Description = "GetEmployeeById")]
		public Employee NET5GetEmployeeById()
		{
			var employee = new Employee();

			for (var i = 0; i < Count; i++)
			{
				employee = _employeeEFCoreRepository.SingleOrDefault(e => e.Id == EmployeeId);
			}

			return employee;
		}

		[Benchmark(Description = "GetEmployeeAndCompanyById")]
		public Employee NET5GetEmployeeAndCompanyById()
		{
			var employee = new Employee();

			for (var i = 0; i < Count; i++)
			{
				employee = _employeeEFCoreRepository.SingleOrDefault(e => e.Id == EmployeeId, _includeQuery);
			}

			return employee;
		}

		[Benchmark(Description = "Get1000Companies")]
		public ICollection<Company> NET5Get1000Companies()
		{
			var companies = _companyEFCoreRepository.Get1000Companies();

			return companies.ToList();
		}
	}
}
