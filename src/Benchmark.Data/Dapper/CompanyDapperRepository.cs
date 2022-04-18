using Benchmark.Data.Constants;
using Benchmark.Data.Entities;
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Benchmark.Data.Dapper
{
	public class CompanyDapperRepository
	{
		public CompanyDapperRepository()
		{
		}

		public IEnumerable<Company> Get1000Companies()
		{
			using (var connection = new MySqlConnection(ConnectionStrings.Value))
			{
				var query = @"
				SELECT *
				FROM Companies
				LIMIT 1000;";

				var companies = connection.Query<Company>(query);

				return companies;
			}
		}
	}
}
