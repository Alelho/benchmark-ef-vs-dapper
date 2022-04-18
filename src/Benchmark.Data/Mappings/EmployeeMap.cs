using Benchmark.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Benchmark.Data.Mappings
{
	public class EmployeeMap : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.ToTable("Employees");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Name)
				.HasMaxLength(128)
				.IsRequired();

			builder.Property(e => e.Code)
				.HasMaxLength(16)
				.IsRequired();

			builder.Property(e => e.PhoneNumber)
				.HasMaxLength(16);

			builder.Property(e => e.Code)
				.HasMaxLength(16)
				.IsRequired();

			builder.Property(e => e.Document)
				.HasMaxLength(12)
				.IsRequired();

			//builder.Property(e => e.Earnings)
			//	.HasPrecision(8, 2);

			builder.Property(o => o.Position)
				.HasMaxLength(64);

			builder.Property(o => o.BirthDate)
				.HasColumnType("Date");
		}
	}
}
