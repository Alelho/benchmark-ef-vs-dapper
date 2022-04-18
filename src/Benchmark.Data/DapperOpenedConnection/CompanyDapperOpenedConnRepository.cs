using Benchmark.Data.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;

namespace Benchmark.Data.DapperOpenedConnection
{
	public class CompanyDapperOpenedConnRepository
	{
		private readonly IDbConnection _connection;

		public CompanyDapperOpenedConnRepository(IDbConnection connection)
		{
			_connection = connection;
		}

		public IEnumerable<Company> Get1000Companies()
		{
			var query = @"
				SELECT *
				FROM Companies
				LIMIT 1000;";

			var companies = _connection.Query<Company>(query);

			return companies;
		}
	}
}
