using Benchmark.Benchmarks;
using Benchmark.Data.Constants;
using Benchmark.Data.Dapper;
using BenchmarkDotNet.Running;
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

			services.AddTransient<EmployeeDapperRepository>();
			services.AddTransient<CompanyDapperRepository>();
			services.AddTransient(provider => new CompanyDapperOpenedConnRepository(connection));
			services.AddTransient(provider => new EmployeeDapperOpenedConnRepository(connection));

			var serviceProvider = services.BuildServiceProvider();

			var companyDapperRepository = serviceProvider.GetService<CompanyDapperRepository>();
			var employeeDapperRepository = serviceProvider.GetService<EmployeeDapperRepository>();

			var companyDapperOpenedConnRepository = serviceProvider.GetService<CompanyDapperOpenedConnRepository>();
			var employeeDapperOpenedConnRepository = serviceProvider.GetService<EmployeeDapperOpenedConnRepository>();

			var companies = companyDapperRepository.GetAllCompanies();
			var companies2 = companyDapperOpenedConnRepository.GetAllCompanies();

#else
			new BenchmarkSwitcher(typeof(BenchmarkBase).Assembly).Run(args);

			Console.WriteLine("Done!");
			Console.ReadKey();
#endif
		}
	}
}
