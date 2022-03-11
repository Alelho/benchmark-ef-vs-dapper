using Benchmark.Data;
using Benchmark.Data.Constants;
using Benchmark.Data.Dapper;
using Benchmark.Data.efcore;
using Benchmark.Data.Entities;
using BenchmarkDotNet.Attributes;
using EFCoreUnitOfWork.Builders;
using EFCoreUnitOfWork.Extensions;
using EFCoreUnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Benchmark.Benchmarks
{
	[BenchmarkCategory("ORM")]
	public class BenchmarkBase
	{
		private readonly Random _rnd = new Random();

		protected IUnitOfWork<EmployeeDbContext> _uow;
		protected IncludeQuery<Employee> _includeQuery;

		protected CompanyDapperRepository _companyDapperRepository;
		protected EmployeeDapperRepository _employeeDapperRepository;

		protected IGenericRepository<Employee> _employeeEFCoreRepository;
		protected CompanyRepository _companyEFCoreRepository;

		public BenchmarkBase()
		{

		}

		protected long EmployeeId => _rnd.Next(100_000, 500_000);
		protected int Count => 100;

		protected void BaseSetup()
		{
			var connectionString = ConnectionStrings.Value;

			var services = new ServiceCollection();

			services.AddDbContext<EmployeeDbContext>(options =>
				options.UseMySql(connectionString, serverVersion: ServerVersion.AutoDetect(connectionString)));

			services.AddUnitOfWork<EmployeeDbContext>();
			services.AddTransient<DbContext, EmployeeDbContext>();

			services.AddTransient<EmployeeDapperRepository>();
			services.AddTransient<CompanyDapperRepository>();

			var serviceProvider = services.BuildServiceProvider();

			_uow = serviceProvider.GetService<IUnitOfWork<EmployeeDbContext>>();

			_companyEFCoreRepository = _uow.GetRepository<CompanyRepository>();
			_employeeEFCoreRepository = _uow.GetGenericRepository<Employee>();

			_companyDapperRepository = serviceProvider.GetService<CompanyDapperRepository>();
			_employeeDapperRepository = serviceProvider.GetService<EmployeeDapperRepository>();

			_includeQuery = IncludeQuery<Employee>.Builder()
				.Include(o => o.Include(e => e.Company));
		}
	}
}
