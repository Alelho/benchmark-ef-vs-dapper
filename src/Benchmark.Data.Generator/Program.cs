using Benchmark.Data.Constants;
using EFCoreUnitOfWork.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;
using System.Diagnostics;
using System.IO;

namespace Benchmark.Data.Generator
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var connectionString = ConnectionStrings.Value;

			var services = new ServiceCollection();
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			services.AddSingleton<IConfiguration>(configuration);

			services.AddDbContext<EmployeeDbContext>(options =>
				options.UseMySql(connectionString, opt => opt.ServerVersion(ServerVersion.AutoDetect(connectionString))));

			services.AddUnitOfWork<EmployeeDbContext>();
			services.AddTransient<DbContext, EmployeeDbContext>();

			services.AddTransient<GeneratorService>();

			var serviceProvider = services.BuildServiceProvider();

			var dbContext = serviceProvider.CreateScope().ServiceProvider.GetService<EmployeeDbContext>();
			dbContext.Database.Migrate();

			var generateService = serviceProvider.GetService<GeneratorService>();


			Console.WriteLine($"Starting populate database");
			var sw = new Stopwatch();

			sw.Start();
			generateService.Populate();
			sw.Stop();

			var processingTime = string.Format("{0:00}:{1:00}", sw.Elapsed.Minutes, sw.Elapsed.Seconds);
			Console.WriteLine($"Finished populate database. Processing time: {processingTime} mm:ss");

			Console.ReadLine();
		}
	}
}
