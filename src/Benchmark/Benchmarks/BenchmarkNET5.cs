using Benchmark.Data.Entities;
using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Benchmark.Benchmarks
{
	public class BenchmarkNET5 : BenchmarkBase
	{
		[GlobalSetup]
		public void Setup()
		{
			BaseSetup();
		}

		[Benchmark]
		public Employee NET5GetEmployeeById()
		{
			var employee = new Employee();

			for (var i = 0; i < Count; i++)
			{
				employee = _employeeEFCoreRepository.SingleOrDefault(e => e.Id == EmployeeId);
			}

			return employee;
		}

		[Benchmark]
		public Employee NET5GetEmployeeAndCompanyById()
		{
			var employee = new Employee();

			for (var i = 0; i < Count; i++)
			{
				employee = _employeeEFCoreRepository.SingleOrDefault(e => e.Id == EmployeeId, _includeQuery);
			}

			return employee;
		}

		[Benchmark]
		public ICollection<Company> NET5GetAllCompanies()
		{
			var companies = Enumerable.Empty<Company>();

			for (var i = 0; i < Count; i++)
			{
				companies = _companyEFCoreRepository.Search(c => c.Id > 0);
			}

			return companies.ToList();
		}
	}
}
