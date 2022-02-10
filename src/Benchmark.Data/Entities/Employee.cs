using System;

namespace Benchmark.Data.Entities
{
	public class Employee
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public DateTime BirthDate { get; set; }
		public string Position { get; set; }
		public DateTime AdmissionDate { get; set; }
		public decimal Earnings { get; set; }
		public string PhoneNumber { get; set; }
		public string Document { get; set; }
		public long CompanyId { get; set; }
		public virtual Company Company { get; set; }
	}
}
