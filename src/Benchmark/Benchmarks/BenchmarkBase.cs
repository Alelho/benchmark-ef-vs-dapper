using Benchmark.Data;
using Benchmark.Data.Constants;
using Benchmark.Data.Dapper;
using Benchmark.Data.efcore;
using Benchmark.Data.Options;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System;
using System.Data;

namespace Benchmark.Benchmarks
{
	[BenchmarkCategory("ORM")]
	public class BenchmarkBase
	{
		private readonly Random _rnd = new Random();

		protected CompanyDapperRepository _companyDapperRepository;
		protected EmployeeDapperRepository _employeeDapperRepository;

		protected CompanyRepository _companyEFCoreRepository;
		protected EmployeeRepository _employeeEFCoreRepository;

		protected IDbConnection Connection;

		public BenchmarkBase()
		{
		}

		protected long EmployeeId => _rnd.Next(100_000, 500_000);
		protected int Count => 100;

		protected void BaseSetup()
		{
			Console.WriteLine("Base setup");
			var connectionString = ConnectionStrings.Value;

			Connection = new MySqlConnection(connectionString);
			OpenConnection();

			var services = new ServiceCollection();

			services.Configure<ConnectionOptions>(o => o.Connection = Connection);

			services.AddDbContext<EmployeeDbContext>(options =>
				options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

			services.AddTransient<DbContext, EmployeeDbContext>();

			services.AddTransient<EmployeeRepository>();
			services.AddTransient<CompanyRepository>();

			services.AddTransient<EmployeeDapperRepository>();
			services.AddTransient<CompanyDapperRepository>();

			var serviceProvider = services.BuildServiceProvider();

			_companyEFCoreRepository = serviceProvider.GetService<CompanyRepository>();
			_employeeEFCoreRepository = serviceProvider.GetService<EmployeeRepository>();

			_companyDapperRepository = serviceProvider.GetService<CompanyDapperRepository>();
			_employeeDapperRepository = serviceProvider.GetService<EmployeeDapperRepository>();
		}

		private void OpenConnection()
		{
			if (Connection.State == ConnectionState.Open) return;

			Connection.Open();
		}
	}
}
