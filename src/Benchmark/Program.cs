using Benchmark.Benchmarks;
using Benchmark.Data;
using Benchmark.Data.Constants;
using Benchmark.Data.Dapper;
using Benchmark.Data.efcore;
using Benchmark.Data.Options;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System;

namespace Benchmark
{
	internal class Program
	{
		static void Main(string[] args)
		{
#if DEBUG
			var connectionString = ConnectionStrings.Value;

			var connection = new MySqlConnection(connectionString);
			connection.Open();

			var services = new ServiceCollection();

			services.Configure<ConnectionOptions>(o => o.Connection = connection);
			services.AddDbContext<EmployeeDbContext>(options =>
				options.UseMySql(connectionString, ServerVersion.AutoDetect(connection)));

			services.AddTransient<DbContext, EmployeeDbContext>();

			services.AddTransient<EmployeeRepository>();
			services.AddTransient<CompanyRepository>();
			services.AddTransient<EmployeeDapperRepository>();
			services.AddTransient<CompanyDapperRepository>();

			var serviceProvider = services.BuildServiceProvider();

			var companyEfCoreRepository = serviceProvider.GetService<CompanyRepository>();
			var employeeEfCoreRepository = serviceProvider.GetService<EmployeeRepository>();

			var companyDapperRepository = serviceProvider.GetService<CompanyDapperRepository>();
			var employeeDapperRepository = serviceProvider.GetService<EmployeeDapperRepository>();

			var employee = employeeEfCoreRepository.GetEmployeeById(10);
			var employeeAndCompany = employeeEfCoreRepository.GetEmployeeAndCompanyById(5);
			var companies = companyEfCoreRepository.Get1000Companies();

#else
			new BenchmarkSwitcher(typeof(BenchmarkBase).Assembly).Run(args);

			Console.WriteLine("Done!");
			Console.ReadKey();
#endif
		}
	}
}
