using Benchmark.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Benchmark.Data.efcore
{
	public class EmployeeRepository
	{
		private readonly EmployeeDbContext _dbContext;

		public EmployeeRepository(EmployeeDbContext employeeDbContext)
		{
			_dbContext = employeeDbContext;
		}

		public Employee GetEmployeeById(long employeeId)
		{
			return _dbContext.Set<Employee>()
				.AsNoTracking()
				.SingleOrDefault(e => e.Id == employeeId);
		}

		public Employee GetEmployeeAndCompanyById(long employeeId)
		{
			return _dbContext.Set<Employee>()
				.AsNoTracking()
				.Include(e => e.Company)
				.SingleOrDefault(e => e.Id == employeeId);
		}
	}
}
