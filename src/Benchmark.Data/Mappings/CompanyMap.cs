using Benchmark.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Benchmark.Data.Mappings
{
	public class CompanyMap : IEntityTypeConfiguration<Company>
	{
		public void Configure(EntityTypeBuilder<Company> builder)
		{
			builder.ToTable("Companies");

			builder.HasKey(x => x.Id);

			builder.Property(o => o.Address)
				.HasMaxLength(256)
				.IsRequired();

			builder.Property(o => o.City)
				.HasMaxLength(64)
				.IsRequired();

			builder.Property(o => o.Country)
				.HasMaxLength(64)
				.IsRequired();

			builder.Property(o => o.Code)
				.HasMaxLength(16)
				.IsRequired();

			builder.Property(o => o.FantasyName)
				.HasMaxLength(128)
				.IsRequired();

			builder.Property(o => o.State)
				.HasMaxLength(64)
				.IsRequired();

			builder.Property(o => o.Phone)
				.HasMaxLength(16);

			builder.Property(o => o.FoundedAt)
				.HasColumnType("Date");
		}
	}
}
