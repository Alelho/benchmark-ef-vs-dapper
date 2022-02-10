using Benchmark.Data.Constants;
using Benchmark.Data.Entities;
using Dapper;
using MySqlConnector;
using System.Collections.Generic;

namespace Benchmark.Data.Dapper
{
	internal class CompanyDapperRepository
	{
		public CompanyDapperRepository()
		{
		}

		public IEnumerable<Company> GetAllCompanies()
		{
			using var connection = new MySqlConnection(ConnectionStrings.Value);

			var query = @$"
				SELECT *
				FROM Companies;";

			var companies = connection.Query<Company>(query);

			return companies;
		}
	}
}
