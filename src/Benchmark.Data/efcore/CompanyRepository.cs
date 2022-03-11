using Benchmark.Data.Entities;
using EFCoreUnitOfWork.Interfaces;
using EFCoreUnitOfWork.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Benchmark.Data.efcore
{
	public class CompanyRepository : GenericRepository<Company>, IGenericRepository<Company>
	{
		public CompanyRepository(EmployeeDbContext employeeDbContext)
			: base(employeeDbContext)
		{
		}

		public IEnumerable<Company> Get1000Companies()
		{
			return DbContext.Set<Company>()
				.AsNoTracking()
				.Take(1000)
				.ToList();
		}
	}
}
