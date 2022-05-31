using Benchmark.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Benchmark.Data.efcore
{
	public class CompanyRepository
	{
		private readonly EmployeeDbContext _dbContext;

		public CompanyRepository(EmployeeDbContext employeeDbContext)
		{
			_dbContext = employeeDbContext;
		}

		public IEnumerable<Company> Get1000Companies()
		{
			return _dbContext.Set<Company>()
				.AsNoTracking()
				.Take(1000)
				.ToList();
		}
	}
}
