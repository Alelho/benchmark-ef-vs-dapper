using Benchmark.Data.Entities;
using Bogus;
using EFCoreUnitOfWork.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Benchmark.Data.Generator
{
	public class GeneratorService
	{
		private IConfiguration _configuration;
		private IUnitOfWork<EmployeeDbContext> _uow;

		public GeneratorService(
			IConfiguration configuration,
			IUnitOfWork<EmployeeDbContext> uow)
		{
			_configuration = configuration;
			_uow = uow;
		}

		public void Populate()
		{
			var companyCount = _configuration.GetValue<int>("Company:Count");
			var employeeCount = _configuration.GetValue<int>("Company:Employees");

			var batchSize = 100;
			var companies = new List<Company>();

			var companyRepository = _uow.GetGenericRepository<Company>();

			for (var i = 0; i < companyCount; i++)
			{
				var employees = GetEmployees(employeeCount);

				var companyFake = new Faker<Company>()
					.RuleFor(o => o.FantasyName, f => f.Company.CompanyName())
					.RuleFor(o => o.FoundedAt, f => f.Date.Past(1))
					.RuleFor(o => o.Code, f => f.Random.AlphaNumeric(16))
					.RuleFor(o => o.Phone, f => f.Phone.PhoneNumberFormat())
					.RuleFor(o => o.Address, f => f.Address.StreetAddress())
					.RuleFor(o => o.City, f => f.Address.City())
					.RuleFor(o => o.Country, f => f.Address.Country())
					.RuleFor(o => o.State, f => f.Address.State());

				var company = companyFake.Generate();

				company.Employees = employees;

				companies.Add(company);

				if (batchSize == companies.Count)
				{
					companyRepository.AddRange(companies);
					_uow.SaveChanges();
					companies.Clear();
				}
			}
		}

		private static IEnumerable<Employee> GetEmployees(int count)
		{
			var employeeFaker = new Faker<Employee>()
				.RuleFor(o => o.Name, f => f.Name.FullName())
				.RuleFor(o => o.Code, f => f.Random.AlphaNumeric(16))
				.RuleFor(o => o.BirthDate, f => f.Person.DateOfBirth)
				.RuleFor(o => o.AdmissionDate, f => f.Date.Past(18))
				.RuleFor(o => o.Document, f => f.Random.AlphaNumeric(12))
				.RuleFor(o => o.PhoneNumber, f => f.Phone.PhoneNumberFormat())
				.RuleFor(o => o.Earnings, f => f.Finance.Amount(min: 1250, max: 20_000));

			return employeeFaker.Generate(count);
		}
	}
}
