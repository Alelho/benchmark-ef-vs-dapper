using Benchmark.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Benchmark.Data
{
	public class EmployeeDbContextFactory : IDesignTimeDbContextFactory<EmployeeDbContext>
	{
		public EmployeeDbContext CreateDbContext(string[] args)
		{
			var connectionString = ConnectionStrings.Value;

			var optionsBuilder = new DbContextOptionsBuilder<EmployeeDbContext>();
			optionsBuilder.UseMySql(connectionString, serverVersion: ServerVersion.AutoDetect(connectionString));

			return new EmployeeDbContext(optionsBuilder.Options);
		}
	}
}
