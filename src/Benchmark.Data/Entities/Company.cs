using System;
using System.Collections.Generic;

namespace Benchmark.Data.Entities
{
	public class Company
	{
		public long Id { get; set; }
		public string FantasyName { get; set; }
		public string Code { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }
		public DateTime FoundedAt { get; set; }
		public IEnumerable<Employee> Employees { get; set; }
	}
}
