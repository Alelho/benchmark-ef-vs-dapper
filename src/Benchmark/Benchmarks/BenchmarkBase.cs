using Benchmark.Data;
using Benchmark.Data.Constants;
using Benchmark.Data.Dapper;
using Benchmark.Data.DapperOpenedConnection;
using Benchmark.Data.efcore;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
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

		protected CompanyDapperOpenedConnRepository _companyDapperOpenedConnRepository;
		protected EmployeeDapperOpenedConnRepository _employeeDapperOpenedConnRepository;

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
			Connection.Open();

			var services = new ServiceCollection();

			services.AddDbContext<EmployeeDbContext>(options =>
				options.UseMySql(connectionString));

			services.AddTransient<DbContext, EmployeeDbContext>();

			services.AddTransient<EmployeeRepository>();
			services.AddTransient<CompanyRepository>();

			services.AddTransient<EmployeeDapperRepository>();
			services.AddTransient<CompanyDapperRepository>();

			services.AddTransient(provider => new CompanyDapperOpenedConnRepository(Connection));
			services.AddTransient(provider => new EmployeeDapperOpenedConnRepository(Connection));

			var serviceProvider = services.BuildServiceProvider();

			_companyEFCoreRepository = serviceProvider.GetService<CompanyRepository>();
			_employeeEFCoreRepository = serviceProvider.GetService<EmployeeRepository>();

			_companyDapperRepository = serviceProvider.GetService<CompanyDapperRepository>();
			_employeeDapperRepository = serviceProvider.GetService<EmployeeDapperRepository>();

			_companyDapperOpenedConnRepository = serviceProvider.GetService<CompanyDapperOpenedConnRepository>();
			_employeeDapperOpenedConnRepository = serviceProvider.GetService<EmployeeDapperOpenedConnRepository>();
		}
	}
}
