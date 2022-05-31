using Benchmark.Data.Entities;
using Benchmark.Data.Options;
using Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;

namespace Benchmark.Data.Dapper
{
	public class CompanyDapperRepository
	{
		private readonly IDbConnection _connection;

		public CompanyDapperRepository(IOptions<ConnectionOptions> connectionOptions)
		{
			_connection = connectionOptions.Value.Connection;
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